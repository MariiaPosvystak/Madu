using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace Madu
{
    internal class Madu_Start //Основной класс для запуска программы
    {
        public static void Main(string[] args)
        {
            Console.CursorVisible = false;

            //Params pr = new Params();
            //Sounds sounds = new Sounds(pr.GetResourceFolder());
            //string pathToMedia = @"..\..\..\..\resources";

            //Воспроизведение звуков с класса Sounds, папки resources 
            Sounds sounds = new Sounds(@"..\..\..\resources");
            
            Console.WriteLine("Tere tulemast mängu!");

            //Выбор уровня
            Tasemeted settings = new Tasemeted();
            settings.ChooseLevel();

            int mapWidth = settings.MapWidth;
            int mapHeight = settings.MapHeight;
            int sleep = settings.Sleep;

            Console.Clear();
            //Выбор количества и типа еды которые будут появлятся на экране
            Console.WriteLine("Kui palju õunu kuvatakse? (0 - juhuslikult, 1-6 - kindel arv)");
            int foodCount = 0;
            if (!int.TryParse(Console.ReadLine(), out foodCount)) foodCount = 0;
            Console.WriteLine("Mida sa süüa tahad?");
            Console.WriteLine("1 - Õun");
            Console.WriteLine("2 - Mustikas");
            Console.WriteLine("3 - Banaan");
            Console.WriteLine("4 - Kirs");
            Color color = new Color();
            color.Choosfood();
            char sym = color.sfood();
            Console.ResetColor();
            var foodCreator = new FoodCreator(mapWidth, mapHeight, sym);
            foodCount = foodCreator.GetFood(foodCount);
            Console.Clear();
            //Выбор цвета змейки
            Console.WriteLine("Mis värvi sa tahad, et madu oleks?");
            Console.WriteLine("1 - Roheline");
            Console.WriteLine("2 - Sinine");
            Console.WriteLine("3 - Hall");
            Console.WriteLine("4 - Punane");
            Console.WriteLine("5 - Valge");
            color.ChoosColor();
            Console.Clear();

            //Запуск музыки и установка параметров размера окна
            sounds.Play("foon.mp3");
            Console.SetWindowSize(mapWidth, mapHeight);
            Console.SetBufferSize(mapWidth, mapHeight);

            //Рисовка змеи и полей игры
            Walls walls = new Walls(mapWidth, mapHeight);
            walls.Draw();
            Point start = new Point(4, 5, '■');
            Snake snake = new Snake(start, 4, Direction.RIGHT);
            snake.Drow();
            Console.ResetColor();

            //Рисование еды
            List<Point> foods = new List<Point>();
            for (int i = 0; i < foodCount; i++)
            {
                color.Setfood();
                Point f = foodCreator.CreateFood();
                foods.Add(f);
                f.Draw();
            }
            Lõpp over = new Lõpp();
            //Вывод очков на экран 
            int points = 0;
            over.DrawScore(points);

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail()) break;

                bool ate = false;
                for (int i = 0; i < foods.Count; i++)
                {
                    if (snake.Eat(foods[i]))
                    {
                        ate = true;
                        //Когда змейка ест играет звук и добавляються очки
                        sounds.Play("eat.mp3");

                        points += 10;
                        over.DrawScore(points);

                        color.Setfood();
                        foods[i] = foodCreator.CreateFood();
                        foods[i].Draw();
                        break;
                    }
                }

                if (!ate)//усли не ест то просто движется дальше
                    color.SetParameters();
                    snake.Move();
                    Console.ResetColor();

                Thread.Sleep(sleep);
                if (Console.KeyAvailable)
                    snake.HandleKey(Console.ReadKey(true).Key);
            }

            sounds.Play("gameover.mp3");//когда проигрываеш отображаються очки и играет звук
            over.WriteGameOver(points);
            
            
        }
    }
}
