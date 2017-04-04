
using TicTacToe.Domain.Core;

namespace TicTacToe.Application.Dto
{
    public class BoardDto :  Immutable<BoardDto>
    {
        public TokenDto[,] Grid { get; set; }
        public StateDto State { get; set; }
        public BoardDto() { }
        public BoardDto(TokenDto[,] grid, StateDto state)
        {
            this.Grid = grid;
            this.State = state;
        }

    }
}
