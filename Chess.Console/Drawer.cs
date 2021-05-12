namespace Chess.Console
{
    using Chess.Logic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    public static class Drawer
    {
        public static string[] MenuItems { get; private set; } = new string[] {"   Начать игру   ", "    Продолжить   ", "    Настройки    ",
                                           "     Рекорды     ", "      Выход      "};
        public static string[] SettingItems { get; private set; } = new string[] {"   Цвет Поля   ", "    Отступ Слева    ",
                                                                "    Отступ Сверху   ", "    Назад в меню    "};

        public static Dictionary<int, ConsoleColor> Colors = new Dictionary<int, ConsoleColor>()
        {
            { 0, ConsoleColor.DarkRed},
            { 1, ConsoleColor.Green},
            { 2, ConsoleColor.DarkYellow},
            { 3, ConsoleColor.Blue},
            { 4, ConsoleColor.DarkMagenta}
        };
        public static void DrawBoard() 
        {
            int topIndent = Settings.TopIndent,
                widthCell = 3,
                leftIndent = Settings.LeftIndent;
            Console.ForegroundColor = Colors[Settings.Color];
            Console.Clear();
            Console.WriteLine(String.Concat(Enumerable.Repeat('\n', topIndent)));
            DrawLine('┌', '┐', '─', '┬', Board.Size.X, widthCell, leftIndent, -1);
            for (int i = 0; i < Board.Size.Y; i++)
            {
                DrawLine('│', '│', ' ', '│', Board.Size.X, widthCell, leftIndent, i);

                bool endOfField = i == (Board.Size.Y - 1);
                DrawLine(endOfField ? '└' : '├', endOfField ? '┘' : '┤', '─', endOfField ? '┴' : '┼', Board.Size.X, widthCell, leftIndent, -1);
            }
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
        }
        private static void DrawLine(char begin, char end,char indent, char center,
           int widthField, int widthCell, int leftIndent, int positionY)
        {
            Console.Write(String.Concat(Enumerable.Repeat(' ', leftIndent)));
            Console.Write(begin);
            for (int i = 0; i < widthField; i++)
            {
                for (int j = 0; j < widthCell; j++) 
                {
                    if (i == Game.CursorPosition.X && positionY == Game.CursorPosition.Y)
                        Console.BackgroundColor = ConsoleColor.Red;

                    if (j == (widthCell - 1) / 2)
                    {
                        if (positionY != -1)
                            if (Board.Figures[i, positionY] != null)
                                Console.Write(Board.Figures[i, positionY].Abbreviation);
                            else
                                Console.Write(indent); 
                        else
                            Console.Write(indent);
                    }
                    else 
                    {
                        Console.Write(indent);
                    }   
                    Console.BackgroundColor = ConsoleColor.White;
                }
                bool endOfField = i == (Board.Size.X - 1);
                Console.Write(endOfField ? end : center);
            }
            Console.WriteLine("");
        }
        public static void DrawMenu(int position)
        {
            Console.SetWindowSize(120, 30);
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            DrawHeading();
            PrintItems(position, MenuItems);
        }
        private static void DrawHeading()
        {
            int i = Console.WindowHeight / 4;
            AppearHeading(i++, "╔═══╗ ╔╗ ╔╗ ╔════╗ ╔═══╗ ╔═══╗");
            AppearHeading(i++, "║╔══╝ ║║ ║║ ║╔═══╝ ║╔══╝ ║╔══╝");
            AppearHeading(i++, "║║    ║╚═╝║ ║╚═══╗ ║╚══╗ ║╚══╗");
            AppearHeading(i++, "║║    ║╔═╗║ ║╔═══╝ ╚═╗ ║ ╚═╗ ║");
            AppearHeading(i++, "║╚══╗ ║║ ║║ ║╚═══╗ ╔═╝ ║ ╔═╝ ║");
            AppearHeading(i++, "╚═══╝ ╚╝ ╚╝ ╚════╝ ╚═══╝ ╚═══╝");
        }
        private static void AppearHeading(int y, string symbols)
        {
            Console.SetCursorPosition(GetPositionX(symbols.Length), y);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine(symbols);
        }

        private static void PrintItems(int position, string[] items)
        {
            
            Console.ForegroundColor = ConsoleColor.Black;
            for (int i = 0; i < items.Length; i++)
            {
                if (i == position) 
                {
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.SetCursorPosition(GetPositionX(items[i].Length), GetPositionY() + i);
                Console.WriteLine(items[i]);
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }
        private static int GetPositionX(int x) => (Console.WindowWidth / 2) - (x / 2);
        private static int GetPositionY() => Console.WindowHeight / 2;

        public static void DrawSettings(int position) 
        {
            Console.Clear();
            PrintItems(position, GetSettingsItems());
        }

        public static string[] GetSettingsItems() 
        {
            string[] items = new string[4];
            items[0] = "      Цвет Поля     " + Settings.Color;
            items[1] = "    Отступ Слева    " + Settings.TopIndent;
            items[2] = "    Отступ Сверху   " + Settings.LeftIndent;
            items[3] = "    Назад в меню    ";

            return items;
        }
    }
}
