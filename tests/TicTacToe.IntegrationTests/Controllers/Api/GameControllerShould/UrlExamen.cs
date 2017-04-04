using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public class UrlGame
    {
        public static string ApplyPlayerSelectionOnGame = "/Game/ApplyPlayerSelectionOnGame";
        public static string CreateGame = "/Game/CreateGame";
        public static string CreateGameWithGrid = "/Game/CreateGameWithGrid";
        public static string DoesThisGameExist = "/Game/DoesThisGameExist";
        public static string GetBoardById = "/Game/GetBoardById";
        public static string GetAllGamesInfos = "/Game/GetAllGamesInfos";

    }
}
