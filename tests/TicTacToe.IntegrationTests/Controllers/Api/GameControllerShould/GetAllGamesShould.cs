
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using TicTacToe.Application.Dto;
using TicTacToe.IntegrationTests.Core;
using TicTacToe.Web;
using Xunit;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public class GetAllGamesShould
    {
        private readonly TestServer _server;
        private readonly ControllerCaller _client;


        public GetAllGamesShould()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<StartupForTest>());
            _client = new ControllerCaller(_server.CreateClient());
            
        }
        
        [FactWithAutomaticDisplayName]
        [Trait("Category", "integration")]
        public void When_GetAllGamesInfos_is_Called_Then_results_must_be_valid()
        {
            //Arrange        
            var DataDbLoader = new DataDbLoader(_client);


            //Act
            var response = _client.Get<GameInformationDto[]>(UrlGame.GetAllGamesInfos);


            // Assert
            Assert.True(response.Success, $"Erreur retournée par l'API : {response.Error}");
            Assert.NotNull(response.Value);

        }
    }
}
