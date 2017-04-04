using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Domain.Core;
using TicTacToe.Domain.Game.ValueObject;

namespace TicTacToe.Tests.Domain.BoardShould
{
    public class WithMoveAtDataSetsBuilder : IWithMoveAtDataSetsCommonBuilder, IWithMoveAtDataSetsSuccessBuilder, IWithMoveAtDataSetsFailureBuilder
    {
        // Data for 'Arrange'
        private bool initialGridIsFull = false;
        private bool initialGridIsXWinning = false;
        private bool initialGridIsOWinning = false;
        private bool initialGridCanBeSelected = true;
        private int initialMoveNumber = 0;

        // Data for 'Act'
        private Token moveToken = Token.X;
        private int moveLine = 0;
        private int moveColumn = 0;

        // Data for 'Assert'
        private bool expectedSuccess = true;
        private string expectedErrorMessage = string.Empty;
        private bool expectedOutputGridIsFull = false;
        private bool expectedOutputGridIsXWinning = false;
        private bool expectedOutputGridIsOWinning = false;

        public IWithMoveAtDataSetsCommonBuilder WithInitialGridIsFull(bool isFull)
        { this.initialGridIsFull = isFull; return this; }
        public IWithMoveAtDataSetsCommonBuilder WithInitialGridIsXWinning(bool isXWinning)
        { this.initialGridIsXWinning = isXWinning; return this; }
        public IWithMoveAtDataSetsCommonBuilder WithInitialGridIsOWinning(bool isOWinning)
        { this.initialGridIsOWinning = isOWinning; return this; }
        public IWithMoveAtDataSetsCommonBuilder WithInitialGridCanBeSelected(bool canBeSelected)
        { this.initialGridCanBeSelected = canBeSelected; return this; }
        public IWithMoveAtDataSetsCommonBuilder WithInitialMoveNumber(int moveNumber)
        { this.initialMoveNumber = moveNumber; return this; }
        public IWithMoveAtDataSetsCommonBuilder WithMove(Token token, int line, int column)
        {
            this.moveToken = token;
            this.moveLine = line;
            this.moveColumn = column;
            return this;
        }
        

        public IWithMoveAtDataSetsSuccessBuilder WithExpectedSuccess()
        {
            expectedSuccess = true;
            return this;
        }

        public IWithMoveAtDataSetsFailureBuilder WithExpectedFailure()
        {
            expectedSuccess = false;
            return this;
        }

        public IWithMoveAtDataSetsFailureBuilder WithExpectedErrorMessage(string expectedErrorMessage)
        {
            this.expectedErrorMessage = expectedErrorMessage;
            return this;
        }
        public IWithMoveAtDataSetsSuccessBuilder WithExpectedOutputIsFull(bool isFull) { this.expectedOutputGridIsFull = isFull; return this; }
        public IWithMoveAtDataSetsSuccessBuilder WithExpectedOutputIsXWinning(bool isXWinning) { this.expectedOutputGridIsXWinning = isXWinning; return this; }
        public IWithMoveAtDataSetsSuccessBuilder WithExpectedOutputIsOWinning(bool isOWinning) { this.expectedOutputGridIsOWinning = isOWinning; return this; }

        public object[] Create()
        {
            var expectedOutputGridStub = new Mock<Grid>();
            //expectedOutputGridStub.Setup(grid => grid.IsFull()).Returns(expectedOutputGridIsFull);
            expectedOutputGridStub.Setup(grid => grid.IsXWinning()).Returns(expectedOutputGridIsXWinning);
            expectedOutputGridStub.Setup(grid => grid.IsOWinning()).Returns(expectedOutputGridIsOWinning);

            var initialGridStub = new Mock<Grid>();
            //initialGridStub.Setup(grid => grid.IsFull()).Returns(initialGridIsFull);
            initialGridStub.Setup(grid => grid.IsXWinning()).Returns(initialGridIsXWinning);
            initialGridStub.Setup(grid => grid.IsOWinning()).Returns(initialGridIsOWinning);
            initialGridStub.Setup(grid => grid.CanBeSelected(moveLine, moveColumn)).Returns(initialGridCanBeSelected);
            initialGridStub.Setup(grid => grid.IsInRange(moveLine, moveColumn)).CallBase();
            initialGridStub.Setup(grid => grid.WithSelection(moveToken, moveLine, moveColumn)).Returns(expectedOutputGridStub.Object);

            return new object[]
            {
                //initialBoardWithGridStub
                new Board(initialGridStub.Object, initialMoveNumber),
                moveToken,
                moveLine,
                moveColumn,
                //expectedOutputBoardWithGridStub
                expectedSuccess ?
                    Result.Ok(new Board(expectedOutputGridStub.Object, initialMoveNumber + 1)) :
                    Result.Fail<Board>(expectedErrorMessage)
                
            };
            
        }

        
    }
}
