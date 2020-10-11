// <copyright file="PlayerResponseCard.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Cards
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AdaptiveCards;
    using BotDontLie.Common.Models;
    using BotDontLie.Common.Properties;
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

            AdaptiveCard playerCard = new AdaptiveCard(new AdaptiveSchemaVersion(1, 2))
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        Text = string.Format(CultureInfo.InvariantCulture, BotResource.PlayerFullNameFormat, player.FirstName, player.LastName),
                        Wrap = true,
                    },
                },
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = playerCard,
            };
        }
    }
}