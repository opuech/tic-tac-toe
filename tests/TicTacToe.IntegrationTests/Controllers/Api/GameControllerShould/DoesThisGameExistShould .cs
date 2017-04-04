using AutoMapper;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould;
using TicTacToe.IntegrationTests.Core;
using TicTacToe.Web;
using Xunit;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public class DoesThisGameExistShould
    {
        private readonly TestServer _server;
        private readonly ControllerCaller _client;

        public DoesThisGameExistShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupForTest>());
            _client = new ControllerCaller(_server.CreateClient());
            
        }

        [FactWithAutomaticDisplayName]
        [Trait("Category", "integration")]
        public void Given_an_existing_id_When_DoesThisGameExist_is_called_Then_results_must_be_true()
        {

            // Arrange
            var DataDbLoader = new DataDbLoader(_client);

            //Act
            var response = 
                _client.Get<bool>(
                    UrlGame.DoesThisGameExist,
                    new Dictionary<string, string>()
                    {
                        { "gameId", DataDbLoader.existingGameInformationsId }
                    });


            // Assert
            Assert.True(response.Success, $"Erreur retournée par l'API : {response.Error}");
            Assert.True(response.Value);

        }

        [FactWithAutomaticDisplayName]
        [Trait("Category", "integration")]
        public void Given_a_not_existing_id_When_DoesThisGameExist_is_called_Then_results_must_be_false()
        {

            //Act
            var DataDbLoader = new DataDbLoader(_client);
            var response =
                _client.Get<bool>(
                    UrlGame.DoesThisGameExist,
                    new Dictionary<string, string>()
                    {
                        { "gameId", DataDbLoader.notFoundGameInformationsId }
                    });


            // Assert
            Assert.True(response.Success, $"Erreur retournée par l'API : {response.Error}");
            Assert.False(response.Value);
            

        }
        
    }
}
