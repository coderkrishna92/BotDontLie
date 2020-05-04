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
                GetCard(BotResource.FunctionCardTitleText, BotResource.FunctionCardText, appBaseUri + "/content/Askaquestion.png"),
                GetCard(BotResource.AskAnExpertTitleText, BotResource.AskAnExpertText, appBaseUri + "/content/Expertinquiry.png"),
                GetCard(BotResource.ShareFeedbackTitleText, BotResource.FeedbackText, appBaseUri + "/content/Sharefeedback.png"),
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