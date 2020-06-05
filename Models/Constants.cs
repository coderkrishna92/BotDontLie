﻿// <copyright file="Constants.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    /// <summary>
    /// This class defines necessary constants for the bot to function.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// This command/constant allows for the ability to take a tour.
        /// </summary>
        public const string TakeATour = "take a tour";

        /// <summary>
        /// This command/constant allows for querying all teams.
        /// </summary>
        public const string SyncAllTeams = "sync all teams";

        /// <summary>
        /// This command/constant allows to query all games.
        /// </summary>
        public const string SyncAllGames = "sync all games";

        /// <summary>
        /// This command/constant allows to sync all players.
        /// </summary>
        public const string SyncAllPlayers = "sync all players";

        /// <summary>
        /// This command/constant allows to list all stats.
        /// </summary>
        public const string SyncAllStats = "sync all stats";

        /// <summary>
        /// This constant is representing the TeamInfo table.
        /// </summary>
        public const string TeamInfoTableName = "TeamsInfo";

        /// <summary>
        /// This constant is representing the PlayerInfo table.
        /// </summary>
        public const string PlayerInfoTableName = "PlayersInfo";

        /// <summary>
        /// This constant is representing the GamesInfo table.
        /// </summary>
        public const string GamesInfoTableName = "GamesInfo";

        /// <summary>
        /// This constant is representing the StatisticsInfo table.
        /// </summary>
        public const string StatisticsInfoTableName = "StatisticsInfo";
    }
}