using System;
using System.Collections.Generic;
using System.Linq;

namespace _7_4_PlayersTop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Player> allPlayers = new List<Player>()
            {
                new Player("Вова228", 15, 10),
                new Player("ВитяКрутоI2008", 25, 2),
                new Player("VladIsLove2021", 45, 15),
                new Player("VityaAKA", 50, 9),
                new Player("Петя_Мамин_Симпатяга09", 10, 8),
                new Player("FreshMeet01", 91, 6),
                new Player("HardStype67", 13, 19),
            };

            int sizeOfTop = 3;

            Console.WriteLine("Все игроки: \n");

            ShowPlayers(allPlayers);

            var playersTopPower = allPlayers.OrderByDescending(player => player.Power).Take(sizeOfTop).ToList();

            Console.WriteLine($"\nТоп {sizeOfTop} игрока по силе:\n");

            ShowPlayers(playersTopPower);

            var playerTopLevel = allPlayers.OrderByDescending(player => player.Level).Take(sizeOfTop).ToList();

            Console.WriteLine($"\nТоп {sizeOfTop} игроков по уровню:\n");

            ShowPlayers(playerTopLevel);

            void ShowPlayers(List<Player> players)
            {
                foreach (Player player in players)
                {
                    Console.WriteLine($"{player.Name}, {player.Power} силы, {player.Level} уровень");
                }
            }
        }
    }

    class Player
    {
        public Player(string name, int power, int level)
        {
            Name = name;
            Power = power;
            Level = level;
        }

        public string Name { get; private set; }
        public int Power { get; private set; }
        public int Level { get; private set; }
    }
}
