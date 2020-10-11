// <copyright file="StatsResponse.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
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
#pragma warning disable CA2227 // Collection properties should be read only
        public List<Statistic> Statistics { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}