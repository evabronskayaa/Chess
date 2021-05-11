using System;
using System.Collections.Generic;
using System.Text;

namespace Chess.Logic
{
    public static class Settings
    {
        private static int color;
        private static int topIndent;
        private static int leftIndent;
        public static int Color
        {
            get
            {
                return color;
            }
            set
            {
                if (value >= 0 && value < 5)
                    color = value;
            }
        }
        public static int TopIndent
        {
            get
            {
                return topIndent;
            }
            set
            {
                if (value >= 0 && value < 5)
                    topIndent = value;
            }
        }
        public static int LeftIndent
        {
            get
            {
                return leftIndent;
            }
            set
            {
                if (value >= 0 && value < 5)
                    leftIndent = value;
            }
        }

        public static SettingsIndexer SettingsIndexer = new SettingsIndexer();

    }
    public class SettingsIndexer
    {
        public int lenght = 8;
        public int this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return Settings.Color;
                    case 1: return Settings.TopIndent;
                    case 2: return Settings.LeftIndent;
                    default: return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0: Settings.Color = (int)value; break;
                    case 1: Settings.TopIndent = (int)value; break;
                    case 2: Settings.LeftIndent = (int)value; break;
                }
            }
        }
    }
}
