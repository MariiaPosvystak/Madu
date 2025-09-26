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
        char sym;

        Random random = new Random();

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
            Random rnd = new Random();
            if (foodCount == 0)
            {
                foodCount = rnd.Next(1, 7);
                return foodCount;
            }
            else if (foodCount < 0 || foodCount > 6)
            {
                Console.WriteLine("Palun sisesta õige number(0-6)");
                foodCount = int.Parse(Console.ReadLine());
                GetFood(foodCount);
            }
            else
            {
                return foodCount;
            }
            return foodCount;
        }
    }
}
