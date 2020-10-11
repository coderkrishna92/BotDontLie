// <copyright file="StatsGame.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class defines the game object with regards to the statistics response.
    /// </summary>
    public class StatsGame
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the home team id.
        /// </summary>
        [JsonProperty("home_team_id")]
        public long HomeTeamId { get; set; }

        /// <summary>
        /// Gets or sets the home team name.
        /// </summary>
        [JsonProperty("home_team_score")]
        public long HomeTeamScore { get; set; }

        /// <summary>
        /// Gets or sets the period (or even the quarter).
        /// </summary>
        [JsonProperty("period")]
        public long Period { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not a game is in the postseason.
        /// </summary>
        [JsonProperty("postseason")]
        public bool Postseason { get; set; }

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        [JsonProperty("season")]
        public long Season { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the visitor team id.
        /// </summary>
        [JsonProperty("visitor_team_id")]
        public long VisitorTeamId { get; set; }

        /// <summary>
        /// Gets or sets the visitor team score.
        /// </summary>
        [JsonProperty("visitor_team_score")]
        public long VisitorTeamScore { get; set; }
    }
}