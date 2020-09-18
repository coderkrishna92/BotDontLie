// <copyright file="IBallDontLieService.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Services
{
    using System.Threading.Tasks;
    using BotDontLie.Common.Models;
    using BotDontLie.Common.Models.AzureStorage;

    /// <summary>
    /// Interface that contains the necessary method definitions to query the BallDontLie API.
    /// </summary>
    public interface IBallDontLieService
    {
        /// <summary>
        /// Method definition that will retrieve all 30 NBA franchises.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="bool"/> which determines if all franchises have been synced or not.</returns>
        Task<bool> SyncAllTeamsAsync();

        /// <summary>
        /// Method definition that will get all the players.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="bool"/> which determines if all players have been synced or not.</returns>
        Task<bool> SyncAllPlayersAsync();

        /// <summary>
        /// Method definition that will get all the statistics.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="bool"/> which determines if all statistics have been synced or not.</returns>
        Task<bool> SyncAllStatisticsAsync();

        /// <summary>
        /// Method definition that will retrieve all games.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="GamesResponse"/>.</returns>
        Task<bool> SyncAllGamesAsync();

        /// <summary>
        /// Method definition that will retrieve a specific NBA franchise by their name.
        /// </summary>
        /// <param name="teamName">The name of the team (not the full name).</param>
        /// <returns>A unit of execution that contains the type of <see cref="Team"/>.</returns>
        Task<Team> GetTeamByNameAsync(string teamName);

        /// <summary>
        /// Method definition that will retrieve a specific NBA franchise by their full name.
        /// </summary>
        /// <param name="teamFullName">The full name of the team (i.e. Oklahoma City Thunder).</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        Task<Team> GetTeamByFullNameAsync(string teamFullName);

        /// <summary>
        /// Method definition to get the playerId by the first and last name of the player (i.e. Carmelo Anthony).
        /// </summary>
        /// <param name="firstName">The first name of the player.</param>
        /// <param name="lastName">The last name of the player.</param>
        /// <returns>A task that contains data of type <see cref="long"/> which represents the playerID.</returns>
        Task<long> GetPlayerIdByFirstLastNameAsync(string firstName, string lastName);

        /// <summary>
        /// Method definition to get the player entity by the playerID.
        /// </summary>
        /// <param name="playerId">The player ID.</param>
        /// <returns>A unit of execution that contains a type of <see cref="PlayerEntity"/>.</returns>
        Task<Player> GetPlayerByIdAsync(long playerId);
    }
}