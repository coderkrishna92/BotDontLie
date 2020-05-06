// <copyright file="BallDontLieService.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Services
{
    using System.Threading.Tasks;
    using BotDontLie.Models;

    /// <summary>
    /// This class will implement the methods that are defined in the interface <see cref="IBallDontLieService"/>.
    /// </summary>
    public class BallDontLieService : IBallDontLieService
    {
        /// <summary>
        /// Method implementation to return all 30 of the NBA franchises.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="TeamsResponse"/>.</returns>
        public async Task<TeamsResponse> RetrieveAllTeams()
        {
            return null;
        }
    }
}