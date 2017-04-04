using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Application.Dto;

namespace TicTacToe.IntegrationTests.Controllers.Api.GameControllerShould
{
    class ApplyPlayerSelectionDataSets_Success : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = 
            new List<object[]>
        {
            // Grille initiale avec selection ligne 0 colonne 0 par X
            new ApplyPlayerSelectionDataSetBuilder()
                .WithInitialGrid(
                    new TokenDto[,] {  { TokenDto._, TokenDto._, TokenDto._ },
                                       { TokenDto._, TokenDto._, TokenDto._ },
                                       { TokenDto._, TokenDto._, TokenDto._ } })
                .WithSelection(tokenPlayer: TokenDto.X, selectedLine: 0, selectedColumn: 0)
                .WithExpectedSuccess()
                .WithExpectedGridAfterMove(
                    new TokenDto[,]{  { TokenDto.X, TokenDto._, TokenDto._ },
                                      { TokenDto._, TokenDto._, TokenDto._ },
                                      { TokenDto._, TokenDto._, TokenDto._ } })
                .WithExpectedStatusAfterMove(StateDto.WaitingPlayerOMove)
                .Create(),

            // Grille milieu de partie avec selection ligne 1 colonne 0 par O
            new ApplyPlayerSelectionDataSetBuilder()
                .WithInitialGrid(
                    new TokenDto[,] {  { TokenDto.X, TokenDto.X, TokenDto.O },
                                       { TokenDto._, TokenDto.O, TokenDto._ },
                                       { TokenDto.X, TokenDto._, TokenDto._ } })
                .WithSelection(tokenPlayer: TokenDto.O, selectedLine: 1, selectedColumn: 0)
                .WithExpectedSuccess()
                .WithExpectedGridAfterMove(
                    new TokenDto[,]{  { TokenDto.X, TokenDto.X, TokenDto.O },
                                      { TokenDto.O, TokenDto.O, TokenDto._ },
                                      { TokenDto.X, TokenDto._, TokenDto._ } })
                .WithExpectedStatusAfterMove(StateDto.WaitingPlayerXMove)
                .Create()
        };

        public IEnumerator<object[]> GetEnumerator()
        { return _data.GetEnumerator(); }

        IEnumerator IEnumerable.GetEnumerator()
        { return GetEnumerator(); }
    }
}
