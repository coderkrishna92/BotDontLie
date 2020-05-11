// <copyright file="Startup.cs" company="Tata Consultancy Services Ltd">
// Copyright (c) Tata Consultancy Services Ltd. All rights reserved.
// </copyright>

namespace BotDontLie
{
    using System;
    using System.Net.Http;
    using BotDontLie.Bots;
    using BotDontLie.Services;
    using Microsoft.ApplicationInsights;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Bot.Builder;
    using Microsoft.Bot.Builder.Integration.AspNet.Core;
    using Microsoft.Bot.Connector.Authentication;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // Create the Bot Framework Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();

            // Create the Bot App Credentials.
            services.AddSingleton(new MicrosoftAppCredentials(this.Configuration["MicrosoftAppId"], this.Configuration["MicrosoftAppPassword"]));

            // Having the necessary services instantiated.
            services.AddSingleton<IBallDontLieService, BallDontLieService>((provider) => new BallDontLieService(
                provider.GetRequiredService<TelemetryClient>(),
                provider.GetRequiredService<IHttpClientFactory>()));

            // Create the bot as a transient. In this case the ASP Controller is expecting an IBot.
            services.AddTransient<IBot, NbaBot>((provider) => new NbaBot(
                provider.GetRequiredService<TelemetryClient>(),
                provider.GetRequiredService<MicrosoftAppCredentials>(),
                provider.GetRequiredService<IBallDontLieService>(),
                this.Configuration["AppBaseUri"]));

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
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }
    }
}