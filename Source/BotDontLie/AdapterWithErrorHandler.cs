// <copyright file="AdapterWithErrorHandler.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie
{
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Bot.Builder.TraceExtensions;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// The class of the adapter with the error handler.
    /// </summary>
    public class AdapterWithErrorHandler : BotFrameworkHttpAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AdapterWithErrorHandler"/> class.
        /// </summary>
        /// <param name="configuration">Collection of key/value settings for the application.</param>
        /// <param name="logger">The logger mechanism.</param>
        public AdapterWithErrorHandler(IConfiguration configuration, ILogger<BotFrameworkHttpAdapter> logger)
            : base(configuration, logger)
        {
            this.OnTurnError = async (turnContext, exception) =>
            {
                // Log any leaked exception from the application.
                logger.LogError(exception, $"[OnTurnError] unhandled error : {exception.Message}");

                // Send a message to the user
                await turnContext.SendActivityAsync("The bot encountered an error or bug.").ConfigureAwait(false);
                await turnContext.SendActivityAsync("To continue to run this bot, please fix the bot source code.").ConfigureAwait(false);

                // Send a trace activity, which will be displayed in the Bot Framework Emulator
                await turnContext.TraceActivityAsync("OnTurnError Trace", exception.Message, "https://www.botframework.com/schemas/error", "TurnError").ConfigureAwait(false);
            };
        }
    }
}