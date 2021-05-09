using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Chess.Logic
{
    public class Board
    {
        public readonly Point Size = new Point(8, 8);
        public static Figure[,] Figures { get; private set; } 
        
    }
}
