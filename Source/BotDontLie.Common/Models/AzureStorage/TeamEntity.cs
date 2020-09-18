// <copyright file="TeamEntity.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Models.AzureStorage
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.Azure.Cosmos.Table;
    using Newtonsoft.Json;

    /// <summary>
    /// This class defines the NBA Team to be stored in Azure table storage.
    /// </summary>
    public class TeamEntity : TableEntity
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Key]
        [JsonProperty("TeamId")]
        public long TeamId { get; set; }

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