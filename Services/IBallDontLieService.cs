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
        Task<TeamsResponse> RetrieveAllTeams();
    }
}