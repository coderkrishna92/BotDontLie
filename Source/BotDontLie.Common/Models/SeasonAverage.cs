// <copyright file="SeasonAverage.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the season average of a player.
    /// </summary>
    public class SeasonAverage
    {
        /// <summary>
        /// Gets or sets the games played.
        /// </summary>
        [JsonProperty("games_played")]
        public long GamesPlayed { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [JsonProperty("player_id")]
        public long PlayerId { get; set; }

        [JsonProperty("season")]
        public int Season { get; set; }

        [JsonProperty("min")]
        public string Minutes { get; set; }

        [JsonProperty("fgm")]
        public double FieldGoalsMade { get; set; }

        [JsonProperty("fga")]
        public double FielGoalsAttempted { get; set; }

        [JsonProperty("fg3m")]
        public double ThreePointFieldGoalsMade { get; set; }

        [JsonProperty("fg3a")]
        public double ThreePointFieldGoalsAttempted { get; set; }

        [JsonProperty("ftm")]
        public double FreeThrowsMade { get; set; }

        [JsonProperty("fta")]
        public double FreeThrowsAttempted { get; set; }

        [JsonProperty("oreb")]
        public double OffensiveRebounds { get; set; }

        [JsonProperty("dreb")]
        public double DefensiveRebounds { get; set; }

        [JsonProperty("reb")]
        public double Rebounds { get; set; }

        [JsonProperty("ast")]
        public double Assists { get; set; }

        [JsonProperty("stl")]
        public double Steals { get; set; }

        [JsonProperty("blk")]
        public double Blocks { get; set; }

        [JsonProperty("turnover")]
        public double Turnovers { get; set; }

        [JsonProperty("pf")]
        public double PersonalFouls { get; set; }

        [JsonProperty("pts")]
        public double Points { get; set; }

        [JsonProperty("fg_pct")]
        public double FieldGoalPct { get; set; }

        [JsonProperty("fg3_pct")]
        public double ThreePointFieldGoalPct { get; set; }

        [JsonProperty("ft_pct")]
        public double FieldThrowPct { get; set; }
    }
}