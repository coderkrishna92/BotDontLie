// <copyright file="TeamResponseCard.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Cards
{
    using System;
    using System.Collections.Generic;
    using AdaptiveCards;
    using BotDontLie.Common.Models;
    using BotDontLie.Common.Properties;
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

            AdaptiveCard teamCard = new AdaptiveCard(new AdaptiveSchemaVersion(1, 2))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Weight = AdaptiveTextWeight.Bolder,
                        Text = BotResource.TeamResponseTitleText,
                        Size = AdaptiveTextSize.Medium,
                    },
                },
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = teamCard,
            };
        }
    }
}