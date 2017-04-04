using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Infrastructure.Database.DbModels
{
    public class Game
    {
        [Key]
        public string GuidIdentifiant { get; set; }
        public string NomPlayer1 { get; set; }
        public string NomPlayer2 { get; set; }
        public int Grid { get; set; }
        public int MoveCounter { get; set; }
    }
}
