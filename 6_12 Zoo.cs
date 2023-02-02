using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_12_Zoo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();

            zoo.Work();
        }

        enum Gender { Male, Female}
        
        enum Size { Small, Medium, Big} //пойдёт или реализовать через словарь? Да - словарь где напистаь

        enum NameOfType { Giraffe , Lion, Penguin, Horse }

        abstract class Animal
        {
            public Animal(Random random)
            {
                int maleGender = (int)Gender.Male;
                int femaleGender = (int)Gender.Female;

                Gender = (Gender)random.Next(maleGender, femaleGender + 1);
            }

            public int Age { get; protected set; }
            public Gender Gender { get; protected set; }
            public NameOfType NameOfType { get; protected set;  }
            public Size Size { get; protected set; }
            public string Sound { get; protected set; }
        }

        class Giraffe: Animal
        {
            public Giraffe(Random random) : base(random)
            {
                int minAge = 1;
                int maxAge = 25;

                Age = random.Next(minAge, maxAge + 1);

                Size = Size.Big;

                NameOfType = NameOfType.Giraffe;

                Sound = "прпрпр фыр *жуёт траву*";
            }
        }

        class Lion: Animal
        {
            public Lion(Random random) : base(random)
            {
                int minAge = 1;
                int maxAge = 20;

                Age = random.Next(minAge, maxAge + 1);

                Size = Size.Big;

                NameOfType = NameOfType.Lion;

                Sound = "гыыыыыыыыыыр *зевнул*";
            }
        }

        class Penguin: Animal
        {
            public Penguin(Random random) : base(random)
            {
                int minAge = 1;
                int maxAge = 15;

                Age = random.Next(minAge, maxAge + 1);

                Size = Size.Small;

                NameOfType = NameOfType.Penguin;

                Sound = "квяу, квяу *топнул ножками*";
            }
        }

        class Horse: Animal
        {
            public Horse(Random random) : base(random)
            {
                int minAge = 1;
                int maxAge = 20;

                Age = random.Next(minAge, maxAge + 1);

                Size = Size.Medium;

                NameOfType = NameOfType.Horse;

                Sound = "Игогооо *теребит копытом*";
            }
        }

        class Aviary
        {
            private List<Animal> _animals;
            public Aviary (NameOfType nameOfType)
            {
                Random random = new Random();

                _animals = new List<Animal>();

                Animal animal = null;

                switch (nameOfType)
                {
                    case NameOfType.Giraffe:
                        animal = new Giraffe(random);
                        break;

                    case NameOfType.Lion:
                        animal = new Lion(random);
                        break;

                    case NameOfType.Penguin:
                        animal = new Penguin(random);
                        break;

                    case NameOfType.Horse:
                        animal = new Horse(random);
                        break;
                }

                Capacity = GiveCapacityByAnimal(animal);
                SoundOfAnimals = animal.Sound;
                Inhabitant = animal.NameOfType;

                _animals.Add(animal);

                for (int i = 0; i < Capacity - 1; i++)
                {
                    if (animal is Giraffe)
                    {
                        _animals.Add(new Giraffe(random));
                    }
                    else if (animal is Lion)
                    {
                        _animals.Add(new Lion(random));
                    } 
                    else if (animal is Penguin)
                    {
                        _animals.Add(new Penguin(random));
                    }
                    else
                    {
                        _animals.Add(new Horse(random));
                    }
                }
            }

            public int Capacity { get; private set; }
            public string SoundOfAnimals { get; private set; }
            public NameOfType Inhabitant { get; private set; } 

            public void ShowAnimals()
            {
                Console.WriteLine($"Вы видите {_animals.Count} животных:");

                foreach(Animal animal in _animals)
                {
                    Console.WriteLine($"{animal.NameOfType}, {animal.Age} лет, {animal.Gender}");
                }

                Console.WriteLine();
                Console.WriteLine($"Вы слышите звуки \"{SoundOfAnimals}\"");
            }

            private int GiveCapacityByAnimal(Animal animal)
            {
                int smallCapacity = 10;
                int mediumCapacity = 6;
                int bigCapacity = 4;

                int capacity;

                if (animal.Size == Size.Small)
                {
                    capacity = smallCapacity;
                }
                else if (animal.Size == Size.Medium)
                {
                    capacity = mediumCapacity;

                }
                else
                {
                    capacity = bigCapacity;
                }

                return capacity;
            }
        }

        class Zoo
        {
            private List<Aviary> _aviaries;

            public Zoo()
            {
                int size = 4;//магическое ли число? Или получить размер перечисления?

                _aviaries = new List<Aviary>();

                for (int i = 0; i < size; i++)
                {
                    _aviaries.Add(new Aviary((NameOfType)i));
                }
            }

            public void Work()
            {
                const string LookAtGiraffesAvirayCommandNumber = "1";
                const string LookAtPenguinsAvirayCommandNumber = "2";
                const string LookAtLionsAvirayCommandNumber = "3";
                const string LookAtHorsesAvirayCommandNumber = "4";
                const string ExitCommandNumber = "5";

                string lookAtGiraffesAvirayCommand = "подойти к вольеру с жирафами";
                string lookAtPenguinsAvirayCommand = "подойти к вольеру с пингвинами";
                string lookAtLionsAvirayCommand = "подойти к вольеру со львами";
                string lookAtHorsesAvirayCommand = "подойти к вольеру с лошадями";
                string exitCommand = "выйти из программы";

                bool isNeedWork = true;

                string userCommand;

                while (isNeedWork)
                {
                    Console.WriteLine("Добро пожаловать в зоопарк! Выберите вольер, к которому хотите подойти:");
                    Console.WriteLine($"{LookAtGiraffesAvirayCommandNumber} - {lookAtGiraffesAvirayCommand}" +
                        $"\n{LookAtPenguinsAvirayCommandNumber} - {lookAtPenguinsAvirayCommand}" +
                        $"\n{LookAtLionsAvirayCommandNumber} - {lookAtLionsAvirayCommand}" +
                        $"\n{LookAtHorsesAvirayCommandNumber} - {lookAtHorsesAvirayCommand}" +
                        $"\n{ExitCommandNumber} - {exitCommand}");

                    userCommand = Console.ReadLine();

                    switch (userCommand)
                    {
                        case LookAtGiraffesAvirayCommandNumber:
                            GiveAviary(NameOfType.Giraffe).ShowAnimals();
                            break;

                        case LookAtPenguinsAvirayCommandNumber:
                            GiveAviary(NameOfType.Penguin).ShowAnimals();
                            break;

                        case LookAtLionsAvirayCommandNumber:
                            GiveAviary(NameOfType.Lion).ShowAnimals();
                            break;

                        case LookAtHorsesAvirayCommandNumber:
                            GiveAviary(NameOfType.Horse).ShowAnimals();
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
            }

            private Aviary GiveAviary(NameOfType nameOfTypeAnimal)
            {
                return _aviaries.Find(Aviary => Aviary.Inhabitant == nameOfTypeAnimal);
            }
        }
    }
}
