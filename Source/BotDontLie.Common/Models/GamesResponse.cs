// <copyright file="GamesResponse.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class represents the HTTP response for a games request.
    /// </summary>
    public class GamesResponse
    {
        /// <summary>
        /// Gets or sets the games.
        /// </summary>
        [JsonProperty("data")]
#pragma warning disable CA2227 // Collection properties should be read only
        public List<Game> Games { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}