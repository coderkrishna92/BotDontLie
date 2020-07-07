// <copyright file="StatsResponse.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class defines the StatsResponse.
    /// </summary>
    public class StatsResponse
    {
        /// <summary>
        /// Gets or sets the necessary statistics.
        /// </summary>
        [JsonProperty("data")]
        public List<Statistic> Statistics { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}