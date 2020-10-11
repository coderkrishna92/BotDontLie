// <copyright file="TeamsProvider.cs" company="PK Software LLC">
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
    /// This class implements methods defined in <see cref="ITeamsProvider"/>.
    /// </summary>
    public class TeamsProvider : ITeamsProvider
    {
        /// <summary>
        /// This is the partition key for the Team table.
        /// </summary>
        private const string PartitionKey = "NbaTeam";

        private readonly Lazy<Task> initializeTask;
        private readonly TelemetryClient telemetryClient;
        private CloudTable teamCloudTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamsProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The Azure Table connection string.</param>
        /// <param name="telemetryClient">ApplicationInsights DI.</param>
        public TeamsProvider(string connectionString, TelemetryClient telemetryClient)
        {
            this.initializeTask = new Lazy<Task>(() => this.InitializeTableStorageAsync(connectionString));
            this.telemetryClient = telemetryClient;
        }

        /// <summary>
        /// Saves a team entity.
        /// </summary>
        /// <param name="teamEntity">The team to save.</param>
        /// <returns>A unit of execution.</returns>
        public Task UpsertNbaTeamAsync(TeamEntity teamEntity)
        {
            if (teamEntity is null)
            {
                throw new ArgumentNullException(nameof(teamEntity));
            }

            this.telemetryClient.TrackTrace("UpsertNbaTeamAsync called.");

            teamEntity.PartitionKey = PartitionKey;
            teamEntity.RowKey = teamEntity.TeamId.ToString(CultureInfo.InvariantCulture);

            return this.StoreOrUpdateTeamEntityAsync(teamEntity);
        }

        /// <summary>
        /// Method to retrieve a <see cref="TeamEntity"/> using the full name.
        /// </summary>
        /// <param name="teamFullName">The full name for an NBA team.</param>
        /// <returns>A unit of execution that returns a <see cref="TeamEntity"/> boxed in.</returns>
        public async Task<TeamEntity> GetTeamByFullNameAsync(string teamFullName)
        {
            this.telemetryClient.TrackTrace($"GetTeamByFullNameAsync being called for: {teamFullName}");
            await this.EnsureInitializedAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(teamFullName))
            {
                return null;
            }

            var searchOperation = TableOperation.Retrieve<TeamEntity>(PartitionKey, teamFullName);
            var searchResult = await this.teamCloudTable.ExecuteAsync(searchOperation).ConfigureAwait(false);

            return (TeamEntity)searchResult.Result;
        }

        /// <summary>
        /// Method to retrieve a <see cref="TeamEntity"/> using the team name.
        /// </summary>
        /// <param name="teamName">The name of the NBA team.</param>
        /// <returns>A unit of execution that returns a <see cref="TeamEntity"/> boxed in.</returns>
        public async Task<TeamEntity> GetTeamByNameAsync(string teamName)
        {
            this.telemetryClient.TrackTrace($"GetTeamByNameAsync being called for: {teamName}");
            await this.EnsureInitializedAsync().ConfigureAwait(false);

            if (string.IsNullOrEmpty(teamName))
            {
                return null;
            }

            var searchOperation = TableOperation.Retrieve<TeamEntity>(PartitionKey, teamName);
            var searchResult = await this.teamCloudTable.ExecuteAsync(searchOperation).ConfigureAwait(false);

            return (TeamEntity)searchResult.Result;
        }

        private async Task EnsureInitializedAsync()
        {
            this.telemetryClient.TrackTrace("Ensuring that the Azure Table storage is initialized.");
            await this.initializeTask.Value.ConfigureAwait(false);
        }

        private async Task InitializeTableStorageAsync(string connectionString)
        {
            this.telemetryClient.TrackTrace($"Initializing the table storage: {Constants.TeamInfoTableName}");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            this.teamCloudTable = cloudTableClient.GetTableReference(Constants.TeamInfoTableName);

            await this.teamCloudTable.CreateIfNotExistsAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Store or update ticket entity in table storage.
        /// </summary>
        /// <param name="entity">Represents ticket entity used for storage and retrieval.</param>
        /// <returns><see cref="Task"/> that represents configuration entity is saved or updated.</returns>
        private async Task<TableResult> StoreOrUpdateTeamEntityAsync(TeamEntity entity)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);
            TableOperation addOrUpdateOperation = TableOperation.InsertOrReplace(entity);
            return await this.teamCloudTable.ExecuteAsync(addOrUpdateOperation).ConfigureAwait(false);
        }
    }
}