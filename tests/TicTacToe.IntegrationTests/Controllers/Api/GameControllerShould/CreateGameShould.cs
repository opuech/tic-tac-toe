using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

using Newtonsoft.Json;
using System.Text;
using System.Net;
using TicTacToe.Application.Dto;
using TicTacToe.IntegrationTests.Controllers.Api;
using TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould;
using TicTacToe.Web;
using TicTacToe.IntegrationTests.Core;
using Xunit.Abstractions;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public class CreateGameShould
    {
        private readonly TestServer _server;
        private readonly ControllerCaller _client;
        private readonly ITestOutputHelper output;

        public CreateGameShould(ITestOutputHelper output)
        {
            this.output = output;
       
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupForTest>());
            _client = new ControllerCaller(_server.CreateClient());
        }
        
        [FactWithAutomaticDisplayName]
        [Trait("Category", "integration")]
        public void Given_a_GameInformation_When_CreateGame_is_called_Then_creation_is_done_with_success()
        {
            //Arrange
            var gameInformationDto = new GameInformationDto()
            {
                Id = Guid.NewGuid().ToString(),
                NomPlayer1 = "Player1",
                NomPlayer2 = "Player2"
            };

            //Act
            var response = _client.Post<GameInformationDto>(UrlGame.CreateGame, gameInformationDto);


            // Assert
            Assert.True(response.Success, $"Erreur retournée par l'API : {response.Error}");
            Assert.Equal<GameInformationDto>(gameInformationDto, response.Value);
            
        }

        [FactWithAutomaticDisplayName]
        [Trait("Category", "integration")]
        public void Given_a_game_When_CreateGame_is_called_Then_creation_is_done_with_success()
        {
            //Arrange
            var gameDto = new GameDto()
            {
                GameInformation = new GameInformationDto()
                {
                    Id = Guid.NewGuid().ToString(),
                    NomPlayer1 = "Player1",
                    NomPlayer2 = "Player2"
                },
                Grid = new TokenDto[,] {  { TokenDto.X, TokenDto.X, TokenDto.O },
                                          { TokenDto._, TokenDto.O, TokenDto._ },
                                          { TokenDto.X, TokenDto._, TokenDto._ } },
                MoveCounter = 5
            };

            //Act
            var response = _client.Post<GameDto>(UrlGame.CreateGameWithGrid, gameDto);


            // Assert
            Assert.True(response.Success, $"Erreur retournée par l'API : {response.Error}");
            Assert.Equal<GameDto>(gameDto, response.Value);
        }

    }
}

