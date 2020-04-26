﻿// <copyright file="Team.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class models the team.
    /// </summary>
    public class Team
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        [JsonProperty("abbreviation")]
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the conference.
        /// </summary>
        [JsonProperty("conference")]
        public string Conference { get; set; }

        /// <summary>
        /// Gets or sets the division.
        /// </summary>
        [JsonProperty("division")]
        public string Division { get; set; }

        /// <summary>
        /// Gets or sets the full_name.
        /// </summary>
        [JsonProperty("full_name")]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}