#pragma once
#include <sstream>

using namespace std;

using namespace System;
using namespace System::ComponentModel;
using namespace System::Collections;
using namespace System::Windows::Forms;
using namespace System::Data;
using namespace System::Drawing;

#define BJ_VERSION "2.0"
#define BJ_LASTCHANGE "Sunday, October 23, 2005"


namespace blackjack
{
	/// <summary> 
	/// Summary for about_box
	///
	/// WARNING: If you change the name of this class, you will need to change the 
	///          'Resource File Name' property for the managed resource compiler tool 
	///          associated with all .resx files this class depends on.  Otherwise,
	///          the designers will not be able to interact properly with localized
	///          resources associated with this form.
	/// </summary>
	public __gc class about_box : public System::Windows::Forms::Form
	{
	public: 
		about_box(void)
		{
			buffer = new ostringstream;
			InitializeComponent();
		}
	private: System::Windows::Forms::Label *  version_label;
	private: System::Windows::Forms::Label *  label1;
	private: System::Windows::Forms::Label *  label2;
	private: System::Windows::Forms::Label *  label3;
	private: System::Windows::Forms::Label *  last_change;
	private: System::Windows::Forms::PictureBox *  card5;
	private: System::Windows::Forms::PictureBox *  pictureBox1;
	private: System::Windows::Forms::Label *  label4;





			 ostringstream *buffer;
        
	protected: 
		void Dispose(Boolean disposing)
		{
			if (disposing && components)
			{
				components->Dispose();
			}
			__super::Dispose(disposing);
		}
	private: System::Windows::Forms::Button *  aboutbox_ok;



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
			System::Resources::ResourceManager *  resources = new System::Resources::ResourceManager(__typeof(blackjack::about_box));
			this->aboutbox_ok = new System::Windows::Forms::Button();
			this->version_label = new System::Windows::Forms::Label();
			this->label1 = new System::Windows::Forms::Label();
			this->label2 = new System::Windows::Forms::Label();
			this->label3 = new System::Windows::Forms::Label();
			this->last_change = new System::Windows::Forms::Label();
			this->card5 = new System::Windows::Forms::PictureBox();
			this->pictureBox1 = new System::Windows::Forms::PictureBox();
			this->label4 = new System::Windows::Forms::Label();
			this->SuspendLayout();
			// 
			// aboutbox_ok
			// 
			this->aboutbox_ok->Location = System::Drawing::Point(448, 272);
			this->aboutbox_ok->Name = S"aboutbox_ok";
			this->aboutbox_ok->TabIndex = 0;
			this->aboutbox_ok->Text = S"ok";
			this->aboutbox_ok->Click += new System::EventHandler(this, &blackjack::about_box::aboutbox_ok_Click);
			// 
			// version_label
			// 
			this->version_label->Font = new System::Drawing::Font(S"Arial Black", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, (System::Byte)0);
			this->version_label->Location = System::Drawing::Point(56, 24);
			this->version_label->Name = S"version_label";
			this->version_label->Size = System::Drawing::Size(416, 32);
			this->version_label->TabIndex = 1;
			this->version_label->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// label1
			// 
			this->label1->Font = new System::Drawing::Font(S"Arial Black", 11.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, (System::Byte)0);
			this->label1->Location = System::Drawing::Point(48, 80);
			this->label1->Name = S"label1";
			this->label1->Size = System::Drawing::Size(248, 24);
			this->label1->TabIndex = 2;
			this->label1->Text = S"Written by: Nathaniel Collier";
			// 
			// label2
			// 
			this->label2->Location = System::Drawing::Point(136, 112);
			this->label2->Name = S"label2";
			this->label2->Size = System::Drawing::Size(240, 32);
			this->label2->TabIndex = 3;
			this->label2->Text = S"Console Version Created on Wednesday, August 24, 2005, 2:41:39 PM";
			// 
			// label3
			// 
			this->label3->Location = System::Drawing::Point(136, 152);
			this->label3->Name = S"label3";
			this->label3->Size = System::Drawing::Size(240, 32);
			this->label3->TabIndex = 4;
			this->label3->Text = S"Visual C++ .net version created on Monday, October 17, 2005, 3:02:42 PM";
			// 
			// last_change
			// 
			this->last_change->Location = System::Drawing::Point(136, 200);
			this->last_change->Name = S"last_change";
			this->last_change->Size = System::Drawing::Size(240, 23);
			this->last_change->TabIndex = 5;
			// 
			// card5
			// 
			this->card5->Image = (__try_cast<System::Drawing::Image *  >(resources->GetObject(S"card5.Image")));
			this->card5->Location = System::Drawing::Point(392, 120);
			this->card5->Name = S"card5";
			this->card5->Size = System::Drawing::Size(71, 96);
			this->card5->TabIndex = 23;
			this->card5->TabStop = false;
			// 
			// pictureBox1
			// 
			this->pictureBox1->Image = (__try_cast<System::Drawing::Image *  >(resources->GetObject(S"pictureBox1.Image")));
			this->pictureBox1->Location = System::Drawing::Point(48, 120);
			this->pictureBox1->Name = S"pictureBox1";
			this->pictureBox1->Size = System::Drawing::Size(71, 96);
			this->pictureBox1->TabIndex = 24;
			this->pictureBox1->TabStop = false;
			// 
			// label4
			// 
			this->label4->Location = System::Drawing::Point(136, 232);
			this->label4->Name = S"label4";
			this->label4->Size = System::Drawing::Size(240, 40);
			this->label4->TabIndex = 25;
			this->label4->Text = S"Standard Card Images obtained from cards.dll bonus cards, jokers and the back of " 
				S"the cards created using thegimp";
			// 
			// about_box
			// 
			this->AutoScaleBaseSize = System::Drawing::Size(5, 13);
			this->ClientSize = System::Drawing::Size(536, 310);
			this->Controls->Add(this->label4);
			this->Controls->Add(this->pictureBox1);
			this->Controls->Add(this->card5);
			this->Controls->Add(this->last_change);
			this->Controls->Add(this->label3);
			this->Controls->Add(this->label2);
			this->Controls->Add(this->label1);
			this->Controls->Add(this->version_label);
			this->Controls->Add(this->aboutbox_ok);
			this->Icon = (__try_cast<System::Drawing::Icon *  >(resources->GetObject(S"$this.Icon")));
			this->MaximizeBox = false;
			this->MinimizeBox = false;
			this->Name = S"about_box";
			this->Text = S"About Blackjack";
			this->Load += new System::EventHandler(this, &blackjack::about_box::about_box_Load);
			this->ResumeLayout(false);

		}		
	      
		// clicking the ok button calls this function
	private: System::Void aboutbox_ok_Click(System::Object *  sender, System::EventArgs *  e)
			 {
                 // closes the about box form				
				 about_box::Hide();
			 }
          
			 // called when the form is loaded
	private: System::Void about_box_Load(System::Object *  sender, System::EventArgs *  e)
			 {		
				 string version( "Blackjack"  " "  BJ_VERSION  " ");
				 string lastchange( "Last Change on " BJ_LASTCHANGE );
				 version_label->set_Text( version.c_str() );
				 last_change->set_Text( lastchange.c_str() );
			 }

	};
}