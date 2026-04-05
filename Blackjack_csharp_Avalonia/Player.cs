using System;
using System.Collections.Generic;
using System.Text;

namespace BlackjackGame
{
    public class Player
    {
        private int gamesWon;
        private int drawCount;
        private bool stay;
        private bool isComputer;
        private int totalScore;
        private int numCards;
        private List<Card> hand;
        private int cardCount;
        private string cardlist;
        private const int MAX_HAND_SIZE = 21;

        public Player(bool iscomputer)
        {
            isComputer = iscomputer;
            cardCount = 0;
            gamesWon = 0;
            drawCount = 0;
            numCards = 0;
            totalScore = 0;
            stay = false;
            hand = new List<Card>(MAX_HAND_SIZE);
            cardlist = "";
        }

        /// <summary>
        /// Deal N cards to the player
        /// </summary>
        public void Deal(Card[] deck, int numberOfCards)
        {
            for (int i = 0; i < numberOfCards; ++i)
            {
                Hit(deck);
            }
        }

        /// <summary>
        /// Draw a card from the deck and add to hand
        /// </summary>
        private void Hit(Card[] deck)
        {
            if (numCards >= 12)
            {
                stay = true;
                return;
            }

            if (deck.Length > 0)
            {
                hand.Add(new Card(deck[new Random().Next(0, deck.Length)]));
                cardlist += hand[numCards].AbreviatedName + " ";
                ++numCards;
                CountCards();

                // If score exceeds 21, convert Aces to Jokers
                if (cardCount > 21)
                {
                    int acesConverted = 0;

                    for (int i = 0; i < numCards; ++i)
                    {
                        if (hand[i].AbreviatedName.Length > 0 && hand[i].AbreviatedName[0] == 'A')
                        {
                            hand[i] = new Card(CardDeck.DeckCards[53]); // Joker card (value 1)
                            ++acesConverted;
                        }
                    }

                    if (!isComputer && acesConverted > 0)
                    {
                        cardlist = "Aces have become Jokers\n";
                        for (int i = 0; i < numCards; ++i)
                        {
                            cardlist += hand[i].AbreviatedName + " ";
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Count the value of all cards in hand
        /// </summary>
        public int CountCards()
        {
            cardCount = 0;
            for (int i = 0; i < numCards; ++i)
            {
                cardCount += hand[i].Value;
            }
            return cardCount;
        }

        /// <summary>
        /// Get string representation of cards in hand
        /// </summary>
        public string GetCardlist()
        {
            return cardlist;
        }

        /// <summary>
        /// Set total score (used for high score tracking)
        /// </summary>
        public void SetTotalScore(int newScore)
        {
            totalScore = newScore;
        }

        /// <summary>
        /// Get total score
        /// </summary>
        public int GetTotalScore()
        {
            return totalScore;
        }

        /// <summary>
        /// Get draw count
        /// </summary>
        public int GetDrawCount()
        {
            return drawCount;
        }

        /// <summary>
        /// Set draw count
        /// </summary>
        public void SetDrawCount(int x)
        {
            drawCount = x;
        }

        /// <summary>
        /// Computer AI decision: should the player hit or stay?
        /// Returns false = hit, true = stay
        /// </summary>
        public bool Turn()
        {
            if (stay)
            {
                return true;
            }

            Random random = new Random();

            if (cardCount <= 15)
            {
                stay = false;
                return stay;
            }
            else if (cardCount < 19)
            {
                int chance = random.Next(4, 6); // 4 or 5
                stay = (chance == 5); // 50/50 chance
                return stay;
            }
            else
            {
                int chance = random.Next(1, 101); // 1-100
                stay = (chance != 50); // 1% chance to hit (when == 50)
                return stay;
            }
        }

        /// <summary>
        /// Set stay status
        /// </summary>
        public void SetStay(bool x)
        {
            stay = x;
        }

        /// <summary>
        /// Start a new game for this player
        /// </summary>
        public void NewGame()
        {
            cardCount = 0;
            numCards = 0;
            stay = false;
            hand.Clear();
            cardlist = "";
        }

        /// <summary>
        /// Get games won by this player
        /// </summary>
        public int GetGamesWon()
        {
            return gamesWon;
        }

        /// <summary>
        /// Set games won
        /// </summary>
        public void SetGamesWon(int x)
        {
            gamesWon = x;
        }

        /// <summary>
        /// Get the most recently drawn card
        /// </summary>
        public Card GetCurrentCard()
        {
            if (numCards > 0)
            {
                return hand[numCards - 1];
            }
            return new Card();
        }

        /// <summary>
        /// Check if player should stay (score >= 21)
        /// </summary>
        public bool CheckStay()
        {
            if (cardCount >= 21)
            {
                stay = true;
                return true;
            }
            return stay;
        }

        /// <summary>
        /// Get number of cards in hand
        /// </summary>
        public int GetNumCards()
        {
            return numCards;
        }

        /// <summary>
        /// Get the card at index in hand
        /// </summary>
        public Card GetCard(int index)
        {
            if (index >= 0 && index < numCards)
            {
                return hand[index];
            }
            return new Card();
        }

        /// <summary>
        /// Get the image index of the card at position
        /// </summary>
        public int GetCardIndex(int x)
        {
            if (x >= 0 && x < numCards)
            {
                return hand[x].Index;
            }
            return 0;
        }

        /// <summary>
        /// Undo the last action (used in game UI)
        /// </summary>
        public void Undo()
        {
            if (numCards <= 0)
            {
                numCards = 0;
                return;
            }

            if (totalScore < 10)
            {
                return;
            }

            totalScore -= 10;
            numCards--;

            if (numCards > 0)
            {
                hand.RemoveAt(numCards);
            }

            CountCards();
            cardlist = "";
            for (int i = 0; i < numCards; ++i)
            {
                cardlist += hand[i].AbreviatedName + " ";
            }
        }

        /// <summary>
        /// Add a card manually to the player's hand
        /// </summary>
        public void AddCard(Card card)
        {
            if (numCards >= 12)
            {
                stay = true;
                return;
            }

            hand.Add(new Card(card));
            cardlist += hand[numCards].AbreviatedName + " ";
            ++numCards;
            CountCards();

            // If score exceeds 21, convert Aces to Jokers (value becomes 1)
            if (cardCount > 21)
            {
                int acesConverted = 0;

                for (int i = 0; i < numCards; ++i)
                {
                    if (hand[i].AbreviatedName.Length > 0 && hand[i].AbreviatedName[0] == 'A')
                    {
                        hand[i] = new Card(CardDeck.DeckCards[53]); // Joker card (value 1)
                        ++acesConverted;
                    }
                }

                if (!isComputer && acesConverted > 0)
                {
                    cardlist = "Aces have become Jokers\n";
                    for (int i = 0; i < numCards; ++i)
                    {
                        cardlist += hand[i].AbreviatedName + " ";
                    }
                }
            }
        }

        /// <summary>
        /// Update total score
        /// </summary>
        public void UpdateScore(int x)
        {
            totalScore += x;
        }
    }
}
