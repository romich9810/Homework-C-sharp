using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7_6_Solders
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MilitaryUnit militaryUnit = new MilitaryUnit();

            militaryUnit.ShowNameAndRangSolders();
        }
    }

    class Solder
    {
        public Solder(string rang, string name, string weapoon, int mounthsOfContract)
        {
            Rang = rang;
            Name = name;
            Weapoon = weapoon;
            MounthsOfContract = mounthsOfContract;
        }
        
        public string Rang { get; private set; }
        public string Name { get; private set; }
        public string Weapoon { get; private set; }
        public int MounthsOfContract { get; private set; }
    }

    class MilitaryUnit
    {
        private List<Solder> _solders;

        public MilitaryUnit()
        {
            Random random = new Random();

            int size = 8;

            _solders = GiveRandomSolders(random, size);
        }

        public void ShowNameAndRangSolders()
        {
            var rangAndNameSolders = _solders.Select(solder => new { solder.Name, solder.Rang });

            ShowSolders(_solders);

            Console.WriteLine("\nЗапрос по имени и фамилии:\n");

            foreach(var solder in rangAndNameSolders)
            {
                Console.WriteLine($"{solder.Rang} {solder.Name}");
            }
        }

        private void ShowSolders(List<Solder> solders)
        {
            foreach(Solder solder in solders)
            {
                Console.WriteLine($"{solder.Rang} {solder.Name}, {solder.Weapoon}, {solder.MounthsOfContract} месяцев");
            }
        }

        private List<Solder> GiveRandomSolders(Random random, int count)
        {
            List<string> kindsOfName = new List<string>()
            {
                "Семён", "Виктор", "Владимир", "Аарон", "Пётр", "Роман", "Виталий", "Александр"
            };

            List<string> kindsOfRang = new List<string>()
            {
                "рядовой", "ефрейтор", "сержант", "младший лейтенант", "полковник", "майор"
            };

            List<string> kindsOfWeapoon = new List<string>()
            {
                "АК-47", "АК-74", "\"Винторез\"", "СВД", "пистолет Макарова"
            };

            List<int> kindsOfMounthsOfContract = new List<int>()
            {
                1, 6, 12, 18, 24, 36
            };

            List<Solder> solders = new List<Solder>();

            for (int i = 0; i < count; i++)
            {
                string name = kindsOfName[random.Next(0, kindsOfName.Count)];
                string rang = kindsOfRang[random.Next(0, kindsOfRang.Count)];
                string weapoon = kindsOfWeapoon[random.Next(0, kindsOfWeapoon.Count)];
                int mounthOfContract = kindsOfMounthsOfContract[random.Next(0, kindsOfMounthsOfContract.Count)];

                solders.Add(new Solder(rang, name, weapoon, mounthOfContract));
            }

            return solders;
        }
    }
}
