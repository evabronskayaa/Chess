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
        public abstract bool IsCorrectMove(Point start, Point end);
    }

    public class Pawn : IFigure
    {
        public char Abbreviation => 'P';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null)
            {
                return end.X == start.X && Math.Abs(Math.Abs(end.Y - start.Y) - (2 * (IsFirstPlayer ? -1 : 1))) <= 1;
            }
            else if (IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                return Math.Abs(end.X - start.X) == 1 && end.Y - start.Y == (IsFirstPlayer ? -1 : 1);
            }
            return false;
        }

        public Pawn(bool isFirstPlayer) 
        {
            IsFirstPlayer = isFirstPlayer;
        }
    }

    public class Rook : IFigure//ладья
    {
        public char Abbreviation => 'R';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove( Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null || IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                return end.X == start.X || end.Y == start.Y;
            }
            return false;
        }

        public Rook(bool isFirstPlayer)
        {
            IsFirstPlayer = isFirstPlayer;
        }
    }

    public class Knight : IFigure
    {
        public char Abbreviation => 'H';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null || IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                return Math.Abs(end.X - start.X) == 2 && Math.Abs(end.Y - start.Y) == 1 ||
                       Math.Abs(end.X - start.X) == 1 && Math.Abs(end.Y - start.Y) == 2;
            }
            return false;
        }

        public Knight(bool isFirstPlayer)
        {
            IsFirstPlayer = isFirstPlayer;
        }
    }

    public class Bishop : IFigure//слон
    {
        public char Abbreviation => 'B';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null ||  IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                return Math.Abs(end.Y - start.Y) == Math.Abs(end.X - start.X);
            }
            return false;
        }

        public Bishop(bool isFirstPlayer)
        {
            IsFirstPlayer = isFirstPlayer;
        }
    }

    public class Queen : IFigure//Ферзь
    {
        public char Abbreviation => 'Q';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null || IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                return end.X == start.X || end.Y == start.Y || Math.Abs(end.Y - start.Y) == Math.Abs(end.X - start.X);
            }
            return false;
        }

        public Queen(bool isFirstPlayer)
        {
            IsFirstPlayer = isFirstPlayer;
        }
    }

    public class King : IFigure
    {
        public char Abbreviation => 'K';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null || IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                return end.X == start.X && Math.Abs(end.Y - start.Y) == 1 || end.Y == start.Y && Math.Abs(end.X - start.X) == 1;
            }
            return false;
        }

        public King(bool isFirstPlayer)
        {
            IsFirstPlayer = isFirstPlayer;
        }
    }
}

