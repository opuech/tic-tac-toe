using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;
using TicTacToe.Domain.Core;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    class ApplyPlayerSelectionDataSetBuilder : 
        IApplyPlayerSelectionDataSetCommonBuilder, IApplyPlayerSelectionDataSetSuccessBuilder, IApplyPlayerSelectionDataSetFailureBuilder
    {
        // Data for 'Arrange'
        string nomPlayer1 = "Player1";
        string nomPlayer2 = "Player2";
        TokenDto[,] initialGrid = 
            new TokenDto[,] {  { TokenDto._, TokenDto._, TokenDto._ },
                               { TokenDto._, TokenDto._, TokenDto._ },
                               { TokenDto._, TokenDto._, TokenDto._ } };

        // Data for 'Act'
        TokenDto tokenPlayer = TokenDto.X;
        int selectedLine = 0;
        int selectedColumn = 0;

        // Data for 'Assert'
        bool expectedSuccess = true;
        TokenDto[,] expectedGridAfterMove = 
            new TokenDto[,]{  { TokenDto.X, TokenDto._, TokenDto._ },
                              { TokenDto._, TokenDto._, TokenDto._ },
                              { TokenDto._, TokenDto._, TokenDto._ } };
        StateDto expectedStatusAfterMove = StateDto.WaitingPlayerOMove;
        string expectedErrorMessage = string.Empty;


        public ApplyPlayerSelectionDataSetBuilder() {}

        public IApplyPlayerSelectionDataSetCommonBuilder WithNomPlayer1(string nomPlayer1)
        {
            this.nomPlayer1 = nomPlayer1;
            return this;
        }
        public IApplyPlayerSelectionDataSetCommonBuilder WithNomPlayer2(string nomPlayer2)
        {
            this.nomPlayer2 = nomPlayer2;
            return this;
        }
        public IApplyPlayerSelectionDataSetCommonBuilder WithInitialGrid(TokenDto[,] initialGrid)
        {
            this.initialGrid = initialGrid;
            return this;
        }

        public IApplyPlayerSelectionDataSetCommonBuilder WithSelection(TokenDto tokenPlayer, int selectedLine, int selectedColumn)
        {
            this.tokenPlayer = tokenPlayer;
            this.selectedLine = selectedLine;
            this.selectedColumn = selectedColumn;
            return this;
        }
        public IApplyPlayerSelectionDataSetCommonBuilder WithTokenPlayer(TokenDto tokenPlayer)
        {
            this.tokenPlayer = tokenPlayer;
            return this;
        }

        public IApplyPlayerSelectionDataSetCommonBuilder WithSelectedLine(int selectedLine)
        {
            this.selectedLine = selectedLine;
            return this;
        }
        public IApplyPlayerSelectionDataSetCommonBuilder WithSelectedColumn(int selectedColumn)
        {
            this.selectedColumn = selectedColumn;
            return this;
        }
        public IApplyPlayerSelectionDataSetSuccessBuilder WithExpectedGridAfterMove(TokenDto[,] expectedGridAfterMove)
        {
            this.expectedGridAfterMove = expectedGridAfterMove;
            return this;
        }
        public IApplyPlayerSelectionDataSetSuccessBuilder WithExpectedStatusAfterMove(StateDto expectedStatusAfterMove)
        {
            this.expectedStatusAfterMove = expectedStatusAfterMove;
            return this;
        }

        public IApplyPlayerSelectionDataSetSuccessBuilder WithExpectedSuccess()
        {
            expectedSuccess = true;
            return this;
        }

        public IApplyPlayerSelectionDataSetFailureBuilder WithExpectedFailure()
        {
            expectedSuccess = false;
            return this;
        }

        public IApplyPlayerSelectionDataSetFailureBuilder WithExpectedErrorMessage(string expectedErrorMessage)
        {
            this.expectedErrorMessage = expectedErrorMessage;
            return this;
        }
        public object[] Create()
        {
            string id = Guid.NewGuid().ToString();
            string player = String.Empty;
            if (tokenPlayer.Equals(TokenDto.X)) player = nomPlayer1;
            if (tokenPlayer.Equals(TokenDto.O)) player = nomPlayer2;

            return new object[] 
            {
                new GameDto()
                {
                    GameInformation = new GameInformationDto()
                    {
                        Id = id,
                        NomPlayer1 = nomPlayer1,
                        NomPlayer2 = nomPlayer2
                    },
                    Grid = initialGrid,
                    MoveCounter = initialGrid.Cast<TokenDto>().Where(x => !x.Equals(TokenDto._)).Count()
                },
                new PlayerSelectionDto()
                {
                    GameId = id,
                    NomPlayer = player,
                    Line = selectedLine,
                    Column = selectedColumn
                },
                expectedSuccess ?
                    Result.Ok(new BoardDto()
                    {
                        Grid = expectedGridAfterMove,
                        State = expectedStatusAfterMove
                    }) :
                    Result.Fail<BoardDto>(expectedErrorMessage)
            };
            
        }

    }
}
