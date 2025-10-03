using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    class FoodCreator //содание еды в случайных местах
    {
        int mapWidht;
        int mapHeight;
        public char sym;

        static Random random = new Random();

        public FoodCreator(int mapWidht, int mapHeight, char sym)
        {
            this.mapWidht = mapWidht;
            this.mapHeight = mapHeight;
            this.sym = sym;
        }
        public Point CreateFood()
        {
            int x = random.Next(2, mapWidht - 2);
            int y = random.Next(2, mapHeight - 2);
            return new Point(x, y, sym);
        }
        public int GetFood(int foodCount)
        {
            if (foodCount == 0)
            {
                return random.Next(1, 7);
            }
            else if (foodCount < 0 || foodCount > 6)
            {
                Console.WriteLine("Palun sisesta õige number (0-6): ");
                if (int.TryParse(Console.ReadLine(), out int fc))
                {
                    return GetFood(fc);
                }
                else
                {
                    return GetFood(0);
                }
            }
            else
            {
                return foodCount;
            }
        }
    }
}
