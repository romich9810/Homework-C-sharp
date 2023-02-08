using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_7_SquadSwap
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Solder> firstSquad = new List<Solder>()
            {
                new Solder("Роман", "Шевченко"),
                new Solder("Илья", "Броненосец"),
                new Solder("Фёдор", "Иващенко"),
                new Solder("Сергей", "Бричка"),
                new Solder("Аарон", "Свекла")
            };

            List<Solder> secondSquad = new List<Solder>()
            {
                new Solder("Джон", "Уикленд"),
                new Solder("Ким" ,"Шень"),
                new Solder("Бретт", "Смитт"),
                new Solder("Фёдор", "Дзюба"),
                new Solder("Уолт", "Бриксон")
            };

            List<Solder> allSoldersInFirstSquadWithParametr = new List<Solder>();

            string startLastNameParametr = "Б";

            Console.WriteLine("До объединения:\n");

            ShowSolders(firstSquad);
            ShowSolders(secondSquad);

            allSoldersInFirstSquadWithParametr = firstSquad.Where(solder => solder.LastName.ToUpper().StartsWith(startLastNameParametr)).ToList();
            firstSquad = firstSquad.Except(allSoldersInFirstSquadWithParametr).ToList();
            secondSquad = secondSquad.Concat(allSoldersInFirstSquadWithParametr).ToList();

            Console.WriteLine("После объединения:\n");

            ShowSolders(firstSquad);
            ShowSolders(secondSquad);

            void ShowSolders(List<Solder> solders)
            {
                foreach(Solder solder in solders)
                {
                    Console.WriteLine($"{solder.LastName} {solder.Name}");
                }

                Console.WriteLine();
            }
        }
    }

    class Solder
    {
        public Solder(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }

        public string Name { get; private set; }
        public string LastName { get; private set; }
    }
}
