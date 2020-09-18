// <copyright file="NbaBot.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using BotDontLie.Common.Cards;
    using BotDontLie.Common.Helpers;
    using BotDontLie.Common.Models;
    using BotDontLie.Common.Properties;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class is for the NBA Bot.
    /// </summary>
    public class NbaBot : ActivityHandler
    {
        private readonly string appBaseUri;
        private readonly TelemetryClient telemetryClient;
        private readonly IDataHelpers dataHelpers;

        /// <summary>
        /// Initializes a new instance of the <see cref="NbaBot"/> class.
        /// </summary>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="appBaseUri">The application base URI.</param>
        /// <param name="dataHelpers">The data helpers class DI.</param>
        public NbaBot(
            TelemetryClient telemetryClient,
            string appBaseUri,
            IDataHelpers dataHelpers)
        {
            this.appBaseUri = appBaseUri;
            this.telemetryClient = telemetryClient;
            this.dataHelpers = dataHelpers;
        }

        /// <summary>
        /// This method will fire whenever there is an activity between the user and the bot.
        /// </summary>
        /// <param name="turnContext">The current turn/execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        public override Task OnTurnAsync(
            ITurnContext turnContext,
            CancellationToken cancellationToken = default)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            return turnContext.Activity.Type switch
            {
                ActivityTypes.Message => this.OnMessageActivityAsync(new DelegatingTurnContext<IMessageActivity>(turnContext), cancellationToken),
                ActivityTypes.ConversationUpdate => this.OnConversationUpdateActivityAsync(new DelegatingTurnContext<IConversationUpdateActivity>(turnContext), cancellationToken),
                ActivityTypes.MessageReaction => this.OnMessageReactionActivityAsync(new DelegatingTurnContext<IMessageReactionActivity>(turnContext), cancellationToken),
                _ => base.OnTurnAsync(turnContext, cancellationToken),
            };
        }

        /// <summary>
        /// This method fires whenever a message is coming into the bot.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMessageActivityAsync(
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            try
            {
                var message = turnContext.Activity;

                this.telemetryClient.TrackTrace("Received message activity");
                this.telemetryClient.TrackTrace($"from: {message.From?.Id}, conversation: {message.Conversation.Id}, replyToId: {message.ReplyToId}");

                await this.SendTypingIndicatorAsync(turnContext).ConfigureAwait(false);

                await this.OnMessageActivityInPersonalChatAsync(message, turnContext, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.telemetryClient.TrackTrace($"Error processing message: {ex.Message}", SeverityLevel.Error);
                this.telemetryClient.TrackException(ex);
                throw;
            }
        }

        /// <summary>
        /// This method executes whenever there is an on conversation update happening.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnConversationUpdateActivityAsync(
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            try
            {
                var activity = turnContext.Activity;

                this.telemetryClient.TrackTrace($"Received conversationUpdate activity");
                this.telemetryClient.TrackTrace($"conversationType: {activity.Conversation.ConversationType}, membersAdded: {activity.MembersAdded?.Count}, membersRemoved: {activity.MembersRemoved?.Count}");

                await this.OnMembersAddedAsync(activity.MembersAdded, turnContext, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                this.telemetryClient.TrackTrace($"Error processing conversationUpdate: {ex.Message}", SeverityLevel.Error);
                this.telemetryClient.TrackException(ex);
                throw;
            }
        }

        /// <summary>
        /// This method fires whenever a new member has been added.
        /// </summary>
        /// <param name="membersAdded">The list of members being added.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMembersAddedAsync(
            IList<ChannelAccount> membersAdded,
            ITurnContext<IConversationUpdateActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            if (membersAdded is null)
            {
                throw new ArgumentNullException(nameof(membersAdded));
            }

            this.telemetryClient.TrackTrace("Sending the welcome message");
            var welcomeText = BotResource.WelcomeText;
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    var userWelcomeCardAttachment = WelcomeCard.GetCard(welcomeText);
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(userWelcomeCardAttachment), cancellationToken).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Handles message activity in 1:1 chat.
        /// </summary>
        /// <param name="message">The incoming activity.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        private async Task OnMessageActivityInPersonalChatAsync(
            IMessageActivity message,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(message.ReplyToId) && (message.Value != null) && ((JObject)message.Value).HasValues)
            {
                this.telemetryClient.TrackTrace("Card submit in 1:1 chat");
                await this.OnAdaptiveCardSubmitInPersonalChatAsync(message?.Text, turnContext, cancellationToken).ConfigureAwait(false);
                return;
            }

            string text = (message.Text ?? string.Empty).Trim().ToLower(CultureInfo.InvariantCulture);

            switch (text)
            {
                case Constants.TakeATour:
                    this.telemetryClient.TrackTrace("Sending the user tour card");
                    var userTourCards = TourCarousel.GetUserTourCards(this.appBaseUri);
                    await turnContext.SendActivityAsync(MessageFactory.Carousel(userTourCards), cancellationToken).ConfigureAwait(false);
                    break;
                case Constants.SyncAllTeams:
                    this.telemetryClient.TrackTrace("Querying to list all of the NBA Teams");
                    await this.dataHelpers.SyncTeamsAsync(turnContext).ConfigureAwait(false);
                    break;
                case Constants.SyncAllGames:
                    this.telemetryClient.TrackTrace("Syncing all games from 1979 to present");
                    await this.dataHelpers.SyncGamesAsync(turnContext).ConfigureAwait(false);
                    break;
                case Constants.SyncAllPlayers:
                    this.telemetryClient.TrackTrace("Syncing all the players");
                    await this.dataHelpers.SyncPlayersAsync(turnContext).ConfigureAwait(false);
                    break;
                case Constants.SyncAllStats:
                    this.telemetryClient.TrackTrace("Syncing all the stats for the players");
                    await this.dataHelpers.SyncStatsAsync(turnContext).ConfigureAwait(false);
                    break;
                default:
                    this.telemetryClient.TrackTrace("There may be some other actions taking place");
                    await this.ActOnMoreInformationAsync(text, turnContext, cancellationToken).ConfigureAwait(false);
                    break;
            }
        }

        private async Task ActOnMoreInformationAsync(
            string messageText,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            this.telemetryClient.TrackTrace($"There is something to be done: {messageText}");
            if (messageText.Contains(Constants.FindPlayerInformation, StringComparison.InvariantCultureIgnoreCase))
            {
                this.telemetryClient.TrackTrace("Finding the player information");
                await this.dataHelpers.GetPlayerInformationAsync(messageText, turnContext, cancellationToken).ConfigureAwait(false);
            }
            else if (messageText.Contains(Constants.FindTeamInformation, StringComparison.InvariantCultureIgnoreCase))
            {
                this.telemetryClient.TrackTrace("Finding the team information");
                await this.dataHelpers.GetTeamInformationAsync(messageText, turnContext, cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task OnAdaptiveCardSubmitInPersonalChatAsync(
            string text,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken)
        {
            this.telemetryClient.TrackTrace("Adaptive card submit in personal chat");

            switch (text)
            {
                case Constants.TakeATour:
                    this.telemetryClient.TrackTrace("Sending the user tour card");
                    var userTourCards = TourCarousel.GetUserTourCards(this.appBaseUri);
                    await turnContext.SendActivityAsync(MessageFactory.Carousel(userTourCards), cancellationToken).ConfigureAwait(false);
                    break;
                default:
                    this.telemetryClient.TrackTrace("Not sure of what's going on here, sending the unrecognized input card");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Not sure of what I can do here, instead take a tour to find out more"), cancellationToken).ConfigureAwait(false);
                    break;
            }
        }

        // Sending the typing indicator to the user.
        private async Task SendTypingIndicatorAsync(ITurnContext turnContext)
        {
            try
            {
                var typingActivity = turnContext.Activity.CreateReply();
                typingActivity.Type = ActivityTypes.Typing;
                await turnContext.SendActivityAsync(typingActivity).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                // Do not fail on errors sending the typing indicator
                this.telemetryClient.TrackTrace($"Failed to send a typing indicator: {ex.Message}", SeverityLevel.Warning);
                this.telemetryClient.TrackException(ex);
                throw;
            }
        }
    }
}