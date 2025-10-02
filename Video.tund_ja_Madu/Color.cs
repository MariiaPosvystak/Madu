using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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
                if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    color = 4;
                    break;
                }
                if (key.Key == ConsoleKey.D5 || key.Key == ConsoleKey.NumPad5)
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
        public int food { get; private set; } = 1;
        public void Choosfood()
        {
            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    food = 1;
                    break;
                }
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    food = 2;
                    break;
                }
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    food = 3;
                    break;
                }
                if (key.Key == ConsoleKey.D4 || key.Key == ConsoleKey.NumPad4)
                {
                    food = 4;
                    break;
                }
            }
        }
        public void Setfood()
        {
            switch (food)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 4:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }
            
        }
        public char sfood()
        {
            char sym = 'o';
            switch (food)
            {
                case 1:
                    sym = 'o';
                    break;
                case 2:
                    sym = '¤';
                    break;
                case 3:
                    sym = 'J';
                    break;
                case 4:
                    sym = '%';
                    break;
            }
            return sym;
        }
    }
}
