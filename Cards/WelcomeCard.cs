// <copyright file="WelcomeCard.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Cards
{
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
            return null;
        }
    }
}