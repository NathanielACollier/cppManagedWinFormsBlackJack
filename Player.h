#pragma once
#include "card.h"
#include <string>

using namespace std;

#define RNUM( min, max ) rand() % (max - min + 1) + min 

#ifndef PLAYER
#define PLAYER

class Player
{
private:
  int games_won;
  int draw_count;
  bool stay;
  bool is_computer;
  int total_score;
  int num_cards;
  card hand[21];
  int card_count;
  void hit(card sdeck[]);
  string cardlist;

public:
	string get_cardlist() const;
	Player(bool iscomputer);
	~Player(void);
	void deal(card sdeck[],int number_of_cards);
	int count_cards();
	void tscore( int new_score);
	int get_drawcount() const;
	void set_drawcount( int x );
	int tscore() const;
	bool turn();
	void set_stay( bool x );
	void new_game();
	int get_gameswon() const;
	void set_gameswon( int x );
	card get_currentcard() const;
	bool check_stay();
	int get_num_cards() const;
	int get_cardindex( int x ) const;
	void undo();
	void add_card(card x);
	void update_score( int x );
};





///////////////////////////// Private Methods ///////////////////////////


void Player::hit(card sdeck[])
{
	if( num_cards >= 12 ) { stay = true; return; }
 hand[num_cards] = sdeck[rand() % ( 0 - DECKL + 1 ) + 0];
 cardlist += hand[num_cards].abv_name;
 cardlist += " ";
 ++num_cards;
 count_cards();
 if( card_count > 21 ) // ACEs become  Jokers if score is over 21
      { 
	   int ac(0); 
	   
	   for( int i(0); i < num_cards; ++i )
	   {
		   if( hand[i].abv_name[0] == deck[1].abv_name[0] ) 
		   { 
			hand[i] = deck[53]; 
			++ac; 
		   }
	   }
      	if( is_computer == false && ac > 0 )
		{
		 cardlist.clear();
		 cardlist = "Aces have become Jokers\n";
         for( int i(0); i < num_cards; ++i )
		 {
          cardlist += hand[i].abv_name;
		  cardlist += " ";
		 }
		}
		
	  }
}

//////////////////////////// Public Methods ///////////////////////////
Player::Player(bool iscomputer)
{
 is_computer = iscomputer;
 card_count = 0;
 games_won = 0;
 draw_count = 0;
 num_cards = 0;
 total_score = 0;
 stay = false;
}

// player destructor
Player::~Player(void)
{
}

// runs hit number_of_cards times
void Player::deal(card sdeck[],int number_of_cards)
{
  for( int i(0); i < number_of_cards; ++i )  hit(sdeck);
}

int Player::count_cards()
{
 card_count = 0;
 for( int i=0; i < num_cards; ++i )  card_count += hand[i].value;
 return card_count;
}

string Player::get_cardlist() const
{
 return cardlist;
}

void Player::tscore( int new_score)
{
 total_score = new_score;
}

int Player::tscore() const
{
 return total_score;
}

int Player::get_drawcount() const
{
 return draw_count;
}

	// computer_turn function
bool Player::turn( )
{
 int chance(0);
 if( stay == true ) return true;
 
 if( card_count <= 15 ) { stay = false; return stay; }
 else {
  if( card_count < 19 )
  {
   chance = RNUM(4,5);
   if( chance == 4) { stay = false; return stay; }
   if( chance == 5) { stay = true; return stay; }
  } else {
   chance = RNUM(1,100);
   if( chance != 50 ) { stay = true; return stay; }
   else { stay = false; return stay; }
  }
 }
 return stay;
}// end of computer_turn function

//set_stay function
void Player::set_stay( bool x )
{
 stay = x;
}// end of set_stay function

void Player::new_game()
{
 card_count = 0;
 num_cards = 0;
 stay = false;
 cardlist.clear();
}

int Player::get_gameswon() const
{
  return games_won;
}

void Player::set_gameswon( int x )
{
 games_won = x;
}

void Player::set_drawcount( int x )
{
 draw_count = x;
}

card Player::get_currentcard() const
{
 return hand[num_cards-1];
}

bool Player::check_stay()
{
 if( card_count >= 21 ) { stay = true; return true; }
  else
   return stay;
}


int Player::get_num_cards() const
{
 return num_cards;
}

int Player::get_cardindex( int x ) const
{
 return hand[x].index;
}

void Player::undo()
{
 if( num_cards <= 0 ) { num_cards = 0; return; }
 if( total_score < 10 ) { return; }
 total_score -= 10;
 num_cards--;
 int t1 = count_cards();
 cardlist.clear();
 for( int i(0); i < num_cards; ++i )
 {
    cardlist += hand[i].abv_name;
    cardlist += " ";
 }
}

void Player::add_card(card x)
{
 if( num_cards >= 12 ) { stay = true; return; }
 hand[num_cards] = x;
 cardlist += hand[num_cards].abv_name;
 cardlist += " ";
 ++num_cards;
 count_cards();
 if( card_count > 21 ) // ACEs become  Jokers if score is over 21
      { 
	   int ac(0); 
	   
	   for( int i(0); i < num_cards; ++i )
	   {
		   if( hand[i].abv_name[0] == deck[1].abv_name[0] ) 
		   { 
			hand[i] = deck[53]; 
			++ac; 
		   }
	   }
      	if( is_computer == false && ac > 0 )
		{
		 cardlist.clear();
		 cardlist = "Aces have become Jokers\n";
         for( int i(0); i < num_cards; ++i )
		 {
          cardlist += hand[i].abv_name;
		  cardlist += " ";
		 }
		}
		
	  }
 
}

void Player::update_score( int x )
{
 total_score += x;
}







#endif