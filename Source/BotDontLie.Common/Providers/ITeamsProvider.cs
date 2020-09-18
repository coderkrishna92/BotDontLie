// <copyright file="ITeamsProvider.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Providers
{
    using System.Threading.Tasks;
    using BotDontLie.Common.Models.AzureStorage;

    /// <summary>
    /// This interface defines the methods to perform CRUD operations on the Teams table in Azure table storage.
    /// </summary>
    public interface ITeamsProvider
    {
        /// <summary>
        /// Save or update the team entity.
        /// </summary>
        /// <param name="teamEntity">The team that is being retrieved from the original Ball Dont Lie API.</param>
        /// <returns><see cref="Task"/> that resolves successfully if the data was saved properly.</returns>
        Task UpsertNbaTeamAsync(TeamEntity teamEntity);

        /// <summary>
        /// Method definition to get the team by the full name.
        /// </summary>
        /// <param name="teamFullName">The full name of the NBA team (i.e. New York Knicks).</param>
        /// <returns>A unit of execution that contains a type of <see cref="TeamEntity"/>.</returns>
        Task<TeamEntity> GetTeamByFullNameAsync(string teamFullName);

        /// <summary>
        /// Method definition to get the team by name.
        /// </summary>
        /// <param name="teamName">The name of the NBA team.</param>
        /// <returns>A unit of execution that contains a type of <see cref="TeamEntity"/>.</returns>
        Task<TeamEntity> GetTeamByNameAsync(string teamName);
    }
}
