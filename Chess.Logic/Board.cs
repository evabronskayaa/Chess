using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace Chess.Logic
{
    public static class Board
    {
        public static readonly Point Size = new Point(8, 8);
        public static IFigure[,] Figures { get; private set; } = new IFigure[Size.X, Size.Y];

        public static IFigure[,] FillBoard() 
        {
            IEnumerator<IFigure> enumerator1 = GetAnotherFigures(false);
            IEnumerator<IFigure> enumerator2 = GetAnotherFigures(true);

            for (int i = 0; i < Figures.GetLength(0); i++)
            {
                enumerator1.MoveNext();
                Figures[i, 0] = enumerator1.Current;
                Figures[i, 1] = new Pawn(false);

                enumerator2.MoveNext();
                Figures[i, 6] = new Pawn(true);
                Figures[i, 7] = enumerator2.Current;
            }

            return Figures;
        }

        private static IEnumerator<IFigure> GetAnotherFigures(bool isFirstPlayer) 
        {
            IFigure[] figures = new IFigure[Size.X];
            figures[0] = new Rook(isFirstPlayer);
            figures[1] = new Knight(isFirstPlayer);
            figures[2] = new Bishop(isFirstPlayer);
            figures[3] = new King(isFirstPlayer);
            figures[4] = new Queen(isFirstPlayer);
            figures[5] = new Bishop(isFirstPlayer);
            figures[6] = new Knight(isFirstPlayer);
            figures[7] = new Rook(isFirstPlayer);
            
            foreach(IFigure e in figures)
                    yield return e;
        }

        public static class DataWorker
        {
            public static void SaveBoard()
            {
                StringBuilder builder = new StringBuilder();
                foreach (IFigure? e in Figures) 
                {
                    if (e != null)
                        builder.Append("nn");
                    else
                        builder.Append(e.Abbreviation.ToString() + (e.IsFirstPlayer? 1: 0).ToString());
                }

                File.WriteAllText("stateOfGame", builder.ToString());
            }

            public static void LoadBoard() 
            {
                Figures = new IFigure[Size.X,  Size.Y];
                var loading = File.ReadAllText("stateOfGame");
                for (int y = 0; y < Size.Y; y++)
                    for (int x = 0; x < Size.X; x++)
                    {
                        switch (loading[y * Size.X + x * 2])
                        {
                            case 'P':
                                Figures[x, y] = new Pawn(loading[y * Size.X + x * 2 + 1] == '1');
                                break;
                            case 'R':
                                Figures[x, y] = new Rook(loading[y * Size.X + x * 2 + 1] == '1');
                                break;
                            case 'H':
                                Figures[x, y] = new Knight(loading[y * Size.X + x * 2 + 1] == '1');
                                break;
                            case 'B':
                                Figures[x, y] = new Bishop(loading[y * Size.X + x * 2 + 1] == '1');
                                break;
                            case 'Q':
                                Figures[x, y] = new Queen(loading[y * Size.X + x * 2 + 1] == '1');
                                break;
                            case 'K':
                                Figures[x, y] = new King(loading[y * Size.X + x * 2 + 1] == '1');
                                break;
                        }
                    }
            }
        }
    }
}
