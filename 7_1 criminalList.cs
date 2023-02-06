using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_1_CriminalList
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string ShowAllCriminalCommandNumber = "1";
            const string SearchNeedableCriminalCommandNumber = "2";
            const string ExitCommandNumber = "3";

            string showAllCriminalCommand = "показать всех преступников";
            string searchNeedableCriminalCommand = "найти преступников по параметрам";
            string exitCommand = "выход из программы";

            Random random = new Random();

            List<Criminal> allCriminals = new List<Criminal>()
            {
                 new Criminal(random, "Джон Уик", "канадец"),
                 new Criminal(random, "Владимир Ленин"),
                 new Criminal(random, "Аль Капоне", "итальянец"),
                 new Criminal(random, "Зубенко Михаил Петрович"),
                 new Criminal(random, "Абдулла Ибн Аль Мухаммед", "иранец"),
                 new Criminal(random, "Ким Со Хун", "кореец"),
                 new Criminal(random, "Джонни Диллинджер", "американец")
            };

            bool isNeedWork = true;

            string userCommand;

            while (isNeedWork)
            {
                allCriminals[1].IsValueIncludingInPotentialValue(75, 100);

                Console.WriteLine("Добро пожаловать в записки детектива!\n");
                Console.WriteLine($"{ShowAllCriminalCommandNumber} - {showAllCriminalCommand}\n" +
                    $"{SearchNeedableCriminalCommandNumber} - {searchNeedableCriminalCommand}\n" +
                    $"{ExitCommandNumber} - {exitCommand}\n");

                Console.WriteLine("Введите команду: ");

                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case ShowAllCriminalCommandNumber:
                        ShowAllCriminals();
                        break;

                    case SearchNeedableCriminalCommandNumber:
                        ShowNeedableCriminals();
                        break;

                    case ExitCommandNumber:
                        isNeedWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }

            void ShowNeedableCriminals()
            {
                bool isPotentialHeightCorrect;
                bool isPotentialWeightCorrect;
                string potentialNationally;

                Console.WriteLine("Введите вес преступника: ");
                isPotentialWeightCorrect = int.TryParse(Console.ReadLine(), out int potentialWeight);

                Console.WriteLine("Введите рост преступника: ");
                isPotentialHeightCorrect = int.TryParse(Console.ReadLine(), out int potentialHeight);

                Console.WriteLine("Введите национальность преступника: ");
                potentialNationally = Console.ReadLine();

                if ((isPotentialWeightCorrect && potentialWeight > 0) && (isPotentialHeightCorrect && potentialHeight > 0))
                {
                    var needableCriminals = allCriminals.Where(criminal => criminal.isInPrison == false)
                        .Where(criminal => criminal.Nationally == potentialNationally)
                        .Where(criminal => criminal.IsValueIncludingInPotentialValue(criminal.Weight, potentialWeight) == true)
                        .Where(criminal => criminal.IsValueIncludingInPotentialValue(criminal.Height, potentialHeight) == true);

                    if (needableCriminals.Count() > 0)
                    {
                        Console.WriteLine("Потенциальные преступники:\n");

                        foreach (var criminal in needableCriminals)
                        {
                            Console.WriteLine($"{criminal.NameLastName}, {criminal.Nationally}, {criminal.Weight} кг, {criminal.Height} см");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Нет подходящих преступников.");
                    }
                }
                else
                {
                    Console.WriteLine("Параметры веса или роста некорректны.");
                }                
            }

            void ShowAllCriminals()
            {
                Console.Clear();

                Console.WriteLine("Список всех преступников: \n");

                foreach (Criminal criminal in allCriminals)
                {
                    Console.Write($"{criminal.NameLastName}, {criminal.Nationally}, {criminal.Weight} кг, {criminal.Height} см");

                    if (criminal.isInPrison == false)
                    {
                        Console.Write(", на свободе");
                    }

                    Console.WriteLine();
                }
            }
        }
    }

    class Criminal
    {
        public Criminal(Random random, string nameLastName, string nationally = "русский" )
        {
            int minPersentArrest = 1;
            int maxPersentArrest = 100;

            int persentArrestReal = 60;

            int persentArrest = random.Next(minPersentArrest, maxPersentArrest + 1);

            int minHeight = 150;
            int maxHeight = 220;

            int minWeight = 55;
            int maxWeight = 150;

            if (persentArrest <= persentArrestReal)
            {
                isInPrison = true;
            }
            else
            {
                isInPrison = false;
            }


            Height = random.Next(minHeight, maxHeight + 1);
            Weight = random.Next(minWeight, maxWeight + 1);

            NameLastName = nameLastName;
            Nationally = nationally;
        }

        public string NameLastName { get; private set; }
        public bool isInPrison { get; private set; }
        public int Weight { get; private set; }
        public int Height { get; private set; }
        public string Nationally { get; private set; }

        public bool IsValueIncludingInPotentialValue(int value, int potentialValue)
        {
            int maxDifference = 40;
            int minDifference = 0;

            int difference = Math.Abs(potentialValue - value);

            if (difference <= maxDifference && difference >= minDifference)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
