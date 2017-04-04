using System.Collections;
using System.Collections.Generic;
using TicTacToe.Domain.Game.ValueObject;

namespace TicTacToe.Tests.Domain.BoardShould
{
    internal class WithMoveAtDataSets_GridIsFull : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
            new List<object[]>
        {
            // Grille initiale avec selection ligne 0 colonne 0 par X
            new WithMoveAtDataSetsBuilder()
                            .WithInitialGridIsFull(true)
                            .WithInitialMoveNumber(8)
                            .WithMove(Token.X, 0, 0)
                            .WithExpectedFailure()
                            .WithExpectedErrorMessage(Board.MESSAGE_IF_GRID_IS_FULL)
                            .Create(),
            new WithMoveAtDataSetsBuilder()
                            .WithInitialGridIsFull(true)
                            .WithInitialMoveNumber(8)
                            .WithMove(Token.X, 2, 2)
                            .WithExpectedFailure()
                            .WithExpectedErrorMessage(Board.MESSAGE_IF_GRID_IS_FULL)
                            .Create(),
            new WithMoveAtDataSetsBuilder()
                            .WithInitialGridIsFull(true)
                            .WithInitialMoveNumber(8)
                            .WithMove(Token.X, 1, 0)
                            .WithExpectedFailure()
                            .WithExpectedErrorMessage(Board.MESSAGE_IF_GRID_IS_FULL)
                            .Create()
        };
       
        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}