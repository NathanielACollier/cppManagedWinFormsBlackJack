using System;
using System.Collections.Generic;

namespace BlackjackGame
{
    public class Deck : IDisposable
    {
        private List<Card> data;
        private const int DECKL = 52;
        private const int ALLOC_SIZE = 65;

        public Deck()
        {
            data = new List<Card>(ALLOC_SIZE);
        }

        public int GetLength()
        {
            return data.Count;
        }

        public bool IsEmpty()
        {
            return data.Count <= 0;
        }

        /// <summary>
        /// Shuffle the deck - populates with random cards from master deck
        /// </summary>
        public void Shuffle()
        {
            data.Clear();
            Random random = new Random();
            bool[] used = new bool[DECKL];

            // Randomly select DECKL cards from the master deck
            for (int i = 0; i < DECKL; ++i)
            {
                int pos = random.Next(0, DECKL);

                // Find an unused position if already taken
                if (used[pos])
                {
                    while (used[pos] && pos < DECKL)
                    {
                        pos = random.Next(0, DECKL);
                    }
                }

                if (pos < DECKL)
                {
                    data.Add(new Card(CardDeck.DeckCards[pos]));
                    used[pos] = true;
                }
            }
        }

        /// <summary>
        /// Draw a card from the front of the deck
        /// </summary>
        public Card Draw()
        {
            if (data.Count <= 0)
            {
                return new Card();
            }

            Card temp = data[0];
            data.RemoveAt(0);
            return temp;
        }

        /// <summary>
        /// Add a card to the front of the deck
        /// </summary>
        public void AddCardFront(Card card)
        {
            if (data.Count >= ALLOC_SIZE - 1)
            {
                throw new Exception("Extreme Error! Duplicate cards exist in main deck");
            }

            data.Insert(0, new Card(card));
        }

        /// <summary>
        /// Add a card to the back of the deck
        /// </summary>
        public void AddCardBack(Card card)
        {
            if (data.Count >= ALLOC_SIZE - 1)
            {
                throw new Exception("Extreme Error! Duplicate cards exist in main deck");
            }

            data.Add(new Card(card));
        }

        public void Dispose()
        {
            data?.Clear();
            data = null;
        }
    }
}
