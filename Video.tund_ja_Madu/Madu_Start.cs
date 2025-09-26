using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    internal class Madu_Start //Основной класс для запуска программы
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //Console.SetBufferSize(80, 25);

            Console.WriteLine("Tere tulemast mängima!");
            Console.WriteLine("Сколько вы хотите что бы появлялось еды? введите количество от 1-6, или рандомным образом(введите 0)");
            int foodCount = int.Parse(Console.ReadLine());
            
            Console.Clear();


            Walls walls = new Walls(80, 25);
            walls.Draw();

            // Отрисовка точек			
            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.RIGHT);
            snake.Drow();

            FoodCreator foodCreator = new FoodCreator(80, 25, '¤');
            Point food = foodCreator.CreateFood();
            foodCount = foodCreator.GetFood(foodCount);
            for (int i = 0; i < foodCount; i++)
                food.Draw();



            while (true)
            {
                
                
                if (walls.IsHit(snake) || snake.IsHitTail())
                {
                    break;
                }
                
                if (snake.Eat(food))
                {
                    food = foodCreator.CreateFood();
                    for (int i = 0; i < foodCount; i++)
                    {
                        food.Draw();
                    }
                }
                else
                {
                    snake.Move();
                }

                Thread.Sleep(100);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey();
                    snake.HandleKey(key.Key);
                }
                
            }
            WriteGameOver();
            Console.ReadLine();
        }
        static void WriteGameOver()
        {
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(xOffset, yOffset++);
            WriteText("============================", xOffset, yOffset++);
            WriteText("И Г Р А    О К О Н Ч Е Н А", xOffset + 1, yOffset++);
            yOffset++;
            WriteText("", xOffset + 2, yOffset++);
            WriteText("", xOffset + 1, yOffset++);
            WriteText("", xOffset, yOffset++);
        }

        static void WriteText(String text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
        }
    }
}
