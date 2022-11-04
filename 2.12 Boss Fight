using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2_12_BossFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string NameSpellGeneral = "\"Луч света\"";
            const string NameSpellCombo = "\"Пространственную кару\"";
            const string NameSpellDefense = "\"Жизнесосальщик\"";
            const string NameSpellUltraDamage = "\"Выход Смерти\"";
            const string CommandSpellGeneral = "1";
            const string CommandSpellCombo = "2";
            const string CommandSpellDefense = "3";
            const string CommandSpellUltraDamage = "4";
            const string CommandInfoOfSpells = "5";

            Random random = new Random();
            string nameUser;
            string userComand;
            string nameBoss = "Диадункис";
            int maxHealthPointsBoss = 4000;
            int minHealthPointsBoss = 2000;
            int maxHealthPointsPlayer = 1000;
            int minHealthPointsPlayer = 200;

            int playerHealth = random.Next(minHealthPointsPlayer, maxHealthPointsPlayer + 1);
            int bossHealth = random.Next(minHealthPointsBoss, maxHealthPointsBoss + 1);
            int previousBossHealth = bossHealth;
            int beginningHealthPlayer = playerHealth;

            int maxPersentDamageBoss = 66;
            int minPersentDamageBoss = 20;
            int damageBoss = playerHealth - (playerHealth * random.Next(minPersentDamageBoss, maxPersentDamageBoss + 1)) % 100;
            int defaultPlayerDamage = 150;
            int realPlayerDamage;

            int maxPersentSpellGeneral = 15;
            int minPersentSpellGeneral = 1;

            int persentOfDamageForCombo = 10;
            int lessThemPersentOfDamageCustCombo = 3;
            int moreThemPersentOfDamageCustCombo = 5;

            int minHealingSpellDefense;
            int maxHealingSpellDefense;
            int sumOfHealingSpellDefense;
            int dividerOfMinHealingSpellDefense = 2;

            int sumOfMoveBeforeUsingUltraDamage;
            int countForUsingUltraDamage = 4;
            int coefficienIncreaseUltraDamage = 10;

            int scoreOfMove = 0;

            bool isSpellGeneralUsed = false;

            Console.WriteLine("Добро пожаловать в дандж! Пожалйста, представьтесь: ");
            nameUser = Console.ReadLine();

            Console.WriteLine($"Привет, {nameUser}, мы тебя ждали! \nВот уже 100 лет как наша деревня из поколения в поколение" +
                $" борется с ужасным {nameBoss}. Настала пора нам одержать верх!.");

            while ((bossHealth > 0) || (playerHealth > 0))
            {
                Console.WriteLine($"ХП босса = {bossHealth}, ваши хп = {playerHealth}.\nПеред вами босс! Выберите заклинание:\n" +
                    $"{CommandSpellGeneral} - использовать {NameSpellGeneral}.\n{CommandSpellCombo} - использовать {NameSpellCombo}\n" +
                    $"{CommandSpellDefense} - использовать {NameSpellDefense}\n{CommandSpellUltraDamage} - использовать {NameSpellUltraDamage} \n" +
                    $"{CommandInfoOfSpells} - узнать подробности про заклинания.");
                userComand = Console.ReadLine();

                switch (userComand)
                {
                    case CommandSpellGeneral:
                        Console.WriteLine("Испытай излучение!!!");
                        realPlayerDamage = defaultPlayerDamage + (defaultPlayerDamage * random.Next(minPersentSpellGeneral, maxPersentSpellGeneral + 1) % 100);
                        previousBossHealth = bossHealth;
                        bossHealth -= realPlayerDamage;
                        playerHealth -= damageBoss;
                        isSpellGeneralUsed = true;
                        scoreOfMove++;
                        break;
                    case CommandSpellCombo:
                        
                        if (isSpellGeneralUsed == false)
                        {
                            Console.WriteLine($"Вы не использовали {NameSpellGeneral}!");
                        }

                        else
                        {
                            Console.WriteLine("Да познай мощь пространства!!!");

                            if ((previousBossHealth - bossHealth) > (previousBossHealth / persentOfDamageForCombo)) 
                            {
                                realPlayerDamage = defaultPlayerDamage * moreThemPersentOfDamageCustCombo;
                                bossHealth -= realPlayerDamage;
                                playerHealth -= damageBoss;
                            }

                            else
                            {
                                realPlayerDamage = defaultPlayerDamage * lessThemPersentOfDamageCustCombo; 
                                bossHealth -= realPlayerDamage;
                                playerHealth -= damageBoss;
                            }
                            
                            isSpellGeneralUsed = false;
                            scoreOfMove++;
                        }
                        break;
                    case CommandSpellDefense:
                        Console.WriteLine("Всасываю твою жизнь, защищаю свою...");
                        minHealingSpellDefense = (beginningHealthPlayer - playerHealth) / dividerOfMinHealingSpellDefense;
                        maxHealingSpellDefense = beginningHealthPlayer - playerHealth;
                        sumOfHealingSpellDefense = random.Next(minHealingSpellDefense, maxHealingSpellDefense + 1);
                        playerHealth += sumOfHealingSpellDefense;
                        bossHealth -= sumOfHealingSpellDefense;
                        scoreOfMove++;
                        break;
                    case CommandSpellUltraDamage:

                        if ((scoreOfMove % countForUsingUltraDamage == 0) && (scoreOfMove != 0)) 
                        {
                            Console.WriteLine("УЗРИ КОДЗИМОВСКУЮ СИЛУ!");
                            realPlayerDamage = defaultPlayerDamage * coefficienIncreaseUltraDamage; 
                            bossHealth -= realPlayerDamage;
                            playerHealth -= damageBoss;
                            scoreOfMove++;
                        }

                        else
                        {
                            sumOfMoveBeforeUsingUltraDamage = countForUsingUltraDamage - (scoreOfMove % countForUsingUltraDamage);
                            Console.WriteLine($"Вы пока что не можете использовать {NameSpellUltraDamage} ещё {sumOfMoveBeforeUsingUltraDamage} ходов!");
                        }
                        break;
                    case CommandInfoOfSpells:
                        Console.Clear();
                        Console.WriteLine($"{NameSpellGeneral} - наносит урон по врагу в размере от полного стандартного урона до увеличенного на {maxPersentSpellGeneral}% от " +
                            $" стандартного урона в {defaultPlayerDamage} единиц.");
                        Console.WriteLine($"{NameSpellCombo} - Можно использовать только после {NameSpellGeneral}!" +
                            $" Если на предыдущем ходе было нанесено урона боссу больше {persentOfDamageForCombo}% от его хп - кастует х{moreThemPersentOfDamageCustCombo} от вашего стандартного" +
                            $" урона в {defaultPlayerDamage} единиц. \nЕсли было нанесено меньше {persentOfDamageForCombo}% - х{lessThemPersentOfDamageCustCombo}.");
                        Console.WriteLine($"{NameSpellDefense} - Добавляет к вашему здоровью рандомное количество" +
                            $" здоровья. Количество здоровья не может быть больше, чем было изначально, в {beginningHealthPlayer} ед.");
                        Console.WriteLine($"{NameSpellUltraDamage} - Ультиматив. Наносит х{coefficienIncreaseUltraDamage} от вашего стандартного" +
                            $" урона. Может быть применен только каждый {countForUsingUltraDamage}-й ход.");
                        break;
                    default:
                        Console.WriteLine("Неправильная команда! Попробуйте ещё раз.");
                        break;
                }
            }

            if ((bossHealth <= 0) && (playerHealth > 0))
            {
                bossHealth = 0;
                Console.WriteLine($"Ваши HP = {playerHealth}, HP босса = {bossHealth}.\nПоздравляем! Великий {nameBoss} повержен!");
            }

            else if ((bossHealth > 0) && (playerHealth <= 0))
            {
                playerHealth = 0;
                Console.WriteLine($"Ваши HP = {playerHealth}, HP босса = {bossHealth}.\nВам не удалось сразить {nameBoss}. Игра окончена!");
            }

            else if ((bossHealth <= 0) && (playerHealth <= 0))
            {
                bossHealth = 0;
                playerHealth = 0;
                Console.WriteLine($"Ваши HP = {playerHealth}, HP босса = {bossHealth}.\nПоследнее заклинание для {nameBoss} было смертельным, однако ему удалось вас парализовать своим ядом" +
                    $" с его когтей. Вы чувствуете, как задыхаетесь... Что ж, вы погибли не напрасно! Теперь деревня сможет жить в гармонии долгие годы.");
            }
        }
    }
}
