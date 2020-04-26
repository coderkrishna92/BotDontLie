﻿// <copyright file="Meta.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models
{
    using Newtonsoft.Json;

    /// <summary>
    /// This class defines the meta properties.
    /// </summary>
    public class Meta
    {
        /// <summary>
        /// Gets or sets the total_pages.
        /// </summary>
        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }

        /// <summary>
        /// Gets or sets the current_page.
        /// </summary>
        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the next_page.
        /// </summary>
        [JsonProperty("next_page")]
        public int NextPage { get; set; }

        /// <summary>
        /// Gets or sets the per_page.
        /// </summary>
        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        /// <summary>
        /// Gets or sets the total_count.
        /// </summary>
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }
    }
}