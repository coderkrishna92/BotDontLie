// <copyright file="TourCarousel.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Cards
{
    using System;
    using System.Collections.Generic;
    using BotDontLie.Common.Properties;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This class is for rendering the tour carousel.
    /// </summary>
    public static class TourCarousel
    {
        /// <summary>
        /// Create the set of cards that comprise the user tour carousel.
        /// </summary>
        /// <param name="appBaseUri">The base URI where the app is hosted.</param>
        /// <returns>The user tour in the form of a carousel.</returns>
        public static IEnumerable<Attachment> GetUserTourCards(string appBaseUri)
        {
            if (appBaseUri is null)
            {
                throw new ArgumentNullException(nameof(appBaseUri));
            }

            return new List<Attachment>()
            {
                GetCard(BotResource.FindPlayersTitleText, BotResource.FindPlayersText, appBaseUri + "/content/FindPlayers.png"),
                GetCard(BotResource.FindTeamsTitleText, BotResource.FindTeamsText, appBaseUri + "/content/NbaTeams.png"),
                GetCard(BotResource.FindGamesTitleText, BotResource.FindGamesText, appBaseUri + "/content/FindGames.png"),
            };
        }

        private static Attachment GetCard(string title, string text, string imageUri)
        {
            HeroCard tourCarouselCard = new HeroCard()
            {
                Title = title,
                Text = text,
                Images = new List<CardImage>()
                {
                    new CardImage(imageUri),
                },
            };

            return tourCarouselCard.ToAttachment();
        }
    }
}