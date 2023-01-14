using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace _6_4_CardsSteck
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandToTakeCardsNumber = "1";
            const string CommandToShowAllCardsNumber = "2";
            const string CommandToFinishNumber = "3";

            string CommandToTakeCards = "взять карты";
            string CommandToFinish = "завершить игру";
            string CommandToShowAllCards = "посмотреть все карты в руке";

            GameTable table = new GameTable();

            bool isNeedPlayContune = true;
            string commandFromUser;

            while (isNeedPlayContune)
            {
                Console.WriteLine($"Перед вами колода карт." +
                    $"\n{CommandToTakeCardsNumber} - {CommandToTakeCards}" +
                    $"\n{CommandToShowAllCardsNumber} - {CommandToShowAllCards}" +
                    $"\n{CommandToFinishNumber} - {CommandToFinish}");

                commandFromUser = Console.ReadLine();

                switch (commandFromUser)
                {
                    case CommandToTakeCardsNumber:
                        table.TakeCards();
                        break;

                    case CommandToShowAllCardsNumber:
                        table.ShowAllPlayerCards();
                        break;

                    case CommandToFinishNumber:
                        isNeedPlayContune = false;
                        break;

                    default:
                        Console.WriteLine("Неверное значение команды!");
                        break;
                }

                Console.ReadKey();
                Console.Clear();
            }
        }

        private enum Suit { Hearts , Spades, Diamonds, Clubs };
        private enum ValueCard { Two , Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace };

        class Card
        {
            private Suit _suit;
            private ValueCard _valueCard;
            public Card(Suit suit, ValueCard valueCard)
            {
                _suit = suit;
                _valueCard = valueCard;
            }

            public Suit Suit { get { return _suit; } }
            public ValueCard ValueCard { get { return _valueCard; } }

            public void ShowCard()
            {
                Console.WriteLine($"{Suit}, {ValueCard}");
            }
        }

        class Player
        {
            private List<Card> _playerCard = new List<Card>(); //сделать метод для работы с приватным листом, свойство убратть

            public void TakeCard(Card card)
            {
                _playerCard.Add(card);
            }

            public void ShowAllCardOfPlayer()
            {
                Console.WriteLine("У игрока на руках следующие карты: ");
                
                foreach(Card card in _playerCard)
                {
                    card.ShowCard();
                }

                Console.WriteLine();
            }
        }

        class Pack
        {
            private Stack<Card> _cardPack;

            public Pack()
            {
                _cardPack = Shuffle(GiveAllCards());
            }

            private List<Card> GiveAllCards()
            {
                int scoreOfSuit = 4;
                int scoreOfValueCard = 13;

                List<Card> cardsPack = new List<Card>();

                for (int i = 0; i < scoreOfSuit; i++)
                {
                    for (int j = 0; j < scoreOfValueCard; j++)
                    {
                        cardsPack.Add(new Card((Suit)i, (ValueCard)j));
                    }
                }

                return cardsPack;
            }

            private Stack<Card> Shuffle(List<Card> cardsPack)
            {
                Stack<Card> cards = new Stack<Card>();
                Random random = new Random();

                bool isPackEmpty = false;

                Card removingCard;               

                while (isPackEmpty == false)
                {
                    int indexOfCard = random.Next(0, cardsPack.Count); 
                    removingCard = cardsPack[indexOfCard];//Возможны проблемы с ссылками
                    cards.Push(removingCard);
                    cardsPack.RemoveAt(indexOfCard);

                    if (cardsPack.Count == 0)
                    {
                        isPackEmpty = true;
                    }
                }

                return cards;
            }

            public Card GiveCard()
            {
                if (_cardPack.Count == 0)
                {
                    Console.WriteLine("Колода пуста!");
                    return null;
                }
                else
                {
                    return _cardPack.Pop();
                }
            }
            
            public int GiveCountOfCards()
            {
                return _cardPack.Count;
            }
        }

        class GameTable
        {
            private Player _player = new Player();
            private Pack _cardsPack = new Pack();

            public void TakeCards()
            {
                bool isValueOfCardsCorrect;

                Console.WriteLine("Перед вами лежит колода карт. Сколько вы хотие взять карт на руку?");
                isValueOfCardsCorrect = int.TryParse(Console.ReadLine(), out int valueOfCards);

                if (isValueOfCardsCorrect & (_cardsPack.GiveCountOfCards() >= valueOfCards))
                {
                    for (int i = 0; i < valueOfCards; i++)
                    {
                        Card cardForPlayer = _cardsPack.GiveCard();
                        _player.TakeCard(cardForPlayer);
                    }
                }
                else 
                {
                    Console.WriteLine($"Неверное значение количества карт! Вы хотите взять {valueOfCards} шт, а в колоде осталось {_cardsPack.GiveCountOfCards()} шт."); 
                }
            }

            public void ShowAllPlayerCards()
            {
                _player.ShowAllCardOfPlayer();
            }
        }
    }
}
