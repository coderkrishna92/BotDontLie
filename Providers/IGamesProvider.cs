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
    }
}