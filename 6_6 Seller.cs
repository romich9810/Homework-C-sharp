using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace _6_6_Seller
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Shop shop = new Shop();

            shop.Work();
        }

       
    }

    abstract class Person
    {
        protected List<Item> Items = new List<Item>();

        public int Money { get; protected set; }

        public virtual void ShowItems() { }
    }

    class Seller : Person
    {
        public Seller()
        {
            Money = 0;
            Items = GenerateItems();
        }

        public override void ShowItems()
        {
            int position = 0;

            Console.WriteLine("Вот что я могу предложить: \n");

            foreach (Item item in Items)
            {
                Console.Write(++position + ") ");
                item.ShowDescritpion();
            }

            Console.WriteLine();
        }

        public Item SellItem(int position, int moneyUser)
        {
            if (moneyUser == 0 | position >= Items.Count)
            {
                Console.WriteLine("Вы и сами не знаете, чего хотие, голубчик. (некорректная позиция либо не хватает денег)");
                return null;
            }
            else
            {
                Item item = Items[position];
                Items.RemoveAt(position);

                Console.WriteLine($"Пожалуйста! Вот ваш {item.Name}. С вас {item.Price}");

                Money += moneyUser;

                return item;
            }
        }

        public Item GiveItemForMakeChoise(int position)
        {
            return Items[position];
        }

        private List<Item> GenerateItems()
        {
            List<Item> foods = new List<Item>()
                {
                    new Food(15,"банан","жёлтое вытянутое создание, пахнет вкусно", 96),
                    new Food(8,"картошка", "белорусский фрукт, овощ, хлеб, завтрак, обед, торт", 77),
                    new Food(50, "cникерс", "сладкий батончик с орехами, от которого \"не тормозят\"", 392),
                    new Food(100, "арбуз", "съешь меня и пожалеешь в дороге", 50),
                };

            List<Item> tools = new List<Item>()
                {
                    new Tool(300,"лопата", "фитнес-тренажёр, а также твой друг на лето у бабушки", "копать грунт"),
                    new Tool(200, "ручка шариковая", "ручка - она и в Африке ручка. Хотя там перья, но это не точно", "записывать информацию"),
                    new Tool(1000, "топор", "и кашу сваришь, и дрова нарубишь", "рубить деревья, рубить плоть"),
                    new Tool(5000, "Перфоратор \"Bosh\"", "достал? @бошь!", "сверлить стены"),
                };

            List<Item> others = new List<Item>()
                {
                    new OtherItem(150,"Носки чёрные летние", "защити свои ножки от грязи и ночного зноя", "выглядят объёмными и прочными"),
                    new OtherItem(500,"Книга \"Радиоаппаратура своими руками\"","старая советская книга, которую он любил больше, чем жену", "страницы книги хорошо горят, есть схема приёмника на 5 стр."),
                    new OtherItem(250,"набор ниток и иголок","такие наборы выдавались в номерах при заселении","иголки достаточно острые, нитки режут бумагу"),
                    new OtherItem(1000, "бинокль", "явно от твоего дяди-моряка", "в детстве жёг им муравьев и подглядывал за Наташкой")
                };

            List<Item> randomItems = new List<Item>();

            Random random = new Random();

            int maxCountOfFood = foods.Count;
            int maxCountOfTools = tools.Count;
            int maxCountOfOthers = others.Count;

            int minCountOfItems = 0;

            randomItems.Add(foods[random.Next(minCountOfItems, maxCountOfFood)]);
            randomItems.Add(tools[random.Next(minCountOfItems, maxCountOfTools)]);
            randomItems.Add(others[random.Next(minCountOfItems, maxCountOfOthers)]);

            return randomItems;
        }
    }

    class User : Person
    {
        public int WannaPosition { get; private set; }

        public User()
        {
            int maxMoney = 1000;
            int minMoney = 0;

            Random random = new Random();
            Money = random.Next(minMoney, maxMoney);
        }

        public bool IsWantingPositionCorrect()
        {
            bool isPositionReadCorrect;

            Console.WriteLine("Я хочу предмет на позиции (введите): ");
            isPositionReadCorrect = int.TryParse(Console.ReadLine(), out int wantPosition);

            if (isPositionReadCorrect & wantPosition > 0)
            {
                WannaPosition = wantPosition - 1;
                return true;
            }
            else
            {
                Console.WriteLine("Позиция некорректна!");
                Console.ReadKey();
                Console.Clear();
                return false;
            }
        }

        public int GiveMoneyToSeller(Item item)
        {
            if (Money < item.Price)
            {
                Console.WriteLine($"У меня не хватает денег на {item.Name}");

                return 0;
            }
            else
            {
                Money -= item.Price;

                Console.WriteLine($"*Протягиваю продавцу {item.Price} за {item.Name}*");

                return item.Price;
            }
        }

        public void PutItemInBag(Item item)
        {
            Items.Add(item);
            Console.WriteLine($"{item.Name} у вас в сумке.");
        }

        public void ShowMoney()
        {
            Console.SetCursorPosition(50, 0);
            Console.WriteLine($"У вас  есть {Money} ед. денег.");
            Console.SetCursorPosition(0, 0);
        }

        public override void ShowItems()
        {
            Console.Clear();
            Console.WriteLine("Содержимое сумки: ");

            foreach (Item item in Items)
            {
                Console.WriteLine(item.Name);
            }

            Console.ReadKey();
        }
    }

    class Shop
    {
        private Seller _seller = new Seller();
        private User _user = new User();

        public void Work()
        {
            const string TradeCommandNumber = "1";
            const string ShowMyBagCommandNumber = "2";
            const string ExitCommandNumber = "3";

            string TradeCommand = "купить товар";
            string ShowMyBagCommand = "посмотреть мои купленыне предметы";
            string ExitCommand = "выход";

            string userCommand;

            bool isNeedWorkContune = true;

            while (isNeedWorkContune)
            {
                Console.WriteLine("Добро пожаловать в магазин!\n");
                Console.WriteLine($"{TradeCommandNumber} - {TradeCommand}\n{ShowMyBagCommandNumber} - {ShowMyBagCommand}\n{ExitCommandNumber} - {ExitCommand}");
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case TradeCommandNumber:
                        TradeItem();
                        break;

                    case ShowMyBagCommandNumber:
                        _user.ShowItems();
                        break;

                    case ExitCommandNumber:
                        isNeedWorkContune = false;
                        break;

                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }

                Console.Clear();
            }
        }

        private void TradeItem()
        {
            Console.Clear();
            _user.ShowMoney();
            _seller.ShowItems();

            if (_user.IsWantingPositionCorrect())
            {
                int positionForTrade = _user.WannaPosition;
                int moneyForSeller = _user.GiveMoneyToSeller(_seller.GiveItemForMakeChoise(positionForTrade));

                if (moneyForSeller == 0)
                {
                    Console.WriteLine("Cделка не удалась!");
                }
                else
                {
                    Item itemForTrade = _seller.SellItem(positionForTrade, moneyForSeller);
                    _user.PutItemInBag(itemForTrade);
                }

                Console.ReadKey();
            }
        }
    }

    abstract class Item
    {
        protected Item(int price, string name, string description)
        {
            Price = price;
            Name = name;
            Description = description;
        }

        public int Price { get; }
        public string Name { get; }
        public string Description { get; }

        public virtual void ShowDescritpion()
        {
            Console.WriteLine("Характеристики предмета: ");
            Console.Write($"Цена: {Price},\nНаименование: {Name},\nОписание: {Description}\n");
        }
    }

    class Food : Item
    {
        public Food(int price, string name, string description, int callories) : base(price, name, description)
        {
            Callories = callories;
        }

        public int Callories { get; }

        public override void ShowDescritpion()
        {
            base.ShowDescritpion();
            Console.WriteLine($"Каллории: {Callories}");
            Console.WriteLine();
        }
    }

    class Tool : Item
    {
        public Tool(int price, string name, string description, string purpose) : base(price, name, description)
        {
            Purpose = purpose;
        }

        public string Purpose { get; }

        public override void ShowDescritpion()
        {
            base.ShowDescritpion();
            Console.WriteLine($"Предназначение: {Purpose}");
            Console.WriteLine();
        }
    }

    class OtherItem : Item
    {
        public OtherItem(int price, string name, string description, string possiblyPurpose) : base(price, name, description)
        {
            PossiblyPurpose = possiblyPurpose;
        }

        public string PossiblyPurpose { get; }

        public override void ShowDescritpion()
        {
            base.ShowDescritpion();
            Console.WriteLine($"Возможное предназначение: {PossiblyPurpose}");
            Console.WriteLine();
        }
    }
}
