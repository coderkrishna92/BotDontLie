// <copyright file="BallDontLieService.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using Microsoft.ApplicationInsights;

    /// <summary>
    /// This class will implement the methods that are defined in the interface <see cref="IBallDontLieService"/>.
    /// </summary>
    public class BallDontLieService : IBallDontLieService
    {
        private readonly TelemetryClient telemetryClient;
        private readonly IHttpClientFactory httpClientFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallDontLieService"/> class.
        /// </summary>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="httpClientFactory">The HTTP Client Factory DI.</param>
        public BallDontLieService(TelemetryClient telemetryClient, IHttpClientFactory httpClientFactory)
        {
            this.telemetryClient = telemetryClient;
            this.httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Method implementation to return all 30 NBA franchises.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="TeamsResponse"/>.</returns>
        public async Task<TeamsResponse> RetrieveAllTeams()
        {
            this.telemetryClient.TrackTrace("Requesting to get all NBA teams");
            var httpClient = this.httpClientFactory.CreateClient("BallDontLieAPI");
            return null;
        }
    }
}