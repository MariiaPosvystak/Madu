using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Madu
{
    static class Mängijate
    {
        private static string file = @"..\..\..\Mängijate.txt";

        public static List<Punkt> Load()
        {
            var list = new List<Punkt>();
            if (!File.Exists(file)) return list;

            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length == 2 && int.TryParse(parts[1], out int score))
                    list.Add(new Punkt { Name = parts[0], Score = score });
            }
            return list;
        }

        public static void Save(List<Punkt> list)
        {
            File.WriteAllLines(file, list.Select(s => $"{s.Name};{s.Score}"));
        }

        public static void UpdateScore(string name, int score)
        {
            var list = Load();
            var existing = list.FirstOrDefault(s => s.Name.Equals(name));
            if (existing != null)
            {
                if (score > existing.Score) existing.Score = score;
            }
            else
            {
                list.Add(new Punkt { Name = name, Score = score });
            }
            list = list.OrderByDescending(s => s.Score).ToList();
            Save(list);
        }
    }
}
