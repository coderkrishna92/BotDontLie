// <copyright file="BotController.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Controllers
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;

    /// <summary>
    /// This ASP Controller is created to handle a request. Dependency Injection will provide the Adapter and IBot
    /// implementation at runtime. Multiple different IBot implementations running at different endpoints can be
    /// achieved by specifying a more specific type for the bot constructor argument.
    /// </summary>
    [Route("api/messages")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IBotFrameworkHttpAdapter botFrameworkHttpAdapter;
        private readonly IBot bot;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotController"/> class.
        /// </summary>
        /// <param name="adapter">The bot framework adapter.</param>
        /// <param name="bot">The bot itself.</param>
        public BotController(IBotFrameworkHttpAdapter adapter, IBot bot)
        {
            this.botFrameworkHttpAdapter = adapter;
            this.bot = bot;
        }

        /// <summary>
        /// The method that will posting messages.
        /// </summary>
        /// <returns>A unit of execution.</returns>
        [HttpPost]
        [HttpGet]
        public async Task PostAsync()
        {
            // Delegate the processing of the HTTP POST to the adapter.
            // The adapter will invoke the bot.
            await this.botFrameworkHttpAdapter.ProcessAsync(this.Request, this.Response, this.bot).ConfigureAwait(false);
        }
    }
}