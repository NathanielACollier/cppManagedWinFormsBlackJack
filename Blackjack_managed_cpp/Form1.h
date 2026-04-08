#pragma once
#include "Player.h" // this class also is used by this class
#include "about_box.h" // needed for this class to access the about_box class
#include "high_score.h" // class needed so that it can be accessed here


#include <ctime> // date and time functions



enum player_type_identifier
{
	PC = 0, COMPUTER
};


namespace blackjack
{
	using namespace System;
	using namespace System::ComponentModel;
	using namespace System::Collections;
	using namespace System::Windows::Forms;
	using namespace System::Data;
	using namespace System::Drawing;

	/// <summary> 
	/// Summary for Form1
	///
	/// WARNING: If you change the name of this class, you will need to change the 
	///          'Resource File Name' property for the managed resource compiler tool 
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
    /// </summary>
	public ref class Form1 : public System::Windows::Forms::Form
	{
	public:
		Form1(void) // constructor (where members are initialized)
		{
			sdeck = new card [DECKL];
			pc = new Player(false);
			computer = new Player(true);
			buffer = new ostringstream;
			new_game_flag = false;
			bv = new int [4];
			current_game = new high_score_entry;
			InitializeComponent();
		}

       ~Form1(void) // Destructor (where memory is released)
		{
		 delete sdeck;
		 delete pc;
		 delete computer;
		 delete buffer;
		 if (components) delete components;
		}
	card * sdeck;
	ostringstream* buffer;  // output string stream - very usefull stl class
    high_score_entry* current_game;
	int *bv;

    
	Player * pc;
	Player * computer;
	
	bool new_game_flag;

    private: System::Windows::Forms::MainMenu^  MainMenu;
	private: System::Windows::Forms::MenuItem^  NewGame_item;
	private: System::Windows::Forms::MenuItem^  menuItem2;
	private: System::Windows::Forms::StatusBar^  main_statusbar;
	private: System::Windows::Forms::PictureBox^  card_drawn;
	private: System::Windows::Forms::ImageList^  cardlist;
	private: System::Windows::Forms::PictureBox^  card2;
	private: System::Windows::Forms::PictureBox^  card3;
	private: System::Windows::Forms::PictureBox^  card7;
	private: System::Windows::Forms::PictureBox^  card6;
	private: System::Windows::Forms::PictureBox^  card4;
	private: System::Windows::Forms::PictureBox^  card5;
	private: System::Windows::Forms::PictureBox^  card8;
	private: System::Windows::Forms::PictureBox^  card9;
	private: System::Windows::Forms::PictureBox^  card10;
	private: System::Windows::Forms::PictureBox^  card1;
	private: System::Windows::Forms::Label^  CardCountLabel;
	private: System::Windows::Forms::Label^  cardcount_textbox;
	private: System::Windows::Forms::MenuItem^  undo_item;
	private: System::Windows::Forms::MenuItem^  about_item;
	private: System::Windows::Forms::PictureBox^  bonus_card1;
	private: System::Windows::Forms::PictureBox^  bonus_card3;
	private: System::Windows::Forms::PictureBox^  bonus_card2;
	private: System::Windows::Forms::PictureBox^  bonus_card4;
	private: System::Windows::Forms::PictureBox^  card11;
	private: System::Windows::Forms::Label^  none_wins_label;
	private: System::Windows::Forms::Label^  nonewins_textbox;
	private: System::Windows::Forms::MenuItem^  highscore_menu;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::Label^  number_games_textbox;
	private: System::Windows::Forms::MenuItem^  Exit_item;


	private: System::ComponentModel::ComponentResourceManager^ resources;


protected:
		// Designer cleanup moved to destructor

    private: System::Windows::Forms::MenuItem^  menuItem1;


    private: System::Windows::Forms::Label^  hand_textbox;




    private: System::Windows::Forms::Label^  playerwin_textbox;
	private: System::Windows::Forms::Label^  computerwin_textbox;
	private: System::Windows::Forms::Label^  PlayerWinLabel;
	private: System::Windows::Forms::Label^  ComputerWinLabel;
	private: System::Windows::Forms::Label^  playerscore_textbox;
	private: System::Windows::Forms::Label^  computerscore_textbox;
	private: System::Windows::Forms::Label^  ComputerScoreLabel;
	private: System::Windows::Forms::Label^  PlayerScoreLabel;
	private: System::Windows::Forms::Button^  hit_button;
	private: System::Windows::Forms::Button^  stay_button;
	private: System::ComponentModel::IContainer^  components;
	
   /*
    char_change
	receives a char and changes it to
	a symbol using a code
	Only used in the console based version of this program
	This function is mostly hear for historical reasons
   */
    char char_change( char x )
	{ 
	 switch( x ) {
		  case 'H': return (char)259; break;
		  case 'D': return (char)260; break;
		  case 'C': return (char)261; break;
		  case 'S': return (char)262; break;
		  case 'O': return (char)257; break;
		  case 'T': return (char)258; break;
		  }
	 return x;
	}
    
	/*
    write_save_game
	It is assumed that the data for the current game is allready 
	written to the current_game structure
	if those conditions are true
	then it will write out what you want to the output file
	*/
	void write_save_game()
	{
      ofstream fout;

	  
	  fout.open( SAVE_GAME_FILE, ios::app );
	  if( !fout )
	  {
        // The file should open but if it doesn't
		  // usualy some kind of hardware problem do something here
	  }
      
	  fout<< current_game->date << "\n"
		  << current_game->player_name << "\n"
		  << current_game->player_score << " "
		  << current_game->computer_score << " "
		  << current_game->player_win << " "
		  << current_game->computer_win << " "
		  << current_game->none_win << "\n";

	  fout.close();
	}

    
	/*
    save_game
    This function is called so that the current game information will be 
	saved into the "saved game file"    "saved_games.txt" AKA SAVE_GAME_FILE
	*/
	void save_game()
	{  
      current_game->computer_score = computer->tscore();
	  current_game->computer_win = computer->get_gameswon();
	  current_game->date = "Day, Month 00, 0000";
	  current_game->none_win = pc->get_drawcount();
	  current_game->player_name = "Player Name";
	  current_game->player_score = pc->tscore();
	  current_game->player_win = pc->get_gameswon();

	  /* now that we have this information we need to save it to a file so
	     that it can be used latter when this program is ran again */
	  write_save_game();
	}
    
	/*
    display_cards
	An easy way to understand why this function was written like this is to think of each
	picture box as an on off switch either it has a picture or it doesn't
	this box checks for each condition and "turns on" the picture box that meets the condition
	*/
	void display_cards()
	{  
	 int x = pc->get_num_cards();
     
	 if( x == 1 ) return;
     if( x >= 2 )
		 card1->Image = cardlist->Images[ pc->get_cardindex(0) ];
     if( x >= 3 )
		 card2->Image = cardlist->Images[ pc->get_cardindex(1) ];
     if( x >= 4 )
		 card3->Image = cardlist->Images[ pc->get_cardindex(2) ];
     if( x >= 5 )
		 card4->Image = cardlist->Images[ pc->get_cardindex(3) ];
     if( x >= 6 )
		 card5->Image = cardlist->Images[ pc->get_cardindex(4) ];
     if( x >= 7 )
		 card6->Image = cardlist->Images[ pc->get_cardindex(5) ];
     if( x >= 8 )
		 card7->Image = cardlist->Images[ pc->get_cardindex(6) ];
     if( x >= 9 )
		 card8->Image = cardlist->Images[ pc->get_cardindex(7) ];
     if( x >= 10)
		 card9->Image = cardlist->Images[ pc->get_cardindex(8) ];
     if( x >= 11)
		 card10->Image = cardlist->Images[ pc->get_cardindex(9) ];
     if( x >= 12)
		 card11->Image = cardlist->Images[ pc->get_cardindex(10) ];

	}
    
	/*
    clear_cards
	sets the card_drawn box to the default back_bj.bmp card
	and all the other cards to NULL or empty
	*/
	void clear_cards()
	{
    card_drawn->Image = cardlist->Images[54];
	card1->Image = nullptr;
	 card2->Image = nullptr; card3->Image = nullptr; card4->Image = nullptr; card5->Image = nullptr;
	card6->Image = nullptr; card7->Image = nullptr; card8->Image = nullptr; card9->Image = nullptr; card10->Image = nullptr;
	card11->Image = nullptr;
	bonus_card1->Hide(); bonus_card2->Hide(); bonus_card3->Hide(); bonus_card4->Hide();
	bonus_card1->Image = nullptr; bonus_card2->Image = nullptr; bonus_card3->Image = nullptr; bonus_card4->Image = nullptr;
	}

	void display_bonus()
	{
	 int pscore = pc->tscore();
     // 4 levels of bonus cards
	 if( pscore >= 15 ) 
	 {
		 bonus_card1->Show();
         bv[0] = RNUM( 55, 64 );
         bonus_card1->Image = cardlist->Images[ bv[0] ];
	 }
	 if( pscore >= 30 ) 
	 {
		 bonus_card2->Show();
             bv[1] = RNUM( 55, 64 );
             bonus_card2->Image = cardlist->Images[ bv[1] ];
	 }
	 if( pscore >= 45 )
	 {
	     bonus_card3->Show();
         bv[2] = RNUM( 55, 64 );
         bonus_card3->Image = cardlist->Images[ bv[2] ];
	 }
	 if( pscore >= 60 ) 
	 {
         bonus_card4->Show();
         bv[3] = RNUM( 55, 64 );
         bonus_card4->Image = cardlist->Images[ bv[3] ];
	 }
	}

	/*
    Shuffle Function
	Puts random "cards" from the struct constant deck (card.h) into
	an array of cards called sdeck wich is a dynamicly allocated card array
	*/
	void shuffle()
    {
       int cur_card = 0;
       int prev_card = 0;
 
       for( int i= rand() % ( 50 - 1 + 1 ) + 1;    i < 50;   ++i  )
       {
        for( int i=0; i < DECKL; ++i )
        {
         while( cur_card == prev_card )
         { 
	      cur_card = rand() % ( 0 - DECKL + 1 ) + 0; }
          sdeck[i] = deck[cur_card];
          /* The below line is only used in the console version of this program  */
		 // sdeck[i].abv_name[1] = char_change( sdeck[i].abv_name[1] );
          prev_card = cur_card;
         }
        }
     }// end of shuffle function


	int current_game_score( int id )
	{
      int score = 0;
      switch( id )
	  {
	    case PC:
	       score = (pc->get_gameswon() - computer->get_gameswon());
		   if( score <= 0 )
		   {  score = 21; }
		   else
		   {  score *= 21; }
		break;
		case COMPUTER:
		   score = (computer->get_gameswon() - pc->get_gameswon());
		   if( score <= 0 )
		   {  score = 21; }
		   else
		   {  score *= 21; }
		break;
		default: 
		  return 0;
	    break;
	  }
	  return score;
	}

	// calc score function
    void calc_score( int winner )
    {
	  int pcs = pc->tscore(),     // Player Current Score
	      ccs = computer->tscore(), // Computer Current Score
		  pcc = pc->count_cards(), // Player Card Count
		  ccc = computer->count_cards(); // Computer Card Count

      if( winner == 1 ) pc->tscore( pcs + current_game_score( PC ) ); // this function sets the total_score in pc to the new score
      if( winner == 2 ) computer->tscore( ccs + current_game_score( COMPUTER ) );
	  if( winner == 0 ) { pc->tscore( pcs - ( (pcc > 21 ) ? (21 - pcc) : 0 ) ) ; 
    	                computer->tscore( ccs - ( (ccc > 21 ) ? (21 - ccc) : 0 )); } 
    }// end of calc score function

	/*
	 update_stats
     uses the buffer to put the different stats into strings
	 then assigns the textboxes to those strings so that the stats will be displayed
	*/
	void update_stats(  )
	{
     string player_win,comp_win,none_win,player_score,comp_score,number_games;
	 buffer->str("");
	 *buffer << pc->get_gameswon();
	 player_win = buffer->str();
	 buffer->str("");
	 *buffer << computer->get_gameswon();
	 comp_win = buffer->str();
	 buffer->str("");
	 *buffer << pc->tscore();
	 player_score = buffer->str();
	 buffer->str("");
	 *buffer << computer->tscore();
	 comp_score = buffer->str();
	 buffer->str("");
	 *buffer << pc->get_drawcount();
	 none_win = buffer->str();
	 buffer->str("");
	 *buffer << ( pc->get_gameswon() + computer->get_gameswon() + pc->get_drawcount() ); // player wins + computer wins + none wins = total number of games played
     number_games = buffer->str();
	 buffer->str("");
     
     playerscore_textbox->Text = gcnew System::String(player_score.c_str());
	 playerwin_textbox->Text = gcnew System::String(player_win.c_str());   
	 computerscore_textbox->Text = gcnew System::String(comp_score.c_str());
	 computerwin_textbox->Text = gcnew System::String(comp_win.c_str());
	 nonewins_textbox->Text = gcnew System::String(none_win.c_str());
	 number_games_textbox->Text = gcnew System::String(number_games.c_str());
	}// end of update_stats

	void out_winner(int x)
	{
	  string temp;
	  buffer->str(""); // clears out the buffer
     switch( x )
	 {
	  case 1:  // player won
	  *buffer << "Winner:           Player\n"
	  		  << "Computers Cards:  " << computer->get_cardlist() << "\n"
			  << "Computers Points: " << computer->count_cards() << "\n"
			  << "Computers Score:  " << computer->tscore() << "\n"
			  << "Computer Wins:    " << computer->get_gameswon() << "\n";
	  break;
	  case 2:  // computer won
     *buffer << "Winner:           Computer\n"
		     << "Computers Cards:  " << computer->get_cardlist() << "\n"
			 << "Computers Points: " << computer->count_cards() << "\n"
			 << "Computers Score:  " << computer->tscore() << "\n"
			 << "Computer Wins:    " << computer->get_gameswon() << "\n";
	  break;
	  case 3: // no one won
	 *buffer << "Winner:           None\n"
		     << "Computers Cards:  " << computer->get_cardlist() << "\n"
			 << "Computers Points: " << computer->count_cards() << "\n"
			 << "Computers Score:  " << computer->tscore() << "\n"
			 << "Computer Wins:    " << computer->get_gameswon() << "\n";
	     
	  break;
      
	 }
     main_statusbar->Text = L" Press Hit to start new game ";
	 temp = buffer->str();        // converts buffer into a string
	 hand_textbox->Text = gcnew System::String(temp.c_str()); // sets cardcount_textbox to the string(const char*)
	 buffer->str(""); // clears out the buffer

	}

	void setup_newgame()
	{
	  pc->new_game();
	  computer->new_game();
      cardcount_textbox->Text = L"0";
	  buffer->str(""); // clears out the buffer
	   clear_cards();
	}

	void update_table()
	{
		string temp = pc->get_cardlist();  // puts all the abv_name card names into a string and returns it
        hand_textbox->Text = gcnew System::String(temp.c_str());  // sets the string(const char*) to the hand_textbox
		*buffer << pc->count_cards(); // ostringstream used to convert count_cards wich is the value
			                               // of all the cards added up into a string
		temp = buffer->str();        // converts buffer into a string
        cardcount_textbox->Text = gcnew System::String(temp.c_str()); // sets cardcount_textbox to the string(const char*)
		buffer->str(""); // clears out the buffer
		card cur_card = pc->get_currentcard(); // gets the card that was drawn when hit was pressed
        card_drawn->Image = cardlist->Images[ cur_card.index ]; // uses the cardlist image list and the index of the
			                                                                         // current card to display the card drawn
		display_cards(); 
	}

	// calc_winner function
	void calc_winner( )
    {
     int player_score = pc->count_cards();
     int computer_score = computer->count_cards();
     int winner(0);
  
      if( player_score <= 21 && computer_score <= 21 )
      { if( player_score == 21 && computer_score == 21 ) winner = 0;
       else if( player_score == 21 && computer_score < 21 ) winner = 1;
       else if( player_score < 21 && computer_score == 21 ) winner = 2;
       else if( player_score < 21 && computer_score < 21 ) {
       	if( player_score > computer_score ) winner = 1;
       	else if( player_score == computer_score ) winner = 0;
       	else winner = 2; } 
     } else { if( player_score > 21 && computer_score == 21 ) winner = 2;
     	else if( player_score == 21 && computer_score > 21 ) winner = 1;
     	else if( player_score < 21 && computer_score > 21 ) winner = 1;
     	else if( player_score > 21 && computer_score < 21 ) winner = 2; 
     	else winner = 0; }  


	 if( winner == 1 ) pc->set_gameswon( pc->get_gameswon() + 1 );
	 if( winner == 2 ) computer->set_gameswon( computer->get_gameswon() + 1 );
	 if( winner == 0 ) pc->set_drawcount( pc->get_drawcount() + 1 );
	 
	 calc_score(  winner );
	 update_stats();
     
	 switch ( winner ) { case 1: out_winner( 1 ); break;
    	                case 2: out_winner( 2 ); break;
   	                    case 0: out_winner( 3 ); break;  }

	 new_game_flag = true;
	 shuffle();
   
  } // end of calc_winner function
  
	



		private:
		/// <summary>
		/// Required designer variable.
		/// </summary>

        /// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
            this->components = gcnew System::ComponentModel::Container();
			this->resources = gcnew System::ComponentModel::ComponentResourceManager(Form1::typeid);
			this->MainMenu = gcnew System::Windows::Forms::MainMenu(this->components);
			this->menuItem1 = gcnew System::Windows::Forms::MenuItem();
			this->NewGame_item = gcnew System::Windows::Forms::MenuItem();
			this->undo_item = gcnew System::Windows::Forms::MenuItem();
			this->highscore_menu = gcnew System::Windows::Forms::MenuItem();
			this->Exit_item = gcnew System::Windows::Forms::MenuItem();
			this->menuItem2 = gcnew System::Windows::Forms::MenuItem();
			this->about_item = gcnew System::Windows::Forms::MenuItem();
			this->hand_textbox = gcnew System::Windows::Forms::Label();
			this->playerwin_textbox = gcnew System::Windows::Forms::Label();
			this->computerwin_textbox = gcnew System::Windows::Forms::Label();
			this->PlayerWinLabel = gcnew System::Windows::Forms::Label();
			this->ComputerWinLabel = gcnew System::Windows::Forms::Label();
			this->playerscore_textbox = gcnew System::Windows::Forms::Label();
			this->computerscore_textbox = gcnew System::Windows::Forms::Label();
			this->ComputerScoreLabel = gcnew System::Windows::Forms::Label();
			this->PlayerScoreLabel = gcnew System::Windows::Forms::Label();
			this->hit_button = gcnew System::Windows::Forms::Button();
			this->stay_button = gcnew System::Windows::Forms::Button();
			this->main_statusbar = gcnew System::Windows::Forms::StatusBar();
			this->card_drawn = gcnew System::Windows::Forms::PictureBox();
			this->cardlist = gcnew System::Windows::Forms::ImageList(this->components);
			this->card2 = gcnew System::Windows::Forms::PictureBox();
			this->card3 = gcnew System::Windows::Forms::PictureBox();
			this->card7 = gcnew System::Windows::Forms::PictureBox();
			this->card6 = gcnew System::Windows::Forms::PictureBox();
			this->card4 = gcnew System::Windows::Forms::PictureBox();
			this->card5 = gcnew System::Windows::Forms::PictureBox();
			this->card8 = gcnew System::Windows::Forms::PictureBox();
			this->card9 = gcnew System::Windows::Forms::PictureBox();
			this->card10 = gcnew System::Windows::Forms::PictureBox();
			this->card1 = gcnew System::Windows::Forms::PictureBox();
			this->CardCountLabel = gcnew System::Windows::Forms::Label();
			this->cardcount_textbox = gcnew System::Windows::Forms::Label();
			this->bonus_card1 = gcnew System::Windows::Forms::PictureBox();
			this->bonus_card3 = gcnew System::Windows::Forms::PictureBox();
			this->bonus_card2 = gcnew System::Windows::Forms::PictureBox();
			this->bonus_card4 = gcnew System::Windows::Forms::PictureBox();
			this->card11 = gcnew System::Windows::Forms::PictureBox();
			this->none_wins_label = gcnew System::Windows::Forms::Label();
			this->nonewins_textbox = gcnew System::Windows::Forms::Label();
			this->label1 = gcnew System::Windows::Forms::Label();
			this->number_games_textbox = gcnew System::Windows::Forms::Label();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card_drawn)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card2)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card3)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card7)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card6)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card4)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card5)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card8)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card9)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card10)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card1)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card1)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card3)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card2)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card4)->BeginInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card11)->BeginInit();
			this->SuspendLayout();
			// 
			// MainMenu
			// 
            cli::array<System::Windows::Forms::MenuItem^>^ __mcTemp__1 = gcnew cli::array<System::Windows::Forms::MenuItem^>(2);
			__mcTemp__1[0] = this->menuItem1;
			__mcTemp__1[1] = this->menuItem2;
			this->MainMenu->MenuItems->AddRange(__mcTemp__1);
			// 
			// menuItem1
			// 
			this->menuItem1->Index = 0;
            cli::array<System::Windows::Forms::MenuItem^>^ __mcTemp__2 = gcnew cli::array<System::Windows::Forms::MenuItem^>(4);
			__mcTemp__2[0] = this->NewGame_item;
			__mcTemp__2[1] = this->undo_item;
			__mcTemp__2[2] = this->highscore_menu;
			__mcTemp__2[3] = this->Exit_item;
			this->menuItem1->MenuItems->AddRange(__mcTemp__2);
			this->menuItem1->Text = L"Game";
			// 
			// NewGame_item
			// 
			this->NewGame_item->Index = 0;
			this->NewGame_item->Shortcut = System::Windows::Forms::Shortcut::CtrlN;
            this->NewGame_item->Text = L"New Game";
			this->NewGame_item->Click += gcnew System::EventHandler(this, &Form1::NewGame_item_Click);
			// 
			// undo_item
			// 
			this->undo_item->Index = 1;
			this->undo_item->Shortcut = System::Windows::Forms::Shortcut::CtrlU;
            this->undo_item->Text = L"Undo";
			this->undo_item->Click += gcnew System::EventHandler(this, &Form1::undo_item_Click);
			// 
			// highscore_menu
			// 
			this->highscore_menu->Index = 2;
            this->highscore_menu->Text = L"High Score List";
			this->highscore_menu->Click += gcnew System::EventHandler(this, &Form1::highscore_menu_Click);
			// 
			// Exit_item
			// 
			this->Exit_item->Index = 3;
			this->Exit_item->Shortcut = System::Windows::Forms::Shortcut::CtrlE;
            this->Exit_item->Text = L"Exit";
			this->Exit_item->Click += gcnew System::EventHandler(this, &Form1::menuItem3_Click);
			// 
			// menuItem2
			// 
			this->menuItem2->Index = 1;
            cli::array<System::Windows::Forms::MenuItem^>^ __mcTemp__3 = gcnew cli::array<System::Windows::Forms::MenuItem^>(1);
			__mcTemp__3[0] = this->about_item;
			this->menuItem2->MenuItems->AddRange(__mcTemp__3);
			this->menuItem2->Text = L"Help";
			// 
			// about_item
			// 
			this->about_item->Index = 0;
            this->about_item->Text = L"About";
			this->about_item->Click += gcnew System::EventHandler(this, &Form1::menuItem3_Click_1);
			// 
			// hand_textbox
			// 
			this->hand_textbox->BackColor = System::Drawing::Color::Transparent;
			this->hand_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->hand_textbox->Location = System::Drawing::Point(528, 16);
            this->hand_textbox->Name = L"hand_textbox";
			this->hand_textbox->Size = System::Drawing::Size(192, 88);
			this->hand_textbox->TabIndex = 0;
            this->hand_textbox->Text = L"0";
			// 
			// playerwin_textbox
			// 
			this->playerwin_textbox->BackColor = System::Drawing::Color::Transparent;
			this->playerwin_textbox->ForeColor = System::Drawing::Color::Black;
			this->playerwin_textbox->Location = System::Drawing::Point(371, 65);
            this->playerwin_textbox->Name = L"playerwin_textbox";
			this->playerwin_textbox->Size = System::Drawing::Size(116, 28);
			this->playerwin_textbox->TabIndex = 4;
            this->playerwin_textbox->Text = L"0";
			// 
			// computerwin_textbox
			// 
			this->computerwin_textbox->BackColor = System::Drawing::Color::Transparent;
			this->computerwin_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->computerwin_textbox->Location = System::Drawing::Point(371, 20);
            this->computerwin_textbox->Name = L"computerwin_textbox";
			this->computerwin_textbox->Size = System::Drawing::Size(116, 28);
			this->computerwin_textbox->TabIndex = 5;
            this->computerwin_textbox->Text = L"0";
			// 
			// PlayerWinLabel
			// 
			this->PlayerWinLabel->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->PlayerWinLabel->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->PlayerWinLabel->Location = System::Drawing::Point(280, 56);
			this->PlayerWinLabel->Name = L"PlayerWinLabel";
			this->PlayerWinLabel->Size = System::Drawing::Size(72, 36);
			this->PlayerWinLabel->TabIndex = 6;
			this->PlayerWinLabel->Text = L"Player Wins";
			// 
			// ComputerWinLabel
			// 
			this->ComputerWinLabel->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->ComputerWinLabel->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->ComputerWinLabel->Location = System::Drawing::Point(280, 15);
			this->ComputerWinLabel->Name = L"ComputerWinLabel";
			this->ComputerWinLabel->Size = System::Drawing::Size(72, 32);
			this->ComputerWinLabel->TabIndex = 7;
			this->ComputerWinLabel->Text = L"Computer Wins";
			// 
			// playerscore_textbox
			// 
			this->playerscore_textbox->BackColor = System::Drawing::Color::Transparent;
			this->playerscore_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->playerscore_textbox->Location = System::Drawing::Point(120, 65);
            this->playerscore_textbox->Name = L"playerscore_textbox";
			this->playerscore_textbox->Size = System::Drawing::Size(116, 28);
			this->playerscore_textbox->TabIndex = 8;
            this->playerscore_textbox->Text = L"0";
			// 
			// computerscore_textbox
			// 
			this->computerscore_textbox->BackColor = System::Drawing::Color::Transparent;
			this->computerscore_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->computerscore_textbox->Location = System::Drawing::Point(120, 20);
            this->computerscore_textbox->Name = L"computerscore_textbox";
			this->computerscore_textbox->Size = System::Drawing::Size(116, 28);
			this->computerscore_textbox->TabIndex = 9;
            this->computerscore_textbox->Text = L"0";
			// 
			// ComputerScoreLabel
			// 
			this->ComputerScoreLabel->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->ComputerScoreLabel->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->ComputerScoreLabel->Location = System::Drawing::Point(16, 15);
			this->ComputerScoreLabel->Name = L"ComputerScoreLabel";
			this->ComputerScoreLabel->Size = System::Drawing::Size(72, 40);
			this->ComputerScoreLabel->TabIndex = 10;
			this->ComputerScoreLabel->Text = L"Computer Score";
			// 
			// PlayerScoreLabel
			// 
			this->PlayerScoreLabel->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->PlayerScoreLabel->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->PlayerScoreLabel->Location = System::Drawing::Point(16, 64);
			this->PlayerScoreLabel->Name = L"PlayerScoreLabel";
			this->PlayerScoreLabel->Size = System::Drawing::Size(72, 40);
			this->PlayerScoreLabel->TabIndex = 11;
			this->PlayerScoreLabel->Text = L"Player Score";
			// 
			// hit_button
			// 
			this->hit_button->FlatStyle = System::Windows::Forms::FlatStyle::Flat;
            this->hit_button->Font = (gcnew System::Drawing::Font(L"Arial Black", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->hit_button->Location = System::Drawing::Point(296, 376);
            this->hit_button->Name = L"hit_button";
			this->hit_button->Size = System::Drawing::Size(104, 40);
			this->hit_button->TabIndex = 12;
            this->hit_button->Text = L"Hit";
			this->hit_button->Click += gcnew System::EventHandler(this, &Form1::hit_button_Click);
			// 
			// stay_button
			// 
			this->stay_button->FlatStyle = System::Windows::Forms::FlatStyle::Flat;
            this->stay_button->Font = (gcnew System::Drawing::Font(L"Arial Black", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->stay_button->Location = System::Drawing::Point(416, 376);
            this->stay_button->Name = L"stay_button";
			this->stay_button->Size = System::Drawing::Size(104, 40);
			this->stay_button->TabIndex = 13;
            this->stay_button->Text = L"Stay";
			this->stay_button->Click += gcnew System::EventHandler(this, &Form1::stay_button_Click);
			// 
			// main_statusbar
			// 
			this->main_statusbar->Location = System::Drawing::Point(0, 427);
            this->main_statusbar->Name = L"main_statusbar";
			this->main_statusbar->Size = System::Drawing::Size(728, 22);
			this->main_statusbar->TabIndex = 14;
			// 
			// card_drawn
			// 
			this->card_drawn->Location = System::Drawing::Point(16, 160);
            this->card_drawn->Name = L"card_drawn";
			this->card_drawn->Size = System::Drawing::Size(71, 96);
			this->card_drawn->TabIndex = 15;
			this->card_drawn->TabStop = false;
			// 
			// cardlist
			// 
            this->cardlist = LoadCardsImageListFromCardFiles();
			this->cardlist->TransparentColor = System::Drawing::Color::Transparent;
            this->cardlist->Images->SetKeyName(0, L"");
			this->cardlist->Images->SetKeyName(1, L"");
			this->cardlist->Images->SetKeyName(2, L"");
			this->cardlist->Images->SetKeyName(3, L"");
			this->cardlist->Images->SetKeyName(4, L"");
			this->cardlist->Images->SetKeyName(5, L"");
			this->cardlist->Images->SetKeyName(6, L"");
			this->cardlist->Images->SetKeyName(7, L"");
			this->cardlist->Images->SetKeyName(8, L"");
			this->cardlist->Images->SetKeyName(9, L"");
			this->cardlist->Images->SetKeyName(10, L"");
			this->cardlist->Images->SetKeyName(11, L"");
			this->cardlist->Images->SetKeyName(12, L"");
			this->cardlist->Images->SetKeyName(13, L"");
			this->cardlist->Images->SetKeyName(14, L"");
			this->cardlist->Images->SetKeyName(15, L"");
			this->cardlist->Images->SetKeyName(16, L"");
			this->cardlist->Images->SetKeyName(17, L"");
			this->cardlist->Images->SetKeyName(18, L"");
			this->cardlist->Images->SetKeyName(19, L"");
			this->cardlist->Images->SetKeyName(20, L"");
			this->cardlist->Images->SetKeyName(21, L"");
			this->cardlist->Images->SetKeyName(22, L"");
			this->cardlist->Images->SetKeyName(23, L"");
			this->cardlist->Images->SetKeyName(24, L"");
			this->cardlist->Images->SetKeyName(25, L"");
			this->cardlist->Images->SetKeyName(26, L"");
			this->cardlist->Images->SetKeyName(27, L"");
			this->cardlist->Images->SetKeyName(28, L"");
			this->cardlist->Images->SetKeyName(29, L"");
			this->cardlist->Images->SetKeyName(30, L"");
			this->cardlist->Images->SetKeyName(31, L"");
			this->cardlist->Images->SetKeyName(32, L"");
			this->cardlist->Images->SetKeyName(33, L"");
			this->cardlist->Images->SetKeyName(34, L"");
			this->cardlist->Images->SetKeyName(35, L"");
			this->cardlist->Images->SetKeyName(36, L"");
			this->cardlist->Images->SetKeyName(37, L"");
			this->cardlist->Images->SetKeyName(38, L"");
			this->cardlist->Images->SetKeyName(39, L"");
			this->cardlist->Images->SetKeyName(40, L"");
			this->cardlist->Images->SetKeyName(41, L"");
			this->cardlist->Images->SetKeyName(42, L"");
			this->cardlist->Images->SetKeyName(43, L"");
			this->cardlist->Images->SetKeyName(44, L"");
			this->cardlist->Images->SetKeyName(45, L"");
			this->cardlist->Images->SetKeyName(46, L"");
			this->cardlist->Images->SetKeyName(47, L"");
			this->cardlist->Images->SetKeyName(48, L"");
			this->cardlist->Images->SetKeyName(49, L"");
			this->cardlist->Images->SetKeyName(50, L"");
			this->cardlist->Images->SetKeyName(51, L"");
			this->cardlist->Images->SetKeyName(52, L"");
			this->cardlist->Images->SetKeyName(53, L"");
			this->cardlist->Images->SetKeyName(54, L"");
			this->cardlist->Images->SetKeyName(55, L"");
			this->cardlist->Images->SetKeyName(56, L"");
			this->cardlist->Images->SetKeyName(57, L"");
			this->cardlist->Images->SetKeyName(58, L"");
			this->cardlist->Images->SetKeyName(59, L"");
			this->cardlist->Images->SetKeyName(60, L"");
			this->cardlist->Images->SetKeyName(61, L"");
			this->cardlist->Images->SetKeyName(62, L"");
			this->cardlist->Images->SetKeyName(63, L"");
			this->cardlist->Images->SetKeyName(64, L"");
			// 
			// card2
			// 
			this->card2->Location = System::Drawing::Point(176, 160);
            this->card2->Name = L"card2";
			this->card2->Size = System::Drawing::Size(71, 96);
			this->card2->TabIndex = 17;
			this->card2->TabStop = false;
			// 
			// card3
			// 
			this->card3->Location = System::Drawing::Point(256, 160);
            this->card3->Name = L"card3";
			this->card3->Size = System::Drawing::Size(71, 96);
			this->card3->TabIndex = 18;
			this->card3->TabStop = false;
			// 
			// card7
			// 
			this->card7->Location = System::Drawing::Point(96, 264);
            this->card7->Name = L"card7";
			this->card7->Size = System::Drawing::Size(71, 96);
			this->card7->TabIndex = 19;
			this->card7->TabStop = false;
			// 
			// card6
			// 
			this->card6->Location = System::Drawing::Point(16, 264);
            this->card6->Name = L"card6";
			this->card6->Size = System::Drawing::Size(71, 96);
			this->card6->TabIndex = 20;
			this->card6->TabStop = false;
			// 
			// card4
			// 
			this->card4->Location = System::Drawing::Point(336, 160);
            this->card4->Name = L"card4";
			this->card4->Size = System::Drawing::Size(71, 96);
			this->card4->TabIndex = 21;
			this->card4->TabStop = false;
			// 
			// card5
			// 
			this->card5->Location = System::Drawing::Point(416, 160);
            this->card5->Name = L"card5";
			this->card5->Size = System::Drawing::Size(71, 96);
			this->card5->TabIndex = 22;
			this->card5->TabStop = false;
			// 
			// card8
			// 
			this->card8->Location = System::Drawing::Point(176, 264);
            this->card8->Name = L"card8";
			this->card8->Size = System::Drawing::Size(71, 96);
			this->card8->TabIndex = 23;
			this->card8->TabStop = false;
			// 
			// card9
			// 
			this->card9->Location = System::Drawing::Point(256, 264);
            this->card9->Name = L"card9";
			this->card9->Size = System::Drawing::Size(71, 96);
			this->card9->TabIndex = 24;
			this->card9->TabStop = false;
			// 
			// card10
			// 
			this->card10->Location = System::Drawing::Point(336, 264);
            this->card10->Name = L"card10";
			this->card10->Size = System::Drawing::Size(71, 96);
			this->card10->TabIndex = 25;
			this->card10->TabStop = false;
			// 
			// card1
			// 
			this->card1->Location = System::Drawing::Point(96, 160);
            this->card1->Name = L"card1";
			this->card1->Size = System::Drawing::Size(71, 96);
			this->card1->TabIndex = 26;
			this->card1->TabStop = false;
			// 
			// CardCountLabel
			// 
			this->CardCountLabel->BackColor = System::Drawing::Color::ForestGreen;
			this->CardCountLabel->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->CardCountLabel->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->CardCountLabel->Location = System::Drawing::Point(16, 120);
			this->CardCountLabel->Name = L"CardCountLabel";
			this->CardCountLabel->Size = System::Drawing::Size(96, 24);
			this->CardCountLabel->TabIndex = 3;
			this->CardCountLabel->Text = L"Card Count";
			// 
			// cardcount_textbox
			// 
			this->cardcount_textbox->BackColor = System::Drawing::Color::Transparent;
			this->cardcount_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->cardcount_textbox->Location = System::Drawing::Point(120, 112);
            this->cardcount_textbox->Name = L"cardcount_textbox";
			this->cardcount_textbox->Size = System::Drawing::Size(116, 28);
			this->cardcount_textbox->TabIndex = 2;
            this->cardcount_textbox->Text = L"0";
			// 
			// bonus_card1
			// 
			this->bonus_card1->Location = System::Drawing::Point(560, 160);
            this->bonus_card1->Name = L"bonus_card1";
			this->bonus_card1->Size = System::Drawing::Size(71, 96);
			this->bonus_card1->TabIndex = 27;
			this->bonus_card1->TabStop = false;
            this->bonus_card1->Click += gcnew System::EventHandler(this, &Form1::bonus_card1_Click);
			// 
			// bonus_card3
			// 
			this->bonus_card3->Location = System::Drawing::Point(560, 264);
            this->bonus_card3->Name = L"bonus_card3";
			this->bonus_card3->Size = System::Drawing::Size(71, 96);
			this->bonus_card3->TabIndex = 28;
			this->bonus_card3->TabStop = false;
            this->bonus_card3->Click += gcnew System::EventHandler(this, &Form1::bonus_card3_Click);
			// 
			// bonus_card2
			// 
			this->bonus_card2->Location = System::Drawing::Point(640, 160);
            this->bonus_card2->Name = L"bonus_card2";
			this->bonus_card2->Size = System::Drawing::Size(71, 96);
			this->bonus_card2->TabIndex = 29;
			this->bonus_card2->TabStop = false;
            this->bonus_card2->Click += gcnew System::EventHandler(this, &Form1::bonus_card2_Click);
			// 
			// bonus_card4
			// 
			this->bonus_card4->Location = System::Drawing::Point(640, 264);
            this->bonus_card4->Name = L"bonus_card4";
			this->bonus_card4->Size = System::Drawing::Size(71, 96);
			this->bonus_card4->TabIndex = 30;
			this->bonus_card4->TabStop = false;
            this->bonus_card4->Click += gcnew System::EventHandler(this, &Form1::bonus_card4_Click);
			// 
			// card11
			// 
			this->card11->Location = System::Drawing::Point(416, 264);
            this->card11->Name = L"card11";
			this->card11->Size = System::Drawing::Size(71, 96);
			this->card11->TabIndex = 31;
			this->card11->TabStop = false;
			// 
			// none_wins_label
			// 
			this->none_wins_label->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->none_wins_label->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->none_wins_label->Location = System::Drawing::Point(280, 104);
			this->none_wins_label->Name = L"none_wins_label";
			this->none_wins_label->Size = System::Drawing::Size(72, 40);
			this->none_wins_label->TabIndex = 32;
			this->none_wins_label->Text = L"None       Wins";
			// 
			// nonewins_textbox
			// 
			this->nonewins_textbox->BackColor = System::Drawing::Color::Transparent;
			this->nonewins_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->nonewins_textbox->Location = System::Drawing::Point(371, 112);
            this->nonewins_textbox->Name = L"nonewins_textbox";
			this->nonewins_textbox->Size = System::Drawing::Size(116, 28);
			this->nonewins_textbox->TabIndex = 33;
            this->nonewins_textbox->Text = L"0";
			// 
			// label1
			// 
			this->label1->FlatStyle = System::Windows::Forms::FlatStyle::Popup;
            this->label1->Font = (gcnew System::Drawing::Font(L"Arial Black", 8.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->label1->Location = System::Drawing::Point(557, 376);
			this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(72, 32);
			this->label1->TabIndex = 34;
			this->label1->Text = L"Number Games";
			// 
			// number_games_textbox
			// 
			this->number_games_textbox->BackColor = System::Drawing::Color::Transparent;
			this->number_games_textbox->ForeColor = System::Drawing::SystemColors::ControlText;
			this->number_games_textbox->Location = System::Drawing::Point(616, 380);
            this->number_games_textbox->Name = L"number_games_textbox";
			this->number_games_textbox->Size = System::Drawing::Size(100, 28);
			this->number_games_textbox->TabIndex = 35;
            this->number_games_textbox->Text = L"0";
			// 
			// Form1
			// 
			this->AutoScaleBaseSize = System::Drawing::Size(5, 13);
			this->BackColor = System::Drawing::Color::ForestGreen;
			this->ClientSize = System::Drawing::Size(728, 449);
			this->Controls->Add(this->number_games_textbox);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->nonewins_textbox);
			this->Controls->Add(this->none_wins_label);
			this->Controls->Add(this->card11);
			this->Controls->Add(this->bonus_card4);
			this->Controls->Add(this->bonus_card2);
			this->Controls->Add(this->bonus_card3);
			this->Controls->Add(this->bonus_card1);
			this->Controls->Add(this->card1);
			this->Controls->Add(this->card10);
			this->Controls->Add(this->card9);
			this->Controls->Add(this->card8);
			this->Controls->Add(this->card5);
			this->Controls->Add(this->card4);
			this->Controls->Add(this->card6);
			this->Controls->Add(this->card7);
			this->Controls->Add(this->card3);
			this->Controls->Add(this->card2);
			this->Controls->Add(this->card_drawn);
			this->Controls->Add(this->main_statusbar);
			this->Controls->Add(this->stay_button);
			this->Controls->Add(this->hit_button);
			this->Controls->Add(this->PlayerScoreLabel);
			this->Controls->Add(this->ComputerScoreLabel);
			this->Controls->Add(this->computerscore_textbox);
			this->Controls->Add(this->playerscore_textbox);
			this->Controls->Add(this->ComputerWinLabel);
			this->Controls->Add(this->PlayerWinLabel);
			this->Controls->Add(this->computerwin_textbox);
			this->Controls->Add(this->playerwin_textbox);
			this->Controls->Add(this->CardCountLabel);
			this->Controls->Add(this->cardcount_textbox);
			this->Controls->Add(this->hand_textbox);
            this->Icon = gcnew System::Drawing::Icon("blackjack.ico");
			this->Menu = this->MainMenu;
			this->Name = L"Form1";
			this->Text = L"Blackjack";
			this->Load += gcnew System::EventHandler(this, &Form1::Form1_Load);
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card_drawn)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card2)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card3)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card7)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card6)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card4)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card5)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card8)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card9)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card10)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card1)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card1)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card3)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card2)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->bonus_card4)->EndInit();
			safe_cast<System::ComponentModel::ISupportInitialize^>(this->card11)->EndInit();
			this->ResumeLayout(false);

		}	

    private: System::Void Form1_Load(System::Object^  sender, System::EventArgs^  e)
			 {
				 // sets the Card Drawn image to back_bj.bmp wich is the 54th item in the cardlist
              card_drawn->Image = cardlist->Images[54];
				 // initilizes some textboxes on the main form to 0
			  computerscore_textbox->Text = L"0";
			  computerwin_textbox->Text = L"0";
			  cardcount_textbox->Text = L"0";
			  playerscore_textbox->Text = L"0";
			  playerwin_textbox->Text = L"0";
			  nonewins_textbox->Text = L"0";
			  number_games_textbox->Text = L"0";
			  hand_textbox->Text = L"";
			  shuffle(); // the function shuffles the constant card array 'deck' from the card.h file
						 // into a new card array called sdeck 'shuffled deck'
			 }




	System::Windows::Forms::ImageList^ LoadCardsImageListFromCardFiles()
	{
		try
		{
			// Create new ImageList
			System::Windows::Forms::ImageList^ imageList = gcnew System::Windows::Forms::ImageList();
			imageList->ImageSize = System::Drawing::Size(71, 96); // Adjust size as needed
			imageList->TransparentColor = System::Drawing::Color::Transparent;

			// Get the path to your cards folder
			System::String^ currentDir = System::IO::Directory::GetCurrentDirectory();
			System::String^ cardsPath = System::IO::Path::Combine(currentDir, "cards");

			// Check if folder exists
			if (!System::IO::Directory::Exists(cardsPath))
			{
				throw gcnew System::IO::DirectoryNotFoundException("Cards folder not found: " + cardsPath);
			}

			// Get all image files from the folder
			cli::array<System::String^>^ imageFiles = System::IO::Directory::GetFiles(cardsPath, "*.bmp");

			// Load each image
			for (int i = 0; i < imageFiles->Length; i++)
			{
				try
				{
					System::Drawing::Image^ img = System::Drawing::Image::FromFile(imageFiles[i]);
					imageList->Images->Add(img);
				}
				catch (System::Exception^ ex)
				{
					// Handle individual file errors but continue loading others
					System::Console::WriteLine("Error loading image " + imageFiles[i] + ": " + ex->Message);
				}
			}

			return imageList;
		}
		catch (System::Exception^ ex)
		{
			System::Windows::Forms::MessageBox::Show("Error loading cards: " + ex->Message);
			return nullptr;
		}
	}
           
			 /*
              Function called when the user clicks on the button
			  labeled hit on the main window of the program
			 */
    private: System::Void hit_button_Click(System::Object^  sender, System::EventArgs^  e)
			 {
			
			  if( new_game_flag == true )
			  {
               setup_newgame();
			   new_game_flag = false;
			  
			  }
			  else
			  { 
			   // if the player has drawn over 11 cards they cannot draw anymore since 11*2 = 22 , obviously 22 is > 21 
               if( pc->check_stay() == true ) { main_statusbar->Text = L"You cannot Draw anymore cards click stay"; return; }
			  }
              main_statusbar->Text = L""; // clears out status bar
			  buffer->str(""); // clears out the buffer
			  pc->deal(sdeck,1); // deals 1 card to the player
			  update_table();
			  display_bonus(); // bonus cards change everytime you hit
			 }

    // File - Exit menu item
	  // called by clicking file then exit
    private: System::Void menuItem3_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		   // pops up a message box       
          System::Windows::Forms::DialogResult x = MessageBox::Show(L"Exit Blackjack ?", L"", MessageBoxButtons::YesNo);
		   // if the user clicks no the while loop ends and the user can go on with the rest of the program

		   // if the user clicks yes Close everything down.
		   if( x == System::Windows::Forms::DialogResult::Yes)
		   {
               save_game(); // function that writes current game information to a file wich is then processed
			                // one the program is ran again
			                // high score table uses this file since the scores are listed in order starting with the highest
			   Application::Exit();
		   }
		 }

		 // Stay_button_click
		 /*
	     called when the player clicks the stay button
		 */
    private: System::Void stay_button_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		  if( new_game_flag == true ) return; // a player has to begin a new game by clicking hit so we don't want this function ran if hit hasn't been clicked yet
		  pc->set_stay(true); // sets players stay value to true
		                     // doesn't mean anything at this time
		  // computers turn
		  while( computer->turn() == false ) computer->deal(sdeck,1);
          calc_winner(); // since the player and computer have stayed we now need to determine the winner of the game
		 }

          // File - New Game menu item
    private: System::Void NewGame_item_Click(System::Object^  sender, System::EventArgs^  e)
		 {
           // calls the newgame setup function
		   setup_newgame();
		 }

          // Help - About menu item
		  /*
           uses the about_box class to create a new about_box
		   and show it to the user the user can then click ok in that dialouge to
		   close it and return to this class
		  */
    private: System::Void menuItem3_Click_1(System::Object^  sender, System::EventArgs^  e)
		 {
			 // declares a new instance of the about_box class wich is a member of the blackjack namespace
			 // then it calls the show function on the new about_box object called about
            blackjack::about_box^ about = gcnew blackjack::about_box();
			 about->Show(); 
			 
		 }


         // Game - undo menu item
    private: System::Void undo_item_Click(System::Object^  sender, System::EventArgs^  e)
		 {
             if( new_game_flag == true ){ main_statusbar->Text = L"You cannot undo a move at this time"; return; }
			 if( pc->tscore() < 10 ) { main_statusbar->Text = L"Score is not high enough to undo last move"; return; }
			 if( pc->get_num_cards() <= 1 ) { main_statusbar->Text = L"Cannot Undo at this time to begin a new game Ctr-N"; return; }
		   main_statusbar->Text = L"Last Move Undone 10 points deducted from score";
		  pc->undo(); // calls an undo function defined by the player class
          clear_cards();   // clears out the cards
		  display_cards(); // redraws the cards based on new information
		  update_stats(); // score needs to be updated so this function is called
		  /* below this line code is also used in the hit function 
		     wich serves the function of updating certain things that need to be updated when the cards change*/
		  	string temp = pc->get_cardlist();  // puts all the abv_name card names into a string and returns it
             hand_textbox->Text = gcnew System::String(temp.c_str());  // sets the string(const char*) to the hand_textbox
			 *buffer << pc->count_cards(); // ostringstream used to convert count_cards wich is the value
			                               // of all the cards added up into a string
			  temp = buffer->str();        // converts buffer into a string
              cardcount_textbox->Text = gcnew System::String(temp.c_str()); // sets cardcount_textbox to the string(const char*)
			  buffer->str(""); // clears out the buffer
          card cur_card = pc->get_currentcard();  // gets the card object for the current card
          card_drawn->Image = cardlist->Images[ cur_card.index ];  // draws the current card
		  display_bonus();
		 }

private: System::Void bonus_card1_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		  if( pc->get_num_cards() >= 12 ) { main_statusbar->Text = L"You cannot Draw anymore cards click stay"; return; }
		  pc->update_score( -15 );
		  pc->add_card( deck[bv[0]-1] );
		  bonus_card1->Image = nullptr;
		  bonus_card1->Hide();
			update_table();
		  update_stats(); // score needs to be updated so this function is called
		 }

private: System::Void bonus_card2_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		 if( pc->get_num_cards() >= 12 ) { main_statusbar->Text = L"You cannot Draw anymore cards click stay"; return; }
		 pc->update_score( -15 );            
		 pc->add_card( deck[bv[1]-1] );
		 bonus_card2->Image = nullptr;
		 bonus_card2->Hide();
		  update_table();
		  update_stats(); // score needs to be updated so this function is called
		 }

private: System::Void bonus_card3_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		 if( pc->get_num_cards() >= 12 ) { main_statusbar->Text = L"You cannot Draw anymore cards click stay"; return; }
		 pc->update_score( -15 );
		 pc->add_card( deck[bv[2]-1] );
		 bonus_card3->Image = nullptr;
		 bonus_card3->Hide();
		  update_table();
		  update_stats(); // score needs to be updated so this function is called
		 }

private: System::Void bonus_card4_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		  if( pc->get_num_cards() >= 12 ) { main_statusbar->Text = L"You cannot Draw anymore cards click stay"; return; }
		  pc->update_score( -15 );
		  pc->add_card( deck[bv[3]-1] );
		  bonus_card4->Image = nullptr;
		  bonus_card4->Hide();
		  update_table();
		  update_stats(); // score needs to be updated so this function is called
		 }

private: System::Void highscore_menu_Click(System::Object^  sender, System::EventArgs^  e)
		 {
		   blackjack::high_score^ score_table = gcnew blackjack::high_score();
		   score_table->Show();
		 }



};
}


