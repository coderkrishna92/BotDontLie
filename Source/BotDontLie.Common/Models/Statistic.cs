// <copyright file="Statistic.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the model of a single statistic.
    /// </summary>
    public class Statistic
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the assists.
        /// </summary>
        [JsonProperty("ast")]
        public long? Assists { get; set; }

        /// <summary>
        /// Gets or sets the blocks.
        /// </summary>
        [JsonProperty("blk")]
        public long? Blocks { get; set; }

        /// <summary>
        /// Gets or sets the defensive rebounds.
        /// </summary>
        [JsonProperty("dreb")]
        public long? DefensiveRebounds { get; set; }

        /// <summary>
        /// Gets or sets the 3 pt field goal percentage.
        /// </summary>
        [JsonProperty("fg3_pct")]
        public double? ThreePointFieldGoalPct { get; set; }

        /// <summary>
        /// Gets or sets the number of 3 point field goals attempted.
        /// </summary>
        [JsonProperty("fg3a")]
        public long? ThreePointFieldGoalsAttempted { get; set; }

        /// <summary>
        /// Gets or sets the number of 3 point field goals made.
        /// </summary>
        [JsonProperty("fg3m")]
        public long? ThreePointFieldGoalsMade { get; set; }

        /// <summary>
        /// Gets or sets the field goal percentage.
        /// </summary>
        [JsonProperty("fg_pct")]
        public double? FieldGoalPct { get; set; }

        /// <summary>
        /// Gets or sets the number of field goals attempted.
        /// </summary>
        [JsonProperty("fga")]
        public long? FieldGoalsAttempted { get; set; }

        /// <summary>
        /// Gets or sets the number of field goals made.
        /// </summary>
        [JsonProperty("fgm")]
        public long? FieldGoalsMade { get; set; }

        /// <summary>
        /// Gets or sets the free throw percentage.
        /// </summary>
        [JsonProperty("ft_pct")]
        public double? FreeThrowPct { get; set; }

        /// <summary>
        /// Gets or sets the free throws attempted.
        /// </summary>
        [JsonProperty("fta")]
        public long? FreeThrowsAttempted { get; set; }

        /// <summary>
        /// Gets or sets the free throws made.
        /// </summary>
        [JsonProperty("ftm")]
        public long? FreeThrowsMade { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        [JsonProperty("game")]
        public StatsGame Game { get; set; }

        /// <summary>
        /// Gets or sets the minutes.
        /// </summary>
        [JsonProperty("min")]
        public string Minutes { get; set; }

        /// <summary>
        /// Gets or sets the offensive rebounds.
        /// </summary>
        [JsonProperty("oreb")]
        public long? OffensiveRebounds { get; set; }

        /// <summary>
        /// Gets or sets the personal fouls.
        /// </summary>
        [JsonProperty("pf")]
        public long? PersonalFouls { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        [JsonProperty("player")]
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        [JsonProperty("pts")]
        public long? Points { get; set; }

        /// <summary>
        /// Gets or sets the rebounds.
        /// </summary>
        [JsonProperty("reb")]
        public long? Rebounds { get; set; }

        /// <summary>
        /// Gets or sets the steals.
        /// </summary>
        [JsonProperty("stl")]
        public long? Steals { get; set; }

        /// <summary>
        /// Gets or sets the team.
        /// </summary>
        [JsonProperty("team")]
        public Team Team { get; set; }

        /// <summary>
        /// Gets or sets the number of turnovers.
        /// </summary>
        [JsonProperty("turnover")]
        public long? Turnover { get; set; }
    }
}