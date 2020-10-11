// <copyright file="IDataHelpers.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Common.Helpers
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Schema;

    /// <summary>
    /// This interface defines methods to help get the necessary data.
    /// </summary>
    public interface IDataHelpers
    {
        /// <summary>
        /// This method definition will get the necessary team information.
        /// </summary>
        /// <param name="messsageText">The raw message that the user sends to the bot.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        Task GetTeamInformationAsync(
            string messsageText,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken);

        /// <summary>
        /// This method definition will get the player information.
        /// </summary>
        /// <param name="messageText">The raw user message to the bot.</param>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A unit of execution.</returns>
        Task GetPlayerInformationAsync(
            string messageText,
            ITurnContext<IMessageActivity> turnContext,
            CancellationToken cancellationToken);

        /// <summary>
        /// This method will sync the teams.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        Task SyncTeamsAsync(ITurnContext<IMessageActivity> turnContext);

        /// <summary>
        /// This method will sync the statistics.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        Task SyncStatsAsync(ITurnContext<IMessageActivity> turnContext);

        /// <summary>
        /// This method will sync the players.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        Task SyncPlayersAsync(ITurnContext<IMessageActivity> turnContext);

        /// <summary>
        /// This method will sync the games.
        /// </summary>
        /// <param name="turnContext">The current turn/execution flow.</param>
        /// <returns>A unit of execution.</returns>
        Task SyncGamesAsync(ITurnContext<IMessageActivity> turnContext);
    }
}