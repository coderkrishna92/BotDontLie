// <copyright file="PlayersProvider.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Providers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using BotDontLie.Models.AzureStorage;
    using Microsoft.ApplicationInsights;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

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

        public async Task<PlayerEntity> GetPlayerEntityByFullNameAsync(string firstName, string lastName)
        {
            return null;
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