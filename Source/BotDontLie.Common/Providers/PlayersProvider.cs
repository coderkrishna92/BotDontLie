// <copyright file="PlayersProvider.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Providers
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading.Tasks;
    using BotDontLie.Common.Models;
    using BotDontLie.Common.Models.AzureStorage;
    using Microsoft.ApplicationInsights;
    using Microsoft.Azure.Cosmos.Table;

    /// <summary>
    /// This class implements the methods defined in <see cref="IPlayersProvider"/> interface.
    /// </summary>
    public class PlayersProvider : IPlayersProvider
    {
        /// <summary>
        /// This is the partition key for the Players table.
        /// </summary>
        private const string PartitionKey = "NbaPlayer";

        private readonly Lazy<Task> initializeTask;
        private readonly TelemetryClient telemetryClient;
        private CloudTable playerCloudTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayersProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The Azure Table connection string.</param>
        /// <param name="telemetryClient">ApplicationInsights DI.</param>
        public PlayersProvider(string connectionString, TelemetryClient telemetryClient)
        {
            this.initializeTask = new Lazy<Task>(() => this.InitializeTableStorageAsync(connectionString));
            this.telemetryClient = telemetryClient;
        }

        /// <summary>
        /// Method to save the player in the Players table in Azure table storage.
        /// </summary>
        /// <param name="player">The player to save.</param>
        /// <returns>A <see cref="Task"/> which resolves successfully upon the successful saving of data in Azure table storage.</returns>
        public Task UpsertNbaPlayerAsync(PlayerEntity player)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            player.PartitionKey = PartitionKey;
            player.RowKey = player.PlayerId.ToString(CultureInfo.InvariantCulture);

            return this.StoreOrUpdatePlayerEntityAsync(player);
        }

        /// <summary>
        /// Having the method to retrieve a player by their first and last name.
        /// </summary>
        /// <param name="firstName">The first name of the player to search.</param>
        /// <param name="lastName">The last name of the player to search.</param>
        /// <returns>A unit of execution that returns a type of <see cref="PlayerEntity"/>.</returns>
        public async Task<PlayerEntity> GetPlayerEntityByFullNameAsync(string firstName, string lastName)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException(nameof(firstName));
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException(nameof(lastName));
            }

            var searchOperation = TableOperation.Retrieve<PlayerEntity>(PartitionKey, firstName, new List<string>() { lastName });
            var searchResult = await this.playerCloudTable.ExecuteAsync(searchOperation).ConfigureAwait(false);
            return (PlayerEntity)searchResult.Result;
        }

        /// <summary>
        /// Method to retrieve the player by their ID.
        /// </summary>
        /// <param name="playerId">The player ID.</param>
        /// <returns>A unit of execution that returns a type of <see cref="PlayerEntity"/>.</returns>
        public async Task<PlayerEntity> GetPlayerEntityByPlayerIdAsync(long playerId)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);

            var searchOperation = TableOperation.Retrieve<PlayerEntity>(PartitionKey, playerId.ToString(CultureInfo.InvariantCulture));
            var searchResult = await this.playerCloudTable.ExecuteAsync(searchOperation).ConfigureAwait(false);
            return (PlayerEntity)searchResult.Result;
        }

        private async Task InitializeTableStorageAsync(string connectionString)
        {
            this.telemetryClient.TrackTrace($"Initializing the table storage: {Constants.PlayerInfoTableName}");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            this.playerCloudTable = cloudTableClient.GetTableReference(Constants.PlayerInfoTableName);

            await this.playerCloudTable.CreateIfNotExistsAsync().ConfigureAwait(false);
        }

        private async Task EnsureInitializedAsync()
        {
            this.telemetryClient.TrackTrace("Ensuring that the Azure Table storage is initialized");
            await this.initializeTask.Value.ConfigureAwait(false);
        }

        private async Task<TableResult> StoreOrUpdatePlayerEntityAsync(PlayerEntity player)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);
            TableOperation addOrUpdateOperation = TableOperation.InsertOrReplace(player);
            return await this.playerCloudTable.ExecuteAsync(addOrUpdateOperation).ConfigureAwait(false);
        }
    }
}