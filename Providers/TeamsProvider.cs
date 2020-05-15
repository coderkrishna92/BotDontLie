// <copyright file="TeamsProvider.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Providers
{
    using System;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using Microsoft.ApplicationInsights;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

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

        public Task UpsertNbaTeamAsync(TeamEntity teamEntity)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamEntity> GetTeamByFullNameAsync(string teamFullName)
        {
            throw new NotImplementedException();
        }

        public async Task<TeamEntity> GetTeamByNameAsync(string teamName)
        {
            throw new NotImplementedException();
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
    }
}