﻿// <copyright file="BallDontLieService.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Services
{
    using System.Globalization;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotDontLie.Models;
    using BotDontLie.Models.AzureStorage;
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
        private readonly IPlayersProvider playersProvider;
        private readonly IGamesProvider gamesProvider;
        private readonly IStatisticsProvider statisticsProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="BallDontLieService"/> class.
        /// </summary>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="httpClientFactory">The HTTP Client Factory DI.</param>
        /// <param name="teamsProvider">The NBA Teams Provider DI.</param>
        /// <param name="playersProvider">The NBA Players Provider DI.</param>
        /// <param name="gamesProvider">The NBA Games Provider DI.</param>
        /// <param name="statisticsProvider">The NBA Statistics Provider DI.</param>
        public BallDontLieService(
            TelemetryClient telemetryClient,
            IHttpClientFactory httpClientFactory,
            ITeamsProvider teamsProvider,
            IPlayersProvider playersProvider,
            IGamesProvider gamesProvider,
            IStatisticsProvider statisticsProvider)
        {
            this.telemetryClient = telemetryClient;
            this.httpClientFactory = httpClientFactory;
            this.teamsProvider = teamsProvider;
            this.playersProvider = playersProvider;
            this.gamesProvider = gamesProvider;
            this.statisticsProvider = statisticsProvider;
        }

        /// <summary>
        /// Method implementation to return all 30 NBA franchises.
        /// </summary>
        /// <returns>A unit of execution that contains the type of <see cref="bool"/>.</returns>
        public async Task<bool> SyncAllTeamsAsync()
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
                    foreach (var item in teamsResponse.Teams)
                    {
                        var teamEntity = this.CreateTeamEntity(item);
                        await this.teamsProvider.UpsertNbaTeamAsync(teamEntity).ConfigureAwait(false);
                    }

                    return true;
                }
                else
                {
                    this.telemetryClient.TrackTrace("Not able to get the teams list fully");
                    return false;
                }
            }
        }

        /// <summary>
        /// Method implementation to get all the games available.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="bool"/>.</returns>
        public async Task<bool> SyncAllGamesAsync()
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
                    foreach (var item in gamesResponse.Games)
                    {
                        var gameEntity = this.CreateGameEntity(item);
                        await this.gamesProvider.UpsertNbaGameAsync(gameEntity).ConfigureAwait(false);
                    }

                    return true;
                }
                else
                {
                    this.telemetryClient.TrackTrace("Not able to get the games fully");
                    return false;
                }
            }
        }

        /// <summary>
        /// Method implementation to get all the players available.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="bool"/>.</returns>
        public async Task<bool> SyncAllPlayersAsync()
        {
            this.telemetryClient.TrackTrace("Requesting to get all players");
            return true;
        }

        /// <summary>
        /// Method implementation to get all the stats available.
        /// </summary>
        /// <returns>A unit of execution that contains a type of <see cref="StatsResponse"/>.</returns>
        public async Task<bool> SyncAllStatisticsAsync()
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
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// Implementation to get the playerID by querying the first and last name of the player.
        /// </summary>
        /// <param name="firstName">The first name of the player.</param>
        /// <param name="lastName">The last name of the player.</param>
        /// <returns>A unit of execution that contains a type <see cref="long"/>.</returns>
        public async Task<long> GetPlayerIdByFirstLastNameAsync(string firstName, string lastName)
        {
            var playerOfInterest = await this.playersProvider.GetPlayerEntityByFullNameAsync(firstName, lastName).ConfigureAwait(false);
            return (long)playerOfInterest?.PlayerId;
        }

        /// <summary>
        /// Method implementation to get a team by their name (i.e. Knicks).
        /// </summary>
        /// <param name="teamName">The team name.</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        public async Task<Team> RetrieveTeamByNameAsync(string teamName)
        {
            this.telemetryClient.TrackTrace($"Getting a team by the name: {teamName}");
            var teamsResponse = await this.teamsProvider.GetTeamByNameAsync(teamName).ConfigureAwait(false);

            return new Team
            {
                Abbreviation = teamsResponse.Abbreviation,
                City = teamsResponse.City,
                Id = teamsResponse.TeamId,
                Conference = teamsResponse.Conference,
                Division = teamsResponse.Division,
                FullName = teamsResponse.FullName,
                Name = teamsResponse.Name,
            };
        }

        /// <summary>
        /// Method implementation to get a team by their full name (i.e. Oklahoma City Thunder).
        /// </summary>
        /// <param name="teamFullName">The full/formal name of the NBA franchise.</param>
        /// <returns>A unit of execution that contains a type of <see cref="Team"/>.</returns>
        public async Task<Team> RetrieveTeamByFullNameAsync(string teamFullName)
        {
            this.telemetryClient.TrackTrace($"Getting a team by the full name: {teamFullName}");
            var teamsResponse = await this.teamsProvider.GetTeamByFullNameAsync(teamFullName).ConfigureAwait(false);

            return new Team
            {
                Id = teamsResponse.TeamId,
                City = teamsResponse.City,
                Abbreviation = teamsResponse.Abbreviation,
                Conference = teamsResponse.Conference,
                Division = teamsResponse.Division,
                FullName = teamsResponse.FullName,
                Name = teamsResponse.Name,
            };
        }

        private TeamEntity CreateTeamEntity(Team team)
        {
            return new TeamEntity
            {
                TeamId = team.Id,
                RowKey = team.Id.ToString(CultureInfo.InvariantCulture),
                PartitionKey = "NbaTeam",
                Abbreviation = team.Abbreviation,
                City = team.City,
                Conference = team.Conference,
                Division = team.Division,
                FullName = team.FullName,
                Name = team.Name,
            };
        }

        private GameEntity CreateGameEntity(Game game)
        {
            return new GameEntity
            {
                GameId = game.Id,
                RowKey = game.Id.ToString(CultureInfo.InvariantCulture),
                PartitionKey = "NbaGame",
                Date = game.Date,
                HomeTeam = game.HomeTeam,
                HomeTeamScore = game.HomeTeamScore,
                Period = game.Period,
                Season = game.Season,
                Postseason = game.Postseason,
                Time = game.Time,
                Status = game.Status,
                VisitorTeam = game.VisitorTeam,
                VisitorTeamScore = game.VisitorTeamScore,
            };
        }
    }
}