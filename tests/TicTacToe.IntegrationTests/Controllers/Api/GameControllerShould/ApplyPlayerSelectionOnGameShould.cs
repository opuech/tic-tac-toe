using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net;
using TicTacToe.Application.Dto;
using TicTacToe.Domain.Core;
using TicTacToe.IntegrationTests.Core;
using TicTacToe.Web;
using Xunit;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public class ApplyPlayerSelectionOnGameShould
    {
        private readonly TestServer _server;
        private readonly ControllerCaller _client;

        public ApplyPlayerSelectionOnGameShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupForTest>());
            _client = new ControllerCaller(_server.CreateClient());
            
        }
        
        [TheoryWithAutomaticDisplayName, ClassData(typeof(ApplyPlayerSelectionDataSets_Success))]
        [Trait("Category", "integration")]
        public void Given_a_game_When_ApplyPlayerSelectionOnGame_is_called_Then_The_Board_Is_Successfully_Updated
         (
            GameDto someGame,
            PlayerSelectionDto playerSelection,
            Result<BoardDto> expectedResultBoard
         )
        {
            Generic_DataDrivenTests_For_ApplyPlayerSelectionOnGame(someGame, playerSelection, expectedResultBoard);
        }
        private void Generic_DataDrivenTests_For_ApplyPlayerSelectionOnGame(
            GameDto someGame,
            PlayerSelectionDto playerSelection,
            Result<BoardDto> expectedResultBoard
         )
        {
            //Arrange        
            _client.Post<GameDto>(UrlGame.CreateGameWithGrid, someGame).OnFailure((error) => throw new Exception ($"Data creation failed. Error : {error}"));

            //Act
            var response =
                _client.Post<BoardDto>(UrlGame.ApplyPlayerSelectionOnGame, playerSelection);

            // Assert
            expectedResultBoard
                .OnSuccess((expectedBoard) => {
                    Assert.True(response.Success, $"Erreur non attendue retournée par l'API : {response.Error}");
                    Assert.Equal<BoardDto>(expectedBoard, response.Value);
                })
                .OnFailure(() => {
                    Assert.True(response.Failure, $"Succès non attendu. réponse de l'API : {response.Value}");
                    Assert.Equal<string>(expectedResultBoard.Error, response.Error);
                });


        }
    }
}
