using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Chess.Logic
{
    public interface IFigure
    {
        public abstract char Abbreviation { get; }
        public abstract bool IsFirstPlayer { get; set; }
        public abstract bool IsCorrectMove(IFigure[,] board, Point start, Point end);
        
    }

    public class Pawn : IFigure
    {
        public char Abbreviation => 'P';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(IFigure[,] board, Point start, Point end)
        {
            if (board[end.X, end.Y] == null)
            {
                if (end.X == start.X && Math.Abs(Math.Abs(end.X - start.X) - 2 * (IsFirstPlayer ? -1 : 1)) == 0)
                {
                    return true;
                }
            }
            else if (IsFirstPlayer != board[end.X, end.Y].IsFirstPlayer)
            {
                if (Math.Abs(end.X - start.X) == 1 && end.Y - start.Y == (IsFirstPlayer ? -1 : 1))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
