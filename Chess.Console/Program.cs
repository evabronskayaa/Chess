namespace Chess.Console
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            int widthField = 8,
                heightField = 8,
                topIndent = 5,
                leftIndent = 4,
                heightCell = 3,
                widthCell = 3,
                selectedCellX = 6,
                selectedCellY = 5;

            Console.WriteLine(String.Concat(Enumerable.Repeat('\n', topIndent)));
            DrawLine('┌', '┐', '─', '┬', widthField, widthCell, leftIndent);
            for (int i = 0; i < heightField; i++)
            {
                for (int j = 0; j < heightCell; j++)
                    DrawLine('│', '│', ' ', '│', widthField, widthCell, leftIndent);

                bool endOfField = i == (heightField - 1);
                DrawLine(endOfField ? '└' : '├', endOfField ? '┘' : '┤', '─', endOfField ? '┴' : '┼', widthField, widthCell, leftIndent);
            }

            //SelectCell(widthCell, heightCell, selectedCellX, selectedCellY, topIndent, leftIndent);

            Console.ReadKey();
        }
        private static void DrawLine(char begin, char end, char indent, char center,
            int widthField, int widthCell, int leftIndent)
        {
            Console.Write(String.Concat(Enumerable.Repeat(' ', leftIndent)));
            Console.Write(begin);
            for (int i = 0; i < widthField - 1; i++)
            {
                for (int j = 0; j < widthCell; j++)
                    Console.Write(indent);
                Console.Write(center);
            }
            for (int i = widthField - 1; i < widthField; i++)
                for (int j = 0; j < widthCell; j++)
                    Console.Write(indent);
            Console.WriteLine(end);
        }

    }
}
