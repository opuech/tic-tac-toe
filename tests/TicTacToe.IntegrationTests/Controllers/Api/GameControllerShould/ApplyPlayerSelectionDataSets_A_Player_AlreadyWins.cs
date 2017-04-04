using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;
using TicTacToe.Domain.Game.ValueObject;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    class ApplyPlayerSelectionDataSets_A_Player_AlreadyWins : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = 
            new List<object[]>
        {
            // Grille X Gagnant avec tentative de selection par O
            new ApplyPlayerSelectionDataSetBuilder()
                .WithInitialGrid(
                    new TokenDto[,] {  { TokenDto.X, TokenDto.X, TokenDto.X },
                                       { TokenDto._, TokenDto.X, TokenDto.O },
                                       { TokenDto.O, TokenDto.O, TokenDto._ } })
                .WithSelection(tokenPlayer: TokenDto.O, selectedLine: 2, selectedColumn: 2)
                .WithExpectedFailure()
                .WithExpectedErrorMessage(Board.MESSAGE_IF_PLAYER_IS_ALREADY_THE_LOSER)
                .Create(),

            // Grille O gagnant avec tentative de selection par X
            new ApplyPlayerSelectionDataSetBuilder()
                .WithInitialGrid(
                    new TokenDto[,] {  { TokenDto.X, TokenDto.X, TokenDto.O },
                                       { TokenDto.X, TokenDto.O, TokenDto._ },
                                       { TokenDto.O, TokenDto._, TokenDto._ } })
                .WithSelection(tokenPlayer: TokenDto.X, selectedLine: 2, selectedColumn: 1)
                .WithExpectedFailure()
                .WithExpectedErrorMessage(Board.MESSAGE_IF_PLAYER_IS_ALREADY_THE_LOSER)
                .Create()
        };

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
