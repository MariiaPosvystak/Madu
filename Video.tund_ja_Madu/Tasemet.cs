using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class Tasemet
    {
        public int Level { get; private set; } = 2;
        public int MapWidth { get; private set; }
        public int MapHeight { get; private set; }
        public int Sleep { get; private set; }

        public void ChooseLevel()
        {
            Console.WriteLine("Vali raskusaste: 1 - Lihtne, 2 - Keskmine, 3 - Raske");

            while (true)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                {
                    Level = 1;
                    break;
                }
                if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                {
                    Level = 2;
                    break;
                }
                if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                {
                    Level = 3;
                    break;
                }
            }

            SetParameters();
        }

        private void SetParameters()
        {
            switch (Level)
            {
                case 1:
                    MapWidth = 60;
                    MapHeight = 20;
                    Sleep = 140;
                    break;
                case 2:
                    MapWidth = 80;
                    MapHeight = 25;
                    Sleep = 100;
                    break;
                case 3:
                    MapWidth = 100;
                    MapHeight = 30;
                    Sleep = 60;
                    break;
            }
        }
    }
}
