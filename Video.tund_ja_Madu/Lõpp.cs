using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Madu
{
    class Lõpp
    {
        public void WriteGameOver(int points)
        {
            Tasemet settings = new Tasemet();
            int sleep = settings.Sleep;
            int xOffset = 25;
            int yOffset = 8;
            Console.ForegroundColor = ConsoleColor.Red;

            WriteText("============================", xOffset, yOffset++);
            WriteText("M Ä N G   L Ä B I", xOffset + 1, yOffset++);
            yOffset++;
            WriteText($"Skoor: {points}", xOffset + 2, yOffset++);
            Thread.Sleep(sleep);
            Console.ReadKey();
            Console.Clear();
            Console.ResetColor();



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
            Console.WriteLine("Parimad 5 mängijat:");
            for (int i = 0; i < Math.Min(5, allScores.Count); i++)
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
        public void DrawScore(int points)
        {
            Console.SetCursorPosition(0, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Punktid: {points}   ");
            Console.ResetColor();
        }
    }
}
