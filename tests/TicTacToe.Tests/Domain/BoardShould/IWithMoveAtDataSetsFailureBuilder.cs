namespace TicTacToe.Tests.Domain.BoardShould
{
    public interface IWithMoveAtDataSetsFailureBuilder
    {

        IWithMoveAtDataSetsFailureBuilder WithExpectedErrorMessage(string expectedErrorMessage);

        object[] Create();
    }
}