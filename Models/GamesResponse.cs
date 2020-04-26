// <copyright file="GamesResponse.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the games response.
    /// </summary>
    public class GamesResponse
    {
        /// <summary>
        /// Gets or sets the games.
        /// </summary>
        [JsonProperty("data")]
        public List<Game> Games { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}