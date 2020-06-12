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
        public static Attachment GetCardForShortTeamName(Team team)
        {
            if (team is null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            return null;
        }

        /// <summary>
        /// This method will get the response card for a Team when queried by its full name.
        /// </summary>
        /// <param name="team">The result of the query for the team by searching the full name.</param>
        /// <returns>An attachment to append to a message.</returns>
        public static Attachment GetCardForFullTeamName(Team team)
        {
            if (team is null)
            {
                throw new ArgumentNullException(nameof(team));
            }

            return null;
        }
    }
}