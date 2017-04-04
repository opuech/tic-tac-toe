using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Domain.Core;

namespace TicTacToe.Domain.Game.Entity
{
    public class GameInformation : Entity<string>
    {
        public readonly string guidIdentifiant;
        public string NomPlayer1 { get; set; }
        public string NomPlayer2 { get; set; }

        public override string Id
        {
            get {return guidIdentifiant; }
        }
        
        public GameInformation(string guidIdentifiant)
        {
            this.guidIdentifiant = guidIdentifiant;
            this.NomPlayer1 = "Player1";
            this.NomPlayer2 = "Player2";
        }
    }
}
