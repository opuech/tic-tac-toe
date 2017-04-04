using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TicTacToe.Domain.Core;

namespace TicTacToe.Domain.Game.ValueObject
{
    public class Grid : ValueObject<Grid>
    {
        #region conversions
        public static explicit operator Grid(Token[,] tokens)  
        {
            return new Grid(tokens);
        }
        public static explicit operator Token[,] (Grid grid)
        {
            return grid.innerGrid;
        }
        public static explicit operator Grid(int encodedGrid)  
        {
            var tokens = new Token[3,3];
            int multiplier = 3;
            for (int line = 0; line < 3; line++)
            {
                for (int column = 0; column < 3; column++)
                {
                    var encodedToken = encodedGrid % multiplier;
                    encodedGrid = (encodedGrid - encodedToken) / multiplier;
                    tokens[line, column] = (Token)encodedToken;
                }
            }
            return new Grid(tokens);
        }
        public static explicit operator int(Grid grid)  
        {   
            int encodedGrid = 0;
            int multiplier = 3;
            int coef = 1;
            for (int line = 0; line < 3; line++)
            {
                for (int column = 0; column < 3; column++)
                {
                    var encodedToken = (int)grid.innerGrid[line, column];
                    encodedGrid = encodedGrid  + encodedToken * coef;
                    coef *= multiplier;
                }
            }
           
            return encodedGrid;
        }
        
        #endregion

        public readonly Token[,] innerGrid;
        
        public Grid():this(new Token[,] {  { Token._, Token._, Token._ },
                                           { Token._, Token._, Token._ },
                                           { Token._, Token._, Token._ } })
        {}
        
        public Grid(Token[,] innerGrid)
        {
            this.innerGrid = innerGrid;
        }
        public virtual bool IsXWinning() { return IsWinning(Token.X); }
        public virtual bool IsOWinning() { return IsWinning(Token.O); }
        
        //public virtual bool IsFull()
        //{
        //    return !innerGrid.Cast<Token>().Any(x => x.Equals(Token._)); ;
        //}
        public virtual Token[,] ToTokenArray()
        {
            return innerGrid;
        }

        public virtual bool CanBeSelected(int lineNumber, int columnNumber)
        {
            return innerGrid[lineNumber, columnNumber] == Token._;
        }

        public virtual bool IsInRange(int lineNumber, int columnNumber)
        {
            return 0 <= lineNumber && lineNumber < 3 &&
                   0 <= columnNumber && columnNumber < 3;
        }

        public virtual Grid WithSelection(Token token, int lineNumber, int columnNumber)
        {
            Token[,] newInnerGrid = (Token[,])innerGrid.Clone();
            newInnerGrid[lineNumber, columnNumber] = token;
            return new Grid(newInnerGrid);
        } 

        private bool IsWinning(Token token)
        {
            //Victoire par des lignes
            for (int line = 0; line < 3; line++)
            {
                if (innerGrid[line, 0] == token &&
                    innerGrid[line, 0] == innerGrid[line, 1] &&
                    innerGrid[line, 1] == innerGrid[line, 2])
                    return true;
            }

            //Victoire par colonne
            for (int column = 0; column < 3; column++)
            {
                if (innerGrid[0, column] == token &&
                   innerGrid[0, column] == innerGrid[1, column] &&
                   innerGrid[1, column] == innerGrid[2, column])
                    return true;
            }

            //Victoire par diagonale
            if (innerGrid[1, 1] == token) //Inutile de valider si la case du milieu n'est même pas sélectionné
            {
                // X _ _
                // _ X _
                // _ _ X
                if (innerGrid[0, 0] == innerGrid[1, 1] &&
                    innerGrid[1, 1] == innerGrid[2, 2])
                    return true;

                // _ _ X
                // _ X _
                // X _ _
                if (innerGrid[2, 0] == innerGrid[1, 1] &&
                   innerGrid[1, 1] == innerGrid[0, 2])
                    return true;
            }

            return false;
        }
    }
}
