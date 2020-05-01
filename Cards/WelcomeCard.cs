﻿// <copyright file="WelcomeCard.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Cards
{
    using System.Collections.Generic;
    using AdaptiveCards;
    using BotDontLie.Models;
    using BotDontLie.Properties;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This class represents the user welcome card.
    /// </summary>
    public static class WelcomeCard
    {
        /// <summary>
        /// Method that generates the welcome card.
        /// </summary>
        /// <param name="welcomeText">The content of the welcome text.</param>
        /// <returns>An attachment to be appended to a message.</returns>
        public static Attachment GetCard(string welcomeText)
        {
            AdaptiveCard userWelcomeCard = new AdaptiveCard("1.0")
            {
                Body = new List<AdaptiveElement>
                {
                    new AdaptiveTextBlock
                    {
                        HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
                        Text = welcomeText,
                        Wrap = true,
                    },
                },
                Actions = new List<AdaptiveAction>
                {
                    new AdaptiveSubmitAction
                    {
                        Title = BotResource.TakeATourCTATitle,
                        Data = new TeamsAdaptiveSubmitActionData
                        {
                            MsTeams = new CardAction
                            {
                                Type = ActionTypes.MessageBack,
                                DisplayText = BotResource.TakeATourCTATitle,
                                Text = Constants.TakeATour,
                            },
                        },
                    },
                },
            };

            return new Attachment
            {
                ContentType = AdaptiveCard.ContentType,
                Content = userWelcomeCard,
            };
        }
    }
}