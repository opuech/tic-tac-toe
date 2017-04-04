using TicTacToe.Domain.Game.ValueObject;

namespace TicTacToe.Tests.Domain.BoardShould
{
    public interface IWithMoveAtDataSetsCommonBuilder
    {
        IWithMoveAtDataSetsCommonBuilder WithInitialGridIsFull(bool isFull);
        IWithMoveAtDataSetsCommonBuilder WithInitialGridIsXWinning(bool isXWinning);
        IWithMoveAtDataSetsCommonBuilder WithInitialGridIsOWinning(bool isOWinning);
        IWithMoveAtDataSetsCommonBuilder WithInitialGridCanBeSelected(bool canBeSelected);
        IWithMoveAtDataSetsCommonBuilder WithInitialMoveNumber(int moveNumber);
        IWithMoveAtDataSetsCommonBuilder WithMove(Token token, int line, int column);

        IWithMoveAtDataSetsSuccessBuilder WithExpectedSuccess();

        IWithMoveAtDataSetsFailureBuilder WithExpectedFailure();

    }
}