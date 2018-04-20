#include <string>
using namespace std;

#define DECKL 52

#ifndef CARD_STRUCT
#define CARD_STRUCT

struct card
{
 int value;
 /*
 Type
 1 hearts
 2 diamonds
 3 spades
 4 clubs
 */
 int type;
 string name;
 string abv_name;
 int index;
};
 

enum card_value
{
 V_JOKER = 1,  V_TWO = 2,V_THREE = 3,V_FOUR = 4,V_FIVE = 5,
 V_SIX = 6,V_SEVEN = 7,V_EIGHT = 8,V_NINE = 9, V_TEN = 10, V_JACK = 10,
 V_QUEEN = 10, V_KING = 10 , V_ACE = 11
};

/* standard 52 card deck 
    2 special cards 
	10 bonus cards
  */
const card deck[64] =
{
 { V_ACE, 1, "Ace of hearts" ,  "AH"  , 0},
 { V_ACE, 2, "Ace of diamonds", "AD"  , 1 },
 { V_ACE, 3, "Ace of spades", "AS"  , 2 },
 { V_ACE, 4, "Ace of clubs", "AC"  , 3 },
 { V_TWO, 1, "2 of hearts", "2H" , 4 },
 { V_TWO, 2, "2 of diamonds", "2D" , 5 },
 { V_TWO, 3, "2 of spades", "2S" , 6 },
 { V_TWO, 4, "2 of clubs", "2C" , 7 },       
 { V_THREE, 1, "3 of hearts", "3H" , 8 },
 { V_THREE, 2, "3 of diamonds", "3D" , 9 },
 { V_THREE, 3, "3 of spades", "3S" , 10 },
 { V_THREE, 4, "3 of clubs", "3C" , 11 },
 { V_FOUR, 1, "4 of hearts", "4H" , 12 },
 { V_FOUR, 2, "4 of diamonds", "4D" , 13 },
 { V_FOUR, 3, "4 of spades", "4S" , 14 },
 { V_FOUR, 4, "4 of clubs", "4C" , 15 },
 { V_FIVE, 1, "5 of hearts", "5H" , 16 },
 { V_FIVE, 2, "5 of diamonds", "5D" , 17 },
 { V_FIVE, 3, "5 of spades", "5S" , 18 },
 { V_FIVE, 4, "5 of clubs", "5C" , 19 },
 { V_SIX, 1, "6 of hearts", "6H" , 20 },
 { V_SIX, 2, "6 of diamonds", "6D" , 21 },
 { V_SIX, 3, "6 of spades", "6S" , 22 },
 { V_SIX, 4, "6 of clubs", "6C" , 23 },
 { V_SEVEN, 1, "7 of hearts", "7H" , 24 },
 { V_SEVEN, 2, "7 of diamonds", "7D" , 25 },
 { V_SEVEN, 3, "7 of spades", "7S" , 26 },
 { V_SEVEN, 4, "7 of clubs", "7C" , 27 },
 { V_EIGHT, 1, "8 of hearts", "8H" , 28 },
 { V_EIGHT, 2, "8 of diamonds", "8D" , 29 },
 { V_EIGHT, 3, "8 of spades", "8S" , 30 },
 { V_EIGHT, 4, "8 of clubs", "8C" , 31 },
 { V_NINE, 1, "9 of hearts", "9H" , 32 },
 { V_NINE, 2, "9 of diamonds", "9D" , 33 },
 { V_NINE, 3, "9 of spades", "9S" , 34 },
 { V_NINE, 4, "9 of clubs", "9C" , 35 },
 { V_TEN, 1, "10 of hearts", "TH" , 36 },
 { V_TEN, 2, "10 of diamonds", "TD" , 37 },
 { V_TEN, 3, "10 of spades", "TS" , 38 },
 { V_TEN, 4, "10 of clubs", "TC" , 39 },
 { V_JACK, 1, "Jack of hearts", "JH" , 40 },
 { V_JACK, 2, "Jack of diamonds", "JD" , 41 },
 { V_JACK, 3, "Jack of spades", "JS" , 42 },
 { V_JACK, 4, "Jack of clubs", "JC" , 43 },
 { V_QUEEN, 1, "Queen of hearts", "QH" , 44 },
 { V_QUEEN, 2, "Queen of diamonds", "QD" , 45 },
 { V_QUEEN, 3, "Queen of spades", "QS" , 46 },
 { V_QUEEN, 4, "Queen of clubs", "QC" , 47 },
 { V_KING, 1, "King of hearts", "KH" , 48 },
 { V_KING, 2, "King of diamonds", "KD" , 49 },
 { V_KING, 3, "King of spades", "KS" , 50 },
 { V_KING, 4, "King of clubs", "KC" , 51 },
 { V_JOKER,  1, "Joker Wild 1", "JO" , 52 },
 { V_JOKER,  1, "Joker Wild 2", "JT" , 53 },    
	{ 1,  1, "Bonus Plus 1", "+1" , 55 }, // 54 reserved for back of deck
	{ -1, 1, "Bonus Minus 1", "-1", 56 },
	{ 2,  2, "Bonus Plus 2", "+2", 57 },
	{ -2, 2, "Bonus Minus 2", "-2", 58 },
	{ 3,  3, "Bonus Plus 3", "+3", 59 },
	{ -3, 3, "Bonus Minus 3", "-3", 60 },
	{ 4,  4, "Bonus Plus 4", "+4", 61 },
	{ -4, 4, "Bonus Minus 4", "-4", 62 },
	{ 5,  5, "Bonus Plus 5", "+5", 63 },
	{ -5, 5, "Bonus Minus 5", "-5", 64 }
};


#endif

     
     








