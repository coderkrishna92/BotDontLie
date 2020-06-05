// <copyright file="StatisticsProvider.cs" company="Tata Consultancy Services Ltd">
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
    /// This class implements all of the methods defined in <see cref="IStatisticsProvider"/>.
    /// </summary>
    public class StatisticsProvider : IStatisticsProvider
    {
        /// <summary>
        /// This is the partition key for the statistics table.
        /// </summary>
        private const string PartitionKey = "NbaStatistic";

        private readonly Lazy<Task> initializeTask;
        private readonly TelemetryClient telemetryClient;
        private CloudTable statisticsCloudTable;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticsProvider"/> class.
        /// </summary>
        /// <param name="connectionString">The Azure table storage connection string.</param>
        /// <param name="telemetryClient">Application Insights DI.</param>
        public StatisticsProvider(string connectionString, TelemetryClient telemetryClient)
        {
            this.initializeTask = new Lazy<Task>(() => this.InitializeTableStorageAsync(connectionString));
            this.telemetryClient = telemetryClient;
        }

        /// <summary>
        /// Saves or updates the NBA statistics into Azure Table storage.
        /// </summary>
        /// <param name="statistic">The statistic to save or update.</param>
        /// <returns>A <see cref="Task"/> that would resolve successfully if the data has been properly saved.</returns>
        public Task UpsertNbaStatisticAsync(StatisticsEntity statistic)
        {
            if (statistic is null)
            {
                throw new ArgumentNullException(nameof(statistic));
            }

            statistic.PartitionKey = PartitionKey;
            statistic.RowKey = statistic.StatisticsId.ToString(CultureInfo.InvariantCulture);

            return this.StoreOrUpdateStatisticEntityAsync(statistic);
        }

        private async Task InitializeTableStorageAsync(string connectionString)
        {
            this.telemetryClient.TrackTrace($"Initializing the table storage: {Constants.StatisticsInfoTableName}");
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
            CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
            this.statisticsCloudTable = cloudTableClient.GetTableReference(Constants.StatisticsInfoTableName);

            await this.statisticsCloudTable.CreateIfNotExistsAsync().ConfigureAwait(false);
        }

        private async Task EnsureInitializedAsync()
        {
            this.telemetryClient.TrackTrace("Ensuring that the Azure Table storage is properly initialized.");
            await this.initializeTask.Value.ConfigureAwait(false);
        }

        private async Task<TableResult> StoreOrUpdateStatisticEntityAsync(StatisticsEntity statistic)
        {
            await this.EnsureInitializedAsync().ConfigureAwait(false);
            TableOperation addOrUpdateOperation = TableOperation.InsertOrReplace(statistic);
            return await this.statisticsCloudTable.ExecuteAsync(addOrUpdateOperation).ConfigureAwait(false);
        }
    }
}