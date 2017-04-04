using System;
using System.Linq;
using TicTacToe.Domain.Core;
using TicTacToe.Domain.Game.Entity;
using TicTacToe.Domain.Game.ValueObject;

namespace TicTacToe.Domain.Game
{
    public class Game : Entity<string>
    {
        public GameInformation GameInfo { get; set; }
        public Board Board { get; private set; }
        

        public override string Id
        {
            get{return GameInfo.Id; }
        }

        public Game(GameInformation gameInfo) : this(gameInfo, new Board()) {}

        public Game(GameInformation gameInfo, Board board)
        {
            if (gameInfo == null)
                throw new ArgumentNullException(nameof(gameInfo));
            if (board == null)
                throw new ArgumentNullException(nameof(board));

            this.GameInfo = gameInfo;
            this.Board = board;

        }
        
        private Result<Token> GetTokenFromPlayerCode(string playerCode)
        {
            if (playerCode == GameInfo.NomPlayer1) return Result.Ok(Token.X);
            else
            {
                if (playerCode == GameInfo.NomPlayer2) return Result.Ok(Token.O);
                else return Result.Fail<Token>($"Coup non autorisé. La partie oppose '{GameInfo.NomPlayer1}' à '{GameInfo.NomPlayer2}'. Vous êtes authentifié en tant que '{playerCode}'.");
            }
        }

        public Result ApplyPlayerSelection(string playerCode, int lineNumber, int columnNumber)
        {
            return GetTokenFromPlayerCode(playerCode)
                    .OnSuccess(token =>
                    {
                        return Board.WithMoveAt(token, lineNumber, columnNumber)
                                .OnSuccess(newGrid =>
                                {
                                    Board = newGrid;
                                    return Result.Ok();
                                });    
                    });

        }
    }
}
