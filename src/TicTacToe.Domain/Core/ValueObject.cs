using System;

namespace TicTacToe.Domain.Core
{
    [Serializable]
    public class ValueObject<T> : Immutable<T>
            where T : ValueObject<T>
    {
    }
}
