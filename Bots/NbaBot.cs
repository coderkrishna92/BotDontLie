// <copyright file="NbaBot.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie.Bots
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.ApplicationInsights;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This class is for the NBA Bot.
    /// </summary>
    public class NbaBot : ActivityHandler
    {
        private readonly string appBaseUri;
        private readonly TelemetryClient telemetryClient;
        private readonly MicrosoftAppCredentials microsoftAppCredentials;

        /// <summary>
        /// Initializes a new instance of the <see cref="NbaBot"/> class.
        /// </summary>
        /// <param name="telemetryClient">Application Insights DI.</param>
        /// <param name="microsoftAppCredentials">Microsoft App Credentials DI.</param>
        /// <param name="appBaseUri">The application base URI.</param>
        public NbaBot(TelemetryClient telemetryClient, MicrosoftAppCredentials microsoftAppCredentials, string appBaseUri)
        {
            this.appBaseUri = appBaseUri;
            this.telemetryClient = telemetryClient;
            this.microsoftAppCredentials = microsoftAppCredentials;
        }

        /// <summary>
        /// This method executes whenever there is a message coming into the bot.
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

            var replyText = $"Echo: {turnContext.Activity.Text}";
            await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// This method fires whenever a new member is added, or if the bot is installed in a personal scope.
        /// </summary>
        /// <param name="membersAdded">The list of members that are added.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            if (membersAdded is null)
            {
                throw new ArgumentNullException(nameof(membersAdded));
            }

            if (turnContext is null)
            {
                throw new ArgumentNullException(nameof(turnContext));
            }

            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken).ConfigureAwait(false);
                }
            }
        }
    }
}