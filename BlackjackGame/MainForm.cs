using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace BlackjackGame
{
    public partial class MainForm : Form
    {
        private Card[] deckCards;
        private Player playerCharacter;
        private Player computer;
        private ImageList cardImageList;
        private int[] bonusValues;
        private bool newGameFlag = false;

        public MainForm()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            deckCards = new Card[52];
            playerCharacter = new Player(false);
            computer = new Player(true);
            bonusValues = new int[4];

            LoadCardImages();
        }

        private void LoadCardImages()
        {
            cardImageList = new ImageList
            {
                ImageSize = new Size(71, 96),
                ColorDepth = ColorDepth.Depth32Bit
            };

            string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            if (!Directory.Exists(resourcesPath))
            {
                MessageBox.Show("Resources directory not found!");
                return;
            }

            // Load all card images in the correct order
            string[] imageFiles = Directory.GetFiles(resourcesPath, "*.bmp");
            Array.Sort(imageFiles);

            foreach (string imageFile in imageFiles)
            {
                try
                {
                    // Skip directories
                    if (!File.Exists(imageFile))
                        continue;

                    cardImageList.Images.Add(Image.FromFile(imageFile));
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error loading image {imageFile}: {ex.Message}");
                }
            }

            // Ensure we have enough images loaded
            if (cardImageList.Images.Count < 65)
            {
                MessageBox.Show($"Warning: Only {cardImageList.Images.Count} card images loaded. Expected at least 65.");
            }
        }

        private void DisplayCards()
        {
            int x = playerCharacter.GetNumCards();

            if (x == 1)
                return;

            if (x >= 2)
                card1.Image = GetCardImage(playerCharacter.GetCardIndex(0));
            if (x >= 3)
                card2.Image = GetCardImage(playerCharacter.GetCardIndex(1));
            if (x >= 4)
                card3.Image = GetCardImage(playerCharacter.GetCardIndex(2));
            if (x >= 5)
                card4.Image = GetCardImage(playerCharacter.GetCardIndex(3));
            if (x >= 6)
                card5.Image = GetCardImage(playerCharacter.GetCardIndex(4));
            if (x >= 7)
                card6.Image = GetCardImage(playerCharacter.GetCardIndex(5));
            if (x >= 8)
                card7.Image = GetCardImage(playerCharacter.GetCardIndex(6));
            if (x >= 9)
                card8.Image = GetCardImage(playerCharacter.GetCardIndex(7));
            if (x >= 10)
                card9.Image = GetCardImage(playerCharacter.GetCardIndex(8));
            if (x >= 11)
                card10.Image = GetCardImage(playerCharacter.GetCardIndex(9));
            if (x >= 12)
                card11.Image = GetCardImage(playerCharacter.GetCardIndex(10));
        }

        private Image GetCardImage(int index)
        {
            if (index >= 0 && index < cardImageList.Images.Count)
            {
                return cardImageList.Images[index];
            }
            return null;
        }

        private void ClearCards()
        {
            // Clear card display
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

            // Set card_drawn to back card image if available
            if (cardImageList?.Images.Count > 54)
            {
                cardDrawn.Image = cardImageList.Images[54];
            }

            // Hide bonus cards
            bonusCard1.Hide();
            bonusCard2.Hide();
            bonusCard3.Hide();
            bonusCard4.Hide();

            bonusCard1.Image = null;
            bonusCard2.Image = null;
            bonusCard3.Image = null;
            bonusCard4.Image = null;
        }

        private void DisplayBonus()
        {
            int playerScore = playerCharacter.GetTotalScore();

            if (playerScore >= 15)
            {
                bonusCard1.Show();
                bonusValues[0] = new Random().Next(55, 65);
                bonusCard1.Image = GetCardImage(bonusValues[0]);
            }

            if (playerScore >= 30)
            {
                bonusCard2.Show();
                bonusValues[1] = new Random().Next(55, 65);
                bonusCard2.Image = GetCardImage(bonusValues[1]);
            }

            if (playerScore >= 45)
            {
                bonusCard3.Show();
                bonusValues[2] = new Random().Next(55, 65);
                bonusCard3.Image = GetCardImage(bonusValues[2]);
            }

            if (playerScore >= 60)
            {
                bonusCard4.Show();
                bonusValues[3] = new Random().Next(55, 65);
                bonusCard4.Image = GetCardImage(bonusValues[3]);
            }
        }

        private void Shuffle()
        {
            Deck deck = new Deck();
            deck.Shuffle();

            for (int i = 0; i < 52; ++i)
            {
                if (i < deck.GetLength())
                {
                    deckCards[i] = deck.Draw();
                }
            }
            deck.Dispose();
        }

        private void NewGameMenuClick(object sender, EventArgs e)
        {
            newGameFlag = true;

            playerCharacter.NewGame();
            computer.NewGame();

            ClearCards();
            mainStatusLabel.Text = "New Game Started";

            // Deal initial cards
            Shuffle();

            playerCharacter.Deal(deckCards, 2);
            computer.Deal(deckCards, 2);

            DisplayCards();

            // Update score labels
            playerScoreTextBox.Text = playerCharacter.GetTotalScore().ToString();
            computerScoreTextBox.Text = computer.GetTotalScore().ToString();

            hitButton.Enabled = true;
            stayButton.Enabled = true;
        }

        private void HitButtonClick(object sender, EventArgs e)
        {
            if (!newGameFlag)
                return;

            if (playerCharacter.GetNumCards() < 12)
            {
                Dialog hitDialog = new Dialog();
                if (hitDialog.ShowDialog() == DialogResult.OK)
                {
                    // Deal card to player
                    playerCharacter.Deal(deckCards, 1);
                    DisplayCards();

                    int score = playerCharacter.CountCards();
                    playerScoreTextBox.Text = score.ToString();

                    if (score >= 21)
                    {
                        playerCharacter.SetStay(true);
                    }

                    DisplayBonus();
                }
            }
        }

        private void StayButtonClick(object sender, EventArgs e)
        {
            if (!newGameFlag)
                return;

            playerCharacter.SetStay(true);
            hitButton.Enabled = false;
            stayButton.Enabled = false;

            // Computer's turn
            ComputerTurn();

            // Determine winner
            DetermineWinner();
        }

        private void ComputerTurn()
        {
            while (!computer.Turn())
            {
                computer.Deal(deckCards, 1);
                computerScoreTextBox.Text = computer.CountCards().ToString();
                System.Threading.Thread.Sleep(500); // Delay for visual effect
            }
        }

        private void DetermineWinner()
        {
            int playerScore = playerCharacter.CountCards();
            int computerScore = computer.CountCards();

            string result = "";

            if (playerScore > 21)
            {
                result = "Player Busted! Computer Wins!";
                computer.SetGamesWon(computer.GetGamesWon() + 1);
            }
            else if (computerScore > 21)
            {
                result = "Computer Busted! Player Wins!";
                playerCharacter.SetGamesWon(playerCharacter.GetGamesWon() + 1);
            }
            else if (playerScore > computerScore)
            {
                result = "Player Wins!";
                playerCharacter.SetGamesWon(playerCharacter.GetGamesWon() + 1);
            }
            else if (computerScore > playerScore)
            {
                result = "Computer Wins!";
                computer.SetGamesWon(computer.GetGamesWon() + 1);
            }
            else
            {
                result = "Draw!";
                playerCharacter.SetDrawCount(playerCharacter.GetDrawCount() + 1);
            }

            mainStatusLabel.Text = result;
            playerWinTextBox.Text = playerCharacter.GetGamesWon().ToString();
            computerWinTextBox.Text = computer.GetGamesWon().ToString();
            noneWinsTextBox.Text = playerCharacter.GetDrawCount().ToString();

            newGameFlag = false;
        }

        private void UndoMenuClick(object sender, EventArgs e)
        {
            if (newGameFlag && playerCharacter.GetNumCards() > 0)
            {
                playerCharacter.Undo();
                DisplayCards();
                playerScoreTextBox.Text = playerCharacter.CountCards().ToString();
            }
        }

        private void AboutMenuClick(object sender, EventArgs e)
        {
            AboutDialog aboutDialog = new AboutDialog();
            aboutDialog.ShowDialog();
        }

        private void HighScoreMenuClick(object sender, EventArgs e)
        {
            HighScoreDialog highScoreDialog = new HighScoreDialog();
            highScoreDialog.ShowDialog();
        }

        private void ExitMenuClick(object sender, EventArgs e)
        {
            // Save current game score
            HighScoreEntry gameEntry = new HighScoreEntry
            {
                Date = "Day, Month 00, 0000",
                PlayerName = "Player Name",
                PlayerScore = playerCharacter.GetTotalScore(),
                ComputerScore = computer.GetTotalScore(),
                PlayerWin = playerCharacter.GetGamesWon(),
                ComputerWin = computer.GetGamesWon(),
                NoneWin = playerCharacter.GetDrawCount()
            };

            HighScoreManager.SaveHighScore(gameEntry);
            Application.Exit();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form properties
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Name = "MainForm";
            this.Text = "Blackjack Game v2.0";
            this.FormClosed += (s, e) => ExitMenuClick(null, null);

            // Menu Strip
            MenuStrip menuStrip = new MenuStrip();

            ToolStripMenuItem fileMenu = new ToolStripMenuItem { Text = "&File" };
            ToolStripMenuItem newGameItem = new ToolStripMenuItem { Text = "&New Game" };
            newGameItem.Click += NewGameMenuClick;
            fileMenu.DropDownItems.Add(newGameItem);

            ToolStripMenuItem undoItem = new ToolStripMenuItem { Text = "&Undo" };
            undoItem.Click += UndoMenuClick;
            fileMenu.DropDownItems.Add(undoItem);

            ToolStripMenuItem aboutItem = new ToolStripMenuItem { Text = "&About" };
            aboutItem.Click += AboutMenuClick;
            fileMenu.DropDownItems.Add(aboutItem);

            ToolStripMenuItem highScoreItem = new ToolStripMenuItem { Text = "&High Scores" };
            highScoreItem.Click += HighScoreMenuClick;
            fileMenu.DropDownItems.Add(highScoreItem);

            ToolStripMenuItem exitItem = new ToolStripMenuItem { Text = "E&xit" };
            exitItem.Click += ExitMenuClick;
            fileMenu.DropDownItems.Add(exitItem);

            menuStrip.Items.Add(fileMenu);
            this.Controls.Add(menuStrip);
            this.MainMenuStrip = menuStrip;

            // Status Strip
            StatusStrip statusStrip = new StatusStrip();
            mainStatusLabel = new ToolStripStatusLabel { Text = "Ready to Play" };
            statusStrip.Items.Add(mainStatusLabel);
            this.Controls.Add(statusStrip);

            // Card Picture Boxes
            cardDrawn = new PictureBox { Width = 71, Height = 96, Left = 450, Top = 20 };
            this.Controls.Add(cardDrawn);

            card1 = new PictureBox { Width = 71, Height = 96, Left = 50, Top = 200 };
            card2 = new PictureBox { Width = 71, Height = 96, Left = 130, Top = 200 };
            card3 = new PictureBox { Width = 71, Height = 96, Left = 210, Top = 200 };
            card4 = new PictureBox { Width = 71, Height = 96, Left = 290, Top = 200 };
            card5 = new PictureBox { Width = 71, Height = 96, Left = 370, Top = 200 };
            card6 = new PictureBox { Width = 71, Height = 96, Left = 450, Top = 200 };
            card7 = new PictureBox { Width = 71, Height = 96, Left = 530, Top = 200 };
            card8 = new PictureBox { Width = 71, Height = 96, Left = 610, Top = 200 };
            card9 = new PictureBox { Width = 71, Height = 96, Left = 690, Top = 200 };
            card10 = new PictureBox { Width = 71, Height = 96, Left = 770, Top = 200 };
            card11 = new PictureBox { Width = 71, Height = 96, Left = 850, Top = 200 };

            this.Controls.Add(card1);
            this.Controls.Add(card2);
            this.Controls.Add(card3);
            this.Controls.Add(card4);
            this.Controls.Add(card5);
            this.Controls.Add(card6);
            this.Controls.Add(card7);
            this.Controls.Add(card8);
            this.Controls.Add(card9);
            this.Controls.Add(card10);
            this.Controls.Add(card11);

            // Bonus Cards
            bonusCard1 = new PictureBox { Width = 60, Height = 80, Left = 50, Top = 350 };
            bonusCard2 = new PictureBox { Width = 60, Height = 80, Left = 130, Top = 350 };
            bonusCard3 = new PictureBox { Width = 60, Height = 80, Left = 210, Top = 350 };
            bonusCard4 = new PictureBox { Width = 60, Height = 80, Left = 290, Top = 350 };

            this.Controls.Add(bonusCard1);
            this.Controls.Add(bonusCard2);
            this.Controls.Add(bonusCard3);
            this.Controls.Add(bonusCard4);

            // Hit/Stay Buttons
            hitButton = new Button { Text = "Hit", Width = 80, Height = 40, Left = 450, Top = 400 };
            hitButton.Click += HitButtonClick;
            stayButton = new Button { Text = "Stay", Width = 80, Height = 40, Left = 540, Top = 400 };
            stayButton.Click += StayButtonClick;

            this.Controls.Add(hitButton);
            this.Controls.Add(stayButton);

            // Score Labels
            Label playerScoreLabel = new Label { Text = "Player Score:", Left = 50, Top = 500 };
            playerScoreTextBox = new Label { Text = "0", Left = 150, Top = 500 };
            Label computerScoreLabel = new Label { Text = "Computer Score:", Left = 300, Top = 500 };
            computerScoreTextBox = new Label { Text = "0", Left = 450, Top = 500 };

            this.Controls.Add(playerScoreLabel);
            this.Controls.Add(playerScoreTextBox);
            this.Controls.Add(computerScoreLabel);
            this.Controls.Add(computerScoreTextBox);

            // Win Count Labels
            Label playerWinLabel = new Label { Text = "Player Wins:", Left = 50, Top = 550 };
            playerWinTextBox = new Label { Text = "0", Left = 150, Top = 550 };
            Label computerWinLabel = new Label { Text = "Computer Wins:", Left = 300, Top = 550 };
            computerWinTextBox = new Label { Text = "0", Left = 450, Top = 550 };
            Label noneWinLabel = new Label { Text = "Draws:", Left = 600, Top = 550 };
            noneWinsTextBox = new Label { Text = "0", Left = 700, Top = 550 };

            this.Controls.Add(playerWinLabel);
            this.Controls.Add(playerWinTextBox);
            this.Controls.Add(computerWinLabel);
            this.Controls.Add(computerWinTextBox);
            this.Controls.Add(noneWinLabel);
            this.Controls.Add(noneWinsTextBox);

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // UI Controls
        private ToolStripStatusLabel mainStatusLabel;
        private PictureBox cardDrawn;
        private PictureBox card1, card2, card3, card4, card5, card6, card7, card8, card9, card10, card11;
        private PictureBox bonusCard1, bonusCard2, bonusCard3, bonusCard4;
        private Button hitButton, stayButton;
        private Label playerScoreTextBox, computerScoreTextBox;
        private Label playerWinTextBox, computerWinTextBox;
        private Label noneWinsTextBox;
    }
}
