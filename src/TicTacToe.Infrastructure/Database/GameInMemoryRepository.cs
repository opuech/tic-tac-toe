using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Domain;
using TicTacToe.Domain.Core;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.Entity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Infrastructure.Database
{
    public class GameInMemoryRepository : IGameRepository
    {
        public static readonly string MESSAGE_IF_NOT_FOUND_ITEM = "Partie non trouvée en BD(identifiant de la partie : '{0}'";

        private ILogger<GameInMemoryRepository> logger;
        private GameContext gameContext;

        private static HashSet<Game> Games = 
            new HashSet<Game>()
            {
                new Game(new GameInformation($"PreLoaded_{Guid.NewGuid().ToString()}")
                {

                    NomPlayer1 = "Player1",
                    NomPlayer2 = "Player2"
                })
            };

        public GameInMemoryRepository(GameContext gameContext, ILogger<GameInMemoryRepository> logger)
        {
            this.gameContext = gameContext ?? throw new ArgumentNullException(nameof(gameContext));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        public Result AddGame(Game game)
        {
            gameContext.Games.Add(Mapper.Map<DbModels.Game>(game));
            return Result.Ok();
        }

        public Result Update(Game game)
        {
            var dbGame = gameContext.Games.Find(game.Id);

            if(dbGame == null)
                return Result.Fail<Game>($"Modification de la partie impossible car non trouvée en BD (identifiant de la partie : '{game.Id}')");

            Mapper.Map(game, dbGame);
 
            return Result.Ok();
        }
            
        public Result<Game> GetGameById(string id)
        {
            var dbGame = gameContext.Games.Find(id);

            if (dbGame == null)
                return Result.Fail<Game>(string.Format(MESSAGE_IF_NOT_FOUND_ITEM, id));

            var game = Mapper.Map<Game>(dbGame);

           return Result.Ok(game);
        }

        public Result SaveAll()
        {
            if (gameContext.SaveChanges() > 0)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Erreur lors de la sauvegarde de la partie");
            }
            
        }

        public IEnumerable<GameInformation> GetAllGamesInfos()
        {
            foreach (var dbGame in gameContext.Games)
            {
                var game = Mapper.Map<Game>(dbGame);
                if(game != null && game.GameInfo != null)
                    yield return Mapper.Map<Game>(dbGame).GameInfo;
            }
        }

    }
}
