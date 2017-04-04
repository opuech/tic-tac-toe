using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Domain.Core;

namespace TicTacToe.Application.Dto
{
    public class GameInformationDto : Immutable<GameInformationDto>
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string NomPlayer1 { get; set; }

        [Required]
        public string NomPlayer2 { get; set; }
    }
}
