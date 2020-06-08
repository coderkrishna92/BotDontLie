// <copyright file="IStatisticsProvider.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Providers
{
    using System.Threading.Tasks;
    using BotDontLie.Models.AzureStorage;

    /// <summary>
    /// This interface will define methods to insert table into Azure table storage.
    /// </summary>
    public interface IStatisticsProvider
    {
        /// <summary>
        /// Save or update the NBA statistic.
        /// </summary>
        /// <param name="statisticsEntity">The statistic entity to save.</param>
        /// <returns>A task that would resolve successfully to indicate whether or not data was saved.</returns>
        Task UpsertNbaStatisticAsync(StatisticsEntity statisticsEntity);

        /// <summary>
        /// Method definition to get the necessary statistics entity by their ID.
        /// </summary>
        /// <param name="statisticsId">The ID of the statistics entities.</param>
        /// <returns>A unit of execution that contains <see cref="StatisticsEntity"/>.</returns>
        Task<StatisticsEntity> GetStatisticsEntityByIdAsync(long statisticsId);
    }
}