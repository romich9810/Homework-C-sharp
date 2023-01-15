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
                arena.ShowFighters();
                arena.Fight();

                isNeedContune = arena.IsArenaFill();

                Console.ReadKey();
                Console.Clear();
            }
        }  
    }

    abstract class Unit
    {
        public string Name { get; protected set; }
        public string NameCLass { get; protected set; }
        public int Healthpoint { get; protected set; } = 100;
        public int Armor { get; protected set; } = 100;
        public int DamageByHand { get; protected set; }
        public int PersentOfTakeDamageByArmor { get; protected set; } 
        public int ScoreOfAttacks { get; protected set; } = 0;

        public bool IsAlive { get; protected set; } = true;

        public virtual int GetAttack() { return 0; }//нужен ли ретурн ноль?

        public void TakeDamage(int damage)
        {
            if (IsAlive)
            {
                Armor -= (damage * PersentOfTakeDamageByArmor) / 100;
                Healthpoint -= (damage * (100 - PersentOfTakeDamageByArmor)) / 100;
            }
            else
            {
                Console.WriteLine("Боец погиб.");
            }

            if (Armor < 0)
            {
                Armor = 0;
            }

            if (Healthpoint < 0)
            {
                IsAlive = false;
                Healthpoint = 0;
            }
        }

        public void Heal()
        {
            Healthpoint = 100;
            Armor = 100;
        }

        protected int Attack()
        {
            ScoreOfAttacks++;
            return DamageByHand;
        }
    }

    class Warrior : Unit
    { 
        public Warrior(Random random)
        {
            int minimalPersentOfTakeDamageByArmor = 30;
            int maximalPersentOfTakeDamageByArmor = 42;

            int minimalDamageByHand = 15;
            int maximalDamageByHand = 17;

            Name = GetName(random);
            PersentOfTakeDamageByArmor = random.Next(minimalPersentOfTakeDamageByArmor, maximalPersentOfTakeDamageByArmor + 1);
            DamageByHand = random.Next(minimalDamageByHand, maximalDamageByHand + 1);

            NameCLass = "воин";
        }

        public override int GetAttack()
        {
            int scoreForUsingUltraAttack = 3;
            int modifier = 2;

            if (ScoreOfAttacks == scoreForUsingUltraAttack)
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;
                return DamageByHand * modifier;
            }
            else return Attack();
        }

        private string GetName(Random random) 
        {
            string[] names = new string[] { "Вовчик мощный", "Стас потрошитель", "Ванёк острый кинжал", "Геральд из Ривии", "Ким Чен Руб", "Витёк Свежеватель" };
            int indexName = random.Next(0, names.Count());
            string name = names[indexName];

            return name;
        }
    }

    class Archer : Unit
    { 
        public Archer(Random random)
        {
            int minimalPersentOfTakeDamageByArmor = 20;
            int maximalPersentOfTakeDamageByArmor = 28;

            int minimalDamageByHand = 20;
            int maximalDamageByHand = 24;


            Name = GetName(random);
            PersentOfTakeDamageByArmor = random.Next(minimalPersentOfTakeDamageByArmor, maximalPersentOfTakeDamageByArmor + 1);
            DamageByHand = random.Next(minimalDamageByHand, maximalDamageByHand + 1);

            NameCLass = "лучник";
        }

        public override int GetAttack()
        {
            Random random = new Random();

            int modifier = 3;

            int countForModifier = 2;
            int countForRemodifier = 1;

            int count = random.Next(countForRemodifier, countForModifier + 1);

            int scoreForUsingUltraAttack = 3;

            if ((count == countForModifier) & (ScoreOfAttacks == scoreForUsingUltraAttack))
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;
                return DamageByHand * modifier;
            }
            else
            {
                return Attack();
            }
        }

        public string GetName(Random random)
        {
            string[] names = new string[] { "Рустемчик чОткий", "Стас попади в глаз", "Иванушка Дурачок", "Леголас", "Гномий тетевунодёр", "Пьер бер перьев" };

            int indexName = random.Next(0, names.Count());
            string name = names[indexName];

            return name;
        }
    }

    class Arena
    {
        private List<Unit> _fighters = new List<Unit>();

        public Arena()
        {
            Random random = new Random();

            int countOfFilling = 5;

            for (int i = 0; i < countOfFilling; i++)
            {
                _fighters.Add(GetUnit(random));
            }
        }

        public bool IsArenaFill()
        {
            int onlyOneFighter = 1;

            if (_fighters.Count == onlyOneFighter)
            {
                Console.WriteLine($"Остался всего один боец! Победа!{_fighters[onlyOneFighter - 1].Name}") ;
                Console.ReadKey();

                return false;
            }

            return _fighters.Count >= 0;
        }

        public void ShowFighters()
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

        public void Fight()
        {
            Random random = new Random();

            List<Unit> fighters = new List<Unit>();
            Unit firstFighter;
            Unit secondFighter;

            int indexFirstFighter = 1;
            int indexSecondFighter = 2;

            int chooseOfRandom;

            fighters = ChooseFighter();

            if (fighters != null)                          
            {
                firstFighter = fighters[0];
                secondFighter = fighters[1];

                while (firstFighter.IsAlive && secondFighter.IsAlive)
                {
                    chooseOfRandom = random.Next(indexFirstFighter, indexSecondFighter + 1);

                    if (chooseOfRandom == indexFirstFighter)
                    {
                        Console.WriteLine($"Бьёт {firstFighter.Name}");

                        secondFighter.TakeDamage(firstFighter.GetAttack());

                        Console.WriteLine($"После удара:\n{secondFighter.Name} - {secondFighter.Healthpoint} хп, {secondFighter.Armor} брони.");
                    }

                    if (chooseOfRandom == indexSecondFighter)
                    {
                        Console.WriteLine($"Бьёт {secondFighter.Name}");

                        firstFighter.TakeDamage(secondFighter.GetAttack());

                        Console.WriteLine($"После удара:\n{firstFighter.Name} - {firstFighter.Healthpoint} хп, {firstFighter.Armor} брони.");
                    }
                }

                if (firstFighter.IsAlive == false && secondFighter.IsAlive == true)
                {
                    Console.WriteLine($"Победа бойца {secondFighter.Name}!\nHP - {secondFighter.Healthpoint}, броня - {secondFighter.Armor}");

                    secondFighter.Heal();
                    _fighters.Add(secondFighter);
                }

                if (firstFighter.IsAlive == true && secondFighter.IsAlive == false)
                {
                    Console.WriteLine($"Победа бойца {firstFighter.Name}!\nHP - {firstFighter.Healthpoint}, броня - {firstFighter.Armor}");

                    firstFighter.Heal();
                    _fighters.Add(firstFighter);
                }

                if (firstFighter.IsAlive == false && secondFighter.IsAlive == false)
                {
                    Console.WriteLine("Ничья! Оба бойцы полегли.");
                }
            }  
        }

        private List<Unit> ChooseFighter() 
        {
            List<Unit> fighters = new List<Unit>();
            Unit firstFighter;
            Unit secondFighter;

            bool isIndexFirstFighterCorrect;
            bool isIndexSecondFighterCorrect;

            int firstFighterIndex = 1;
            int secondFighterIndex = 2;

            Console.WriteLine($"Выберите {firstFighterIndex} бойца");
            isIndexFirstFighterCorrect = int.TryParse(Console.ReadLine(),  out int indexForChooseFirstFighter);

            Console.WriteLine($"Выберите {secondFighterIndex} бойца");
            isIndexSecondFighterCorrect = int.TryParse(Console.ReadLine(), out int indexForChooseSecondFighter);

            if ((isIndexFirstFighterCorrect & isIndexSecondFighterCorrect ) & (_fighters.Count >= indexForChooseFirstFighter) 
                & (_fighters.Count >= indexForChooseSecondFighter) & (indexForChooseFirstFighter != indexForChooseSecondFighter))
            {
                firstFighter =  _fighters[indexForChooseFirstFighter - 1];

                Console.WriteLine($"Выбран боец {firstFighter.Name}");

                secondFighter = _fighters[indexForChooseSecondFighter - 1];

                Console.WriteLine($"Выбран боец {secondFighter.Name}");

                fighters.Add(firstFighter);
                fighters.Add(secondFighter);

                _fighters.RemoveAt(indexForChooseFirstFighter - 1);
                _fighters.RemoveAt(indexForChooseSecondFighter - 2);

                return fighters;
            }
            else
            {
                Console.WriteLine($"Неверный индекс одного из бойцов!");
                return null;
            }
        }

        private Unit GetUnit(Random random)
        {
            int warriorIndex = 1;
            int archerIndex = 2;

            int indexForRandom = random.Next(warriorIndex, archerIndex + 1);

            if (indexForRandom == warriorIndex)
            {
                return new Warrior(random);
            }
            else
            {
                return new Archer(random);
            }
        }
    }
}
