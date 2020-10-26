// <copyright file="SeasonAverageResponse.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the season average response for a player.
    /// </summary>
    public class SeasonAverageResponse
    {
        /// <summary>
        /// Gets or sets the season average.
        /// </summary>
        [JsonProperty("data")]
        public SeasonAverage SeasonAverage { get; set; }
    }
}