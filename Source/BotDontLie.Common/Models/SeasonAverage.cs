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

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        [JsonProperty("season")]
        public int Season { get; set; }

        /// <summary>
        /// Gets or sets the minutes that the player played.
        /// </summary>
        [JsonProperty("min")]
        public string Minutes { get; set; }

        /// <summary>
        /// Gets or sets the field goals made.
        /// </summary>
        [JsonProperty("fgm")]
        public double FieldGoalsMade { get; set; }

        /// <summary>
        /// Gets or sets the number of field goals attempted.
        /// </summary>
        [JsonProperty("fga")]
        public double FieldGoalsAttempted { get; set; }

        /// <summary>
        /// Gets or sets the number of 3 point field goals made.
        /// </summary>
        [JsonProperty("fg3m")]
        public double ThreePointFieldGoalsMade { get; set; }

        /// <summary>
        /// Gets or sets the number of 3 point field goals attempted.
        /// </summary>
        [JsonProperty("fg3a")]
        public double ThreePointFieldGoalsAttempted { get; set; }

        /// <summary>
        /// Gets or sets the number of free throws made.
        /// </summary>
        [JsonProperty("ftm")]
        public double FreeThrowsMade { get; set; }

        /// <summary>
        /// Gets or sets the number of free throws attempted.
        /// </summary>
        [JsonProperty("fta")]
        public double FreeThrowsAttempted { get; set; }

        /// <summary>
        /// Gets or sets the number of offensive rebounds.
        /// </summary>
        [JsonProperty("oreb")]
        public double OffensiveRebounds { get; set; }

        /// <summary>
        /// Gets or sets the number of defensive rebounds.
        /// </summary>
        [JsonProperty("dreb")]
        public double DefensiveRebounds { get; set; }

        /// <summary>
        /// Gets or sets the number of rebounds.
        /// </summary>
        [JsonProperty("reb")]
        public double Rebounds { get; set; }

        /// <summary>
        /// Gets or sets the number of assists.
        /// </summary>
        [JsonProperty("ast")]
        public double Assists { get; set; }

        /// <summary>
        /// Gets or sets the number of steals.
        /// </summary>
        [JsonProperty("stl")]
        public double Steals { get; set; }

        /// <summary>
        /// Gets or sets the number of blocks.
        /// </summary>
        [JsonProperty("blk")]
        public double Blocks { get; set; }

        /// <summary>
        /// Gets or sets the number of turnovers.
        /// </summary>
        [JsonProperty("turnover")]
        public double Turnovers { get; set; }

        /// <summary>
        /// Gets or sets the number of personal fouls.
        /// </summary>
        [JsonProperty("pf")]
        public double PersonalFouls { get; set; }

        /// <summary>
        /// Gets or sets the number of points.
        /// </summary>
        [JsonProperty("pts")]
        public double Points { get; set; }

        /// <summary>
        /// Gets or sets the field goal percentage.
        /// </summary>
        [JsonProperty("fg_pct")]
        public double FieldGoalPct { get; set; }

        /// <summary>
        /// Gets or sets the 3 pt field goal percentage.
        /// </summary>
        [JsonProperty("fg3_pct")]
        public double ThreePointFieldGoalPct { get; set; }

        /// <summary>
        /// Gets or sets the free throw percentage.
        /// </summary>
        [JsonProperty("ft_pct")]
        public double FreeThrowPct { get; set; }
    }
}