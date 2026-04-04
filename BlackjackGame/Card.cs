using System;

namespace BlackjackGame
{
    public enum CardValue
    {
        Joker = 1,
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5,
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 10,
        Queen = 10,
        King = 10,
        Ace = 11
    }

    public class Card
    {
        public int Value { get; set; }
        /// <summary>
        /// Type: 1=hearts, 2=diamonds, 3=spades, 4=clubs
        /// </summary>
        public int Type { get; set; }
        public string Name { get; set; }
        public string AbreviatedName { get; set; }
        public int Index { get; set; }

        public Card() { }

        public Card(int value, int type, string name, string abvName, int index)
        {
            Value = value;
            Type = type;
            Name = name;
            AbreviatedName = abvName;
            Index = index;
        }

        public Card(Card other)
        {
            Value = other.Value;
            Type = other.Type;
            Name = other.Name;
            AbreviatedName = other.AbreviatedName;
            Index = other.Index;
        }
    }

    public static class CardDeck
    {
        private const int DECK_SIZE = 52;
        private const int ALLOC_SIZE = 65;

        /// <summary>
        /// Standard 52 card deck + 2 jokers + 10 bonus cards = 64 cards total
        /// </summary>
        public static readonly Card[] DeckCards = new Card[ALLOC_SIZE]
        {
            // Aces
            new Card((int)CardValue.Ace, 1, "Ace of hearts", "AH", 0),
            new Card((int)CardValue.Ace, 2, "Ace of diamonds", "AD", 1),
            new Card((int)CardValue.Ace, 3, "Ace of spades", "AS", 2),
            new Card((int)CardValue.Ace, 4, "Ace of clubs", "AC", 3),
            // Twos
            new Card((int)CardValue.Two, 1, "2 of hearts", "2H", 4),
            new Card((int)CardValue.Two, 2, "2 of diamonds", "2D", 5),
            new Card((int)CardValue.Two, 3, "2 of spades", "2S", 6),
            new Card((int)CardValue.Two, 4, "2 of clubs", "2C", 7),
            // Threes
            new Card((int)CardValue.Three, 1, "3 of hearts", "3H", 8),
            new Card((int)CardValue.Three, 2, "3 of diamonds", "3D", 9),
            new Card((int)CardValue.Three, 3, "3 of spades", "3S", 10),
            new Card((int)CardValue.Three, 4, "3 of clubs", "3C", 11),
            // Fours
            new Card((int)CardValue.Four, 1, "4 of hearts", "4H", 12),
            new Card((int)CardValue.Four, 2, "4 of diamonds", "4D", 13),
            new Card((int)CardValue.Four, 3, "4 of spades", "4S", 14),
            new Card((int)CardValue.Four, 4, "4 of clubs", "4C", 15),
            // Fives
            new Card((int)CardValue.Five, 1, "5 of hearts", "5H", 16),
            new Card((int)CardValue.Five, 2, "5 of diamonds", "5D", 17),
            new Card((int)CardValue.Five, 3, "5 of spades", "5S", 18),
            new Card((int)CardValue.Five, 4, "5 of clubs", "5C", 19),
            // Sixes
            new Card((int)CardValue.Six, 1, "6 of hearts", "6H", 20),
            new Card((int)CardValue.Six, 2, "6 of diamonds", "6D", 21),
            new Card((int)CardValue.Six, 3, "6 of spades", "6S", 22),
            new Card((int)CardValue.Six, 4, "6 of clubs", "6C", 23),
            // Sevens
            new Card((int)CardValue.Seven, 1, "7 of hearts", "7H", 24),
            new Card((int)CardValue.Seven, 2, "7 of diamonds", "7D", 25),
            new Card((int)CardValue.Seven, 3, "7 of spades", "7S", 26),
            new Card((int)CardValue.Seven, 4, "7 of clubs", "7C", 27),
            // Eights
            new Card((int)CardValue.Eight, 1, "8 of hearts", "8H", 28),
            new Card((int)CardValue.Eight, 2, "8 of diamonds", "8D", 29),
            new Card((int)CardValue.Eight, 3, "8 of spades", "8S", 30),
            new Card((int)CardValue.Eight, 4, "8 of clubs", "8C", 31),
            // Nines
            new Card((int)CardValue.Nine, 1, "9 of hearts", "9H", 32),
            new Card((int)CardValue.Nine, 2, "9 of diamonds", "9D", 33),
            new Card((int)CardValue.Nine, 3, "9 of spades", "9S", 34),
            new Card((int)CardValue.Nine, 4, "9 of clubs", "9C", 35),
            // Tens
            new Card((int)CardValue.Ten, 1, "10 of hearts", "TH", 36),
            new Card((int)CardValue.Ten, 2, "10 of diamonds", "TD", 37),
            new Card((int)CardValue.Ten, 3, "10 of spades", "TS", 38),
            new Card((int)CardValue.Ten, 4, "10 of clubs", "TC", 39),
            // Jacks
            new Card((int)CardValue.Jack, 1, "Jack of hearts", "JH", 40),
            new Card((int)CardValue.Jack, 2, "Jack of diamonds", "JD", 41),
            new Card((int)CardValue.Jack, 3, "Jack of spades", "JS", 42),
            new Card((int)CardValue.Jack, 4, "Jack of clubs", "JC", 43),
            // Queens
            new Card((int)CardValue.Queen, 1, "Queen of hearts", "QH", 44),
            new Card((int)CardValue.Queen, 2, "Queen of diamonds", "QD", 45),
            new Card((int)CardValue.Queen, 3, "Queen of spades", "QS", 46),
            new Card((int)CardValue.Queen, 4, "Queen of clubs", "QC", 47),
            // Kings
            new Card((int)CardValue.King, 1, "King of hearts", "KH", 48),
            new Card((int)CardValue.King, 2, "King of diamonds", "KD", 49),
            new Card((int)CardValue.King, 3, "King of spades", "KS", 50),
            new Card((int)CardValue.King, 4, "King of clubs", "KC", 51),
            // Jokers
            new Card((int)CardValue.Joker, 1, "Joker Wild 1", "JO", 52),
            new Card((int)CardValue.Joker, 1, "Joker Wild 2", "JT", 53),
            // Blank card (index 54 reserved for back of deck)
            new Card(0, 0, " ", " ", 54),
            // Bonus cards
            new Card(1, 1, "Bonus Plus 1", "+1", 55),
            new Card(-1, 1, "Bonus Minus 1", "-1", 56),
            new Card(2, 2, "Bonus Plus 2", "+2", 57),
            new Card(-2, 2, "Bonus Minus 2", "-2", 58),
            new Card(3, 3, "Bonus Plus 3", "+3", 59),
            new Card(-3, 3, "Bonus Minus 3", "-3", 60),
            new Card(4, 4, "Bonus Plus 4", "+4", 61),
            new Card(-4, 4, "Bonus Minus 4", "-4", 62),
            new Card(5, 5, "Bonus Plus 5", "+5", 63),
            new Card(-5, 5, "Bonus Minus 5", "-5", 64)
        };

        /// <summary>
        /// Gets a card from the deck by its abbreviated name
        /// Returns a card with type=-1 if not found
        /// </summary>
        public static Card GetCardWithAbreviation(string abvName)
        {
            string upperName = abvName?.ToUpper() ?? "";

            foreach (var card in DeckCards)
            {
                if (card.AbreviatedName.ToUpper() == upperName)
                {
                    return new Card(card);
                }
            }

            // Return empty card if not found
            return new Card { Type = -1 };
        }
    }
}
