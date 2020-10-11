// <copyright file="GameEntity.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models.AzureStorage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Azure.Cosmos.Table;
    using Newtonsoft.Json;

    /// <summary>
    /// This model defines the properties that are to be captured as part of the GameInfo table.
    /// </summary>
    public class GameEntity : TableEntity
    {
        /// <summary>
        /// Gets or sets the gameId.
        /// </summary>
        [Key]
        [JsonProperty("GameId")]
        public long GameId { get; set; }

        /// <summary>
        /// Gets or sets the date of the game.
        /// </summary>
        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        /// <summary>
        /// Gets or sets the home team.
        /// </summary>
        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        /// <summary>
        /// Gets or sets the home team score.
        /// </summary>
        [JsonProperty("home_team_score")]
        public long HomeTeamScore { get; set; }

        /// <summary>
        /// Gets or sets the period.
        /// </summary>
        [JsonProperty("period")]
        public long Period { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not the game was in the postseason.
        /// </summary>
        [JsonProperty("postseason")]
        public bool Postseason { get; set; }

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        [JsonProperty("season")]
        public long Season { get; set; }

        /// <summary>
        /// Gets or sets the status of the game.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the time of the game.
        /// </summary>
        [JsonProperty("time")]
        public string Time { get; set; }

        /// <summary>
        /// Gets or sets the visitor team.
        /// </summary>
        [JsonProperty("visitor_team")]
        public Team VisitorTeam { get; set; }

        /// <summary>
        /// Gets or sets the visitor team score.
        /// </summary>
        [JsonProperty("visitor_team_score")]
        public long VisitorTeamScore { get; set; }
    }
}