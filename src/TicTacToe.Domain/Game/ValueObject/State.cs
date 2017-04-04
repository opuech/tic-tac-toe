using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToe.Domain.Game.ValueObject
{
    public enum State
    {
        WaitingPlayerXMove = 0,
        WaitingPlayerOMove = 1,
        //GridFull = 2,
        PlayerXWins = 3,
        PlayerOWins = 4
    }
}
