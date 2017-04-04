using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.Domain.Game;
using TicTacToe.Domain.Game.Entity;

namespace TicTacToe.Infrastructure.Database
{
    public class InitializerToSeedGameContext 
    {
        GameContext context;
        public InitializerToSeedGameContext(GameContext context)
        {
            this.context = context;
        }

        public void Seed()
        {
            var gamelist = new[] {
                Mapper.Map<DbModels.Game>(
                    new Game(
                        new GameInformation($"PreLoaded_{Guid.NewGuid().ToString()}")
                        {
                            NomPlayer1 = "Player1",
                            NomPlayer2 = "Player2"
                        }
                    )
                )
            };
            
            context.Games.AddRange(gamelist);
            context.SaveChanges();
            
        }
    }
}
