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
        public int Healthpoint { get; protected set; } = 100;
        public int Armor { get; protected set; } = 100;
        public int DamageByHand { get; protected set; }
        public int PersentOfTakeDamageByArmor { get; protected set; } 
        public int ScoreOfAttacks { get; protected set; } = 0;//обнулять после боя!

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

        public void RecoverFighter()
        {
            Healthpoint = 100;
            Armor = 100;
            ScoreOfAttacks = 0;
        }

        protected int Attack()
        {
            Console.WriteLine($"Бьёт {Name}");

            ScoreOfAttacks++;
            return DamageByHand;
        }

        protected string GetName(string[] names)
        {
            Random random = new Random();

            int indexName = random.Next(0, names.Count());
            return names[indexName];
        }
    }

    class Warrior : Unit
    {
        private string[] _names = new string[] { "Вовчик мощный", "Стас потрошитель", "Ванёк острый кинжал", "Геральд из Ривии", "Ким Чен Руб", "Витёк Свежеватель" };

        public Warrior()
        {
            Name = GetName(_names);
            PersentOfTakeDamageByArmor = 42;
            DamageByHand = 17;

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
            else 
            {
                return Attack();
            }                
        }
    }

    class Archer : Unit
    {
        private string[] _names = new string[] { "Рустемчик чОткий", "Стас попади в глаз", "Иванушка Дурачок", "Леголас", "Гномий тетевунодёр", "Пьер бер перьев" };

        public Archer()
        {
            Name = GetName(_names);
            PersentOfTakeDamageByArmor = 28;
            DamageByHand = 24;
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
    }

    class Magician: Unit
    {
        private string[] _names = new string[] { "Паха повелитель Палки", "Эдуард мягкий", "Нгуен Ван Тычка", "Виктор Сяськи-Масяськи" };

        public Magician()
        {
            Name = GetName(_names);
            DamageByHand = 48;
            PersentOfTakeDamageByArmor = 7;
            NameCLass = "маг";
        }

        public override int GetAttack()
        {
            Random random = new Random();

            int valueOfUltraDamage = 180;

            int scoreForUsingUltraAttack = 3;

            int percentMinimal = 0;
            int percentMaximal = 100;
            int percentOfGenerateUltraDamage = 15;

            int countProbabilityOfUltraAttack = random.Next(percentMinimal, percentMaximal + 1);

            if ((countProbabilityOfUltraAttack <= percentOfGenerateUltraDamage) & (ScoreOfAttacks == scoreForUsingUltraAttack))
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;

                return valueOfUltraDamage;
            }
            else
            {
                return Attack();
            }
        }
    }

    class Barbarian : Unit
    {
        private string[] _names = new string[] { "Арнольд голый", "Финик из Ливии", "Ашот Рукум", "Групакавр" };

        public Barbarian()
        {
            Name = GetName(_names);
            DamageByHand = 23;
            PersentOfTakeDamageByArmor = 32;
            NameCLass = "варвар";
        }

        public override int GetAttack()
        {
            int scoreForUsingUltraAttack = 3;

            int modifierDamage = DamageByHand + (DamageByHand / 4);

            if (ScoreOfAttacks == scoreForUsingUltraAttack)
            {
                Console.WriteLine($"{Name} применил ультиматив!");

                ScoreOfAttacks = 0;

                return modifierDamage;
            }
            else
            {
                return Attack();
            }
        }
    }

    class Thief : Unit
    {
        private string[] _names = new string[] { "Магомед Суетной", "Михаил Петрович", "Олигарх Петька", "Лил Пунк" };

        public Thief()
        {
            Name = GetName(_names);
            DamageByHand = 13;
            PersentOfTakeDamageByArmor = 18;
            NameCLass = "вор";
        }

        public override int GetAttack()
        {
            Random random = new Random();

            int scoreForUsingUltraAttack = 3;

            int scoreOfComboForUltraDamage = 5;

            int percentMinimal = 0;
            int percentMaximal = 100;
            int percentOfGenerateUltraDamage = 25;

            int countProbabilityOfUltraAttack = random.Next(percentMinimal, percentMaximal + 1);

            if ((countProbabilityOfUltraAttack <= percentOfGenerateUltraDamage) & (ScoreOfAttacks == scoreForUsingUltraAttack))
            {
                int valueOfDamage = 0;

                Console.WriteLine("Противник обезоружен!");

                for (int i = 0; i < scoreOfComboForUltraDamage; i++)
                {
                    valueOfDamage += Attack();
                }

                ScoreOfAttacks = 0;

                return valueOfDamage;
            }
            else
            {
                return Attack();
            }
        }
    }

    class Arena
    {
        private List<Unit> _fighters = new List<Unit>();

        public Arena()
        {
            _fighters.Add(new Warrior());
            _fighters.Add(new Archer());
            _fighters.Add(new Magician());
            _fighters.Add(new Barbarian());
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

            return _fighters.Count >= 0;
        }

        public void Fight()
        {
            Random random = new Random();

            Unit firstFighter;
            Unit secondFighter;

            int indexFirstFighter = 1;
            int indexSecondFighter = 2;

            int chooseOfRandom;

            firstFighter = ChooseFighter();
            Console.WriteLine($"Первый боец - {firstFighter.Name}, {firstFighter.NameCLass}") ;
            secondFighter = ChooseFighter();     

            if (firstFighter != null & secondFighter != null)                          
            {            
                while (firstFighter.IsAlive && secondFighter.IsAlive)
                {
                    chooseOfRandom = random.Next(indexFirstFighter, indexSecondFighter + 1);

                    if (chooseOfRandom == indexFirstFighter)
                    {
                        secondFighter.TakeDamage(firstFighter.GetAttack());

                        Console.WriteLine($"После удара:\n{secondFighter.Name} - {secondFighter.Healthpoint} хп, {secondFighter.Armor} брони.");
                    }

                    if (chooseOfRandom == indexSecondFighter)
                    {
                        firstFighter.TakeDamage(secondFighter.GetAttack());

                        Console.WriteLine($"После удара:\n{firstFighter.Name} - {firstFighter.Healthpoint} хп, {firstFighter.Armor} брони.");
                    }
                }

                ShowResultsOfFight(firstFighter, secondFighter);
            }  
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
                Console.WriteLine($"Победа бойца {secondFighter.Name}!\nHP - {secondFighter.Healthpoint}, броня - {secondFighter.Armor}");

                secondFighter.RecoverFighter();
                _fighters.Add(secondFighter);
            }

            if (firstFighter.IsAlive == true && secondFighter.IsAlive == false)
            {
                Console.WriteLine($"Победа бойца {firstFighter.Name}!\nHP - {firstFighter.Healthpoint}, броня - {firstFighter.Armor}");

                firstFighter.RecoverFighter();
                _fighters.Add(firstFighter);
            }

            if (firstFighter.IsAlive == false && secondFighter.IsAlive == false)
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
                fighter =  _fighters[indexForChooseFighter - 1];

                Console.WriteLine($"Выбран боец {fighter.Name}");              

                _fighters.RemoveAt(indexForChooseFighter - 1);

                Console.ReadKey();
                Console.Clear();

                return fighter;
            }
            else
            {
                Console.WriteLine($"Неверный индекс бойца!");

                Console.ReadKey();
                Console.Clear();

                return null;
            }
        }
    }
}
