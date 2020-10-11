// <copyright file="Game.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models a single game.
    /// </summary>
    public class Game
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
        /// Gets or sets the home_team.
        /// </summary>
        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the home_team_score.
        /// </summary>
        [JsonProperty("home_team_score")]
        public long HomeTeamScore { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        [JsonProperty("period")]
        public long Period { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the game happened in the postseason.
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
        /// Gets or sets the visitor_team.
        /// </summary>
        [JsonProperty("visitor_team")]
        public Team VisitorTeam { get; set; }

        /// <summary>
        /// Gets or sets the visitor_team_score.
        /// </summary>
        [JsonProperty("visitor_team_score")]
        public long VisitorTeamScore { get; set; }
    }
}