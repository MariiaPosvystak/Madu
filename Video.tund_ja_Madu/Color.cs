using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class Color
    {
        public int color{ get; private set; } = 5;
        public void ChoosColor()
        {

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    color = 1;
                    break;
                }
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    color = 2;
                    break;
                }
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    color = 3;
                    break;
                }
                if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad3)
                {
                    color = 4;
                    break;
                }
                if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad3)
                {
                    color = 5;
                    break;
                }
            }
        }
        public void SetParameters()
        {
            switch (color)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case 5:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }
        }
    }
}
