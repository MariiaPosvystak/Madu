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

            Params pr = new Params();
            Sounds sounds = new Sounds(pr.GetResourceFolder());

            Console.WriteLine("Tere tulemast mängu!");
            Console.WriteLine("Vali raskusaste: 1 - Lihtne, 2 - Keskmine, 3 - Raske");

            int level = 2;
            while (true)
            {
                var k = Console.ReadKey(true);
                if (k.Key == ConsoleKey.D1 || k.Key == ConsoleKey.NumPad1) 
                { 
                    level = 1; break; 
                }
                if (k.Key == ConsoleKey.D2 || k.Key == ConsoleKey.NumPad2) 
                { 
                    level = 2; break; 
                }
                if (k.Key == ConsoleKey.D3 || k.Key == ConsoleKey.NumPad3) 
                { 
                    level = 3; break; 
                }
            }

            int mapWidth = level == 1 ? 60 : level == 2 ? 80 : 100;
            int mapHeight = level == 1 ? 20 : level == 2 ? 25 : 30;
            int sleep = level == 1 ? 140 : level == 2 ? 100 : 60;

            Console.Clear();
            Console.WriteLine("Kui palju õunu kuvatakse? (0 - juhuslikult, 1-6 - kindel arv)");
            int foodCount = 0;
            if (!int.TryParse(Console.ReadLine(), out foodCount)) foodCount = 0;

            var foodCreator = new FoodCreator(mapWidth, mapHeight, '¤');
            foodCount = foodCreator.GetFood(foodCount);

            Console.Clear();
            sounds.Play("foon.mp3");
            Console.SetWindowSize(mapWidth, mapHeight);
            Console.SetBufferSize(mapWidth, mapHeight);

            Walls walls = new Walls(mapWidth, mapHeight);
            walls.Draw();

            Point start = new Point(4, 5, '*');
            Snake snake = new Snake(start, 4, Direction.RIGHT);
            snake.Drow();

            List<Point> foods = new List<Point>();
            for (int i = 0; i < foodCount; i++)
            {
                Point f = foodCreator.CreateFood();
                foods.Add(f);
                f.Draw();
            }

            int points = 0;
            DrawScore(points);

            while (true)
            {
                if (walls.IsHit(snake) || snake.IsHitTail()) break;

                bool ate = false;
                for (int i = 0; i < foods.Count; i++)
                {
                    if (snake.Eat(foods[i]))
                    {
                        ate = true;

                        sounds.Play("foon.mp3");

                        points += 10;     
                        DrawScore(points); 

                        foods[i] = foodCreator.CreateFood();
                        foods[i].Draw();
                        break;
                    }
                }

                if (!ate) 
                    snake.Move();

                Thread.Sleep(sleep);
                if (Console.KeyAvailable)
                    snake.HandleKey(Console.ReadKey(true).Key);
            }

            Console.Clear();
            Console.Write("Sisesta oma nimi: ");
            Console.Clear();

            string name = "";
            bool validName = false;

            while (!validName)
            {
                try
                {
                    Console.Write("Sisesta oma nimi (vähemalt 3 tähemärki): ");
                    name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                        throw new Exception("Nimi ei tohi olla tühi!");

                    if (name.Length < 3)
                        throw new Exception("Nimi peab olema vähemalt 3 tähemärki pikk!");

                    validName = true; 
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Viga: {ex.Message}");
                }
            }

            Mängijate.UpdateScore(name, points);

            sounds.Play("gameover.mp3");

            Console.Clear();
            Console.WriteLine($"Mäng läbi! Sinu tulemus: {points}");
            Console.WriteLine();

            var allScores = Mängijate.Load().OrderByDescending(s => s.Score).ToList();
            Console.WriteLine("Parimad 3 mängijat:");
            for (int i = 0; i < Math.Min(3, allScores.Count); i++)
                Console.WriteLine($"{i + 1}. {allScores[i].Name} - {allScores[i].Score}");

            Console.WriteLine();
            Console.WriteLine("Vajuta Enter, et väljuda...");
            Console.ReadLine();
        }

        static void DrawScore(int points)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Punktid: {points}   ");
            Console.ResetColor();
        }
    }
}
