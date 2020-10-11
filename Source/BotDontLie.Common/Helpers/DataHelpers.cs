// <copyright file="DataHelpers.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Helpers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using BotDontLie.Common.Cards;
    using BotDontLie.Common.Services;
    using Microsoft.ApplicationInsights;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This class implements all of the methods defined in <see cref="IDataHelpers"/>.
    /// </summary>
    public class DataHelpers : IDataHelpers
    {
        private readonly IBallDontLieService ballDontLieService;
        private readonly TelemetryClient telemetryClient;
        private readonly ITeamHelpers teamHelpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataHelpers"/> class.
        /// </summary>
        /// <param name="ballDontLieService">The ball dont lie service API DI.</param>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="teamHelpers">Team Helpers DI.</param>
        public DataHelpers(
            IBallDontLieService ballDontLieService,
            TelemetryClient telemetryClient,
            ITeamHelpers teamHelpers)
        {
            this.ballDontLieService = ballDontLieService;
            this.telemetryClient = telemetryClient;
            this.teamHelpers = teamHelpers;
        }

        /// <summary>
        /// This method gets the player information.
        /// </summary>
        /// <param name="messageText">The raw text that the user sends the bot.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        public async Task GetPlayerInformationAsync(
            string messageText,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            if (messageText is null)
            {
                throw new ArgumentNullException(nameof(messageText));
            }

            Attachment playerResponseCard;
            var arrayOfWords = messageText.Split(' ');
            var playerFirstName = arrayOfWords[3];
            var playerLastName = arrayOfWords[4];

            var playerId = await this.ballDontLieService.GetPlayerIdByFirstLastNameAsync(playerFirstName, playerLastName).ConfigureAwait(false);
            var player = await this.ballDontLieService.GetPlayerByIdAsync(playerId).ConfigureAwait(false);

            if (player != null)
            {
                this.telemetryClient.TrackTrace($"Found the player: {player.FirstName} {player.LastName}");
                playerResponseCard = PlayerResponseCard.GetCard(player);
                await turnContext.SendActivityAsync(MessageFactory.Attachment(playerResponseCard), cancellationToken).ConfigureAwait(false);
            }
            else
            {
                this.telemetryClient.TrackTrace($"Could not find the player: {player.FirstName} {player.LastName}");
                await turnContext.SendActivityAsync(MessageFactory.Text($"Rats! Could not find anything on {playerFirstName} {playerLastName}"), cancellationToken).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// This method will get the team information.
        /// </summary>
        /// <param name="messageText">The raw text the user sends the bot.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        public async Task GetTeamInformationAsync(
            string messageText,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (messageText is null)
            {
                throw new ArgumentNullException(nameof(messageText));
            }

            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            Attachment teamResponseCard;
            var arrayOfWords = messageText.Split(' ');
            if (arrayOfWords.Length == 4)
            {
                this.telemetryClient.TrackTrace("Finding the information of a team by the short name");
                var teamShortName = arrayOfWords[3];
                var teamByShortName = await this.ballDontLieService.GetTeamByNameAsync(teamShortName).ConfigureAwait(false);

                if (teamByShortName != null)
                {
                    this.telemetryClient.TrackTrace($"Found the team: {teamByShortName}");
                    teamResponseCard = TeamResponseCard.GetCard(teamByShortName);
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(teamResponseCard), cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    this.telemetryClient.TrackTrace($"Was not able to find data on: {teamShortName}");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Oops! I bricked! I couldn't get your team for you!"), cancellationToken).ConfigureAwait(false);
                }
            }
            else
            {
                this.telemetryClient.TrackTrace("Finding the information of a team by the full name");
                string[] teamFullName = this.teamHelpers.ExtractTeamName(arrayOfWords);
                var teamFullNameStr = this.teamHelpers.GetTeamFullNameStr(teamFullName);
                var teamByFullName = await this.ballDontLieService.GetTeamByFullNameAsync(teamFullNameStr).ConfigureAwait(false);

                if (teamByFullName != null)
                {
                    this.telemetryClient.TrackTrace($"Found the team: {teamByFullName?.FullName}");
                    teamResponseCard = TeamResponseCard.GetCard(teamByFullName);
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(teamResponseCard), cancellationToken).ConfigureAwait(false);
                }
                else
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text("Oops! I bricked! I couldn't get your team for you!"), cancellationToken).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// This method will sync all the games.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        public async Task SyncGamesAsync(ITurnContext<IMessageActivity> turnContext)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var gamesResponse = await this.ballDontLieService.SyncAllGamesAsync().ConfigureAwait(false);
            if (gamesResponse)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("I am able to sync the games in the NBA from 1979 to present - that's a lot of data!")).ConfigureAwait(false);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Not able to get any data with regards to the games - gotta try again later!")).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// This method will sync the players.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        public async Task SyncPlayersAsync(ITurnContext<IMessageActivity> turnContext)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var playersResponse = await this.ballDontLieService.SyncAllPlayersAsync().ConfigureAwait(false);
            if (playersResponse)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Got all the players! Want to build a roster, or you want some information?")).ConfigureAwait(false);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Not able to get any of the players data!")).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// This method will sync the statistics.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        public async Task SyncStatsAsync(ITurnContext<IMessageActivity> turnContext)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var statsResponse = await this.ballDontLieService.SyncAllStatisticsAsync().ConfigureAwait(false);
            if (statsResponse)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("I am able to get the stats for you - that's a lot of numbers to crunch on it!!")).ConfigureAwait(false);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Not able to get any of the statistical data!")).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// This method will sync the NBA teams.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        public async Task SyncTeamsAsync(ITurnContext<IMessageActivity> turnContext)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var teamsResponse = await this.ballDontLieService.SyncAllTeamsAsync().ConfigureAwait(false);
            if (teamsResponse)
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("All the way from downtown - I am able to sync the teams for you!")).ConfigureAwait(false);
            }
            else
            {
                await turnContext.SendActivityAsync(MessageFactory.Text("Not able to get any data for the teams. Will have to try again later.")).ConfigureAwait(false);
            }
        }
    }
}
