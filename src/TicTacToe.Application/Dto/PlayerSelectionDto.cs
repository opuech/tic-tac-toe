using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Application.Dto
{
    public class PlayerSelectionDto
    {
        [Required]
        public string GameId { get; set; }

        [Required]
        public string NomPlayer { get; set; }

        [Required]
        public int Line { get; set; }

        [Required]
        public int Column { get; set; }

    }
}
