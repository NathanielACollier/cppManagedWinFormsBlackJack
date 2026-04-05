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
	/// </summary>
	public ref class about_box : public System::Windows::Forms::Form
	{
	public: 
		about_box(void)
		{
			buffer = new ostringstream;
			InitializeComponent();
		}
	private: System::Windows::Forms::Label^  version_label;
	private: System::Windows::Forms::Label^  label1;
	private: System::Windows::Forms::Label^  label2;
	private: System::Windows::Forms::Label^  label3;
	private: System::Windows::Forms::Label^  last_change;
	private: System::Windows::Forms::PictureBox^  card5;
	private: System::Windows::Forms::PictureBox^  pictureBox1;
	private: System::Windows::Forms::Label^  label4;





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
    private: System::Windows::Forms::Button^  aboutbox_ok;



	private:
		/// <summary>
		/// Required designer variable.
      /// </summary>
		System::ComponentModel::Container^ components;

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		void InitializeComponent(void)
		{
            System::Resources::ResourceManager^ resources = gcnew System::Resources::ResourceManager(about_box::typeid->FullName, about_box::typeid->Assembly);
			this->aboutbox_ok = gcnew System::Windows::Forms::Button();
			this->version_label = gcnew System::Windows::Forms::Label();
			this->label1 = gcnew System::Windows::Forms::Label();
			this->label2 = gcnew System::Windows::Forms::Label();
			this->label3 = gcnew System::Windows::Forms::Label();
			this->last_change = gcnew System::Windows::Forms::Label();
			this->card5 = gcnew System::Windows::Forms::PictureBox();
			this->pictureBox1 = gcnew System::Windows::Forms::PictureBox();
			this->label4 = gcnew System::Windows::Forms::Label();
			this->SuspendLayout();
			// 
			// aboutbox_ok
			// 
			this->aboutbox_ok->Location = System::Drawing::Point(448, 272);
            this->aboutbox_ok->Name = L"aboutbox_ok";
			this->aboutbox_ok->TabIndex = 0;
			this->aboutbox_ok->Text = L"ok";
			this->aboutbox_ok->Click += gcnew System::EventHandler(this, &blackjack::about_box::aboutbox_ok_Click);
			// 
			// version_label
			// 
            this->version_label->Font = gcnew System::Drawing::Font(L"Arial Black", 14.25F, System::Drawing::FontStyle::Bold, System::Drawing::GraphicsUnit::Point, (System::Byte)0);
			this->version_label->Location = System::Drawing::Point(56, 24);
            this->version_label->Name = L"version_label";
			this->version_label->Size = System::Drawing::Size(416, 32);
			this->version_label->TabIndex = 1;
			this->version_label->TextAlign = System::Drawing::ContentAlignment::MiddleCenter;
			// 
			// label1
			// 
            this->label1->Font = gcnew System::Drawing::Font(L"Arial Black", 11.25F, System::Drawing::FontStyle::Regular, System::Drawing::GraphicsUnit::Point, (System::Byte)0);
			this->label1->Location = System::Drawing::Point(48, 80);
            this->label1->Name = L"label1";
			this->label1->Size = System::Drawing::Size(248, 24);
			this->label1->TabIndex = 2;
            this->label1->Text = L"Written by: Nathaniel Collier";
			// 
			// label2
			// 
			this->label2->Location = System::Drawing::Point(136, 112);
            this->label2->Name = L"label2";
			this->label2->Size = System::Drawing::Size(240, 32);
			this->label2->TabIndex = 3;
            this->label2->Text = L"Console Version Created on Wednesday, August 24, 2005, 2:41:39 PM";
			// 
			// label3
			// 
			this->label3->Location = System::Drawing::Point(136, 152);
            this->label3->Name = L"label3";
			this->label3->Size = System::Drawing::Size(240, 32);
			this->label3->TabIndex = 4;
            this->label3->Text = L"Visual C++ .net version created on Monday, October 17, 2005, 3:02:42 PM";
			// 
			// last_change
			// 
			this->last_change->Location = System::Drawing::Point(136, 200);
            this->last_change->Name = L"last_change";
			this->last_change->Size = System::Drawing::Size(240, 23);
			this->last_change->TabIndex = 5;
			// 
			// card5
			// 
            this->card5->Image = (safe_cast<System::Drawing::Image^>(resources->GetObject(L"card5.Image")));
			this->card5->Location = System::Drawing::Point(392, 120);
            this->card5->Name = L"card5";
			this->card5->Size = System::Drawing::Size(71, 96);
			this->card5->TabIndex = 23;
			this->card5->TabStop = false;
			// 
			// pictureBox1
			// 
            this->pictureBox1->Image = (safe_cast<System::Drawing::Image^>(resources->GetObject(L"pictureBox1.Image")));
			this->pictureBox1->Location = System::Drawing::Point(48, 120);
            this->pictureBox1->Name = L"pictureBox1";
			this->pictureBox1->Size = System::Drawing::Size(71, 96);
			this->pictureBox1->TabIndex = 24;
			this->pictureBox1->TabStop = false;
			// 
			// label4
			// 
			this->label4->Location = System::Drawing::Point(136, 232);
            this->label4->Name = L"label4";
			this->label4->Size = System::Drawing::Size(240, 40);
			this->label4->TabIndex = 25;
			this->label4->Text = L"Standard Card Images obtained from cards.dll bonus cards, jokers and the back of the cards created using thegimp";
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
            this->Icon = (safe_cast<System::Drawing::Icon^>(resources->GetObject(L"$this.Icon")));
			this->MaximizeBox = false;
			this->MinimizeBox = false;
            this->Name = L"about_box";
			this->Text = L"About Blackjack";
			this->Load += gcnew System::EventHandler(this, &blackjack::about_box::about_box_Load);
			this->ResumeLayout(false);

		}		
	      
		// clicking the ok button calls this function
    private: System::Void aboutbox_ok_Click(System::Object^  sender, System::EventArgs^  e)
			 {
				 // closes the about box form
				 this->Hide();
			 }
          
			 // called when the form is loaded
    private: System::Void about_box_Load(System::Object^  sender, System::EventArgs^  e)
			 {
				 string version( string("Blackjack") + " " + BJ_VERSION );
				 string lastchange( string("Last Change on ") + BJ_LASTCHANGE );
				 version_label->Text = gcnew System::String(version.c_str());
				 last_change->Text = gcnew System::String(lastchange.c_str());
			 }

	};
}