// <copyright file="PlayersResponse.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the players response.
    /// </summary>
    public class PlayersResponse
    {
        /// <summary>
        /// Gets or sets the list of players.
        /// </summary>
        [JsonProperty("data")]
#pragma warning disable CA2227 // Collection properties should be read only
        public List<Player> Players { get; set; }
#pragma warning restore CA2227 // Collection properties should be read only

        /// <summary>
        /// Gets or sets the meta.
        /// </summary>
        [JsonProperty("meta")]
        public Meta Meta { get; set; }
    }
}