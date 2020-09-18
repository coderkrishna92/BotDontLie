// <copyright file="IGamesProvider.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Providers
{
    using System.Threading.Tasks;
    using BotDontLie.Common.Models.AzureStorage;

    /// <summary>
    /// This interface defines methods to perform CRUD operations on the Games table in Azure table storage.
    /// </summary>
    public interface IGamesProvider
    {
        /// <summary>
        /// Saves or updates a game entity.
        /// </summary>
        /// <param name="game">The game to save or update.</param>
        /// <returns>A <see cref="Task"/> that resolves successfully if the data was correctly saved.</returns>
        Task UpsertNbaGameAsync(GameEntity game);

        /// <summary>
        /// Retrieves a Game entity for the Azure table storage using the gameId.
        /// </summary>
        /// <param name="gameId">The ID of the game entity to fetch.</param>
        /// <returns>A unit of execution, or a <see cref="Task"/> which contains the typ of <see cref="GameEntity"/>.</returns>
        Task<GameEntity> GetGameEntityByGameIdAsync(long gameId);
    }
}