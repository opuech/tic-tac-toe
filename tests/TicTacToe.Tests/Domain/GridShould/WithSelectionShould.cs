using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Domain.Core;
using TicTacToe.Domain.Game.ValueObject;
using TicTacToe.Tests.Core;
using Xunit;

namespace TicTacToe.Tests.Domain.GridShould
{
    public class WithSelectionShould
    {

        [FactWithAutomaticDisplayName]
        [Trait("Category", "unitaire")]
        public void Given_a_grid_When_WithSelection_is_called_Then_new_grid_with_selection_is_returned()
        {
            // Arrange
            var sut = new Grid(new Token[,] {  
                            { Token.X, Token.X, Token.O },
                            { Token._, Token.O, Token._ },
                            { Token._, Token._, Token._ } });

            var expectedGrid = new Grid(new Token[,] {  
                            { Token.X, Token.X, Token.O },
                            { Token.X, Token.O, Token._ },
                            { Token._, Token._, Token._ } });
            
            // Act
            var actualGrid = sut.WithSelection(Token.X, 1, 0);

            // Assert
            Assert.Equal(expectedGrid, actualGrid);
        }
    }
}
