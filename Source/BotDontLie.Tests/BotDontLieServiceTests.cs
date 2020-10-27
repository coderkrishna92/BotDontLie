// <copyright file="BotDontLieServiceTests.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Tests
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using BotDontLie.Common.Models;
    using Newtonsoft.Json;
    using NUnit.Framework;

    /// <summary>
    /// This is the test for all of the Bot Dont Lie service methods.
    /// </summary>
    [TestFixture]
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public class BotDontLieServiceTests
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
#pragma warning disable CA2211 // Non-constant fields should not be visible
#pragma warning disable SA1401 // Fields should be private
        /// <summary>
        /// This is the API client.
        /// </summary>
        public static HttpClient ApiClient;
#pragma warning restore SA1401 // Fields should be private
#pragma warning restore CA2211 // Non-constant fields should not be visible

        /// <summary>
        /// This is the one time setup method.
        /// </summary>
        [OneTimeSetUp]
        public void Setup()
        {
            ApiClient = new HttpClient
            {
                BaseAddress = new Uri("https://balldontlie.io/api/"),
            };
        }

        /// <summary>
        /// This is the test to get all of the teams.
        /// </summary>
        /// <returns>If the method was able to run or not.</returns>
        [Test]
        public async Task GetAllTeamsTest()
        {
            try
            {
#pragma warning disable CA2000 // Dispose objects before losing scope
                var request = new HttpRequestMessage(HttpMethod.Get, "v1/teams");
#pragma warning restore CA2000 // Dispose objects before losing scope
                HttpResponseMessage response = await ApiClient.SendAsync(request).ConfigureAwait(false);

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var teamsResponse = JsonConvert.DeserializeObject<TeamsResponse>(responseContent);
                foreach (var item in teamsResponse.Teams)
                {
                    Console.WriteLine($"{item.Abbreviation}");
                }

                Assert.IsTrue(response.IsSuccessStatusCode);
                Assert.NotZero(teamsResponse.Teams.Count);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception happened! StackTrace: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// This method will test getting the necessary team by their name.
        /// </summary>
        /// <returns>If the method was able to run or not.</returns>
        [Test]
        public async Task GetTeamByTeamNameTest()
        {
            try
            {
                // TODO: Making sure that all of the logic gets implemented.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error happened: {ex.StackTrace}");
                throw;
            }
        }

        /// <summary>
        /// This method will get the team by the ID.
        /// </summary>
        /// <returns>A task that would signify if the method has run properly.</returns>
        [Test]
        public async Task GetTeamByIdTest()
        {
            try
            {
                // TODO: Make sure to have this unit test written.
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error happened: {ex.StackTrace}");
                throw;
            }
        }
    }
}