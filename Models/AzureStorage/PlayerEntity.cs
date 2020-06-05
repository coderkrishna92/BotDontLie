﻿// <copyright file="PlayerEntity.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Models.AzureStorage
{
    using System.ComponentModel.DataAnnotations;
    using Microsoft.WindowsAzure.Storage.Table;
    using Newtonsoft.Json;

    /// <summary>
    /// This model represents the player entity.
    /// </summary>
    public class PlayerEntity : TableEntity
    {
        /// <summary>
        /// Gets or sets the player ID.
        /// </summary>
        [Key]
        [JsonProperty("PlayerId")]
        public long PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the first_name.
        /// </summary>
        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the height of the player in feet.
        /// </summary>
        [JsonProperty("height_feet")]
        public long? HeightFeet { get; set; }

        /// <summary>
        /// Gets or sets the height of the player in inches.
        /// </summary>
        [JsonProperty("height_inches")]
        public long? HeightInches { get; set; }

        /// <summary>
        /// Gets or sets the player last_name.
        /// </summary>
        [JsonProperty("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the position of the player.
        /// </summary>
        [JsonProperty("position")]
        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the team that the player belongs to.
        /// </summary>
        [JsonProperty("team")]
        public Team Team { get; set; }

        /// <summary>
        /// Gets or sets the weight of the player in pounds.
        /// </summary>
        [JsonProperty("weight_pounds")]
        public long? WeightPounds { get; set; }
    }
}