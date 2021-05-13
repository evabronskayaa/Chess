using Chess.Logic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
//using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.Desktop
{
    public static class Drawer
    {
        private static Canvas field;
        public static double CellSize { get; private set; } = 50;
        public static Window MainWindow { get; set; }
        public static System.Drawing.Point CursorPosition { get; private set; }

        private static Cell[,] cells =  new Cell[Board.Size.X, Board.Size.Y];

        public static Dictionary<int, SolidColorBrush> Colors = new Dictionary<int, SolidColorBrush>()
        {
            { 0, Brushes.DarkRed},
            { 1, Brushes.Green},
            { 2, Brushes.Orange},
            { 3, Brushes.Blue},
            { 4, Brushes.DarkMagenta}
        };
        private static bool isFigureChosen;
        private static System.Drawing.Point figurePosition;
        private static IFigure chosenFigure;
        private static bool isCurrentPlayerFirst;

        public static void CreateGameWindow() 
        {
            var gameWindow = new Window();
            field = new Canvas();
            field.MouseUp += Field_MouseUp;
            gameWindow.Content = field;
            

            for (int y = 0; y < Board.Size.Y; y++) 
            {
                for (int x = 0; x < Board.Size.X; x++) 
                {
                    cells[x, y] = new Cell(field, x, y);  
                }
            }
            gameWindow.Show();
            MainWindow.Hide();
        }

        private static void Field_MouseUp(object sender, MouseButtonEventArgs e)
        {
            
            var x = e.GetPosition(field).X;
            var y = e.GetPosition(field).Y;
            var _x = (int)(Math.Floor(x / CellSize));
            var _y = (int)(Math.Floor(y / CellSize));

            var cell = cells[_x, _y];
            CursorPosition = new System.Drawing.Point(_x, _y);
            if (!isFigureChosen)
            { 
                chosenFigure = cell.Figure;
                if (chosenFigure != null && chosenFigure.IsFirstPlayer == isCurrentPlayerFirst)
                {
                    isFigureChosen = true;
                    figurePosition = new System.Drawing.Point(CursorPosition.X, CursorPosition.Y);
                }
            }
            else
            {
                isFigureChosen = false;
                if (chosenFigure.IsCorrectMove(figurePosition, new System.Drawing.Point(CursorPosition.X, CursorPosition.Y)))
                {
                    cells[CursorPosition.X, CursorPosition.Y].Figure = chosenFigure;
                    cells[figurePosition.X, figurePosition.Y].Figure = null;
                    isCurrentPlayerFirst = !isCurrentPlayerFirst;
                }
            }
        }
    }
}
