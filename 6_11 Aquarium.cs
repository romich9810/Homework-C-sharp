using System;
using System.Collections.Generic;

namespace _6_11_Aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string WaitOneYearCommandNumber = "1";
            const string AddFishCommandNumber = "2";
            const string PickUpFishCommandNumber = "3";
            const string PickUpDeadFishesCommandNumber = "4";
            const string ExitCommandNumber = "5";

            string waitOneYearCommand = "подождать один год";
            string addFishCommand = "добавить рыбку в аквариум";
            string pickUpFishCommand = "забрать рыбку из аквариума";
            string pickUpDeadFishesCommand = "убрать мёртвых рыбок";
            string exitCommand = "выйти из программы";

            string userCommand;

            Aquarium aquarium = new Aquarium();

            bool isThereInterestInFishes = true;

            while (isThereInterestInFishes)
            {
                if (aquarium.YearAfterBuy > 0)
                {
                    Console.WriteLine($"Прошло {aquarium.YearAfterBuy} после покупки аквариума.");
                }

                aquarium.ShowFishes();

                Console.WriteLine($"\n{WaitOneYearCommandNumber} - {waitOneYearCommand}\n" +
                    $"{AddFishCommandNumber} - {addFishCommand}\n" +
                    $"{PickUpFishCommandNumber} - {pickUpFishCommand}\n" +
                    $"{PickUpDeadFishesCommandNumber} - {pickUpDeadFishesCommand}\n" +
                    $"{ExitCommandNumber} - {exitCommand}");
                Console.Write("\nВведите команду:");

                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case WaitOneYearCommandNumber:
                        aquarium.GrowOld();
                        break;

                    case AddFishCommandNumber:
                        aquarium.AddFish();
                        break;

                    case PickUpFishCommandNumber:
                        aquarium.PickUpFish();
                        break;

                    case PickUpDeadFishesCommandNumber:
                        aquarium.PickUpDeadFishes();
                        break;

                    case ExitCommandNumber:
                        isThereInterestInFishes = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    static class UserUtils
    {
        private static Random _random = new Random();
        static public int GiveRandoNumber(int firstNumber, int secondNumber)
        {
            return _random.Next(firstNumber, secondNumber);
        }
    }

    class Fish
    {
        public Fish(string nameOfType, int age, int lifeExpentancy)
        {
            NameOfType = nameOfType;
            IsAlive = true;
            Age = age;
            LifeExpentancy = lifeExpentancy;
        }

        public string NameOfType { get; private set; }
        public int Age { get; private set; }
        public int LifeExpentancy { get; private set; }
        public bool IsAlive { get; private set; }

        public void GrowOld()
        {
            int minimalPercentOtherFactors = 5;

            if (IsAlive)
            {
                Age++;

                if (LifeExpentancy < Age)
                {
                    int percentOtherFactors = minimalPercentOtherFactors * (Age - LifeExpentancy);

                    InteractWithMicrobes(percentOtherFactors);
                }
                else
                {
                    InteractWithMicrobes(0);
                }
            }
        }

        private void InteractWithMicrobes(int percentOfInfluenceOtherFactors)
        {
            int minPercentOfProbabilty = 1;
            int maxPercentOfProbabilty = 100;

            int percentOfInfection = 5;

            int percentOfProbabilty = UserUtils.GiveRandoNumber(minPercentOfProbabilty, maxPercentOfProbabilty + 1);

            if ((percentOfProbabilty < percentOfInfection + percentOfInfluenceOtherFactors) & IsAlive)
            {
                IsAlive = false;

                Console.WriteLine($"{NameOfType} заболела и умерла.");
            }
        }
    }

    class Aquarium
    {
        private List<Fish> _allFishes;

        public Aquarium()
        {
            Random random = new Random();

            int maxCapacity = 10;
            int minCapacity = 3;

            Capacity = random.Next(minCapacity, maxCapacity + 1);

            _allFishes = new List<Fish>();

            _allFishes.Add(new Fish("Золотая рыбка", 5, 15));
            _allFishes.Add(new Fish("Скалярия", 3, 10));

            YearAfterBuy = 0;
        }

        public int YearAfterBuy { get; private set; }
        public int Capacity { get; private set; }

        public void AddFish()
        {
            string nameOfType;

            bool isAgeCorrect;
            bool isLifeExpentancyCorreclty;

            if (Capacity <= _allFishes.Count)
            {
                Console.WriteLine("Больше нельзя добавить рыбок - полный аквариум!");
            }
            else
            {
                Console.WriteLine($"Введите вид рыбки:");
                nameOfType = Console.ReadLine();

                Console.WriteLine("Введите возраст рыбки:");
                isAgeCorrect = int.TryParse(Console.ReadLine(), out int age);

                Console.WriteLine($"Введите среднюю продолжительность вида {nameOfType}:");
                isLifeExpentancyCorreclty = int.TryParse(Console.ReadLine(), out int lifeExpentancy);

                if ((isAgeCorrect && isLifeExpentancyCorreclty) && (age > 0 && lifeExpentancy > 0))
                {
                    _allFishes.Add(new Fish(nameOfType, age, lifeExpentancy));
                    Console.WriteLine($"{nameOfType} добавлена в аквариум.");
                }
                else
                {
                    Console.WriteLine("Неверный возраст или средняя продолжительность жизни.");
                }
            }
        }

        public void PickUpFish()
        {
            bool isIndexCorrect;

            ShowFishes();

            Console.WriteLine("\nВведите индекс рыбки:");
            isIndexCorrect = int.TryParse(Console.ReadLine(), out int indexFish);

            if (isIndexCorrect && indexFish > 0 && indexFish <= _allFishes.Count)
            {
                _allFishes.RemoveAt(indexFish - 1);

                Console.WriteLine("Рыбка убрана из аквариума.");
            }
            else
            {
                Console.WriteLine("Индекс рыбки некоректен!");
            }
        }

        public void PickUpDeadFishes()
        {
            if (_allFishes.Exists(fish => fish.IsAlive == false) == false)
            {
                Console.WriteLine("Все рыбки живы ).");
            }
            else
            {
                _allFishes.RemoveAll(fish => fish.IsAlive == false);

                Console.WriteLine("Вы похоронили мертвых рыбок.");
            }
        }

        public void ShowFishes()
        {
            int index = 0;

            Console.WriteLine($"Сейчас в аквариуме {_allFishes.Count} рыбок из {Capacity} возможных:");

            foreach (Fish fish in _allFishes)
            {
                index++;

                Console.Write($"{index}) {fish.NameOfType}, {fish.Age} лет, {fish.LifeExpentancy} лет средняя продолжительность жизни, ");

                if (fish.IsAlive)
                {
                    Console.WriteLine("жива.");
                }
                else
                {
                    Console.WriteLine("умерла.");
                }
            }
        }

        public void GrowOld()
        {
            YearAfterBuy++;

            foreach (Fish fish in _allFishes)
            {
                fish.GrowOld();
            }
        }
    }
}
      
