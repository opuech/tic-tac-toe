namespace TicTacToe.Tests.Domain.BoardShould
{
    public interface IWithMoveAtDataSetsSuccessBuilder
    {
        IWithMoveAtDataSetsSuccessBuilder WithExpectedOutputIsFull(bool isFull);
        IWithMoveAtDataSetsSuccessBuilder WithExpectedOutputIsXWinning(bool isXWinning);
        IWithMoveAtDataSetsSuccessBuilder WithExpectedOutputIsOWinning(bool isOWinning);

        object[] Create();
    }
}