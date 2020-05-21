// <copyright file="IPlayersProvider.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Providers
{
    using System.Threading.Tasks;
    using BotDontLie.Models.AzureStorage;

    /// <summary>
    /// This interface defines the methods to perform CRUD operations on the Players table in Azure table storage.
    /// </summary>
    public interface IPlayersProvider
    {
        /// <summary>
        /// Save or update the player entity.
        /// </summary>
        /// <param name="player">The player to save.</param>
        /// <returns><see cref="Task"/> that resolves successfully if the data was saved properly.</returns>
        Task UpsertNbaPlayerAsync(PlayerEntity player);

        /// <summary>
        /// Gets the player by their name.
        /// </summary>
        /// <param name="firstName">The first name of the player.</param>
        /// <param name="lastName">The last name of the player.</param>
        /// <returns>A unit of execution that contains a type of <see cref="PlayerEntity"/>.</returns>
        Task<PlayerEntity> GetPlayerEntityByFullNameAsync(string firstName, string lastName);
    }
}