using System;
using System.Collections.Generic;
using TicTacToe.Domain.Core;

namespace TicTacToe.Domain
{
    public interface IGameRepository
    {
        Result AddGame(Game.Game game);

        Result<Game.Game> GetGameById(string gameId);

        Result Update(Game.Game game);

        Result SaveAll();

        IEnumerable<Game.Entity.GameInformation> GetAllGamesInfos();
    }
}
