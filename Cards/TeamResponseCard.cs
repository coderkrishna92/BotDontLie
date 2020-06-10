// <copyright file="TeamResponseCard.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Cards
{
    using System;
    using BotDontLie.Models;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This class represents the response card for the team query.
    /// </summary>
    public static class TeamResponseCard
    {
        /// <summary>
        /// This method will get the response card for a Team.
        /// </summary>
        /// <param name="team">The team upon which the card will get rendered.</param>
        /// <returns>An attachment to append to a message.</returns>
        public static Attachment GetCard(Team team)
        {
            if (team is null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            return null;
        }
    }
}