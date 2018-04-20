/*
Some things need to be looked at and changed
High score is not being sorted by highest score when displayed in the program
this needs to be changed

*/

#pragma once

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;

#include <string>
#include <fstream>
#include <list>
#include <sstream>  // buffer needs this file "String Stream STL Class"
using namespace std;

#define SAVE_GAME_FILE "save_game.txt"

struct high_score_entry
{
 string date;
 string player_name; // player name
 int player_score; // player score
 int computer_score; // computer score
 int player_win;
 int computer_win;
 int none_win;
 int number_of_games;

};

namespace blackjack
{
	/// <summary> 
	/// Summary for high_score
	///
	/// WARNING: If you change the name of this class, you will need to change the 
	///          'Resource File Name' property for the managed resource compiler tool 
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public __gc class high_score : public System::Windows::Forms::Form
	{
	public: 
		high_score(void)
		{
			InitializeComponent();
			save_game_list = new list< high_score_entry >;
			temp_game_save = new high_score_entry;
			buffer = new ostringstream;
		
		}

	list< high_score_entry >* save_game_list;
	high_score_entry* temp_game_save;
	ostringstream* buffer;
	
	private: System::Windows::Forms::Label*  high_score_table;


    
    void write_score_table()
	{
	  buffer->str("");
	 
	 list< high_score_entry >::iterator current = save_game_list->begin();
	 list< high_score_entry >::iterator end = save_game_list->end();

	 for(  ; current != end; ++current )
	 {
       *buffer<< " " << current->date << " " << current->player_name << " " << current->player_score << " " << current->computer_score << "\n\n\n";
	 
	 }
     string temp = buffer->str();
	 high_score_table->Text = temp.c_str();
	 buffer->str("");      
	}
			 
	
	void load_save_game()
	{
      ifstream fin;
	  string temp;
	  fin.open( SAVE_GAME_FILE );
	  if( !fin )
	  {
	   // do something if the file did not open
	  }

	  string temp2;

      
	  while( getline( fin, temp_game_save->date ) )
	  {      		  
	   getline( fin,temp_game_save->player_name );	   
	   fin>>temp_game_save->player_score;
	   fin>>temp_game_save->computer_score;
	   fin>>temp_game_save->player_win;
	   fin>>temp_game_save->computer_win;
	   fin>>temp_game_save->none_win;
       getline( fin, temp2 ); // fixs problem with reading the data from the file
	   save_game_list->push_back( *temp_game_save );
	  }

	  write_score_table(); // write the score table to the screen
	}
        
	protected: 
		void Dispose(Boolean disposing)
		{
			if (disposing && components)
			{
				components->Dispose();
			}
			__super::Dispose(disposing);
		}
	private: System::Windows::Forms::Label *  label1;
	private: System::Windows::Forms::Label *  label2;
	private: System::Windows::Forms::Label *  label3;
	private: System::Windows::Forms::Label *  label4;

	private:
		/// <summary>
		/// Required designer variable.
		/// </summary>
		System::ComponentModel::Container* components;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
			this->label1 = (new System::Windows::Forms::Label());
			this->label2 = (new System::Windows::Forms::Label());
			this->label3 = (new System::Windows::Forms::Label());
			this->label4 = (new System::Windows::Forms::Label());
			this->high_score_table = (new System::Windows::Forms::Label());
			this->SuspendLayout();
			// 
			// label1
			// 
			this->label1->Font = (new System::Drawing::Font(S"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->label1->Location = System::Drawing::Point(176, 8);
			this->label1->Name = S"label1";
			this->label1->Size = System::Drawing::Size(176, 32);
			this->label1->TabIndex = 0;
			this->label1->Text = S"Player Name";
			this->label1->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// label2
			// 
			this->label2->Font = (new System::Drawing::Font(S"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->label2->Location = System::Drawing::Point(352, 8);
			this->label2->Name = S"label2";
			this->label2->Size = System::Drawing::Size(176, 32);
			this->label2->TabIndex = 1;
			this->label2->Text = S"Player Score";
			this->label2->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// label3
			// 
			this->label3->Font = (new System::Drawing::Font(S"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->label3->Location = System::Drawing::Point(528, 8);
			this->label3->Name = S"label3";
			this->label3->Size = System::Drawing::Size(176, 32);
			this->label3->TabIndex = 2;
			this->label3->Text = S"Computer Score";
			this->label3->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// label4
			// 
			this->label4->Font = (new System::Drawing::Font(S"Microsoft Sans Serif", 9.75F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, 
				(System::Byte)0));
			this->label4->Location = System::Drawing::Point(0, 8);
			this->label4->Name = S"label4";
			this->label4->Size = System::Drawing::Size(176, 32);
			this->label4->TabIndex = 3;
			this->label4->Text = S"Date";
			this->label4->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// high_score_table
			// 
			this->high_score_table->Location = System::Drawing::Point(31, 55);
			this->high_score_table->Name = S"high_score_table";
			this->high_score_table->Size = System::Drawing::Size(659, 372);
			this->high_score_table->TabIndex = 4;
			// 
			// high_score
			// 
			this->AutoScaleBaseSize = System::Drawing::Size(5, 13);
			this->ClientSize = System::Drawing::Size(704, 441);
			this->Controls->Add(this->high_score_table);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Name = S"high_score";
			this->Text = S"SV Blackjack High Score List";
			this->Load += new System::EventHandler(this, &high_score::high_score_Load);
			this->ResumeLayout(false);

		}		
	private: System::Void high_score_Load(System::Object*  sender, System::EventArgs*  e) 
			 {
			   load_save_game();
			 }
};
}