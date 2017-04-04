using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Domain.Core;

namespace TicTacToe.Application.Dto
{
    public class GameDto : Immutable<GameDto>
    {
        [Required]
        public GameInformationDto GameInformation { get; set; }

        [Required]
        public TokenDto[,] Grid { get; set; }

        [Required]
        [Range(0, 9, ErrorMessage = "Le nombre de déplacement doit être compris entre 0 et 9.")]
        public int MoveCounter { get; set; }

        public GameDto() { }
        public GameDto(GameInformationDto gameInformation, TokenDto[,] grid, int moveCounter)
        {
            GameInformation = gameInformation;
            Grid = grid;
            MoveCounter = moveCounter;
        }
    }
}
