
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;
using TicTacToe.Infrastructure.Database;
using TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould;
using TicTacToe.IntegrationTests.Core;
using TicTacToe.Web;
using Xunit;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public class GetGameStateByIdShould
    {
        private readonly TestServer _server;
        private readonly ControllerCaller _client;


        public GetGameStateByIdShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupForTest>());
            _client = new ControllerCaller(_server.CreateClient());
            
        }
        
        [FactWithAutomaticDisplayName]
        [Trait("Category", "integration")]
        public void Given_a_not_existing_id_When_GetGameStateById_is_called_Then_an_error_message_is_Returned()
        {
            //Arrange        
            var DataDbLoader = new DataDbLoader(_client);
            var expectedErrorMessage = 
                string.Format(GameInMemoryRepository.MESSAGE_IF_NOT_FOUND_ITEM, DataDbLoader.notFoundGameInformationsId);

            //Act
            var response =
                _client.Get<BoardDto>(
                    UrlGame.GetBoardById,
                    new Dictionary<string, string>()
                    {
                        { "gameId", DataDbLoader.notFoundGameInformationsId } });


            // Assert
            Assert.True(response.Failure);
            Assert.Equal<string>(expectedErrorMessage, response.Error);

        }
    }
}
