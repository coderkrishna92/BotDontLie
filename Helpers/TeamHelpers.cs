// <copyright file="TeamHelpers.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Helpers
{
    using System;

    /// <summary>
    /// This class defines the various helper methods with regards to the teams.
    /// </summary>
    public static class TeamHelpers
    {
        /// <summary>
        /// This method extracts the team full name.
        /// </summary>
        /// <param name="arrayOfWords">The input command string.</param>
        /// <returns>An array of string which is the full team name.</returns>
        public static string[] ExtractTeamName(string[] arrayOfWords)
        {
            if (arrayOfWords is null)
            {
                throw new ArgumentNullException(nameof(arrayOfWords));
            }

            return arrayOfWords.Length == 5 ? arrayOfWords.SubArray(3, 2) : arrayOfWords.SubArray(3, 3);
        }

        /// <summary>
        /// This method constructs the team full name.
        /// </summary>
        /// <param name="teamFullName">The full name of the team in an array.</param>
        /// <returns>The string of the team full name.</returns>
        public static string GetTeamFullNameStr(string[] teamFullName)
        {
            return string.Join(' ', teamFullName);
        }
    }
}