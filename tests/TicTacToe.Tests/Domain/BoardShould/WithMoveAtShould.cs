using Moq;
using TicTacToe.Domain.Core;
using TicTacToe.Domain.Game.ValueObject;
using TicTacToe.Tests.Core;
using Xunit;

namespace TicTacToe.Tests.Domain.BoardShould
{
    public class WithMoveAtShould
    {
        

        [TheoryWithAutomaticDisplayName, ClassData(typeof(WithMoveAtDataSets_Success))]
        [Trait("Category", "unitaire")]
        public void Given_a_stubed_grid_and_a_move_When_WithMoveAt_is_called_Then_new_board_is_as_expected
            (
                Board initialBoardWithGridStub, 
                Token someToken, 
                int someLine, 
                int someColumn,
                Result<Board> expectedOutputBoardWithGridStub
            )
           
        {
            Generic_DataDrivenTests_For_WithMoveAt(initialBoardWithGridStub, someToken, someLine, someColumn, expectedOutputBoardWithGridStub);
        }


        [TheoryWithAutomaticDisplayName(Skip = "reason"), ClassData(typeof(WithMoveAtDataSets_GridIsFull))]
        [Trait("Category", "unitaire")]
        public void Given_a_stubed_full_grid_When_WithMoveAt_is_called_Then_it_is_refused_With_friendly_message
         (
                Board initialBoardWithGridStub,
                Token someToken,
                int someLine,
                int someColumn,
                Result<Board> expectedOutputBoardWithGridStub
            )

        {
            Generic_DataDrivenTests_For_WithMoveAt(initialBoardWithGridStub, someToken, someLine, someColumn, expectedOutputBoardWithGridStub);
        }

        private void Generic_DataDrivenTests_For_WithMoveAt(
                Board initialBoardWithGridStub,
                Token someToken,
                int someLine,
                int someColumn,
                Result<Board> expectedOutputBoardWithGridStub
            )

        {
            // Arrange
            var sut = initialBoardWithGridStub;


            // Act
            Result<Board> result = sut.WithMoveAt(someToken, someLine, someColumn);

            // Assert
            expectedOutputBoardWithGridStub
                .OnSuccess((expectedBoard) => {
                    Assert.True(result.Success);
                    Assert.Same(expectedBoard.grid, result.Value.grid);
                    Assert.Equal(expectedBoard.moveCounter, result.Value.moveCounter);
                })
                .OnFailure((error) => {
                    Assert.True(result.Failure, $"Succès non attendu. réponse de l'API : {result.Value}");
                    Assert.Equal<string>(error, result.Error);
                });

        }
    }
}
