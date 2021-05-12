namespace Chess.Console
{
    using System;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Drawer.DrawMenu(0);
            Game.DoMainLoop();
        }
    }
}
