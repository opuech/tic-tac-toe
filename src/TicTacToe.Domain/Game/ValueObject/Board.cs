using System;
using System.Text;
using TicTacToe.Domain.Core;

namespace TicTacToe.Domain.Game.ValueObject
{
    [Serializable]
    public class Board : ValueObject<Board>
    {
        public static readonly string MESSAGE_IF_NOT_IN_RANGE = $"Coup non autorisé. Le coup est hors de la grille";
        public static readonly string MESSAGE_IF_GRID_IS_FULL = $"Coup non autorisé. La grille est pleine, la partie est terminée.";
        public static readonly string MESSAGE_IF_PLAYER_IS_ALREADY_THE_WINNER = $"Coup non autorisé. La partie est terminée et vous avez gagné !";
        public static readonly string MESSAGE_IF_PLAYER_IS_ALREADY_THE_LOSER = $"Coup non autorisé. La partie est terminée et vous avez malheureusement perdu.";
        public static readonly string MESSAGE_IF_NOT_PLAYER_TURN = $"Coup non autorisé. Ce n'est pas à votre tour de jouer !";
        public static readonly string MESSAGE_IF_SQUARE_ALREADY_TAKEN = $"Coup non autorisé. La case est déjà prise";

        public readonly Grid grid;
        public readonly int moveCounter;
        public readonly State state;

        #region constructors

        public Board() :
            this(
                new Grid(
                    new Token[,] {  { Token._, Token._, Token._ },
                                    { Token._, Token._, Token._ },
                                    { Token._, Token._, Token._ } }),
                0)
        { }

        public Board(Grid grid, int moveCounter)
        { 
            if (grid == null) throw new ArgumentNullException(nameof(grid));

            this.grid = grid;
            this.moveCounter = moveCounter;
            this.state = GetState(grid, moveCounter);
        }

        #endregion

        #region constructorsHelpers

        private static State GetState(Grid grid, int moveCounter)
        {
            //if (grid.IsFull()) return State.GridFull;
            if (grid.IsXWinning()) return State.PlayerXWins;
            if (grid.IsOWinning()) return State.PlayerOWins;
            if ((moveCounter % 2) == 0) return State.WaitingPlayerXMove;
            else return State.WaitingPlayerOMove;
        }

        #endregion

        public Result<Board> WithMoveAt(Token token, int lineNumber, int columnNumber)
        {
            if(token.Equals(Token._))
                throw new Exception($"Erreur interne : Jeton '{token}' invalide.");

            if (!grid.IsInRange(lineNumber, columnNumber))
                return Result.Fail<Board>(MESSAGE_IF_NOT_IN_RANGE);

            //if (state.Equals(State.GridFull))
            //    return Result.Fail<Board>(MESSAGE_IF_GRID_IS_FULL);

            if ((state.Equals(State.PlayerXWins) && (token.Equals(Token.X))) ||
                (state.Equals(State.PlayerOWins) && (token.Equals(Token.O))))
                return Result.Fail<Board>(MESSAGE_IF_PLAYER_IS_ALREADY_THE_WINNER);

            if ((state.Equals(State.PlayerXWins) && (!token.Equals(Token.X))) ||
                (state.Equals(State.PlayerOWins) && (!token.Equals(Token.O))))
                return Result.Fail<Board>(MESSAGE_IF_PLAYER_IS_ALREADY_THE_LOSER);

            if ((state.Equals(State.WaitingPlayerXMove) && (!token.Equals(Token.X))) ||
                (state.Equals(State.WaitingPlayerOMove) && (!token.Equals(Token.O))))
                return Result.Fail<Board>(MESSAGE_IF_NOT_PLAYER_TURN);

            if (!grid.CanBeSelected(lineNumber, columnNumber))
                return Result.Fail<Board>(MESSAGE_IF_SQUARE_ALREADY_TAKEN);

            return
                Result.Ok<Board>(
                    new Board(
                        grid.WithSelection(token, lineNumber, columnNumber),
                        moveCounter + 1));

        }
    }
}
