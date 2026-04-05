using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace blackjack
{
    public partial class Form1 : Form
    {
        private Card[] sdeck;
        private Player pc;
        private Player computer;
        private StringBuilder buffer;
        private HighScoreEntry current_game;
        private int[] bv;
        private bool new_game_flag;
        private ImageList cardlist;

        private const string SAVE_GAME_FILE = "save_game.txt";
        private const int DECKL = 52;

        private enum PlayerType
        {
            PC = 0,
            COMPUTER = 1
        }

        public Form1()
        {
            sdeck = new Card[DECKL];
            pc = new Player(false);
            computer = new Player(true);
            buffer = new StringBuilder();
            new_game_flag = false;
            bv = new int[4];
            current_game = new HighScoreEntry();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));

            this.cardlist = new System.Windows.Forms.ImageList(this.components);
            this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.gameMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoItem = new System.Windows.Forms.ToolStripMenuItem();
            this.highscoreMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.exitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutItem = new System.Windows.Forms.ToolStripMenuItem();

            this.handTextbox = new System.Windows.Forms.Label();
            this.playerwinTextbox = new System.Windows.Forms.Label();
            this.computerwinTextbox = new System.Windows.Forms.Label();
            this.PlayerWinLabel = new System.Windows.Forms.Label();
            this.ComputerWinLabel = new System.Windows.Forms.Label();
            this.playerscoreTextbox = new System.Windows.Forms.Label();
            this.computerscoreTextbox = new System.Windows.Forms.Label();
            this.ComputerScoreLabel = new System.Windows.Forms.Label();
            this.PlayerScoreLabel = new System.Windows.Forms.Label();
            this.hit_button = new System.Windows.Forms.Button();
            this.stay_button = new System.Windows.Forms.Button();
            this.main_statusbar = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();

            this.card_drawn = new System.Windows.Forms.PictureBox();
            this.card1 = new System.Windows.Forms.PictureBox();
            this.card2 = new System.Windows.Forms.PictureBox();
            this.card3 = new System.Windows.Forms.PictureBox();
            this.card4 = new System.Windows.Forms.PictureBox();
            this.card5 = new System.Windows.Forms.PictureBox();
            this.card6 = new System.Windows.Forms.PictureBox();
            this.card7 = new System.Windows.Forms.PictureBox();
            this.card8 = new System.Windows.Forms.PictureBox();
            this.card9 = new System.Windows.Forms.PictureBox();
            this.card10 = new System.Windows.Forms.PictureBox();
            this.card11 = new System.Windows.Forms.PictureBox();

            this.bonus_card1 = new System.Windows.Forms.PictureBox();
            this.bonus_card2 = new System.Windows.Forms.PictureBox();
            this.bonus_card3 = new System.Windows.Forms.PictureBox();
            this.bonus_card4 = new System.Windows.Forms.PictureBox();

            this.CardCountLabel = new System.Windows.Forms.Label();
            this.cardcount_textbox = new System.Windows.Forms.Label();
            this.none_wins_label = new System.Windows.Forms.Label();
            this.nonewins_textbox = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.number_games_textbox = new System.Windows.Forms.Label();

            ((System.ComponentModel.ISupportInitialize)(this.card_drawn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.card11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card4)).BeginInit();
            this.SuspendLayout();

            // MainMenuStrip
            this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.gameMenu,
                this.helpMenu});
            this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MainMenuStrip.Name = "MainMenuStrip";
            this.MainMenuStrip.Size = new System.Drawing.Size(728, 24);
            this.MainMenuStrip.TabIndex = 0;
            this.MainMenuStrip.Text = "menuStrip1";

            // gameMenu
            this.gameMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.newGameItem,
                this.undoItem,
                this.highscoreMenu,
                this.exitItem});
            this.gameMenu.Name = "gameMenu";
            this.gameMenu.Size = new System.Drawing.Size(50, 20);
            this.gameMenu.Text = "&Game";

            // newGameItem
            this.newGameItem.Name = "newGameItem";
            this.newGameItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.newGameItem.Size = new System.Drawing.Size(152, 22);
            this.newGameItem.Text = "&New Game";
            this.newGameItem.Click += new System.EventHandler(this.NewGame_item_Click);

            // undoItem
            this.undoItem.Name = "undoItem";
            this.undoItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
            this.undoItem.Size = new System.Drawing.Size(152, 22);
            this.undoItem.Text = "&Undo";
            this.undoItem.Click += new System.EventHandler(this.undo_item_Click);

            // highscoreMenu
            this.highscoreMenu.Name = "highscoreMenu";
            this.highscoreMenu.Size = new System.Drawing.Size(152, 22);
            this.highscoreMenu.Text = "&High Score List";
            this.highscoreMenu.Click += new System.EventHandler(this.highscore_menu_Click);

            // exitItem
            this.exitItem.Name = "exitItem";
            this.exitItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.exitItem.Size = new System.Drawing.Size(152, 22);
            this.exitItem.Text = "E&xit";
            this.exitItem.Click += new System.EventHandler(this.menuItem3_Click);

            // helpMenu
            this.helpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.aboutItem});
            this.helpMenu.Name = "helpMenu";
            this.helpMenu.Size = new System.Drawing.Size(44, 20);
            this.helpMenu.Text = "&Help";

            // aboutItem
            this.aboutItem.Name = "aboutItem";
            this.aboutItem.Size = new System.Drawing.Size(152, 22);
            this.aboutItem.Text = "&About";
            this.aboutItem.Click += new System.EventHandler(this.menuItem3_Click_1);

            // hand_textbox
            this.handTextbox.AutoSize = true;
            this.handTextbox.BackColor = System.Drawing.Color.Transparent;
            this.handTextbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.handTextbox.Location = new System.Drawing.Point(528, 40);
            this.handTextbox.Name = "hand_textbox";
            this.handTextbox.Size = new System.Drawing.Size(192, 88);
            this.handTextbox.TabIndex = 0;
            this.handTextbox.Text = "0";

            // playerwin_textbox
            this.playerwinTextbox.AutoSize = true;
            this.playerwinTextbox.BackColor = System.Drawing.Color.Transparent;
            this.playerwinTextbox.ForeColor = System.Drawing.Color.Black;
            this.playerwinTextbox.Location = new System.Drawing.Point(371, 89);
            this.playerwinTextbox.Name = "playerwin_textbox";
            this.playerwinTextbox.Size = new System.Drawing.Size(116, 28);
            this.playerwinTextbox.TabIndex = 4;
            this.playerwinTextbox.Text = "0";

            // computerwin_textbox
            this.computerwinTextbox.AutoSize = true;
            this.computerwinTextbox.BackColor = System.Drawing.Color.Transparent;
            this.computerwinTextbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.computerwinTextbox.Location = new System.Drawing.Point(371, 44);
            this.computerwinTextbox.Name = "computerwin_textbox";
            this.computerwinTextbox.Size = new System.Drawing.Size(116, 28);
            this.computerwinTextbox.TabIndex = 5;
            this.computerwinTextbox.Text = "0";

            // PlayerWinLabel
            this.PlayerWinLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PlayerWinLabel.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerWinLabel.Location = new System.Drawing.Point(280, 80);
            this.PlayerWinLabel.Name = "PlayerWinLabel";
            this.PlayerWinLabel.Size = new System.Drawing.Size(72, 36);
            this.PlayerWinLabel.TabIndex = 6;
            this.PlayerWinLabel.Text = "Player Wins";

            // ComputerWinLabel
            this.ComputerWinLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ComputerWinLabel.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComputerWinLabel.Location = new System.Drawing.Point(280, 39);
            this.ComputerWinLabel.Name = "ComputerWinLabel";
            this.ComputerWinLabel.Size = new System.Drawing.Size(72, 32);
            this.ComputerWinLabel.TabIndex = 7;
            this.ComputerWinLabel.Text = "Computer Wins";

            // playerscore_textbox
            this.playerscoreTextbox.AutoSize = true;
            this.playerscoreTextbox.BackColor = System.Drawing.Color.Transparent;
            this.playerscoreTextbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.playerscoreTextbox.Location = new System.Drawing.Point(120, 89);
            this.playerscoreTextbox.Name = "playerscore_textbox";
            this.playerscoreTextbox.Size = new System.Drawing.Size(116, 28);
            this.playerscoreTextbox.TabIndex = 8;
            this.playerscoreTextbox.Text = "0";

            // computerscore_textbox
            this.computerscoreTextbox.AutoSize = true;
            this.computerscoreTextbox.BackColor = System.Drawing.Color.Transparent;
            this.computerscoreTextbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.computerscoreTextbox.Location = new System.Drawing.Point(120, 44);
            this.computerscoreTextbox.Name = "computerscore_textbox";
            this.computerscoreTextbox.Size = new System.Drawing.Size(116, 28);
            this.computerscoreTextbox.TabIndex = 9;
            this.computerscoreTextbox.Text = "0";

            // ComputerScoreLabel
            this.ComputerScoreLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.ComputerScoreLabel.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ComputerScoreLabel.Location = new System.Drawing.Point(16, 39);
            this.ComputerScoreLabel.Name = "ComputerScoreLabel";
            this.ComputerScoreLabel.Size = new System.Drawing.Size(72, 40);
            this.ComputerScoreLabel.TabIndex = 10;
            this.ComputerScoreLabel.Text = "Computer Score";

            // PlayerScoreLabel
            this.PlayerScoreLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.PlayerScoreLabel.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PlayerScoreLabel.Location = new System.Drawing.Point(16, 88);
            this.PlayerScoreLabel.Name = "PlayerScoreLabel";
            this.PlayerScoreLabel.Size = new System.Drawing.Size(72, 40);
            this.PlayerScoreLabel.TabIndex = 11;
            this.PlayerScoreLabel.Text = "Player Score";

            // hit_button
            this.hit_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.hit_button.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.hit_button.Location = new System.Drawing.Point(296, 376);
            this.hit_button.Name = "hit_button";
            this.hit_button.Size = new System.Drawing.Size(104, 40);
            this.hit_button.TabIndex = 12;
            this.hit_button.Text = "Hit";
            this.hit_button.Click += new System.EventHandler(this.hit_button_Click);

            // stay_button
            this.stay_button.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.stay_button.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stay_button.Location = new System.Drawing.Point(416, 376);
            this.stay_button.Name = "stay_button";
            this.stay_button.Size = new System.Drawing.Size(104, 40);
            this.stay_button.TabIndex = 13;
            this.stay_button.Text = "Stay";
            this.stay_button.Click += new System.EventHandler(this.stay_button_Click);

            // main_statusbar
            this.main_statusbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                this.statusLabel});
            this.main_statusbar.Location = new System.Drawing.Point(0, 427);
            this.main_statusbar.Name = "main_statusbar";
            this.main_statusbar.Size = new System.Drawing.Size(728, 22);
            this.main_statusbar.TabIndex = 14;

            // statusLabel
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(713, 17);
            this.statusLabel.Spring = true;
            this.statusLabel.Text = "";

            // card_drawn
            this.card_drawn.Location = new System.Drawing.Point(16, 160);
            this.card_drawn.Name = "card_drawn";
            this.card_drawn.Size = new System.Drawing.Size(71, 96);
            this.card_drawn.TabIndex = 15;
            this.card_drawn.TabStop = false;

            // card1
            this.card1.Location = new System.Drawing.Point(96, 160);
            this.card1.Name = "card1";
            this.card1.Size = new System.Drawing.Size(71, 96);
            this.card1.TabIndex = 26;
            this.card1.TabStop = false;

            // card2
            this.card2.Location = new System.Drawing.Point(176, 160);
            this.card2.Name = "card2";
            this.card2.Size = new System.Drawing.Size(71, 96);
            this.card2.TabIndex = 17;
            this.card2.TabStop = false;

            // card3
            this.card3.Location = new System.Drawing.Point(256, 160);
            this.card3.Name = "card3";
            this.card3.Size = new System.Drawing.Size(71, 96);
            this.card3.TabIndex = 18;
            this.card3.TabStop = false;

            // card4
            this.card4.Location = new System.Drawing.Point(336, 160);
            this.card4.Name = "card4";
            this.card4.Size = new System.Drawing.Size(71, 96);
            this.card4.TabIndex = 21;
            this.card4.TabStop = false;

            // card5
            this.card5.Location = new System.Drawing.Point(416, 160);
            this.card5.Name = "card5";
            this.card5.Size = new System.Drawing.Size(71, 96);
            this.card5.TabIndex = 22;
            this.card5.TabStop = false;

            // card6
            this.card6.Location = new System.Drawing.Point(16, 264);
            this.card6.Name = "card6";
            this.card6.Size = new System.Drawing.Size(71, 96);
            this.card6.TabIndex = 20;
            this.card6.TabStop = false;

            // card7
            this.card7.Location = new System.Drawing.Point(96, 264);
            this.card7.Name = "card7";
            this.card7.Size = new System.Drawing.Size(71, 96);
            this.card7.TabIndex = 19;
            this.card7.TabStop = false;

            // card8
            this.card8.Location = new System.Drawing.Point(176, 264);
            this.card8.Name = "card8";
            this.card8.Size = new System.Drawing.Size(71, 96);
            this.card8.TabIndex = 23;
            this.card8.TabStop = false;

            // card9
            this.card9.Location = new System.Drawing.Point(256, 264);
            this.card9.Name = "card9";
            this.card9.Size = new System.Drawing.Size(71, 96);
            this.card9.TabIndex = 24;
            this.card9.TabStop = false;

            // card10
            this.card10.Location = new System.Drawing.Point(336, 264);
            this.card10.Name = "card10";
            this.card10.Size = new System.Drawing.Size(71, 96);
            this.card10.TabIndex = 25;
            this.card10.TabStop = false;

            // card11
            this.card11.Location = new System.Drawing.Point(416, 264);
            this.card11.Name = "card11";
            this.card11.Size = new System.Drawing.Size(71, 96);
            this.card11.TabIndex = 31;
            this.card11.TabStop = false;

            // bonus_card1
            this.bonus_card1.Location = new System.Drawing.Point(560, 160);
            this.bonus_card1.Name = "bonus_card1";
            this.bonus_card1.Size = new System.Drawing.Size(71, 96);
            this.bonus_card1.TabIndex = 27;
            this.bonus_card1.TabStop = false;
            this.bonus_card1.Click += new System.EventHandler(this.bonus_card1_Click);

            // bonus_card2
            this.bonus_card2.Location = new System.Drawing.Point(640, 160);
            this.bonus_card2.Name = "bonus_card2";
            this.bonus_card2.Size = new System.Drawing.Size(71, 96);
            this.bonus_card2.TabIndex = 29;
            this.bonus_card2.TabStop = false;
            this.bonus_card2.Click += new System.EventHandler(this.bonus_card2_Click);

            // bonus_card3
            this.bonus_card3.Location = new System.Drawing.Point(560, 264);
            this.bonus_card3.Name = "bonus_card3";
            this.bonus_card3.Size = new System.Drawing.Size(71, 96);
            this.bonus_card3.TabIndex = 28;
            this.bonus_card3.TabStop = false;
            this.bonus_card3.Click += new System.EventHandler(this.bonus_card3_Click);

            // bonus_card4
            this.bonus_card4.Location = new System.Drawing.Point(640, 264);
            this.bonus_card4.Name = "bonus_card4";
            this.bonus_card4.Size = new System.Drawing.Size(71, 96);
            this.bonus_card4.TabIndex = 30;
            this.bonus_card4.TabStop = false;
            this.bonus_card4.Click += new System.EventHandler(this.bonus_card4_Click);

            // CardCountLabel
            this.CardCountLabel.BackColor = System.Drawing.Color.ForestGreen;
            this.CardCountLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CardCountLabel.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardCountLabel.Location = new System.Drawing.Point(16, 120);
            this.CardCountLabel.Name = "CardCountLabel";
            this.CardCountLabel.Size = new System.Drawing.Size(96, 24);
            this.CardCountLabel.TabIndex = 3;
            this.CardCountLabel.Text = "Card Count";

            // cardcount_textbox
            this.cardcount_textbox.AutoSize = true;
            this.cardcount_textbox.BackColor = System.Drawing.Color.Transparent;
            this.cardcount_textbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cardcount_textbox.Location = new System.Drawing.Point(120, 120);
            this.cardcount_textbox.Name = "cardcount_textbox";
            this.cardcount_textbox.Size = new System.Drawing.Size(116, 28);
            this.cardcount_textbox.TabIndex = 2;
            this.cardcount_textbox.Text = "0";

            // none_wins_label
            this.none_wins_label.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.none_wins_label.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.none_wins_label.Location = new System.Drawing.Point(280, 128);
            this.none_wins_label.Name = "none_wins_label";
            this.none_wins_label.Size = new System.Drawing.Size(72, 40);
            this.none_wins_label.TabIndex = 32;
            this.none_wins_label.Text = "None       Wins";

            // nonewins_textbox
            this.nonewins_textbox.AutoSize = true;
            this.nonewins_textbox.BackColor = System.Drawing.Color.Transparent;
            this.nonewins_textbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.nonewins_textbox.Location = new System.Drawing.Point(371, 136);
            this.nonewins_textbox.Name = "nonewins_textbox";
            this.nonewins_textbox.Size = new System.Drawing.Size(116, 28);
            this.nonewins_textbox.TabIndex = 33;
            this.nonewins_textbox.Text = "0";

            // label1
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label1.Font = new System.Drawing.Font("Arial Black", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(557, 376);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(72, 32);
            this.label1.TabIndex = 34;
            this.label1.Text = "Number Games";

            // number_games_textbox
            this.number_games_textbox.AutoSize = true;
            this.number_games_textbox.BackColor = System.Drawing.Color.Transparent;
            this.number_games_textbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.number_games_textbox.Location = new System.Drawing.Point(616, 380);
            this.number_games_textbox.Name = "number_games_textbox";
            this.number_games_textbox.Size = new System.Drawing.Size(100, 28);
            this.number_games_textbox.TabIndex = 35;
            this.number_games_textbox.Text = "0";

            // Form1
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(728, 449);
            this.Controls.Add(this.MainMenuStrip);
            this.Controls.Add(this.number_games_textbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nonewins_textbox);
            this.Controls.Add(this.none_wins_label);
            this.Controls.Add(this.card11);
            this.Controls.Add(this.bonus_card4);
            this.Controls.Add(this.bonus_card2);
            this.Controls.Add(this.bonus_card3);
            this.Controls.Add(this.bonus_card1);
            this.Controls.Add(this.card1);
            this.Controls.Add(this.card10);
            this.Controls.Add(this.card9);
            this.Controls.Add(this.card8);
            this.Controls.Add(this.card5);
            this.Controls.Add(this.card4);
            this.Controls.Add(this.card6);
            this.Controls.Add(this.card7);
            this.Controls.Add(this.card3);
            this.Controls.Add(this.card2);
            this.Controls.Add(this.card_drawn);
            this.Controls.Add(this.main_statusbar);
            this.Controls.Add(this.stay_button);
            this.Controls.Add(this.hit_button);
            this.Controls.Add(this.PlayerScoreLabel);
            this.Controls.Add(this.ComputerScoreLabel);
            this.Controls.Add(this.computerscoreTextbox);
            this.Controls.Add(this.playerscoreTextbox);
            this.Controls.Add(this.ComputerWinLabel);
            this.Controls.Add(this.PlayerWinLabel);
            this.Controls.Add(this.computerwinTextbox);
            this.Controls.Add(this.playerwinTextbox);
            this.Controls.Add(this.CardCountLabel);
            this.Controls.Add(this.cardcount_textbox);
            this.Controls.Add(this.handTextbox);
            this.MainMenuStrip = this.MainMenuStrip;
            this.Name = "Form1";
            this.Text = "Blackjack";
            this.Load += new System.EventHandler(this.Form1_Load);

            ((System.ComponentModel.ISupportInitialize)(this.card_drawn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.card11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bonus_card4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void DisposePictureBoxes()
        {
            card_drawn?.Dispose();
            card1?.Dispose();
            card2?.Dispose();
            card3?.Dispose();
            card4?.Dispose();
            card5?.Dispose();
            card6?.Dispose();
            card7?.Dispose();
            card8?.Dispose();
            card9?.Dispose();
            card10?.Dispose();
            card11?.Dispose();
            bonus_card1?.Dispose();
            bonus_card2?.Dispose();
            bonus_card3?.Dispose();
            bonus_card4?.Dispose();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                DisposePictureBoxes();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Shuffle()
        {
            int cur_card = 0;
            int prev_card = 0;

            for (int i = RNUM(1, 50); i < 50; ++i)
            {
                for (int j = 0; j < DECKL; ++j)
                {
                    while (cur_card == prev_card)
                    {
                        cur_card = RNUM(0, DECKL - 1);
                    }
                    sdeck[j] = CardDeck.DeckCards[cur_card];
                    prev_card = cur_card;
                }
            }
        }

        private int CurrentGameScore(PlayerType id)
        {
            int score = 0;
            switch (id)
            {
                case PlayerType.PC:
                    score = (pc.GetGamesWon() - computer.GetGamesWon());
                    if (score <= 0)
                    {
                        score = 21;
                    }
                    else
                    {
                        score *= 21;
                    }
                    break;
                case PlayerType.COMPUTER:
                    score = (computer.GetGamesWon() - pc.GetGamesWon());
                    if (score <= 0)
                    {
                        score = 21;
                    }
                    else
                    {
                        score *= 21;
                    }
                    break;
                default:
                    return 0;
            }
            return score;
        }

        private void CalcScore(int winner)
        {
            int pcs = pc.GetTotalScore();
            int ccs = computer.GetTotalScore();
            int pcc = pc.CountCards();
            int ccc = computer.CountCards();

            if (winner == 1) pc.SetTotalScore(pcs + CurrentGameScore(PlayerType.PC));
            if (winner == 2) computer.SetTotalScore(ccs + CurrentGameScore(PlayerType.COMPUTER));
            if (winner == 0)
            {
                pc.SetTotalScore(pcs - ((pcc > 21) ? (21 - pcc) : 0));
                computer.SetTotalScore(ccs - ((ccc > 21) ? (21 - ccc) : 0));
            }
        }

        private void UpdateStats()
        {
            string player_win, comp_win, none_win, player_score, comp_score, number_games;
            buffer.Clear();
            buffer.Append(pc.GetGamesWon());
            player_win = buffer.ToString();
            buffer.Clear();
            buffer.Append(computer.GetGamesWon());
            comp_win = buffer.ToString();
            buffer.Clear();
            buffer.Append(pc.GetTotalScore());
            player_score = buffer.ToString();
            buffer.Clear();
            buffer.Append(computer.GetTotalScore());
            comp_score = buffer.ToString();
            buffer.Clear();
            buffer.Append(pc.GetDrawCount());
            none_win = buffer.ToString();
            buffer.Clear();
            buffer.Append(pc.GetGamesWon() + computer.GetGamesWon() + pc.GetDrawCount());
            number_games = buffer.ToString();
            buffer.Clear();

            playerscoreTextbox.Text = player_score;
            playerwinTextbox.Text = player_win;
            computerscoreTextbox.Text = comp_score;
            computerwinTextbox.Text = comp_win;
            nonewins_textbox.Text = none_win;
            number_games_textbox.Text = number_games;
        }

        private void OutWinner(int x)
        {
            string temp;
            buffer.Clear();
            switch (x)
            {
                case 1:
                    buffer.Append("Winner:           Player\n");
                    buffer.Append("Computers Cards:  " + computer.GetCardlist() + "\n");
                    buffer.Append("Computers Points: " + computer.CountCards() + "\n");
                    buffer.Append("Computers Score:  " + computer.GetTotalScore() + "\n");
                    buffer.Append("Computer Wins:    " + computer.GetGamesWon() + "\n");
                    break;
                case 2:
                    buffer.Append("Winner:           Computer\n");
                    buffer.Append("Computers Cards:  " + computer.GetCardlist() + "\n");
                    buffer.Append("Computers Points: " + computer.CountCards() + "\n");
                    buffer.Append("Computers Score:  " + computer.GetTotalScore() + "\n");
                    buffer.Append("Computer Wins:    " + computer.GetGamesWon() + "\n");
                    break;
                case 3:
                    buffer.Append("Winner:           None\n");
                    buffer.Append("Computers Cards:  " + computer.GetCardlist() + "\n");
                    buffer.Append("Computers Points: " + computer.CountCards() + "\n");
                    buffer.Append("Computers Score:  " + computer.GetTotalScore() + "\n");
                    buffer.Append("Computer Wins:    " + computer.GetGamesWon() + "\n");
                    break;
            }
            statusLabel.Text = " Press Hit to start new game ";
            temp = buffer.ToString();
            handTextbox.Text = temp;
            buffer.Clear();
        }

        private void SetupNewgame()
        {
            pc.NewGame();
            computer.NewGame();
            cardcount_textbox.Text = "0";
            buffer.Clear();
            ClearCards();
        }

        private void UpdateTable()
        {
            string temp = pc.GetCardlist();
            handTextbox.Text = temp;
            buffer.Clear();
            buffer.Append(pc.CountCards());
            temp = buffer.ToString();
            cardcount_textbox.Text = temp;
            buffer.Clear();
            Card cur_card = pc.GetCurrentCard();
            card_drawn.Image = cardlist.Images[cur_card.Index];
            DisplayCards();
        }

        private void CalcWinner()
        {
            int player_score = pc.CountCards();
            int computer_score = computer.CountCards();
            int winner = 0;

            if (player_score <= 21 && computer_score <= 21)
            {
                if (player_score == 21 && computer_score == 21) winner = 0;
                else if (player_score == 21 && computer_score < 21) winner = 1;
                else if (player_score < 21 && computer_score == 21) winner = 2;
                else if (player_score < 21 && computer_score < 21)
                {
                    if (player_score > computer_score) winner = 1;
                    else if (player_score == computer_score) winner = 0;
                    else winner = 2;
                }
            }
            else
            {
                if (player_score > 21 && computer_score == 21) winner = 2;
                else if (player_score == 21 && computer_score > 21) winner = 1;
                else if (player_score < 21 && computer_score > 21) winner = 1;
                else if (player_score > 21 && computer_score < 21) winner = 2;
                else winner = 0;
            }

            if (winner == 1) pc.SetGamesWon(pc.GetGamesWon() + 1);
            if (winner == 2) computer.SetGamesWon(computer.GetGamesWon() + 1);
            if (winner == 0) pc.SetDrawCount(pc.GetDrawCount() + 1);

            CalcScore(winner);
            UpdateStats();

            switch (winner) { case 1: OutWinner(1); break; case 2: OutWinner(2); break; case 0: OutWinner(3); break; }

            new_game_flag = true;
            Shuffle();
        }

        private void DisplayCards()
        {
            int x = pc.GetNumCards();

            if (x == 1) return;
            if (x >= 2) { card1.Image = cardlist.Images[pc.GetCardIndex(0)]; }
            if (x >= 3) { card2.Image = cardlist.Images[pc.GetCardIndex(1)]; }
            if (x >= 4) { card3.Image = cardlist.Images[pc.GetCardIndex(2)]; }
            if (x >= 5) { card4.Image = cardlist.Images[pc.GetCardIndex(3)]; }
            if (x >= 6) { card5.Image = cardlist.Images[pc.GetCardIndex(4)]; }
            if (x >= 7) { card6.Image = cardlist.Images[pc.GetCardIndex(5)]; }
            if (x >= 8) { card7.Image = cardlist.Images[pc.GetCardIndex(6)]; }
            if (x >= 9) { card8.Image = cardlist.Images[pc.GetCardIndex(7)]; }
            if (x >= 10) { card9.Image = cardlist.Images[pc.GetCardIndex(8)]; }
            if (x >= 11) { card10.Image = cardlist.Images[pc.GetCardIndex(9)]; }
            if (x >= 12) { card11.Image = cardlist.Images[pc.GetCardIndex(10)]; }
        }

        private void ClearCards()
        {
            card_drawn.Image = cardlist.Images[54];
            card1.Image = null;
            card2.Image = null;
            card3.Image = null;
            card4.Image = null;
            card5.Image = null;
            card6.Image = null;
            card7.Image = null;
            card8.Image = null;
            card9.Image = null;
            card10.Image = null;
            card11.Image = null;
            bonus_card1.Hide();
            bonus_card2.Hide();
            bonus_card3.Hide();
            bonus_card4.Hide();
            bonus_card1.Image = null;
            bonus_card2.Image = null;
            bonus_card3.Image = null;
            bonus_card4.Image = null;
        }

        private void DisplayBonus()
        {
            int pscore = pc.CountCards();
            if (pscore >= 15)
            {
                bonus_card1.Show();
                bv[0] = RNUM(55, 64);
                bonus_card1.Image = cardlist.Images[bv[0]];
            }
            if (pscore >= 30)
            {
                bonus_card2.Show();
                bv[1] = RNUM(55, 64);
                bonus_card2.Image = cardlist.Images[bv[1]];
            }
            if (pscore >= 45)
            {
                bonus_card3.Show();
                bv[2] = RNUM(55, 64);
                bonus_card3.Image = cardlist.Images[bv[2]];
            }
            if (pscore >= 60)
            {
                bonus_card4.Show();
                bv[3] = RNUM(55, 64);
                bonus_card4.Image = cardlist.Images[bv[3]];
            }
        }

        private void WriteSaveGame()
        {
            try
            {
                using (StreamWriter fout = new StreamWriter(SAVE_GAME_FILE, true))
                {
                    fout.WriteLine(current_game.Date);
                    fout.WriteLine(current_game.PlayerName);
                    fout.WriteLine($"{current_game.PlayerScore} {current_game.ComputerScore} {current_game.PlayerWin} {current_game.ComputerWin} {current_game.NoneWin}");
                }
            }
            catch
            {
                // The file should open but if it doesn't, usually some kind of hardware problem do something here
            }
        }

        private void SaveGame()
        {
            current_game.ComputerScore = computer.GetTotalScore();
            current_game.ComputerWin = computer.GetGamesWon();
            current_game.Date = "Day, Month 00, 0000";
            current_game.NoneWin = pc.GetDrawCount();
            current_game.PlayerName = "Player Name";
            current_game.PlayerScore = pc.GetTotalScore();
            current_game.PlayerWin = pc.GetGamesWon();

            WriteSaveGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCardImages();
            card_drawn.Image = cardlist.Images[54];
            computerscoreTextbox.Text = "0";
            computerwinTextbox.Text = "0";
            cardcount_textbox.Text = "0";
            playerscoreTextbox.Text = "0";
            playerwinTextbox.Text = "0";
            nonewins_textbox.Text = "0";
            number_games_textbox.Text = "0";
            handTextbox.Text = "";
            Shuffle();
        }

        private void hit_button_Click(object sender, EventArgs e)
        {
            if (new_game_flag == true)
            {
                SetupNewgame();
                new_game_flag = false;
            }
            else
            {
                if (pc.CheckStay() == true) { statusLabel.Text = "You cannot Draw anymore cards click stay"; return; }
            }
            statusLabel.Text = "";
            buffer.Clear();
            pc.Deal(sdeck, 1);
            UpdateTable();
            DisplayBonus();
        }

        private void menuItem3_Click(object sender, EventArgs e)
        {
            int x = MessageBox.Show("Exit Blackjack ?", "", MessageBoxButtons.YesNo);

            if (x == (int)DialogResult.Yes)
            {
                SaveGame();
                Application.Exit();
            }
        }

        private void stay_button_Click(object sender, EventArgs e)
        {
            if (new_game_flag == true) return;
            pc.SetStay(true);
            while (computer.Turn() == false) computer.Deal(sdeck, 1);
            CalcWinner();
        }

        private void NewGame_item_Click(object sender, EventArgs e)
        {
            SetupNewgame();
        }

        private void menuItem3_Click_1(object sender, EventArgs e)
        {
            AboutBox about = new AboutBox();
            about.Show();
        }

        private void undo_item_Click(object sender, EventArgs e)
        {
            if (new_game_flag == true) { statusLabel.Text = "You cannot undo a move at this time"; return; }
            if (pc.GetTotalScore() < 10) { statusLabel.Text = "Score is not high enough to undo last move"; return; }
            if (pc.GetNumCards() <= 1) { statusLabel.Text = "Cannot Undo at this time to begin a new game Ctr-N"; return; }
            statusLabel.Text = "Last Move Undone 10 points deducted from score";
            pc.Undo();
            ClearCards();
            DisplayCards();
            UpdateStats();
            string temp = pc.GetCardlist();
            handTextbox.Text = temp;
            buffer.Clear();
            buffer.Append(pc.CountCards());
            temp = buffer.ToString();
            cardcount_textbox.Text = temp;
            buffer.Clear();
            Card cur_card = pc.GetCurrentCard();
            card_drawn.Image = cardlist.Images[cur_card.Index];
            DisplayBonus();
        }

        private void bonus_card1_Click(object sender, EventArgs e)
        {
            if (pc.GetNumCards() >= 12) { statusLabel.Text = "You cannot Draw anymore cards click stay"; return; }
            pc.UpdateScore(-15);
            pc.AddCard(CardDeck.DeckCards[bv[0] - 1]);
            bonus_card1.Image = null;
            bonus_card1.Hide();
            UpdateTable();
            UpdateStats();
        }

        private void bonus_card2_Click(object sender, EventArgs e)
        {
            if (pc.GetNumCards() >= 12) { statusLabel.Text = "You cannot Draw anymore cards click stay"; return; }
            pc.UpdateScore(-15);
            pc.AddCard(CardDeck.DeckCards[bv[1] - 1]);
            bonus_card2.Image = null;
            bonus_card2.Hide();
            UpdateTable();
            UpdateStats();
        }

        private void bonus_card3_Click(object sender, EventArgs e)
        {
            if (pc.GetNumCards() >= 12) { statusLabel.Text = "You cannot Draw anymore cards click stay"; return; }
            pc.UpdateScore(-15);
            pc.AddCard(CardDeck.DeckCards[bv[2] - 1]);
            bonus_card3.Image = null;
            bonus_card3.Hide();
            UpdateTable();
            UpdateStats();
        }

        private void bonus_card4_Click(object sender, EventArgs e)
        {
            if (pc.GetNumCards() >= 12) { statusLabel.Text = "You cannot Draw anymore cards click stay"; return; }
            pc.UpdateScore(-15);
            pc.AddCard(CardDeck.DeckCards[bv[3] - 1]);
            bonus_card4.Image = null;
            bonus_card4.Hide();
            UpdateTable();
            UpdateStats();
        }

        private void highscore_menu_Click(object sender, EventArgs e)
        {
            HighScoreForm score_table = new HighScoreForm();
            score_table.Show();
        }

        private static int RNUM(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }

        private void LoadCardImages()
        {
            string[] cardImageNames = new string[]
            {
                "ace_heart.bmp", "ace_diamond.bmp", "ace_spade.bmp", "ace_club.bmp",
                "two_heart.bmp", "two_diamond.bmp", "two_spade.bmp", "two_club.bmp",
                "three_heart.bmp", "three_diamond.bmp", "three_spade.bmp", "three_club.bmp",
                "four_heart.bmp", "four_diamond.bmp", "four_spade.bmp", "four_club.bmp",
                "five_heart.bmp", "five_diamond.bmp", "five_spade.bmp", "five_club.bmp",
                "six_heart.bmp", "six_diamond.bmp", "six_spade.bmp", "six_club.bmp",
                "seven_heart.bmp", "seven_diamond.bmp", "seven_spade.bmp", "seven_club.bmp",
                "eight_heart.bmp", "eight_diamond.bmp", "eight_spade.bmp", "eight_club.bmp",
                "nine_heart.bmp", "nine_diamond.bmp", "nine_spade.bmp", "nine_club.bmp",
                "ten_heart.bmp", "ten_diamond.bmp", "ten_spade.bmp", "ten_club.bmp",
                "jack_heart.bmp", "jack_diamond.bmp", "jack_spade.bmp", "jack_club.bmp",
                "queen_heart.bmp", "queen_diamond.bmp", "queen_spade.bmp", "queen_club.bmp",
                "king_heart.bmp", "king_diamond.bmp", "king_spade.bmp", "king_club.bmp",
                "joker_one.bmp", "joker_two.bmp",
                "back_bj.bmp",
                "back_bj.bmp", "back_bj.bmp", "back_bj.bmp", "back_bj.bmp", "back_bj.bmp",
                "back_bj.bmp", "back_bj.bmp", "back_bj.bmp", "back_bj.bmp"
            };

            string resourcePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            cardlist.ImageSize = new Size(71, 96);
            cardlist.ColorDepth = ColorDepth.Depth24Bit;

            foreach (string imageName in cardImageNames)
            {
                string imagePath = Path.Combine(resourcePath, imageName);
                if (File.Exists(imagePath))
                {
                    cardlist.Images.Add(Image.FromFile(imagePath));
                }
                else
                {
                    cardlist.Images.Add(null);
                }
            }
        }

        private static int RNUM(int min, int max)
        {
            return new Random().Next(min, max + 1);
        }

        private System.ComponentModel.Container components;
        private MenuStrip MainMenuStrip;
        private ToolStripMenuItem gameMenu;
        private ToolStripMenuItem newGameItem;
        private ToolStripMenuItem undoItem;
        private ToolStripMenuItem highscoreMenu;
        private ToolStripMenuItem exitItem;
        private ToolStripMenuItem helpMenu;
        private ToolStripMenuItem aboutItem;
        private Label handTextbox;
        private Label playerwinTextbox;
        private Label computerwinTextbox;
        private Label PlayerWinLabel;
        private Label ComputerWinLabel;
        private Label playerscoreTextbox;
        private Label computerscoreTextbox;
        private Label ComputerScoreLabel;
        private Label PlayerScoreLabel;
        private Button hit_button;
        private Button stay_button;
        private StatusStrip main_statusbar;
        private ToolStripStatusLabel statusLabel;
        private PictureBox card_drawn;
        private PictureBox card1;
        private PictureBox card2;
        private PictureBox card3;
        private PictureBox card4;
        private PictureBox card5;
        private PictureBox card6;
        private PictureBox card7;
        private PictureBox card8;
        private PictureBox card9;
        private PictureBox card10;
        private PictureBox card11;
        private PictureBox bonus_card1;
        private PictureBox bonus_card2;
        private PictureBox bonus_card3;
        private PictureBox bonus_card4;
        private Label CardCountLabel;
        private Label cardcount_textbox;
        private Label none_wins_label;
        private Label nonewins_textbox;
        private Label label1;
        private Label number_games_textbox;
    }
}
