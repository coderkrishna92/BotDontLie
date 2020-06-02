// <copyright file="GameEntity.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models.AzureStorage
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.WindowsAzure.Storage.Table;
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

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("home_team")]
        public Team HomeTeam { get; set; }

        [JsonProperty("home_team_score")]
        public long HomeTeamScore { get; set; }

        [JsonProperty("period")]
        public long Period { get; set; }

        [JsonProperty("postseason")]
        public bool Postseason { get; set; }

        [JsonProperty("season")]
        public long Season { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("time")]
        public string Time { get; set; }

        [JsonProperty("visitor_team")]
        public Team VisitorTeam { get; set; }

        [JsonProperty("visitor_team_score")]
        public long VisitorTeamScore { get; set; }
    }
}