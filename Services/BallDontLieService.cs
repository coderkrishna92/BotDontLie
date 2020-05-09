// <copyright file="BallDontLieService.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Services
{
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using Microsoft.ApplicationInsights;
    using Newtonsoft.Json;

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

            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "teams")
            {
            })
            {
                var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var teamsResponse = JsonConvert.DeserializeObject<TeamsResponse>(responseContent);
                    return teamsResponse;
                }
                else
                {
                    this.telemetryClient.TrackTrace("Was not able to get the teams list fully");
                    return null;
                }
            }
        }

        /// <summary>
        /// Method implementation to get a team by their name (i.e. Knicks).
        /// </summary>
        /// <param name="teamName">The team name.</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        public async Task<Team> RetrieveTeamByName(string teamName)
        {
            var teamsResponse = await this.RetrieveAllTeams().ConfigureAwait(false);
            return null;
        }

        /// <summary>
        /// Method implementation to get a team by their full name (i.e. Oklahoma City Thunder).
        /// </summary>
        /// <param name="teamFullName">The full/formal name of the NBA franchise.</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        public async Task<Team> RetrieveTeamByFullName(string teamFullName)
        {
            var teamsResponse = await this.RetrieveAllTeams().ConfigureAwait(false);
            return null;
        }
    }
}