using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6_8_Arena
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Arena arena = new Arena();

            bool isNeedContune = true;

            while (isNeedContune)
            {
                arena.Fight();

                isNeedContune = arena.AreFightersAlive();

                Console.ReadKey();
                Console.Clear();
            }
        }  
    }

    abstract class Unit
    {
        public string Name { get; protected set; }
        public string NameCLass { get; protected set; }
        public int HealthPoint { get; protected set; } = 100;
        public int Armor { get; protected set; } = 100;
        public int DamageByHand { get; protected set; }
        public int PersentOfTakeDamageByArmor { get; protected set; } 
        public int ScoreOfAttacks { get; protected set; } = 0;

        public bool IsAlive { get; protected set; } = true;

        public virtual void Attack(Unit enemy)
        {
            Console.WriteLine($"Бьёт {Name}");

            ScoreOfAttacks++;
            enemy.TakeDamage(DamageByHand);
        }

        public void TakeDamage(int damage)
        {
            if (IsAlive)
            {
                int fullPercent = 100;

                Armor -= (damage * PersentOfTakeDamageByArmor) / fullPercent;
                HealthPoint -= (damage * (fullPercent - PersentOfTakeDamageByArmor)) / fullPercent;
            }
            else
            {
                Console.WriteLine("Боец погиб.");
            }

            if (Armor < 0)
            {
                Armor = 0;
            }

            if (HealthPoint < 0)
            {
                IsAlive = false;
                HealthPoint = 0;
            }
        }

        public virtual void RecoverStats()
        {
            HealthPoint = 100;
            Armor = 100;
            ScoreOfAttacks = 0;
        }

        protected string CreateName(string[] names)
        {
            Random random = new Random();

            int indexName = random.Next(0, names.Count());
            return names[indexName];
        }
    }

    class Warrior : Unit
    {
        public Warrior()
        {
            string[] names = new string[] 
            {"Вовчик мощный", "Стас потрошитель", "Ванёк острый кинжал", "Геральд из Ривии", "Ким Чен Руб", "Витёк Свежеватель"};

            Name = CreateName(names);
            PersentOfTakeDamageByArmor = 42;
            DamageByHand = 17;

            NameCLass = "воин";
        }

        public override void Attack(Unit enemy)
        {
            int scoreForUsingUltraAttack = 3;
            int modifier = 2;

            int countUltimateDamage;

            if (ScoreOfAttacks == scoreForUsingUltraAttack)
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;
                countUltimateDamage =  DamageByHand * modifier;

                enemy.TakeDamage(countUltimateDamage);
            }
            else 
            {
                base.Attack(enemy);
            }

            Console.WriteLine($"После удара:\n{enemy.Name} - {enemy.HealthPoint} хп, {enemy.Armor} брони.");
        }
    }

    class Archer : Unit
    {
        public Archer()
        {
            string[] names = new string[] 
            { "Рустемчик чОткий", "Стас попади в глаз", "Иванушка Дурачок", "Леголас", "Гномий тетевунодёр", "Пьер бер перьев" };

            Name = CreateName(names);
            PersentOfTakeDamageByArmor = 28;
            DamageByHand = 24;
            NameCLass = "лучник";
        }

        public override void Attack(Unit enemy)
        {
            Random random = new Random();

            int modifier = 3;

            int minPercentOfProbabilty = 1;
            int maxPercentOfProbabilty = 100;
            int percentForUsingUltraDamage = 50;

            int percentOfProbabilty = random.Next(minPercentOfProbabilty, maxPercentOfProbabilty + 1);

            int countUltimateDamage;

            int scoreForUsingUltraAttack = 3;

            if ((percentOfProbabilty > percentForUsingUltraDamage) & (ScoreOfAttacks == scoreForUsingUltraAttack))
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;

                countUltimateDamage =  DamageByHand * modifier;

                enemy.TakeDamage(countUltimateDamage);
            }
            else
            {
                base.Attack(enemy);
            }

            Console.WriteLine($"После удара:\n{enemy.Name} - {enemy.HealthPoint} хп, {enemy.Armor} брони.");
        }
    }

    class Magician: Unit
    {
        public Magician()
        {
            string[] names = new string[] { "Паха повелитель Палки", "Эдуард мягкий", "Нгуен Ван Тычка", "Виктор Сяськи-Масяськи" };

            Name = CreateName(names);
            DamageByHand = 48;
            PersentOfTakeDamageByArmor = 7;
            NameCLass = "маг";
        }

        public override void Attack(Unit enemy)
        {
            Random random = new Random();

            int valueOfUltraDamage = 180;

            int scoreForUsingUltraAttack = 3;

            int percentMinimal = 1;
            int percentMaximal = 100;
            int percentOfGenerateUltraDamage = 15;

            int countProbabilityOfUltraAttack = random.Next(percentMinimal, percentMaximal + 1);

            if ((countProbabilityOfUltraAttack < percentOfGenerateUltraDamage) & (ScoreOfAttacks == scoreForUsingUltraAttack))
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;

                enemy.TakeDamage(valueOfUltraDamage);
            }
            else
            {
                base.Attack(enemy);
            }

            Console.WriteLine($"После удара:\n{enemy.Name} - {enemy.HealthPoint} хп, {enemy.Armor} брони.");
        }
    }

    class Barbarian : Unit
    {
        public Barbarian()
        {
            string[] names = new string[] { "Арнольд голый", "Финик из Ливии", "Ашот Рукум", "Групакавр" };

            Name = CreateName(names);
            DamageByHand = 23;
            PersentOfTakeDamageByArmor = 32;
            NameCLass = "варвар";
        }

        public override void Attack(Unit enemy)
        {
            int scoreForUsingUltraAttack = 3;

            int fourthPart = 4;

            int modifierDamage = DamageByHand + (DamageByHand / fourthPart);

            if (ScoreOfAttacks == scoreForUsingUltraAttack)
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;

                enemy.TakeDamage(modifierDamage);
            }
            else
            {
                base.Attack(enemy);
            }

            Console.WriteLine($"После удара:\n{enemy.Name} - {enemy.HealthPoint} хп, {enemy.Armor} брони.");
        }
    }

    class Thief : Unit
    {
        public Thief()
        {
            string[] names = new string[] { "Магомед Суетной", "Михаил Петрович", "Олигарх Петька", "Лил Пунк" };

            Name = CreateName(names);
            DamageByHand = 13;
            PersentOfTakeDamageByArmor = 18;
            NameCLass = "вор";
        }

        public override void Attack(Unit enemy)
        {
            Random random = new Random();

            int scoreForUsingUltraAttack = 3;

            int scoreOfComboForUltraDamage = 5;

            int percentMinimal = 1;
            int percentMaximal = 100;
            int percentOfGenerateUltraDamage = 25;

            int countProbabilityOfUltraAttack = random.Next(percentMinimal, percentMaximal + 1);

            if ((countProbabilityOfUltraAttack < percentOfGenerateUltraDamage) & (ScoreOfAttacks == scoreForUsingUltraAttack))
            {
                int valueOfDamage = 0;

                Console.WriteLine("Противник обезоружен!");

                for (int i = 0; i < scoreOfComboForUltraDamage; i++)
                {
                    valueOfDamage += DamageByHand;
                }

                ScoreOfAttacks = 0;

                enemy.TakeDamage(valueOfDamage);
            }
            else
            {
                base.Attack(enemy);
            }

            Console.WriteLine($"После удара:\n{enemy.Name} - {enemy.HealthPoint} хп, {enemy.Armor} брони.");
        }
    }

    class Arena
    {
        private List<Unit> _fighters = new List<Unit>();
        public Arena()
        {
            _fighters.Add(new Warrior());
            _fighters.Add(new Archer());
            _fighters.Add(new Barbarian());
            _fighters.Add(new Magician());
            _fighters.Add(new Thief());
        }

        public bool AreFightersAlive()
        {
            int onlyOneFighter = 1;

            if (_fighters.Count == onlyOneFighter)
            {
                Console.WriteLine($"Остался всего один боец! Победа!{_fighters[onlyOneFighter - 1].Name}") ;
                Console.ReadKey();

                return false;
            }
            else if (_fighters.Count == 0)
            {
                Console.WriteLine("Никто не выжил на арене. Поединок закончен.");

                return false;
            }

            return true;
        }

        public void Fight()
        {
            Random random = new Random();

            Unit firstFighter;
            Unit secondFighter;

            int indexFirstFighter = 1;
            int indexSecondFighter = 2;

            int countOfJointAttack = 3;

            int chooseOfRandom;

            firstFighter = ChooseFighter();
            Console.WriteLine($"Первый боец - {firstFighter.Name}, {firstFighter.NameCLass}");
            secondFighter = ChooseFighter();

            while (firstFighter.IsAlive & secondFighter.IsAlive)
            {
                chooseOfRandom = random.Next(indexFirstFighter, countOfJointAttack + 1);              
                

                if (chooseOfRandom == indexFirstFighter)
                {
                    firstFighter.Attack(secondFighter);
                }
                else if (chooseOfRandom == indexSecondFighter)
                {
                    secondFighter.Attack(firstFighter);
                }
                else
                {
                    firstFighter.Attack(secondFighter);
                    secondFighter.Attack(firstFighter);
                }
            }

            ShowResultsOfFight(firstFighter, secondFighter);
        }

        private void ShowFighters()
        {
            Console.WriteLine("Все имеющиеся бойцы:\n");

            int indexFighter = 0;

            foreach (Unit unit in _fighters)
            {
                indexFighter++;
                Console.WriteLine($"{indexFighter}) Имя: {unit.Name}, класс - {unit.NameCLass}\nпроцент поглощение брони - {unit.PersentOfTakeDamageByArmor}\tурон - {unit.DamageByHand}");
            }

            Console.WriteLine();
        }

        private void ShowResultsOfFight(Unit firstFighter, Unit secondFighter)
        {
            if (firstFighter.IsAlive == false && secondFighter.IsAlive == true)
            {
                Console.WriteLine($"Победа бойца {secondFighter.Name}!\nHP - {secondFighter.HealthPoint}, броня - {secondFighter.Armor}");

                secondFighter.RecoverStats();
                _fighters.Add(secondFighter);
            }
            else if (firstFighter.IsAlive == true && secondFighter.IsAlive == false)
            {
                Console.WriteLine($"Победа бойца {firstFighter.Name}!\nHP - {firstFighter.HealthPoint}, броня - {firstFighter.Armor}");

                firstFighter.RecoverStats();
                _fighters.Add(firstFighter);
            }
            else 
            {
                Console.WriteLine("Ничья! Оба бойцы полегли.");
            }
        }

        private Unit ChooseFighter() 
        {
            Unit fighter;

            bool isIndexFighterCorrect;

            ShowFighters();

            Console.WriteLine("Выберите бойца");

            isIndexFighterCorrect = int.TryParse(Console.ReadLine(),  out int indexForChooseFighter);

            if (isIndexFighterCorrect  & (_fighters.Count >= indexForChooseFighter))
            {
                fighter = _fighters[indexForChooseFighter - 1];

                Console.WriteLine($"Выбран боец {fighter.Name}");

                _fighters.RemoveAt(indexForChooseFighter - 1);
            }
            else
            {
                Random random = new Random();

                int indexOfRandomFighter = random.Next(0, _fighters.Count);

                fighter = _fighters[indexOfRandomFighter];

                _fighters.RemoveAt(indexOfRandomFighter);

                Console.WriteLine($"Неверный индекс бойца! Будет выбран рандомный.");
                Console.WriteLine($"Выбран боец {fighter.Name}");
            }

            Console.ReadKey();
            Console.Clear();

            return fighter;
        }
    }
}
