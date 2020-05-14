// <copyright file="TeamsProvider.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Providers
{
    using System;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// This class implements methods defined in <see cref="ITeamsProvider"/>.
    /// </summary>
    public class TeamsProvider : ITeamsProvider
    {
        private const string PartitionKey = "NbaTeam";
        private readonly Lazy<Task> initializeTask;
        private CloudTable teamCloudTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="TeamsProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The Azure Table connection string.</param>
        public TeamsProvider(string connectionString)
        {
            this.initializeTask = new Lazy<Task>(() => this.InitializeTableStorageAsync(connectionString));
        }

        private async Task EnsureInitializedAsync()
        {
            await this.initializeTask.Value.ConfigureAwait(false);
        }

        private async Task InitializeTableStorageAsync(string connectionString)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            this.teamCloudTable = cloudTableClient.GetTableReference(Constants.TeamInfoTableName);

            await this.teamCloudTable.CreateIfNotExistsAsync().ConfigureAwait(false);
        }
    }
}