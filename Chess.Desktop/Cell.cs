using System;
using System.Collections.Generic;
using System.Windows.Shapes;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;
using Chess.Logic;
using System.Windows;

namespace Chess.Desktop
{
    class Cell
    {
        public Rectangle Rectangle { get; private set; }
        public TextBlock Text { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }
        private IFigure figure;
        public IFigure Figure 
        {
            get 
            {
                return figure;
            }
            set 
            {
                Board.Figures[X, Y] = value;
                figure = Board.Figures[X, Y];
                UpdateControls();
            }
        }

        public Cell(Canvas canvas, int x, int y) 
        {
            X = x;
            Y = y;
            figure = Board.Figures[x, y];
            Rectangle = new Rectangle() 
            {
                Width = Drawer.CellSize,
                Height = Drawer.CellSize,
                Fill = (x + y % 2) % 2 == 0 ? Brushes.Bisque : Brushes.BurlyWood,
                Stroke = Drawer.Colors[Settings.Color]
            };

            Rectangle.Margin = new Thickness(x * Drawer.CellSize + Settings.LeftIndent * 10, y * Drawer.CellSize + Settings.TopIndent * 10, 0, 0);
            canvas.Children.Add(Rectangle);

            Text = new TextBlock();

            Text.FontSize = Drawer.CellSize;
            Text.FontFamily = new FontFamily("Lucida Console");

            if (Board.Figures[x, y] != null)
            {
                Text.Text = Board.Figures[x, y].Abbreviation.ToString();
                Text.Foreground = Board.Figures[x, y].IsFirstPlayer ?  Brushes.Black : Brushes.White;
            }


            Text.Margin = new Thickness(x * Drawer.CellSize + Drawer.CellSize / 5 + Settings.LeftIndent * 10, y * Drawer.CellSize + Settings.TopIndent * 10, 0, 0);
            canvas.Children.Add(Text);
        }

        public void UpdateControls() 
        {
            if (Board.Figures[X, Y] != null)
            {
                Text.Text = Board.Figures[X, Y].Abbreviation.ToString();
                Text.Foreground = Board.Figures[X, Y].IsFirstPlayer ? Brushes.Black : Brushes.White;
            }
            else 
            {
                Text.Text = string.Empty;
            }

        }
    }
}
