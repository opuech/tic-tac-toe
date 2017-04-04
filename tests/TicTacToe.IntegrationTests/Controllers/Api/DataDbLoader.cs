using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;
using TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould;

namespace TicTacToe.IntegrationTests.Controllers.Api
{
    public class DataDbLoader
    {
        private readonly ControllerCaller _client;
        private readonly string testName;
        public readonly GameInformationDto existingGameInformations;
        public readonly string existingGameInformationsId;
        public readonly GameInformationDto notFoundGameInformations;
        public readonly string notFoundGameInformationsId;

        public DataDbLoader(ControllerCaller client, [CallerMemberName]string testName = "")
        {
            _client = client;
            this.testName = testName;

            existingGameInformations = new GameInformationDto()
            {
                Id = Guid.NewGuid().ToString("D"),
                NomPlayer1 = "Player1",
                NomPlayer2 = "Player2"
            };

            existingGameInformationsId = existingGameInformations.Id;

            notFoundGameInformations = new GameInformationDto()
            {
                Id = $"{testName}_NotExistingId_{Guid.NewGuid().ToString()}",
                NomPlayer1 = "Player1",
                NomPlayer2 = "Player2"
            };
            notFoundGameInformationsId = notFoundGameInformations.Id;

            InsertNewGameInDb(existingGameInformations);
        }
  
        private void InsertNewGameInDb(GameInformationDto gameInformations)
        {
            var response = _client.Post<GameInformationDto>(UrlGame.CreateGame, gameInformations);
            if (response.Failure)
            {
                throw new Exception($"Erreur lors de l'insertion par web api de données de tests depuis {nameof(InsertNewGameInDb)}");
            }
        }
    }
}
