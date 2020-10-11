// <copyright file="Startup.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie
{
    using System;
    using System.Net.Http;
    using BotDontLie.Bots;
    using BotDontLie.Common.Helpers;
    using BotDontLie.Common.Providers;
    using BotDontLie.Common.Services;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    /// <summary>
    /// This is the startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">Application key/value settings.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets all the key/value settings of the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the Bot App Credentials.
            services.AddSingleton(new MicrosoftAppCredentials(this.Configuration["MicrosoftAppId"], this.Configuration["MicrosoftAppPassword"]));

            // Adding the teams provider.
            services.AddSingleton<ITeamsProvider, TeamsProvider>((provider) => new TeamsProvider(
                this.Configuration["StorageConnectionString"],
                provider.GetRequiredService<TelemetryClient>()));

            // Adding the players provider.
            services.AddSingleton<IPlayersProvider, PlayersProvider>((provider) => new PlayersProvider(
                this.Configuration["StorageConnectionString"],
                provider.GetRequiredService<TelemetryClient>()));

            // Adding the necessary helper classes as well.
            services.AddSingleton<ITeamHelpers, TeamHelpers>();

            services.AddSingleton<IDataHelpers, DataHelpers>((provider) => new DataHelpers(
                provider.GetRequiredService<IBallDontLieService>(),
                provider.GetRequiredService<TelemetryClient>(),
                provider.GetRequiredService<ITeamHelpers>()));

            // Adding the games provider.
            services.AddSingleton<IGamesProvider, GamesProvider>((provider) => new GamesProvider(
                this.Configuration["StorageConnectionString"],
                provider.GetRequiredService<TelemetryClient>()));

            // Adding the statistics provider.
            services.AddSingleton<IStatisticsProvider, StatisticsProvider>((provider) => new StatisticsProvider(
                this.Configuration["StorageConnectionString"],
                provider.GetRequiredService<TelemetryClient>()));

            // Having the necessary services instantiated.
            services.AddSingleton<IBallDontLieService, BallDontLieService>((provider) => new BallDontLieService(
                provider.GetRequiredService<TelemetryClient>(),
                provider.GetRequiredService<IHttpClientFactory>(),
                provider.GetRequiredService<ITeamsProvider>(),
                provider.GetRequiredService<IPlayersProvider>(),
                provider.GetRequiredService<IGamesProvider>(),
                provider.GetRequiredService<IStatisticsProvider>()));

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, NbaBot>((provider) => new NbaBot(
                provider.GetRequiredService<TelemetryClient>(),
                this.Configuration["AppBaseUri"],
                provider.GetRequiredService<IDataHelpers>()));

            // Adding the HttpClient.
            services.AddHttpClient("BallDontLieAPI", c =>
            {
                c.BaseAddress = new Uri(this.Configuration["BallDontLieApiUrl"]);
            });

            // Adding the ApplicationInsights telemetry.
            services.AddApplicationInsightsTelemetry();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseWebSockets();

            // app.UseHttpsRedirection();
        }
    }
}