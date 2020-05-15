// <copyright file="BallDontLieService.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Services
{
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using BotDontLie.Providers;
    using Microsoft.ApplicationInsights;
    using Newtonsoft.Json;

    /// <summary>
    /// This class will implement the methods that are defined in the interface <see cref="IBallDontLieService"/>.
    /// </summary>
    public class BallDontLieService : IBallDontLieService
    {
        private readonly TelemetryClient telemetryClient;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ITeamsProvider teamsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallDontLieService"/> class.
        /// </summary>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="httpClientFactory">The HTTP Client Factory DI.</param>
        /// <param name="teamsProvider">The NBA Teams Provider DI.</param>
        public BallDontLieService(TelemetryClient telemetryClient, IHttpClientFactory httpClientFactory, ITeamsProvider teamsProvider)
        {
            this.telemetryClient = telemetryClient;
            this.httpClientFactory = httpClientFactory;
            this.teamsProvider = teamsProvider;
        }

        /// <summary>
        /// Method implementation to return all 30 NBA franchises.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="TeamsResponse"/>.</returns>
        public async Task<TeamsResponse> RetrieveAllTeamsAsync()
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
                    this.telemetryClient.TrackTrace("Not able to get the teams list fully");
                    return null;
                }
            }
        }

        /// <summary>
        /// Method implementation to get all the games available.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="GamesResponse"/>.</returns>
        public async Task<GamesResponse> RetrieveAllGamesAsync()
        {
            this.telemetryClient.TrackTrace("Requesting to get all games");
            var httpClient = this.httpClientFactory.CreateClient("BallDontLieAPI");

            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "games")
            {
            })
            {
                var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var gamesResponse = JsonConvert.DeserializeObject<GamesResponse>(responseContent);
                    return gamesResponse;
                }
                else
                {
                    this.telemetryClient.TrackTrace("Not able to get the games fully");
                    return null;
                }
            }
        }

        /// <summary>
        /// Method implementation to get all the stats available.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="StatsResponse"/>.</returns>
        public async Task<StatsResponse> RetrieveAllStatsAsync()
        {
            this.telemetryClient.TrackTrace("Requesting to get all NBA stats");
            var httpClient = this.httpClientFactory.CreateClient("BallDontLieAPI");

            using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "stats")
            {
            })
            {
                var response = await httpClient.SendAsync(httpRequestMessage).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var statsResponse = JsonConvert.DeserializeObject<StatsResponse>(responseContent);
                    return statsResponse;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Method implementation to get a team by their name (i.e. Knicks).
        /// </summary>
        /// <param name="teamName">The team name.</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        public async Task<Team> RetrieveTeamByNameAsync(string teamName)
        {
            this.telemetryClient.TrackTrace($"Getting a team by the name: {teamName}");
            var teamsResponse = await this.RetrieveAllTeamsAsync().ConfigureAwait(false);
            var allTeamsList = teamsResponse.Teams;
            var teamToReturn = allTeamsList.FirstOrDefault(x => x.Name == teamName);
            return teamToReturn;
        }

        /// <summary>
        /// Method implementation to get a team by their full name (i.e. Oklahoma City Thunder).
        /// </summary>
        /// <param name="teamFullName">The full/formal name of the NBA franchise.</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        public async Task<Team> RetrieveTeamByFullNameAsync(string teamFullName)
        {
            this.telemetryClient.TrackTrace($"Getting a team by the full name: {teamFullName}");
            var teamsResponse = await this.RetrieveAllTeamsAsync().ConfigureAwait(false);
            var allTeamsList = teamsResponse.Teams;
            var teamToReturn = allTeamsList.FirstOrDefault(x => x.FullName == teamFullName);
            return teamToReturn;
        }
    }
}