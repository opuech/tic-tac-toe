using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public interface IApplyPlayerSelectionDataSetCommonBuilder
    {
        IApplyPlayerSelectionDataSetCommonBuilder WithNomPlayer1(string nomPlayer1);
        IApplyPlayerSelectionDataSetCommonBuilder WithNomPlayer2(string nomPlayer2);
        IApplyPlayerSelectionDataSetCommonBuilder WithInitialGrid(TokenDto[,] initialGrid);

        IApplyPlayerSelectionDataSetCommonBuilder WithSelection(TokenDto tokenPlayer, int selectedLine, int selectedColumn);

        IApplyPlayerSelectionDataSetSuccessBuilder WithExpectedSuccess();
        IApplyPlayerSelectionDataSetFailureBuilder WithExpectedFailure();
        
    }
}
