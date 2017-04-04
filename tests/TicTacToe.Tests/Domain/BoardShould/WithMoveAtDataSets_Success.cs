using System.Collections;
using System.Collections.Generic;
using TicTacToe.Domain.Game.ValueObject;

namespace TicTacToe.Tests.Domain.BoardShould
{
    internal class WithMoveAtDataSets_Success : IEnumerable<object[]>
    {
        private readonly List<object[]> _data =
            new List<object[]>
        {
            // Grille initiale avec selection ligne 0 colonne 0 par X
            new WithMoveAtDataSetsBuilder()
                            .WithInitialMoveNumber(0)
                            .WithMove(Token.X, 0, 0)
                            .WithExpectedSuccess()
                            .Create(),
            new WithMoveAtDataSetsBuilder()
                            .WithInitialMoveNumber(1)
                            .WithMove(Token.O, 2, 2)
                            .WithExpectedSuccess()
                            .Create(),
            new WithMoveAtDataSetsBuilder()
                            .WithInitialMoveNumber(0)
                            .WithMove(Token.X, 1, 0)
                            .WithExpectedSuccess()
                            .Create()
        };
       
        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}