using System;
using System.Text;

namespace blackjack
{
    public class Player
    {
        private int games_won;
        private int draw_count;
        private bool stay;
        private bool is_computer;
        private int total_score;
        private int num_cards;
        private Card[] hand;
        private int card_count;
        private string cardlist;

        public Player(bool iscomputer)
        {
            is_computer = iscomputer;
            card_count = 0;
            games_won = 0;
            draw_count = 0;
            num_cards = 0;
            total_score = 0;
            stay = false;
            hand = new Card[21];
            cardlist = "";
        }

        public void Deal(Card[] sdeck, int number_of_cards)
        {
            for (int i = 0; i < number_of_cards; ++i)
                Hit(sdeck);
        }

        public int CountCards()
        {
            card_count = 0;
            for (int i = 0; i < num_cards; ++i)
                card_count += hand[i].Value;
            return card_count;
        }

        public string GetCardlist()
        {
            return cardlist;
        }

        public void SetTotalScore(int new_score)
        {
            total_score = new_score;
        }

        public int GetTotalScore()
        {
            return total_score;
        }

        public int GetDrawCount()
        {
            return draw_count;
        }

        public bool Turn()
        {
            int chance = 0;
            if (stay == true) return true;

            if (card_count <= 15) { stay = false; return stay; }
            else
            {
                if (card_count < 19)
                {
                    chance = RNUM(4, 5);
                    if (chance == 4) { stay = false; return stay; }
                    if (chance == 5) { stay = true; return stay; }
                }
                else
                {
                    chance = RNUM(1, 100);
                    if (chance != 50) { stay = true; return stay; }
                    else { stay = false; return stay; }
                }
            }
            return stay;
        }

        public void SetStay(bool x)
        {
            stay = x;
        }

        public void NewGame()
        {
            card_count = 0;
            num_cards = 0;
            stay = false;
            cardlist = "";
        }

        public int GetGamesWon()
        {
            return games_won;
        }

        public void SetGamesWon(int x)
        {
            games_won = x;
        }

        public void SetDrawCount(int x)
        {
            draw_count = x;
        }

        public Card GetCurrentCard()
        {
            return hand[num_cards - 1];
        }

        public bool CheckStay()
        {
            if (card_count >= 21) { stay = true; return true; }
            else
                return stay;
        }

        public int GetNumCards()
        {
            return num_cards;
        }

        public int GetCardIndex(int x)
        {
            return hand[x].Index;
        }

        public void Undo()
        {
            if (num_cards <= 0) { num_cards = 0; return; }
            if (total_score < 10) { return; }
            total_score -= 10;
            num_cards--;
            int t1 = CountCards();
            cardlist = "";
            for (int i = 0; i < num_cards; ++i)
            {
                cardlist += hand[i].AbvName;
                cardlist += " ";
            }
        }

        public void AddCard(Card x)
        {
            if (num_cards >= 12) { stay = true; return; }
            hand[num_cards] = x;
            cardlist += hand[num_cards].AbvName;
            cardlist += " ";
            ++num_cards;
            CountCards();
            if (card_count > 21)
            {
                int ac = 0;

                for (int i = 0; i < num_cards; ++i)
                {
                    if (hand[i].AbvName[0] == CardDeck.DeckCards[1].AbvName[0])
                    {
                        hand[i] = CardDeck.DeckCards[53];
                        ++ac;
                    }
                }
                if (is_computer == false && ac > 0)
                {
                    cardlist = "";
                    cardlist = "Aces have become Jokers\n";
                    for (int i = 0; i < num_cards; ++i)
                    {
                        cardlist += hand[i].AbvName;
                        cardlist += " ";
                    }
                }
            }
        }

        public void UpdateScore(int x)
        {
            total_score += x;
        }

        private void Hit(Card[] sdeck)
        {
            if (num_cards >= 12) { stay = true; return; }
            hand[num_cards] = sdeck[RNUM(0, CardDeck.DECKL - 1)];
            cardlist += hand[num_cards].AbvName;
            cardlist += " ";
            ++num_cards;
            CountCards();
            if (card_count > 21)
            {
                int ac = 0;

                for (int i = 0; i < num_cards; ++i)
                {
                    if (hand[i].AbvName[0] == CardDeck.DeckCards[1].AbvName[0])
                    {
                        hand[i] = CardDeck.DeckCards[53];
                        ++ac;
                    }
                }
                if (is_computer == false && ac > 0)
                {
                    cardlist = "";
                    cardlist = "Aces have become Jokers\n";
                    for (int i = 0; i < num_cards; ++i)
                    {
                        cardlist += hand[i].AbvName;
                        cardlist += " ";
                    }
                }
            }
        }

        private static int RNUM(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }
    }
}
