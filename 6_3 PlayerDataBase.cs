using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _6_3_PlayerDataBase
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string AddPlayerCommandNumber = "1";
            const string BanPlayerCommandNumber = "2";
            const string UnbanPlayerCommandNumber = "3";
            const string DeletePlayerCommandNumber = "4";
            const string ShowDatabaseCommandNumber = "5";
            const string ExitCommandNumber = "6";

            string AddPlayerCommand = "добавить игрока в базу данных"; 
            string BanPlayerCommand = "забанить игрока по его номеру";
            string UnbanPlayerCommand = "разбанить игрока по номеру";
            string DeletePlayerCommand = "удалить игрока";
            string ShowDatabaseCommand = "показать всех игроков в базе";
            string ExitCommand = "выйти из программы";

            bool isNeedWork = true;
            string userCommand;

            Database database = new Database();

            while (isNeedWork)
            {
                Console.WriteLine("Добро пожаловать в базу данных игроков!\n");
                Console.WriteLine($"{AddPlayerCommandNumber} - {AddPlayerCommand}" +
                    $"\n{BanPlayerCommandNumber} - {BanPlayerCommand}\n" +
                    $"{UnbanPlayerCommandNumber} - {UnbanPlayerCommand}" +
                    $"\n{DeletePlayerCommandNumber} - {DeletePlayerCommand}" +
                    $"\n{ShowDatabaseCommandNumber} - {ShowDatabaseCommand}" +
                    $"\n{ExitCommandNumber} - {ExitCommand}");

                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case AddPlayerCommandNumber:
                        database.AddPlayer();
                        break;

                    case BanPlayerCommandNumber:
                        database.BanPlayer();
                        break;

                    case UnbanPlayerCommandNumber:
                        database.UnbanPLayer();
                        break;

                    case DeletePlayerCommandNumber:
                        database.DeletePlayer();
                        break;

                    case ShowDatabaseCommandNumber:
                        database.ShowDataBase();
                        break;

                    case ExitCommandNumber:
                        isNeedWork = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда.");
                        Console.ReadKey();
                        break;
                }

                Console.Clear();
            }
        }

        class Player
        {
            public Player(int number, int level, string nick = "SomePlayer", bool isBanned = false )
            {
                int minimalCorrectLevel = 1;

                Number = number;
                Nick = nick;

                if (level < minimalCorrectLevel)
                {
                    Level = minimalCorrectLevel;
                }
                else
                {
                    Level = level;
                }
                
                IsBanned = isBanned;
            }

            public int Number { get; private set; }
            public string Nick { get; private set; }
            public int Level { get; private set; }
            public bool IsBanned { get; private set; }

            public void GetBan()
            {
                IsBanned = true;
            }

            public void GetUnban()
            {
                IsBanned = false;
            }
        }

        class Database
        {
            private List<Player> _playerList = new List<Player>();

            private int scoreOfPlayers = 0;

            public void AddPlayer()
            {
                int number;
                string nick;

                bool isLevelCorrect;

                Console.WriteLine("Введите ник игрока: ");
                nick = Console.ReadLine();

                number = ++scoreOfPlayers;

                Console.WriteLine("Введите уровень игрока:");
                isLevelCorrect = int.TryParse(Console.ReadLine(), out int level);

                if (isLevelCorrect)
                {
                    _playerList.Add(new Player(number, level, nick));

                    Console.WriteLine("Игрок добавлен.");
                }
                else
                {
                    Console.WriteLine("Неверный формат уровня.");
                }

                Console.ReadKey();
                Console.Clear();
            }

            public void UnbanPLayer()
            {
                if (TryGetPlayer(out Player player))
                {
                    player.GetUnban();

                    Console.WriteLine($"Игрок под номером {player.Number} разбанен.");
                    Console.ReadKey();
                }
            }

            public void BanPlayer()
            {
                if (TryGetPlayer(out Player player))
                {
                    player.GetBan();

                    Console.WriteLine($"Игрок под номером {player.Number} забанен.");
                    Console.ReadKey();
                }
            }

            public void DeletePlayer()
            {
                if (TryGetPlayer(out Player player))
                {
                    _playerList.Remove(player);

                    Console.WriteLine($"Игрок под номером {player.Number} удалён.");
                    Console.ReadKey();
                }
            }
            public void ShowDataBase()
            {
                foreach (Player player in _playerList)
                {
                    Console.Write("Уникальынй номер: " + player.Number + " Ник: " + player.Nick + " Уровень: " + player.Level);

                    if (player.IsBanned)
                    {
                        Console.WriteLine(" статус: забанен");
                    }
                    else
                    {
                        Console.WriteLine(" статус: разбанен");
                    }
                }

                Console.ReadKey();
                Console.Clear();
            }

            private bool TryGetPlayer(out Player player)
            {
                bool isNumberForFindPlayerCorrectly;
                bool isPlayerfounded = false;

                player = null;

                Console.WriteLine("Введите уникальный номер игрока: ");
                isNumberForFindPlayerCorrectly = int.TryParse(Console.ReadLine(), out int numberForFindPlayer);

                if (isNumberForFindPlayerCorrectly)
                {
                    for (int i = 0; i < _playerList.Count; i++)
                    {
                        if (numberForFindPlayer == _playerList[i].Number)
                        {
                            player = _playerList[i];
                            isPlayerfounded = true;
                        }
                    }

                    return isPlayerfounded;
                }
                else
                {
                    Console.WriteLine("Неверный номер!");
                    return isNumberForFindPlayerCorrectly;
                }
            }
        }
    }
}
