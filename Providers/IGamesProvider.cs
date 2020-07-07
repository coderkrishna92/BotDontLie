// <copyright file="IGamesProvider.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Providers
{
    using System.Threading.Tasks;
    using BotDontLie.Models.AzureStorage;

    /// <summary>
    /// This interface defines the methods to perform CRUD operations on the Games table in Azure table storage.
    /// </summary>
    public interface IGamesProvider
    {
        /// <summary>
        /// Save or update the game entity.
        /// </summary>
        /// <param name="game">The game to save.</param>
        /// <returns><see cref="Task"/> that resolves successfully if the data was properly saved.</returns>
        Task UpsertNbaGameAsync(GameEntity game);

        /// <summary>
        /// Retrieves a Game from Azure table storage by the gameId.
        /// </summary>
        /// <param name="gameId">The ID of the game itself.</param>
        /// <returns>A unit of execution, or a <see cref="Task"/> which contains the type of <see cref="GameEntity"/>.</returns>
        Task<GameEntity> GetGameEntityByGameIdAsync(long gameId);
    }
}