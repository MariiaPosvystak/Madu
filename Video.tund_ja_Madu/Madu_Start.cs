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

            Sounds sounds = new Sounds(@"..\..\..\resources");
            
            Console.WriteLine("Tere tulemast mängu!");

            Tasemeted settings = new Tasemeted();
            settings.ChooseLevel();

            int mapWidth = settings.MapWidth;
            int mapHeight = settings.MapHeight;
            int sleep = settings.Sleep;

            Console.Clear();
            Console.WriteLine("Kui palju õunu kuvatakse? (0 - juhuslikult, 1-6 - kindel arv)");
            int foodCount = 0;
            if (!int.TryParse(Console.ReadLine(), out foodCount)) foodCount = 0;
            var foodCreator = new FoodCreator(mapWidth, mapHeight, '§');
            foodCount = foodCreator.GetFood(foodCount);
            Console.Clear();

            Console.WriteLine("Mis värvi sa tahad, et madu oleks?");
            Console.WriteLine("1 - Roheline");
            Console.WriteLine("2 - Sinine");
            Console.WriteLine("3 - Hall");
            Console.WriteLine("4 - Punane");
            Console.WriteLine("5 - Valge");
            Color color = new Color();
            color.ChoosColor();
            Console.Clear();

            sounds.Play("foon.mp3");
            Console.SetWindowSize(mapWidth, mapHeight);
            Console.SetBufferSize(mapWidth, mapHeight);

            Walls walls = new Walls(mapWidth, mapHeight);
            walls.Draw();

            color.SetParameters();
            Point start = new Point(4, 5, '■');
            Snake snake = new Snake(start, 4, Direction.RIGHT);
            snake.Drow();
            Console.ResetColor();

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
                    color.SetParameters();
                    snake.Move();
                    Console.ResetColor();

                Thread.Sleep(sleep);
                if (Console.KeyAvailable)
                    snake.HandleKey(Console.ReadKey(true).Key);
            }

            sounds.Play("gameover.mp3");
            WriteGameOver(points);
            
            
        }
        static void WriteGameOver(int points)
        {

            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;

            WriteText("============================", xOffset, yOffset++);
            WriteText("M Ä N G   L Ä B I", xOffset + 1, yOffset++); 
            yOffset++;
            WriteText($"Skoor: {points}", xOffset + 2, yOffset++); 
            Console.ResetColor();

            Console.Clear();

            string name = "";
            bool validName = false;

            while (!validName)
            {
                try
                {
                    Console.Write("Sisesta oma nimi: ");
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

            Console.Clear();
            Console.WriteLine();

            var allScores = Mängijate.Load().OrderByDescending(s => s.Score).ToList();
            Console.WriteLine("Parimad 3 mängijat:");
            for (int i = 0; i < Math.Min(3, allScores.Count); i++)
                Console.WriteLine($"{i + 1}. {allScores[i].Name} - {allScores[i].Score}");

            Console.WriteLine();
            Console.WriteLine("Vajuta Enter, et väljuda...");
            Console.ReadLine();
        }
        static void WriteText(string text, int xOffset, int yOffset)
        {
            Console.SetCursorPosition(xOffset, yOffset);
            Console.WriteLine(text);
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
