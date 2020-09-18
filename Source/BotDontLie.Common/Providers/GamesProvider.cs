// <copyright file="GamesProvider.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Providers
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using BotDontLie.Common.Models;
    using BotDontLie.Common.Models.AzureStorage;
    using Microsoft.ApplicationInsights;
    using Microsoft.Azure.Cosmos.Table;

    /// <summary>
    /// This class implements the necessary methods that are defined in <see cref="IGamesProvider"/>.
    /// </summary>
    public class GamesProvider : IGamesProvider
    {
        /// <summary>
        /// This is the partition key for the Games table.
        /// </summary>
        private const string PartitionKey = "NbaGame";

        private readonly Lazy<Task> initializeTask;
        private readonly TelemetryClient telemetryClient;
        private CloudTable gamesCloudTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamesProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The Azure Table connection string.</param>
        /// <param name="telemetryClient">ApplicationInsights DI.</param>
        public GamesProvider(string connectionString, TelemetryClient telemetryClient)
        {
            this.initializeTask = new Lazy<Task>(() => this.InitializeTableStorageAsync(connectionString));
            this.telemetryClient = telemetryClient;
        }

        /// <summary>
        /// Method to save the game in the Games table in Azure table storage.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <returns>A <see cref="Task"/> which resolves successfully upon the successful saving of data in Azure table storage.</returns>
        public Task UpsertNbaGameAsync(GameEntity game)
        {
            if (game is null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            game.PartitionKey = PartitionKey;
            game.RowKey = game.GameId.ToString(CultureInfo.InvariantCulture);

            return this.StoreOrUpdateGameEntityAsync(game);
        }

        /// <summary>
        /// Method to get the game by the game ID.
        /// </summary>
        /// <param name="gameId">The game ID to retrieve.</param>
        /// <returns>A <see cref="Task"/> which contains the type of <see cref="GameEntity"/>.</returns>
        public async Task<GameEntity> GetGameEntityByGameIdAsync(long gameId)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);

            var searchOperation = TableOperation.Retrieve<GameEntity>(PartitionKey, gameId.ToString(CultureInfo.InvariantCulture));
            var searchResult = await this.gamesCloudTable.ExecuteAsync(searchOperation).ConfigureAwait(false);
            return (GameEntity)searchResult.Result;
        }

        private async Task InitializeTableStorageAsync(string connectionString)
        {
            this.telemetryClient.TrackTrace($"Initializing the table storage: {Constants.GamesInfoTableName}");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            this.gamesCloudTable = cloudTableClient.GetTableReference(Constants.GamesInfoTableName);

            await this.gamesCloudTable.CreateIfNotExistsAsync().ConfigureAwait(false);
        }

        private async Task EnsureInitializedAsync()
        {
            this.telemetryClient.TrackTrace("Ensuring that the Azure Table storage is initialized");
            await this.initializeTask.Value.ConfigureAwait(false);
        }

        private async Task<TableResult> StoreOrUpdateGameEntityAsync(GameEntity game)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);
            TableOperation addOrUpdateOperation = TableOperation.InsertOrReplace(game);
            return await this.gamesCloudTable.ExecuteAsync(addOrUpdateOperation).ConfigureAwait(false);
        }
    }
}