using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace _6_10_SquadsFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            string firstSide = "слева";
            string secondSide = "справа";

            Battlefield battlefield = new Battlefield(firstSide, secondSide, random);

            battlefield.Fight();
        }
    }

    enum Role { Medic, Comander, SubmachineGunner}

    abstract class Unit
    {
        public int HealthPoint { get; protected set; }
        public Role Role { get; protected set; }
        public int Armor { get; protected set; }
        public int Damage { get; protected set; }
        public bool IsAlive { get; protected set; }

        public virtual void TakeDamage(int damage) { }

        public virtual void ShowStats() 
        {
            Console.Write($"{Role} - {HealthPoint} ХП, {Armor} броня, {Damage} урон");
        }

        public void Attack(Unit enemy)
        {
            enemy.TakeDamage(Damage);

            Console.WriteLine($"{Role} атакует {enemy.Role} на {Damage} урона.");

            if (enemy.IsAlive == false)
            {
                Console.WriteLine($"{enemy.Role} погиб");
            }
        }

        public void RecoveryHealthPoint()
        {
            Random random = new Random();

            int maxCountOfHealthPoint = 100;
            int pieceOfHalfHealing = 2;

            int minPercentProbabiltyOfHealing = 1;
            int maxPercentProbablityOfHealing = 100;

            int percentProbablityForFullHealing = 60;
            int percentProbabiltyForHalfHealing = 30;

            int percentProbabiltyOfHealing = random.Next(minPercentProbabiltyOfHealing, maxPercentProbablityOfHealing + 1);

            if (percentProbabiltyOfHealing > percentProbablityForFullHealing)
            {
                HealthPoint = maxCountOfHealthPoint;

                Console.WriteLine($"{Role} готов к бою! HP - {HealthPoint}");
            }
            else if (percentProbabiltyForHalfHealing < percentProbabiltyOfHealing && percentProbabiltyOfHealing <= percentProbablityForFullHealing)
            {
                HealthPoint += (maxCountOfHealthPoint - HealthPoint) / pieceOfHalfHealing;

                if (HealthPoint > maxCountOfHealthPoint)
                {
                    HealthPoint = maxCountOfHealthPoint;
                }

                Console.WriteLine($"Так, немного подлатали. Теперь у меня {HealthPoint} хп.");
            }
            else
            {
                Console.WriteLine($"Не удалось подлечить. Травмы несоизмеримы с жизнью. Нынешнее здоровье  - {HealthPoint}");
            }
        }
    }

    class Comander : Unit
    {
        public Comander(Random random)
        {
            int maxDamage = 36;
            int minDamage = 31;

            HealthPoint = 100;
            Armor = 35;
            Damage = random.Next(minDamage, maxDamage);

            IsAlive = true;

            Role = Role.Comander;
        }

        public override void ShowStats()
        {
            base.ShowStats();

            Console.WriteLine();
        }

        public override void TakeDamage(int damage)
        {
            int fullPercent = 100;

            if (IsAlive)
            {
                HealthPoint -= damage - (damage * Armor / fullPercent);

                if (HealthPoint <= 0)
                {
                    IsAlive = false;

                    Console.WriteLine($"{Role} погиб.");
                }
            }
        }

        public bool TryBoostMoraleSolders(List<Unit> solders)
        {
            Random random = new Random();

            int maxPercent = 100;
            int minPercent = 0;

            int percentForBoost = 75;

            int percent = random.Next(minPercent, maxPercent + 1);

            if (percent > percentForBoost)
            {
                foreach (Unit unit in solders)
                {
                    if (unit is Solder solder)
                    {
                        solder.BoostMorale();
                    }
                }

                return true;
            }
            else
            {
                foreach (Unit unit in solders)
                {
                    if (unit is Solder solder)
                    {
                        solder.NormalizeMorale();
                    }
                }

                return false;
            }
        }
    }

    class Solder : Unit
    {
        private int _lowMorale = 5;
        private int _normalMorale = 10;
        private int _highMorale = 15;

        public Solder(Random random)
        {
            HealthPoint = 100;

            int firstComplect = 1;
            int secondComplect = 2;

            int complect;

            int[] variantArmors = new int[] { 20, 25 };
            int[] variantDamages = new int[] { 20, 30 };

            complect = random.Next(firstComplect, secondComplect + 1);

            if (complect == firstComplect)
            {
                Armor = variantArmors[firstComplect - 1];
                Damage = variantDamages[firstComplect - 1];
            }
            else
            {
                Armor = variantArmors[secondComplect - 1];
                Damage = variantDamages[secondComplect - 1];
            }

            IsAlive = true;

            Morale = _normalMorale;
        }

        public int Equipment { get; protected set; }
        public int Morale { get; private set; }

        public override void TakeDamage(int damage)
        {
            int fullPercent = 100;

            if (IsAlive)
            {
                int modifierDamage = (damage - (damage * Armor / fullPercent)) * Morale / fullPercent;

                if (Morale == _lowMorale)
                {
                    HealthPoint -= damage + modifierDamage;
                }
                else
                {
                    HealthPoint -= damage - modifierDamage;
                }

                if (HealthPoint <= 0)
                {
                    IsAlive = false;

                    Console.WriteLine($"Боец {Role} погиб.");
                }
            }
        }

        public void NormalizeMorale()
        {
            Morale = _normalMorale;
        }

        public void DebuffMorale()
        {
            Morale = _lowMorale;
        }

        public void BoostMorale()
        {
            Morale = _highMorale;
        }
    }

    class SubmachineGunner : Solder
    {
        public SubmachineGunner(Random random) : base(random)
        {
            int minGrenates = 1;
            int maxGrenates = 3;

            Equipment = random.Next(minGrenates, maxGrenates + 1);

            Role = Role.SubmachineGunner;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.Write($" {Equipment} гранат");
            Console.WriteLine();
        }

        public void FlowGrenate(List<Unit> squadEnemy, Random random)
        {
            int minDamageGrenate = 65;
            int maxDamageGrenate = 300;

            int minPicesOfGrenates = 1;
            int maxPicesOfGrenates = 6;

            if (Equipment == 0)
            {
                Console.WriteLine("*Попытка бросить гранату* Гранат нет!");
            }
            else
            {
                int damageByGrenate = random.Next(minDamageGrenate, maxDamageGrenate);
                int PicesOfGrenates = random.Next(minPicesOfGrenates, maxPicesOfGrenates);

                int damageByOnePiceOfGrenate = damageByGrenate / PicesOfGrenates;

                Console.WriteLine($"*Попытка бросить гранату* Ложись! Граната на {damageByGrenate} урона!");

                for (int i = 0; i < PicesOfGrenates; i++)
                {
                    squadEnemy[random.Next(0, squadEnemy.Count)].TakeDamage(damageByOnePiceOfGrenate);
                }

                Equipment--;

                if (Equipment > 0)
                {
                    Console.WriteLine($"\nУ меня ещё {Equipment}, потом угостим.");
                }
                else
                {
                    Console.WriteLine("\nКинул последнюю гранату.");
                }

                Console.WriteLine();
            }
        }
    }

    class Medic : Solder
    {
        public Medic(Random random) : base(random)
        {
            int minKits = 1;
            int maxKits = 5;

            Equipment = random.Next(minKits, maxKits + 1);

            Role = Role.Medic;
        }

        public override void ShowStats()
        {
            base.ShowStats();
            Console.Write($" {Equipment} аптечек");
            Console.WriteLine();
        }

        public void Heal(Unit solder)
        {
            int triggerPointForHealing = 75;

            if (Equipment > 0 && solder.HealthPoint <= triggerPointForHealing && (solder.IsAlive == true))
            {
                solder.RecoveryHealthPoint();
                Equipment--;

                Console.WriteLine($"{Role}:\"У меня осталось {Equipment}  аптечек.\"");
            }
            else if (solder.IsAlive == false)
            {
                Console.WriteLine($"{solder.Role} погиб, его не вылечить.");
            }
            else
            {
                Console.WriteLine($"{solder.Role} не нуждается в медицинской помощи. HP - {solder.HealthPoint}");
            }
        }
    }

    class Squad
    {
        private List<Unit> _solders = new List<Unit>();

        public Squad(string side, Random random)
        {
            int size = 6;

            int minCountMedics = 1;
            int maxCountMedics = 2;

            int countMedics = random.Next(minCountMedics, maxCountMedics + 1);

            int countSubmachineGunners = size - countMedics;

            _solders.Add(new Comander(random));

            FillSolders(countMedics, countSubmachineGunners, random);

            Side = side;
        }

        public string Side { get; private set; }

        public void ShowCombatReady()
        {
            foreach (Unit unit in _solders)
            {
                if (unit.IsAlive)
                {
                    unit.ShowStats();
                }
            }

            Console.WriteLine();
        }

        public bool IsComanderComand()
        {
            if (IsCommandAlive())
            {
                return true;
            }
            else
            {
                foreach (Unit unit in _solders)
                {
                    if (unit is Solder solder)
                    {
                        solder.DebuffMorale();
                    }
                }

                Console.WriteLine("Отряд потерял командира! Мораль уменьшилась.");

                return false;
            }
        }

        public bool IsCapableFight()
        {
            int countForDefeatWithComander = 2;
            int countForDefeatWithoutComander = 3;

            int countAliveSolders = GiveCountAliveSolders();

            if (IsCommandAlive() && countAliveSolders > countForDefeatWithComander)
            {
                return true;
            }
            else if (countAliveSolders > countForDefeatWithoutComander)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCommandAlive()
        {
            Role comander = Role.Comander;

            TryFindUnit(comander, out Unit comanderFound);

            return comanderFound.IsAlive;
        }

        public void Attack(Squad enemy)
        {
            Random random = new Random();

            Unit attackingSolder;

            List<Unit> attackTargets;

            Role submachineGunner = Role.SubmachineGunner;

            int maxCountShoots = _solders.Count;
            int minCountShoots = 0;

            int countShoots = random.Next(minCountShoots, maxCountShoots + 1);
            int countFlowGrenates = random.Next(minCountShoots, maxCountShoots + 1);

            Console.WriteLine($"Было сделано {countShoots} выстрела:");

            for (int i = 0; i < countShoots; i++)
            {
                attackingSolder = _solders[random.Next(0, _solders.Count)];
                attackTargets = enemy.GiveRandomSolders(random);

                if (attackTargets.Count > 0)
                {
                    attackingSolder.Attack(attackTargets[random.Next(0, attackTargets.Count)]);
                }
                else
                {
                    Console.WriteLine($"{attackingSolder.Role} куда-то стрельнул...");
                }
            }

            Console.WriteLine($"\nБыло сделано {countFlowGrenates} попыток бросить гранату:");

            for (int i = 0; i < countFlowGrenates; i++)
            {
                do
                {
                    attackingSolder = _solders[random.Next(0, _solders.Count)];
                }
                while (attackingSolder.Role != submachineGunner);

                attackTargets = enemy.GiveRandomSolders(random);

                if (attackTargets.Count > 0)
                {
                    ((SubmachineGunner)attackingSolder).FlowGrenate(attackTargets, random);
                }
                else
                {
                    Console.WriteLine($"{attackingSolder.Role} хотел кинуть гранату, но нет врагов в поле видимости для гранаты.");
                }
            }

            Console.WriteLine();
        }

        public void BoostMorale()
        {
            Role comander = Role.Comander;

            if (TryFindUnit(comander, out Unit foundComander) && foundComander.IsAlive)
            {
                if (((Comander)foundComander).TryBoostMoraleSolders(_solders))
                {
                    Console.WriteLine($"Мораль бойцов {Side} повышена.\n");
                }
                else
                {
                    Console.WriteLine($"{foundComander.Role}:не удалось повысить мораль бойцов {Side}\n");
                }
            }
            else
            {
                Console.WriteLine($"Не удалось повысить мораль бойцов {Side}\n");
            }
        }

        public void ProvideMedicalAssistance()
        {
            Random random = new Random();

            List<Unit> potentialWounded; 

            List<Unit> allMedics = _solders.FindAll(solder => solder.Role == Role.Medic);

            Medic medic;

            int minCountMedics = 1;
            int maxCountMedics = allMedics.Count;

            int countMedics = random.Next(minCountMedics, maxCountMedics + 1);

            Console.WriteLine($"Был отдан приказ {countMedics} медикам.");

            for (int i = 0; i < countMedics; i++)
            {
                medic = (Medic) allMedics[random.Next(0, allMedics.Count)];

                potentialWounded = GiveRandomSolders(random);

                if (medic.IsAlive)
                {
                    Console.WriteLine($"{medic.Role} осмотрел {potentialWounded.Count} бойцов:");

                    foreach (Unit unit in potentialWounded)
                    {
                        medic.Heal(unit);
                    }
                }
                else
                {
                    Console.WriteLine($"{medic.Role} погиб и не может оказать помощь.");
                }
            }
        }

        public int GiveCountAliveSolders()
        {
            int countAlives = 0;

            foreach (Unit solder in _solders)
            {
                if (solder.IsAlive)
                {
                    countAlives++;
                }
            }

            return countAlives;
        }

        private bool TryFindUnit(Role role, out Unit foundSolder)
        {
            foundSolder = null;

            bool isSolderFound = false;

            foreach (Unit unit in _solders)
            {
                if (unit.Role == role)
                {
                    foundSolder = unit;

                    isSolderFound = true;
                }
            }

            return isSolderFound;
        }

        private List<Unit> GiveRandomSolders(Random random)
        {
            List<Unit> soldersForAction = new List<Unit>();
            List<Unit> allSolders = new List<Unit>();

            int maxCountSolders = _solders.Count;
            int minCountSolders = 0;

            int countSolders = random.Next(minCountSolders, maxCountSolders + 1);

            foreach (Unit unit in _solders)
            {
                allSolders.Add(unit);
            }

            for (int i = 0; i < countSolders; i++)
            {
                int indexSolder = random.Next(0, allSolders.Count);

                soldersForAction.Add(allSolders[indexSolder]);

                allSolders.RemoveAt(indexSolder);
            }

            return soldersForAction;
        }

        private void FillSolders(int countMedics, int countSubmachineGunners, Random random)
        {
            for (int i = 0; i < countMedics; i++)
            {
                _solders.Add(new Medic(random));
            }

            for (int i = 0; i < countSubmachineGunners; i++)
            {
                _solders.Add(new SubmachineGunner(random));
            }
        }
    }

    class Battlefield
    {
        private Squad _firstArmy;
        private Squad _secondArmy;

        public Battlefield(string firstSide, string secondSide, Random random)
        {
            _firstArmy = new Squad(firstSide, random);
            _secondArmy = new Squad(secondSide, random);
        }

        public void Fight()
        {
            Random random = new Random();

            int fistStage = 1;
            int secondStage = 2;

            int stage;

            while (_firstArmy.IsCapableFight() && _secondArmy.IsCapableFight())
            {
                ShowSquadReady(_firstArmy);
                ShowSquadReady(_secondArmy);

                Console.WriteLine("Нажмите любую клавишу, чтобы начать бой");

                Console.ReadKey();
                Console.Clear();

                stage = random.Next(fistStage, secondStage + 1);

                if (stage == fistStage)
                {
                    ComeSide(_firstArmy, _secondArmy);
                }
                else
                {
                    ComeSide(_secondArmy, _firstArmy);
                }

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить бой");

                Console.ReadKey();
                Console.Clear();
            }

            ShowWinner();
        }

        private void ShowWinner()
        {
            if (_firstArmy.IsCapableFight())
            {
                Console.WriteLine($"Отряд {_secondArmy.Side} больше не может сражаться. Оставшиеся {_secondArmy.GiveCountAliveSolders()} бойцов сдались в плен");
                Console.WriteLine($"Победа бойцов {_firstArmy.Side}");

                _firstArmy.ShowCombatReady();
            }
            else 
            {
                Console.WriteLine($"Отряд {_firstArmy.Side} больше не может сражаться. Оставшиеся {_firstArmy.GiveCountAliveSolders()} бойцов сдались в плен");
                Console.WriteLine($"Победа бойцов {_secondArmy.Side}");

                _secondArmy.ShowCombatReady();
            }
        }

        private void ComeSide(Squad commingSide, Squad enemySide)
        {
            Console.WriteLine($"Ход стороны {commingSide.Side}\n");

            commingSide.IsComanderComand();
            commingSide.Attack(enemySide);
            commingSide.BoostMorale();
            commingSide.ProvideMedicalAssistance();
        }

        private void ShowSquadReady(Squad squad)
        {
            Console.WriteLine($"В отряде стороны {squad.Side} сейчас готовы к бою {squad.GiveCountAliveSolders()} бойцов:");

            squad.ShowCombatReady();
        }
    }
}
