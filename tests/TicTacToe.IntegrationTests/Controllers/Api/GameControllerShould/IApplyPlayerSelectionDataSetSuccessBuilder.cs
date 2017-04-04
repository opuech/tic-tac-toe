using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    public interface IApplyPlayerSelectionDataSetSuccessBuilder
    {

        IApplyPlayerSelectionDataSetSuccessBuilder WithExpectedGridAfterMove(TokenDto[,] expectedGridAfterMove);
        IApplyPlayerSelectionDataSetSuccessBuilder WithExpectedStatusAfterMove(StateDto expectedStatusAfterMove);

        object[] Create();
        
    }
}
