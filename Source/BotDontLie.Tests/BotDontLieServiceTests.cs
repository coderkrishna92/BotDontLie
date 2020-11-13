// <copyright file="BotDontLieServiceTests.cs" company="PK Software LLC">
// Copyright (c) PK Software LLC. All rights reserved.
// </copyright>

namespace BotDontLie.Tests
{
    using System;
    using System.Linq;
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
                    Console.WriteLine($"Abbreviation: {item.Abbreviation}");
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
                var teamNameToTest = "New York Knicks";
#pragma warning disable CA2000 // Dispose objects before losing scope
                var request = new HttpRequestMessage(HttpMethod.Get, "v1/teams");
#pragma warning restore CA2000 // Dispose objects before losing scope
                HttpResponseMessage response = await ApiClient.SendAsync(request).ConfigureAwait(false);
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var teamsResponse = JsonConvert.DeserializeObject<TeamsResponse>(responseContent);
                var teamToReturn = teamsResponse.Teams.FirstOrDefault(x => x.FullName == teamNameToTest);

                Console.WriteLine($"TeamId: {teamToReturn}");

                Assert.IsNotNull(teamToReturn);
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
                var teamIdToTest = 1;
#pragma warning disable CA2000 // Dispose objects before losing scope
                var request = new HttpRequestMessage(HttpMethod.Get, $"v1/teams/{teamIdToTest}");
#pragma warning restore CA2000 // Dispose objects before losing scope
                HttpResponseMessage response = await ApiClient.SendAsync(request).ConfigureAwait(false);
                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var team = JsonConvert.DeserializeObject<Team>(responseContent);

                Console.WriteLine($"TeamId: {team.Id}");
                Assert.IsNotNull(team);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error happened: {ex.StackTrace}");
                throw;
            }
        }
    }
}