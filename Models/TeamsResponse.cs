// <copyright file="TeamsResponse.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the Teams API response.
    /// </summary>
    public class TeamsResponse
    {
        /// <summary>
        /// Gets or sets the list of teams.
        /// </summary>
        [JsonProperty("data")]
        public List<Team> Teams { get; set; }

        /// <summary>
        /// Gets or sets the meta data.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}