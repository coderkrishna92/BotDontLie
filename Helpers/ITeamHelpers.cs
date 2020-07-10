// <copyright file="ITeamHelpers.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Helpers
{
    /// <summary>
    /// This interface defines team helper methods.
    /// </summary>
    public interface ITeamHelpers
    {
        /// <summary>
        /// This method will get the team name.
        /// </summary>
        /// <param name="arrayOfWords">The input list of words.</param>
        /// <returns>The string representing the team name.</returns>
        string[] ExtractTeamName(string[] arrayOfWords);

        /// <summary>
        /// This concats all words in the array to construct the team full name.
        /// </summary>
        /// <param name="teamFullName">The array of the full name of the team.</param>
        /// <returns>A string for the full name of the team.</returns>
        string GetTeamFullNameStr(string[] teamFullName);
    }
}