using Chess.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chess.Desktop
{
    public static class Drawer
    {
        private static string firstPlayerName;
        private static string secondPlayerName;
        private static Canvas field;
        public static double CellSize { get; private set; } = 40;
        public static Window MainWindow { get; set; }
        public static System.Drawing.Point CursorPosition { get; private set; }

        private static Cell[,] cells = new Cell[Board.Size.X, Board.Size.Y];

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
            var gameWindow = GetNewWindow();
            gameWindow.Closed += GameWindow_Closed;
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
        }

        public static void CreatePreviousGameWindow() 
        {
            var previousGameWindow = GetNewWindow();

            var stackPanel = new StackPanel();
            var player1 = new DockPanel();
            var player2 = new DockPanel();
            stackPanel.Children.Add(player1);
            stackPanel.Children.Add(player2);

            var textBlock1 = new TextBlock();
            textBlock1.Text = "Введите имя: ";
            var textBlock2 = new TextBlock();
            textBlock2.Text = "Введите имя: ";

            player1.Children.Add(textBlock1);
            player2.Children.Add(textBlock2);

            var name1 = new TextBox();
            var name2 = new TextBox();

            player1.Children.Add(name1);
            player2.Children.Add(name2);

            var btnDone = new Button();
            btnDone.Content = "Готово";
            btnDone.Click += (object sender, RoutedEventArgs e) => 
            {
                firstPlayerName = name1.Text;
                secondPlayerName = name2.Text;
                CreateGameWindow();
                previousGameWindow.Close();
            };

            stackPanel.Children.Add(btnDone);
            previousGameWindow.Content = stackPanel;
        }

        private static void GameWindow_Closed(object sender, EventArgs e)
        {
            Board.DataWorker.SaveBoard();
            MainWindow.Show();
        }

        private static void CheckFigures()
        {
            int figuresCount1 = 0;
            int figuresCount2 = 0;
            for (int x = 0; x < Board.Size.X; x++)
            {
                for (int y = 0; y < Board.Size.Y; y++)
                {
                    if (Board.Figures[x, y] != null)
                    {
                        if (Board.Figures[x, y].IsFirstPlayer == false)
                            figuresCount1++;
                        else
                            figuresCount2++;
                    }
                }
            }
            if (figuresCount1 == 0)
            {
                CreateWinnersWindow(secondPlayerName, GetRatingFromFile()[secondPlayerName]);
            }
            else if (figuresCount2 == 0) 
            {
                CreateWinnersWindow(firstPlayerName, GetRatingFromFile()[firstPlayerName]);
            }
        }
        private static void CreateWinnersWindow(string winnersName, int count)
        {
            var winnersWindow = GetNewWindow();
            winnersWindow.Closed += (object sender, EventArgs e) =>
            {
                winnersWindow.Close();
            };

            var stackPanel = new StackPanel();
            var textBlock = new TextBlock();
            textBlock.Text = winnersName + " победил(а)!";
            stackPanel.Children.Add(textBlock);

            if (File.ReadAllText("rating.txt").Contains(winnersName + ";" + count))
            {
                File.WriteAllLines("rating.txt",
                        File.ReadLines("rating.txt").Where(l => l != (winnersName + ";" + count)).ToList());
                count += 1;
                File.AppendAllText("rating.txt", winnersName + ";" + count);
            }
            else
            {
                File.AppendAllText("rating.txt", "\n" + winnersName + ";" + 1);
            }

            winnersWindow.Content = stackPanel;
        }
        private static void Field_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var x = e.GetPosition(field).X - Settings.LeftIndent * 10;
            var y = e.GetPosition(field).Y - Settings.TopIndent * 10;
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
                    CheckFigures();
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
                    CheckFigures();
                }
            }
        }
        private static Window GetNewWindow() 
        {
            var newWindow = new Window();
            newWindow.Height = 450;
            newWindow.Width = 450;

            newWindow.Show();
            MainWindow.Hide();

            return newWindow;
        }

        public static void CreateSettingsWindow()
        {
            var settingsWindow = GetNewWindow();
            settingsWindow.Closed += SettingsWindow_Closed;
            var stackPanel = new StackPanel();
            CreateSettingsItem("Цвет границ", 0, stackPanel);
            CreateSettingsItem("Отступ сверху", 1 , stackPanel);
            CreateSettingsItem("Отступ слева", 2, stackPanel);

            settingsWindow.Content = stackPanel;
        }

        private static void SettingsWindow_Closed(object sender, EventArgs e)
        {
            MainWindow.Show();
        }

        private static void CreateSettingsItem(string text, int indexOfSetting, StackPanel stackPanel) 
        {
            var dockPanel = new DockPanel();
            stackPanel.Children.Add(dockPanel);

            TextBlock textBlock = new TextBlock();
            textBlock.Text = text;
            TextBox textBox = new TextBox();
            textBox.Text = Settings.SettingsIndexer[indexOfSetting].ToString();
            textBox.TextChanged += (object sender, TextChangedEventArgs e) =>
            {
                var inputText = ((TextBox)sender).Text;
                if (Int32.TryParse(inputText, out int number)) 
                {
                    Settings.SettingsIndexer[indexOfSetting] = number;
                }
            };
            dockPanel.Children.Add(textBlock);
            dockPanel.Children.Add(textBox);
        }

        public static void CreateRatingWindow() 
        {
            var ratingWindow = GetNewWindow();
            ratingWindow.Closed += (object sender, EventArgs e) => 
            {
                ratingWindow.Close();
                MainWindow.Show();
            };

            var stackPanel = new StackPanel();
            
            foreach (var element in GetRatingFromFile()) 
            {
                var textBlock = new TextBlock();
                textBlock.Text = element.Key + " " + element.Value;
                stackPanel.Children.Add(textBlock);
            }
            
            ratingWindow.Content = stackPanel;
        }

        private static Dictionary<string, int> GetRatingFromFile() 
        {
            string[] lines = File.ReadAllLines("rating.txt");
            var dictionary = new Dictionary<string, int>();

            foreach (string line in lines) 
            {
                string[] e = line.Split(";");
                dictionary.Add(e[0], Int32.Parse(e[1]));
            }
            return dictionary;
        }
    }

}
