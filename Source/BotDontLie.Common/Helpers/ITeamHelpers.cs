// <copyright file="ITeamHelpers.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Helpers
{
    /// <summary>
    /// This interface defines the team helper methods.
    /// </summary>
    public interface ITeamHelpers
    {
        /// <summary>
        /// This method will extract the team name.
        /// </summary>
        /// <param name="arrayOfWords">The input command.</param>
        /// <returns>A string array representing the team name.</returns>
        string[] ExtractTeamName(string[] arrayOfWords);

        /// <summary>
        /// This concats all words in the array to construct the full team name.
        /// </summary>
        /// <param name="teamFullName">The array representing the team name.</param>
        /// <returns>The full name of the NBA franchise.</returns>
        string GetTeamFullNameStr(string[] teamFullName);
    }
}