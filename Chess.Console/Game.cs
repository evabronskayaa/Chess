namespace Chess.Console
{
    using Chess.Logic;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Text;
    public static class Game
    {
        private static int positionInMenu = 0;
        private static bool isFigureChosen = false;
        private static IFigure chosenFigure;
        private static Point figurePosition;
        private static bool isCurrentPlayerFirst = false;
        public static Point CursorPosition { get; private set; }
        public static void DoMainLoop()
        {
            DoMainMenuLoop();

        }
        private static void DoMainMenuLoop()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.DownArrow && positionInMenu <= 4)
                {
                    positionInMenu++;
                }
                else if (key.Key == ConsoleKey.UpArrow && positionInMenu >= 0)
                {
                    positionInMenu--;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    switch (positionInMenu)
                    {
                        case 0:
                            Board.FillBoard();
                            DoGameLoop();
                            break;
                        case 1:
                            if (File.Exists("stateOfGame")) 
                            {
                                Board.DataWorker.LoadBoard();
                                DoGameLoop();
                            } 
                            break;
                        case 2:
                            DoSettingsLoop();
                            break;
                        case 3:
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                    }
                }
                Drawer.DrawMenu(positionInMenu);
            }
        }
        private static void DoGameLoop()
        {
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.DownArrow && CursorPosition.Y < Board.Size.Y - 1)
                    CursorPosition = new Point(CursorPosition.X, CursorPosition.Y + 1);
                else if (key.Key == ConsoleKey.UpArrow && CursorPosition.Y > 0)
                    CursorPosition = new Point(CursorPosition.X, CursorPosition.Y - 1);
                else if (key.Key == ConsoleKey.LeftArrow && CursorPosition.X > 0)
                    CursorPosition = new Point(CursorPosition.X - 1, CursorPosition.Y);
                else if (key.Key == ConsoleKey.RightArrow && CursorPosition.X < Board.Size.X - 1)
                    CursorPosition = new Point(CursorPosition.X + 1, CursorPosition.Y);
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    if (!isFigureChosen)
                    {
                        chosenFigure = Board.Figures[CursorPosition.X, CursorPosition.Y];
                        if (chosenFigure != null && chosenFigure.IsFirstPlayer == isCurrentPlayerFirst)
                        {
                            isFigureChosen = true;
                            figurePosition = new Point(CursorPosition.X, CursorPosition.Y);
                        }
                    }
                    else
                    {
                        isFigureChosen = false;
                        if (chosenFigure.IsCorrectMove(figurePosition, new Point(CursorPosition.X, CursorPosition.Y)))
                        {
                            Board.Figures[CursorPosition.X, CursorPosition.Y] = chosenFigure;
                            Board.Figures[figurePosition.X, figurePosition.Y] = null;
                            isCurrentPlayerFirst = !isCurrentPlayerFirst;
                        }
                    }
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    Board.DataWorker.SaveBoard();
                    break;
                }
                Drawer.DrawBoard();
            }
        }

        private static void DoSettingsLoop()
        {
            int position = 0;
            Drawer.DrawSettings(position);
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.DownArrow && position <= 4)
                    position++;
                else if (key.Key == ConsoleKey.UpArrow && position >= 0)
                    position--;
                else if (key.Key == ConsoleKey.LeftArrow)
                    Settings.SettingsIndexer[position]--;
                else if (key.Key == ConsoleKey.RightArrow)
                    Settings.SettingsIndexer[position]++;
                else if (key.Key == ConsoleKey.Enter && position == 3) 
                    break;
                
                Drawer.DrawSettings(position);
            }
        }
    }
}
