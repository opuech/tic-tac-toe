using System;
using TicTacToe.Domain;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.Entity;

namespace TicTacToe.Application
{
    public class GameService
    {
        IGameRepository gameRepository;
        IApplicationEventChannel domainEventChannel;

        
        public GameService(IApplicationEventChannel domainEventChannel,
                           IGameRepository gameRepository)
        {
            if (domainEventChannel == null)
                throw new NullReferenceException("domainEventChannel");
            this.domainEventChannel = domainEventChannel;

            if(gameRepository == null)
                throw new NullReferenceException("gameRepository");
            this.gameRepository = gameRepository;

        }

        public void GenerateNewGame(GameInformation gameInfo)
        {
            Game game = new Game(gameInfo);
            gameRepository.AddGame(game);
        }

        public void ApplyPlayerSelection(string gameId, string playerCode, int x, int y)
        {
            var game = gameRepository.GetGameById(gameId);
            game.Value.ApplyPlayerSelection(playerCode, x, y);
        }
    }
}
