// <copyright file="NbaBot.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using BotDontLie.Cards;
    using BotDontLie.Models;
    using BotDontLie.Properties;
    using BotDontLie.Services;
    using Microsoft.ApplicationInsights;
    using Microsoft.ApplicationInsights.DataContracts;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// This class is for the NBA Bot.
    /// </summary>
    public class NbaBot : ActivityHandler
    {
        private readonly string appBaseUri;
        private readonly TelemetryClient telemetryClient;
        private readonly MicrosoftAppCredentials microsoftAppCredentials;
        private readonly IBallDontLieService ballDontLieService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NbaBot"/> class.
        /// </summary>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="microsoftAppCredentials">Microsoft App Credentials DI.</param>
        /// <param name="ballDontLieService">Calling NBA APIs DI.</param>
        /// <param name="appBaseUri">The application base URI.</param>
        public NbaBot(TelemetryClient telemetryClient, MicrosoftAppCredentials microsoftAppCredentials, IBallDontLieService ballDontLieService, string appBaseUri)
        {
            this.appBaseUri = appBaseUri;
            this.telemetryClient = telemetryClient;
            this.microsoftAppCredentials = microsoftAppCredentials;
            this.ballDontLieService = ballDontLieService;
        }

        /// <summary>
        /// This method will fire whenever there is an activity between the user and the bot.
        /// </summary>
        /// <param name="turnContext">The current turn/execution.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        public override Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        {
            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            switch (turnContext.Activity.Type)
            {
                case ActivityTypes.Message:
                    return this.OnMessageActivityAsync(new DelegatingTurnContext<IMessageActivity>(turnContext), cancellationToken);

                case ActivityTypes.ConversationUpdate:
                    return this.OnConversationUpdateActivityAsync(new DelegatingTurnContext<IConversationUpdateActivity>(turnContext), cancellationToken);

                case ActivityTypes.MessageReaction:
                    return this.OnMessageReactionActivityAsync(new DelegatingTurnContext<IMessageReactionActivity>(turnContext), cancellationToken);

                default:
                    return base.OnTurnAsync(turnContext, cancellationToken);
            }
        }

        /// <summary>
        /// This method fires whenever a message is coming into the bot.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
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
                    await turnContext.SendActivityAsync(MessageFactory.Attachment(userWelcomeCardAttachment)).ConfigureAwait(false);
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
                    await turnContext.SendActivityAsync(MessageFactory.Carousel(userTourCards)).ConfigureAwait(false);
                    break;
                case Constants.ListAllTeams:
                    this.telemetryClient.TrackTrace("Querying to list all of the NBA Teams");
                    var teamsResponse = await this.ballDontLieService.RetrieveAllTeamsAsync().ConfigureAwait(false);
                    if (teamsResponse != null)
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text($"All the way from downtown - I got {teamsResponse.Teams.Count} teams for you!")).ConfigureAwait(false);
                    }
                    else
                    {
                        await turnContext.SendActivityAsync(MessageFactory.Text("Not able to get any data for the teams. Will have to try again later.")).ConfigureAwait(false);
                    }

                    break;
                case Constants.ListAllGames:
                    this.telemetryClient.TrackTrace("Querying to get all games from 1979 to present");
                    await turnContext.SendActivityAsync(MessageFactory.Text("This functionality is in progress, hold on will get your results soon")).ConfigureAwait(false);
                    break;
                case Constants.ListAllPlayers:
                    this.telemetryClient.TrackTrace("Querying to get all the players");
                    await turnContext.SendActivityAsync(MessageFactory.Text("This functionality is in progress, hold on will get your results")).ConfigureAwait(false);
                    break;
                case Constants.ListAllStats:
                    this.telemetryClient.TrackTrace("Querying to get all the stats for the players");
                    await turnContext.SendActivityAsync(MessageFactory.Text("This functionality is in progress, hold on will get your results")).ConfigureAwait(false);
                    break;
                default:
                    this.telemetryClient.TrackTrace("Not sure of what's going on here, sending the unrecognized input card");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Not sure of what I can do here, instead take a tour to find out more")).ConfigureAwait(false);
                    break;
            }
        }

        private async Task OnAdaptiveCardSubmitInPersonalChatAsync(string text, ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            this.telemetryClient.TrackTrace("Adaptive card submit in personal chat");

            switch (text)
            {
                case Constants.TakeATour:
                    this.telemetryClient.TrackTrace("Sending the user tour card");
                    var userTourCards = TourCarousel.GetUserTourCards(this.appBaseUri);
                    await turnContext.SendActivityAsync(MessageFactory.Carousel(userTourCards)).ConfigureAwait(false);
                    break;
                default:
                    this.telemetryClient.TrackTrace("Not sure of what's going on here, sending the unrecognized input card");
                    await turnContext.SendActivityAsync(MessageFactory.Text("Not sure of what I can do here, instead take a tour to find out more")).ConfigureAwait(false);
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