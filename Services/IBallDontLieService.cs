// <copyright file="IBallDontLieService.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Services
{
    using System.Threading.Tasks;
    using BotDontLie.Models;

    /// <summary>
    /// Interface that contains the necessary method definitions to query the BallDontLie API.
    /// </summary>
    public interface IBallDontLieService
    {
        /// <summary>
        /// Method definition that will retrieve all 30 NBA franchises.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="TeamsResponse"/>.</returns>
        Task<TeamsResponse> SyncAllTeamsAsync();

        /// <summary>
        /// Method definition that will retrieve all games.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="GamesResponse"/>.</returns>
        Task<GamesResponse> RetrieveAllGamesAsync();

        /// <summary>
        /// Method definition that will retrieve all stats.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="StatsResponse"/>.</returns>
        Task<StatsResponse> RetrieveAllStatsAsync();

        /// <summary>
        /// Method definition that will retrieve a specific NBA franchise by their name.
        /// </summary>
        /// <param name="teamName">The name of the team (not the full name).</param>
        /// <returns>A unit of execution that contains the type of <see cref="Team"/>.</returns>
        Task<Team> RetrieveTeamByNameAsync(string teamName);

        /// <summary>
        /// Method definition that will retrieve a specific NBA franchise by their full name.
        /// </summary>
        /// <param name="teamFullName">The full name of the team (i.e. Oklahoma City Thunder).</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        Task<Team> RetrieveTeamByFullNameAsync(string teamFullName);
    }
}