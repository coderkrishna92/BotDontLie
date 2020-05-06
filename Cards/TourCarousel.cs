// <copyright file="TourCarousel.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Cards
{
    using System.Collections.Generic;
    using BotDontLie.Properties;
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