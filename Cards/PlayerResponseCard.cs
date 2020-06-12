﻿// <copyright file="PlayerResponseCard.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Cards
{
    using System;
    using BotDontLie.Models;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This class is the player response card.
    /// </summary>
    public static class PlayerResponseCard
    {
        /// <summary>
        /// This method will render the response card for a player.
        /// </summary>
        /// <param name="player">The player information to render in the response card.</param>
        /// <returns>An attachment to append to a message.</returns>
        public static Attachment GetCard(Player player)
        {
            if (player is null)
            {
                throw new ArgumentNullException(nameof(player));
            }

            return null;
        }
    }
}