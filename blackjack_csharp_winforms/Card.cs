using System;

namespace blackjack
{
    public struct Card
    {
        public int Value { get; set; }
        public int Type { get; set; }
        public string Name { get; set; }
        public string AbvName { get; set; }
        public int Index { get; set; }
    }

    public static class CardValue
    {
        public const int V_JOKER = 1;
        public const int V_TWO = 2;
        public const int V_THREE = 3;
        public const int V_FOUR = 4;
        public const int V_FIVE = 5;
        public const int V_SIX = 6;
        public const int V_SEVEN = 7;
        public const int V_EIGHT = 8;
        public const int V_NINE = 9;
        public const int V_TEN = 10;
        public const int V_JACK = 10;
        public const int V_QUEEN = 10;
        public const int V_KING = 10;
        public const int V_ACE = 11;
    }

    public static class CardDeck
    {
        public const int DECKL = 52;

        public static Card[] DeckCards = new Card[]
        {
            new Card { Value = CardValue.V_ACE, Type = 1, Name = "Ace of hearts", AbvName = "AH", Index = 0 },
            new Card { Value = CardValue.V_ACE, Type = 2, Name = "Ace of diamonds", AbvName = "AD", Index = 1 },
            new Card { Value = CardValue.V_ACE, Type = 3, Name = "Ace of spades", AbvName = "AS", Index = 2 },
            new Card { Value = CardValue.V_ACE, Type = 4, Name = "Ace of clubs", AbvName = "AC", Index = 3 },
            new Card { Value = CardValue.V_TWO, Type = 1, Name = "2 of hearts", AbvName = "2H", Index = 4 },
            new Card { Value = CardValue.V_TWO, Type = 2, Name = "2 of diamonds", AbvName = "2D", Index = 5 },
            new Card { Value = CardValue.V_TWO, Type = 3, Name = "2 of spades", AbvName = "2S", Index = 6 },
            new Card { Value = CardValue.V_TWO, Type = 4, Name = "2 of clubs", AbvName = "2C", Index = 7 },
            new Card { Value = CardValue.V_THREE, Type = 1, Name = "3 of hearts", AbvName = "3H", Index = 8 },
            new Card { Value = CardValue.V_THREE, Type = 2, Name = "3 of diamonds", AbvName = "3D", Index = 9 },
            new Card { Value = CardValue.V_THREE, Type = 3, Name = "3 of spades", AbvName = "3S", Index = 10 },
            new Card { Value = CardValue.V_THREE, Type = 4, Name = "3 of clubs", AbvName = "3C", Index = 11 },
            new Card { Value = CardValue.V_FOUR, Type = 1, Name = "4 of hearts", AbvName = "4H", Index = 12 },
            new Card { Value = CardValue.V_FOUR, Type = 2, Name = "4 of diamonds", AbvName = "4D", Index = 13 },
            new Card { Value = CardValue.V_FOUR, Type = 3, Name = "4 of spades", AbvName = "4S", Index = 14 },
            new Card { Value = CardValue.V_FOUR, Type = 4, Name = "4 of clubs", AbvName = "4C", Index = 15 },
            new Card { Value = CardValue.V_FIVE, Type = 1, Name = "5 of hearts", AbvName = "5H", Index = 16 },
            new Card { Value = CardValue.V_FIVE, Type = 2, Name = "5 of diamonds", AbvName = "5D", Index = 17 },
            new Card { Value = CardValue.V_FIVE, Type = 3, Name = "5 of spades", AbvName = "5S", Index = 18 },
            new Card { Value = CardValue.V_FIVE, Type = 4, Name = "5 of clubs", AbvName = "5C", Index = 19 },
            new Card { Value = CardValue.V_SIX, Type = 1, Name = "6 of hearts", AbvName = "6H", Index = 20 },
            new Card { Value = CardValue.V_SIX, Type = 2, Name = "6 of diamonds", AbvName = "6D", Index = 21 },
            new Card { Value = CardValue.V_SIX, Type = 3, Name = "6 of spades", AbvName = "6S", Index = 22 },
            new Card { Value = CardValue.V_SIX, Type = 4, Name = "6 of clubs", AbvName = "6C", Index = 23 },
            new Card { Value = CardValue.V_SEVEN, Type = 1, Name = "7 of hearts", AbvName = "7H", Index = 24 },
            new Card { Value = CardValue.V_SEVEN, Type = 2, Name = "7 of diamonds", AbvName = "7D", Index = 25 },
            new Card { Value = CardValue.V_SEVEN, Type = 3, Name = "7 of spades", AbvName = "7S", Index = 26 },
            new Card { Value = CardValue.V_SEVEN, Type = 4, Name = "7 of clubs", AbvName = "7C", Index = 27 },
            new Card { Value = CardValue.V_EIGHT, Type = 1, Name = "8 of hearts", AbvName = "8H", Index = 28 },
            new Card { Value = CardValue.V_EIGHT, Type = 2, Name = "8 of diamonds", AbvName = "8D", Index = 29 },
            new Card { Value = CardValue.V_EIGHT, Type = 3, Name = "8 of spades", AbvName = "8S", Index = 30 },
            new Card { Value = CardValue.V_EIGHT, Type = 4, Name = "8 of clubs", AbvName = "8C", Index = 31 },
            new Card { Value = CardValue.V_NINE, Type = 1, Name = "9 of hearts", AbvName = "9H", Index = 32 },
            new Card { Value = CardValue.V_NINE, Type = 2, Name = "9 of diamonds", AbvName = "9D", Index = 33 },
            new Card { Value = CardValue.V_NINE, Type = 3, Name = "9 of spades", AbvName = "9S", Index = 34 },
            new Card { Value = CardValue.V_NINE, Type = 4, Name = "9 of clubs", AbvName = "9C", Index = 35 },
            new Card { Value = CardValue.V_TEN, Type = 1, Name = "10 of hearts", AbvName = "TH", Index = 36 },
            new Card { Value = CardValue.V_TEN, Type = 2, Name = "10 of diamonds", AbvName = "TD", Index = 37 },
            new Card { Value = CardValue.V_TEN, Type = 3, Name = "10 of spades", AbvName = "TS", Index = 38 },
            new Card { Value = CardValue.V_TEN, Type = 4, Name = "10 of clubs", AbvName = "TC", Index = 39 },
            new Card { Value = CardValue.V_JACK, Type = 1, Name = "Jack of hearts", AbvName = "JH", Index = 40 },
            new Card { Value = CardValue.V_JACK, Type = 2, Name = "Jack of diamonds", AbvName = "JD", Index = 41 },
            new Card { Value = CardValue.V_JACK, Type = 3, Name = "Jack of spades", AbvName = "JS", Index = 42 },
            new Card { Value = CardValue.V_JACK, Type = 4, Name = "Jack of clubs", AbvName = "JC", Index = 43 },
            new Card { Value = CardValue.V_QUEEN, Type = 1, Name = "Queen of hearts", AbvName = "QH", Index = 44 },
            new Card { Value = CardValue.V_QUEEN, Type = 2, Name = "Queen of diamonds", AbvName = "QD", Index = 45 },
            new Card { Value = CardValue.V_QUEEN, Type = 3, Name = "Queen of spades", AbvName = "QS", Index = 46 },
            new Card { Value = CardValue.V_QUEEN, Type = 4, Name = "Queen of clubs", AbvName = "QC", Index = 47 },
            new Card { Value = CardValue.V_KING, Type = 1, Name = "King of hearts", AbvName = "KH", Index = 48 },
            new Card { Value = CardValue.V_KING, Type = 2, Name = "King of diamonds", AbvName = "KD", Index = 49 },
            new Card { Value = CardValue.V_KING, Type = 3, Name = "King of spades", AbvName = "KS", Index = 50 },
            new Card { Value = CardValue.V_KING, Type = 4, Name = "King of clubs", AbvName = "KC", Index = 51 },
            new Card { Value = CardValue.V_JOKER, Type = 1, Name = "Joker Wild 1", AbvName = "JO", Index = 52 },
            new Card { Value = CardValue.V_JOKER, Type = 1, Name = "Joker Wild 2", AbvName = "JT", Index = 53 },
            new Card { Value = 1, Type = 1, Name = "Bonus Plus 1", AbvName = "+1", Index = 55 },
            new Card { Value = -1, Type = 1, Name = "Bonus Minus 1", AbvName = "-1", Index = 56 },
            new Card { Value = 2, Type = 2, Name = "Bonus Plus 2", AbvName = "+2", Index = 57 },
            new Card { Value = -2, Type = 2, Name = "Bonus Minus 2", AbvName = "-2", Index = 58 },
            new Card { Value = 3, Type = 3, Name = "Bonus Plus 3", AbvName = "+3", Index = 59 },
            new Card { Value = -3, Type = 3, Name = "Bonus Minus 3", AbvName = "-3", Index = 60 },
            new Card { Value = 4, Type = 4, Name = "Bonus Plus 4", AbvName = "+4", Index = 61 },
            new Card { Value = -4, Type = 4, Name = "Bonus Minus 4", AbvName = "-4", Index = 62 },
            new Card { Value = 5, Type = 5, Name = "Bonus Plus 5", AbvName = "+5", Index = 63 },
            new Card { Value = -5, Type = 5, Name = "Bonus Minus 5", AbvName = "-5", Index = 64 }
        };
    }
}
