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

    static class FiguresMethods 
    {
        public static bool CheckLine(Point start,  Point end, bool isHorizontal)
        {
            for (int coord = start.Y + Math.Sign(end.Y - start.Y);
            Math.Abs(coord - end.Y) > 0;
            coord += Math.Sign(end.Y - start.Y))
            {
                if (isHorizontal)
                {
                    if (Board.Figures[coord, end.X] != null) return false;
                }
                else if (Board.Figures[end.X, coord] != null) return false;
            }
            return true;
        }

        public static bool CheckDiagonal(Point start, Point end)
        {
            int x = start.X + Math.Sign(end.X - start.X);
            int y = start.Y + Math.Sign(end.Y - start.Y);
            for (; x != end.X && y != end.Y;)
            {
                if (Board.Figures[x, y] != null) return false;

                x += Math.Sign(end.X - start.X);
                y += Math.Sign(end.Y - start.Y);
            }
            return true;
        }
    }

    public class Pawn : IFigure
    {
        public char Abbreviation => 'P';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove(Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null)
            {
                if (end.X == start.X && Math.Sign(end.Y - start.Y) == (IsFirstPlayer ? -1 : 1))
                    if (Math.Abs(end.Y - start.Y) == 1)
                        return true;
                    else if (Math.Abs(end.Y - start.Y) == 2)
                        return !(Board.Figures[start.X, start.Y + (IsFirstPlayer ? 1 : -1)] == null);

                return false;
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

    public class Rook : IFigure //ладья
    {
        public char Abbreviation => 'R';
        public bool IsFirstPlayer { get; set; }
        public bool IsCorrectMove( Point start, Point end)
        {
            if (Board.Figures[end.X, end.Y] == null || IsFirstPlayer != Board.Figures[end.X, end.Y].IsFirstPlayer)
            {
                if (end.X == start.X) return FiguresMethods.CheckLine(start, end, false);
                else if (end.Y == start.Y) return FiguresMethods.CheckLine(start, end, true);
                else return false;
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
                if (Math.Abs(end.Y - start.Y) == Math.Abs(end.X - start.X))
                    return FiguresMethods.CheckDiagonal(start, end);
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
                if (end.X == start.X) return FiguresMethods.CheckLine(start, end, false);
                else if (end.Y == start.Y) return FiguresMethods.CheckLine(start, end, true);
                else if (Math.Abs(end.Y - start.Y) == Math.Abs(end.X - start.X)) return FiguresMethods.CheckDiagonal(start, end);
                else return false;
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

