using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Media;

namespace BlackjackGame
{
    public partial class Form1 : Form
    {
        // Game state variables
        private int[] playerHand = new int[11]; // Player's cards (max 11 cards)
        private int[] computerHand = new int[11]; // Computer's cards (max 11 cards)
        private int playerScore = 0;
        private int computerScore = 0;
        private int playerCardCount = 0;
        private int computerCardCount = 0;
        private bool playerTurn = true;
        private bool gameActive = false;
        private int bonusPoints = 0;
        private int gameRound = 0;
        private int highScore = 0;
        private int playerWins = 0;
        private int computerWins = 0;
        private int ties = 0;
        
        // Card images and data
        private Image[] cardImages = new Image[52];
        private Image[] bonusCardImages = new Image[4];
        private int[] deck = new int[52];
        private int[] bonusCards = new int[4];
        private int deckPosition = 0;
        private bool[] bonusCardUsed = new bool[4];
        
        // Game settings
        private int maxCards = 11;
        private int maxScore = 21;
        private int bonusCost = 15;
        private int bonusCardValue = 0;
        
        // UI elements
        private PictureBox[] playerPictureBoxes = new PictureBox[11];
        private PictureBox[] computerPictureBoxes = new PictureBox[11];
        private PictureBox[] bonusPictureBoxes = new PictureBox[4];
        private Label[] playerScoreLabels = new Label[11];
        private Label[] computerScoreLabels = new Label[11];
        private Button hitButton;
        private Button stayButton;
        private Button newGameButton;
        private Button undoButton;
        private Button highScoreButton;
        private TextBox playerScoreTextBox;
        private TextBox computerScoreTextBox;
        private TextBox bonusPointsTextBox;
        private TextBox gameRoundTextBox;
        private TextBox playerWinsTextBox;
        private TextBox computerWinsTextBox;
        private TextBox tiesTextBox;
        private Label statusLabel;
        private Panel gamePanel;
        private Panel bonusPanel;
        private Panel statsPanel;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileMenuItem;
        private ToolStripMenuItem exitMenuItem;
        private ToolStripMenuItem newGameMenuItem;
        private ToolStripMenuItem helpMenuItem;
        private ToolStripMenuItem aboutMenuItem;
        private ToolStripMenuItem gameMenuItem;
        private ToolStripMenuItem undoMenuItem;
        private ToolStripMenuItem highScoreMenuItem;
        private SoundPlayer soundPlayer;
        private Timer gameTimer;
        private Random random;
        private string gameDataPath = "blackjack_data.txt";
        private bool gamePaused = false;
        
        // Game history for undo functionality
        private Stack<GameState> gameHistory;
        
        // Constructor
        public Form1()
        {
            InitializeComponent();
            InitializeGame();
        }
        
        // Initialize game components
        private void InitializeComponent()
        {
            this.Size = new Size(1000, 700);
            this.Text = "Blackjack Game";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.Green;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            // Initialize random number generator
            random = new Random();
            
            // Initialize game history stack
            gameHistory = new Stack<GameState>();
            
            // Initialize game panel
            gamePanel = new Panel();
            gamePanel.Size = new Size(950, 400);
            gamePanel.Location = new Point(25, 100);
            gamePanel.BackColor = Color.DarkGreen;
            gamePanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(gamePanel);
            
            // Initialize bonus panel
            bonusPanel = new Panel();
            bonusPanel.Size = new Size(950, 100);
            bonusPanel.Location = new Point(25, 520);
            bonusPanel.BackColor = Color.LightGreen;
            bonusPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(bonusPanel);
            
            // Initialize stats panel
            statsPanel = new Panel();
            statsPanel.Size = new Size(950, 80);
            statsPanel.Location = new Point(25, 630);
            statsPanel.BackColor = Color.LightGray;
            statsPanel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(statsPanel);
            
            // Initialize menu strip
            menuStrip = new MenuStrip();
            menuStrip.Size = new Size(1000, 24);
            this.Controls.Add(menuStrip);
            
            // File menu
            fileMenuItem = new ToolStripMenuItem("File");
            menuStrip.Items.Add(fileMenuItem);
            
            newGameMenuItem = new ToolStripMenuItem("New Game");
            newGameMenuItem.Click += new EventHandler(NewGame_Click);
            fileMenuItem.DropDownItems.Add(newGameMenuItem);
            
            exitMenuItem = new ToolStripMenuItem("Exit");
            exitMenuItem.Click += new EventHandler(Exit_Click);
            fileMenuItem.DropDownItems.Add(exitMenuItem);
            
            // Game menu
            gameMenuItem = new ToolStripMenuItem("Game");
            menuStrip.Items.Add(gameMenuItem);
            
            undoMenuItem = new ToolStripMenuItem("Undo");
            undoMenuItem.Click += new EventHandler(Undo_Click);
            gameMenuItem.DropDownItems.Add(undoMenuItem);
            
            highScoreMenuItem = new ToolStripMenuItem("High Score");
            highScoreMenuItem.Click += new EventHandler(HighScore_Click);
            gameMenuItem.DropDownItems.Add(highScoreMenuItem);
            
            // Help menu
            helpMenuItem = new ToolStripMenuItem("Help");
            menuStrip.Items.Add(helpMenuItem);
            
            aboutMenuItem = new ToolStripMenuItem("About");
            aboutMenuItem.Click += new EventHandler(About_Click);
            helpMenuItem.DropDownItems.Add(aboutMenuItem);
            
            // Initialize UI controls
            InitializeControls();
            
            // Load card images
            LoadCardImages();
            
            // Load high score
            LoadHighScore();
        }
        
        // Initialize all game controls
        private void InitializeControls()
        {
            // Initialize player PictureBoxes
            for (int i = 0; i < 11; i++)
            {
                playerPictureBoxes[i] = new PictureBox();
                playerPictureBoxes[i].Size = new Size(80, 120);
                playerPictureBoxes[i].Location = new Point(50 + i * 90, 20);
                playerPictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                playerPictureBoxes[i].BorderStyle = BorderStyle.FixedSingle;
                playerPictureBoxes[i].Image = Properties.Resources.back_bj;
                gamePanel.Controls.Add(playerPictureBoxes[i]);
            }
            
            // Initialize computer PictureBoxes
            for (int i = 0; i < 11; i++)
            {
                computerPictureBoxes[i] = new PictureBox();
                computerPictureBoxes[i].Size = new Size(80, 120);
                computerPictureBoxes[i].Location = new Point(50 + i * 90, 200);
                computerPictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                computerPictureBoxes[i].BorderStyle = BorderStyle.FixedSingle;
                computerPictureBoxes[i].Image = Properties.Resources.back_bj;
                gamePanel.Controls.Add(computerPictureBoxes[i]);
            }
            
            // Initialize bonus PictureBoxes
            for (int i = 0; i < 4; i++)
            {
                bonusPictureBoxes[i] = new PictureBox();
                bonusPictureBoxes[i].Size = new Size(80, 120);
                bonusPictureBoxes[i].Location = new Point(20 + i * 100, 20);
                bonusPictureBoxes[i].SizeMode = PictureBoxSizeMode.StretchImage;
                bonusPictureBoxes[i].BorderStyle = BorderStyle.FixedSingle;
                bonusPictureBoxes[i].Image = Properties.Resources.bonus_card;
                bonusPictureBoxes[i].Click += new EventHandler(BonusCard_Click);
                bonusPanel.Controls.Add(bonusPictureBoxes[i]);
            }
            
            // Initialize status label
            statusLabel = new Label();
            statusLabel.Size = new Size(300, 30);
            statusLabel.Location = new Point(350, 350);
            statusLabel.Text = "Welcome to Blackjack!";
            statusLabel.Font = new Font("Arial", 12, FontStyle.Bold);
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            gamePanel.Controls.Add(statusLabel);
            
            // Initialize buttons
            hitButton = new Button();
            hitButton.Size = new Size(100, 40);
            hitButton.Location = new Point(50, 350);
            hitButton.Text = "Hit";
            hitButton.Font = new Font("Arial", 12, FontStyle.Bold);
            hitButton.Click += new EventHandler(Hit_Click);
            gamePanel.Controls.Add(hitButton);
            
            stayButton = new Button();
            stayButton.Size = new Size(100, 40);
            stayButton.Location = new Point(170, 350);
            stayButton.Text = "Stay";
            stayButton.Font = new Font("Arial", 12, FontStyle.Bold);
            stayButton.Click += new EventHandler(Stay_Click);
            gamePanel.Controls.Add(stayButton);
            
            newGameButton = new Button();
            newGameButton.Size = new Size(100, 40);
            newGameButton.Location = new Point(290, 350);
            newGameButton.Text = "New Game";
            newGameButton.Font = new Font("Arial", 12, FontStyle.Bold);
            newGameButton.Click += new EventHandler(NewGame_Click);
            gamePanel.Controls.Add(newGameButton);
            
            undoButton = new Button();
            undoButton.Size = new Size(100, 40);
            undoButton.Location = new Point(410, 350);
            undoButton.Text = "Undo";
            undoButton.Font = new Font("Arial", 12, FontStyle.Bold);
            undoButton.Click += new EventHandler(Undo_Click);
            gamePanel.Controls.Add(undoButton);
            
            // Initialize score textboxes
            playerScoreTextBox = new TextBox();
            playerScoreTextBox.Size = new Size(80, 30);
            playerScoreTextBox.Location = new Point(600, 20);
            playerScoreTextBox.Text = "0";
            playerScoreTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            playerScoreTextBox.TextAlign = HorizontalAlignment.Center;
            playerScoreTextBox.ReadOnly = true;
            gamePanel.Controls.Add(playerScoreTextBox);
            
            computerScoreTextBox = new TextBox();
            computerScoreTextBox.Size = new Size(80, 30);
            computerScoreTextBox.Location = new Point(600, 200);
            computerScoreTextBox.Text = "0";
            computerScoreTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            computerScoreTextBox.TextAlign = HorizontalAlignment.Center;
            computerScoreTextBox.ReadOnly = true;
            gamePanel.Controls.Add(computerScoreTextBox);
            
            bonusPointsTextBox = new TextBox();
            bonusPointsTextBox.Size = new Size(80, 30);
            bonusPointsTextBox.Location = new Point(700, 20);
            bonusPointsTextBox.Text = "0";
            bonusPointsTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            bonusPointsTextBox.TextAlign = HorizontalAlignment.Center;
            bonusPointsTextBox.ReadOnly = true;
            gamePanel.Controls.Add(bonusPointsTextBox);
            
            gameRoundTextBox = new TextBox();
            gameRoundTextBox.Size = new Size(80, 30);
            gameRoundTextBox.Location = new Point(700, 60);
            gameRoundTextBox.Text = "0";
            gameRoundTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            gameRoundTextBox.TextAlign = HorizontalAlignment.Center;
            gameRoundTextBox.ReadOnly = true;
            gamePanel.Controls.Add(gameRoundTextBox);
            
            // Initialize stats textboxes
            playerWinsTextBox = new TextBox();
            playerWinsTextBox.Size = new Size(80, 30);
            playerWinsTextBox.Location = new Point(20, 20);
            playerWinsTextBox.Text = "0";
            playerWinsTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            playerWinsTextBox.TextAlign = HorizontalAlignment.Center;
            playerWinsTextBox.ReadOnly = true;
            statsPanel.Controls.Add(playerWinsTextBox);
            
            computerWinsTextBox = new TextBox();
            computerWinsTextBox.Size = new Size(80, 30);
            computerWinsTextBox.Location = new Point(120, 20);
            computerWinsTextBox.Text = "0";
            computerWinsTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            computerWinsTextBox.TextAlign = HorizontalAlignment.Center;
            computerWinsTextBox.ReadOnly = true;
            statsPanel.Controls.Add(computerWinsTextBox);
            
            tiesTextBox = new TextBox();
            tiesTextBox.Size = new Size(80, 30);
            tiesTextBox.Location = new Point(220, 20);
            tiesTextBox.Text = "0";
            tiesTextBox.Font = new Font("Arial", 12, FontStyle.Bold);
            tiesTextBox.TextAlign = HorizontalAlignment.Center;
            tiesTextBox.ReadOnly = true;
            statsPanel.Controls.Add(tiesTextBox);
            
            // Initialize game timer
            gameTimer = new Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += new EventHandler(GameTimer_Tick);
        }
        
        // Load card images from resources
        private void LoadCardImages()
        {
            try
            {
                // Load standard playing cards
                for (int i = 0; i < 52; i++)
                {
                    // This would normally load from actual image files
                    // For demonstration, we'll create placeholder images
                    cardImages[i] = CreateCardImage(i);
                }
                
                // Load bonus card images
                for (int i = 0; i < 4; i++)
                {
                    bonusCardImages[i] = CreateBonusCardImage(i);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading card images: " + ex.Message);
            }
        }
        
        // Create a placeholder card image
        private Image CreateCardImage(int cardIndex)
        {
            Bitmap bitmap = new Bitmap(80, 120);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.White);
                g.DrawRectangle(Pens.Black, 0, 0, 79, 119);
                
                // Draw card value and suit
                int suit = cardIndex / 13;
                int value = cardIndex % 13;
                
                string suitSymbol = "";
                Color suitColor = Color.Black;
                
                switch (suit)
                {
                    case 0: suitSymbol = "♠"; suitColor = Color.Black; break; // Spades
                    case 1: suitSymbol = "♥"; suitColor = Color.Red; break;   // Hearts
                    case 2: suitSymbol = "♦"; suitColor = Color.Red; break;   // Diamonds
                    case 3: suitSymbol = "♣"; suitColor = Color.Black; break; // Clubs
                }
                
                string valueText = "";
                switch (value)
                {
                    case 0: valueText = "A"; break;
                    case 10: valueText = "J"; break;
                    case 11: valueText = "Q"; break;
                    case 12: valueText = "K"; break;
                    default: valueText = (value + 1).ToString(); break;
                }
                
                Font font = new Font("Arial", 12, FontStyle.Bold);
                SizeF size = g.MeasureString(valueText, font);
                g.DrawString(valueText, font, new SolidBrush(suitColor), 5, 5);
                
                Font smallFont = new Font("Arial", 8, FontStyle.Bold);
                SizeF smallSize = g.MeasureString(suitSymbol, smallFont);
                g.DrawString(suitSymbol, smallFont, new SolidBrush(suitColor), 50, 50);
            }
            return bitmap;
        }
        
        // Create a placeholder bonus card image
        private Image CreateBonusCardImage(int cardIndex)
        {
            Bitmap bitmap = new Bitmap(80, 120);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.Clear(Color.Yellow);
                g.DrawRectangle(Pens.Black, 0, 0, 79, 119);
                
                // Draw bonus card symbol
                Font font = new Font("Arial", 16, FontStyle.Bold);
                SizeF size = g.MeasureString("BONUS", font);
                g.DrawString("BONUS", font, Brushes.Black, 5, 50);
                
                Font smallFont = new Font("Arial", 10, FontStyle.Bold);
                g.DrawString("+" + (cardIndex * 5).ToString(), smallFont, Brushes.Black, 5, 20);
            }
            return bitmap;
        }
        
        // Initialize a new game
        private void InitializeGame()
        {
            try
            {
                // Reset game state
                gameRound = 0;
                bonusPoints = 0;
                playerScore = 0;
                computerScore = 0;
                playerHand.Clear();
                computerHand.Clear();
                deck.Clear();
                
                // Create and shuffle deck
                CreateDeck();
                ShuffleDeck();
                
                // Deal initial cards
                DealInitialCards();
                
                // Update UI
                UpdateUI();
                
                // Start game timer
                gameTimer.Start();
                
                statusLabel.Text = "Game started! Player's turn.";
                statusLabel.ForeColor = Color.Black;
                
                // Enable/disable buttons
                hitButton.Enabled = true;
                stayButton.Enabled = true;
                newGameButton.Enabled = true;
                undoButton.Enabled = true;
                
                // Reset bonus cards
                for (int i = 0; i < 4; i++)
                {
                    bonusPictureBoxes[i].Visible = true;
                }
                
                // Save initial game state for undo
                SaveGameState();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing game: " + ex.Message);
            }
        }
        
        // Create a standard deck of 52 cards
        private void CreateDeck()
        {
            deck.Clear();
            for (int suit = 0; suit < 4; suit++)
            {
                for (int value = 0; value < 13; value++)
                {
                    deck.Add(suit * 13 + value);
                }
            }
        }
        
        // Shuffle the deck using Fisher-Yates algorithm
        private void ShuffleDeck()
        {
            Random rand = new Random();
            for (int i = deck.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                int temp = deck[i];
                deck[i] = deck[j];
                deck[j] = temp;
            }
        }
        
        // Deal initial cards to player and computer
        private void DealInitialCards()
        {
            // Deal two cards to player
            playerHand.Add(deck[0]);
            playerHand.Add(deck[1]);
            deck.RemoveAt(0);
            deck.RemoveAt(0);
            
            // Deal two cards to computer
            computerHand.Add(deck[0]);
            computerHand.Add(deck[1]);
            deck.RemoveAt(0);
            deck.RemoveAt(0);
            
            // Calculate initial scores
            playerScore = CalculateScore(playerHand);
            computerScore = CalculateScore(computerHand);
        }
        
        // Calculate score for a hand
        private int CalculateScore(List<int> hand)
        {
            int score = 0;
            int aces = 0;
            
            foreach (int card in hand)
            {
                int value = card % 13;
                if (value >= 10) // Face cards
                    score += 10;
                else if (value == 0) // Ace
                {
                    score += 11;
                    aces++;
                }
                else
                    score += value + 1;
            }
            
            // Adjust for aces if score is over 21
            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }
            
            return score;
        }
        
        // Handle hit button click
        private void Hit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!gameActive || playerTurn == false)
                    return;
                
                // Save current state for undo
                SaveGameState();
                
                // Deal card to player
                playerHand.Add(deck[0]);
                deck.RemoveAt(0);
                
                // Recalculate score
                playerScore = CalculateScore(playerHand);
                
                // Update UI
                UpdateUI();
                
                // Check for bust
                if (playerScore > 21)
                {
                    EndGame("Player busts! Computer wins.");
                    return;
                }
                
                // Check for blackjack
                if (playerScore == 21)
                {
                    statusLabel.Text = "Blackjack! Player wins!";
                    statusLabel.ForeColor = Color.Green;
                    EndGame("Player wins with blackjack!");
                    return;
                }
                
                statusLabel.Text = "Player's turn. Hit or Stay?";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error handling hit: " + ex.Message);
            }
        }
        
        // Handle stay button click
        private void Stay_Click(object sender, EventArgs e)
        {
            try
            {
                if (!gameActive || playerTurn == false)
                    return;
                
                // Save current state for undo
                SaveGameState();
                
                // Switch to computer's turn
                playerTurn = false;
                statusLabel.Text = "Computer's turn...";
                
                // Computer plays
                ComputerTurn();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error handling stay: " + ex.Message);
            }
        }
        
        // Computer's turn logic
        private void ComputerTurn()
        {
            try
            {
                // Computer plays until score is 17 or higher
                while (computerScore < 17)
                {
                    computerHand.Add(deck[0]);
                    deck.RemoveAt(0);
                    computerScore = CalculateScore(computerHand);
                }
                
                // Update UI
                UpdateUI();
                
                // Determine winner
                DetermineWinner();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in computer turn: " + ex.Message);
            }
        }
        
        // Determine the winner of the game
        private void DetermineWinner()
        {
            try
            {
                // Check for computer bust
                if (computerScore > 21)
                {
                    EndGame("Computer busts! Player wins.");
                    return;
                }
                
                // Check for computer blackjack
                if (computerScore == 21 && computerHand.Count == 2)
                {
                    EndGame("Computer wins with blackjack!");
                    return;
                }
                
                // Compare scores
                if (playerScore > computerScore)
                {
                    EndGame("Player wins!");
                }
                else if (computerScore > playerScore)
                {
                    EndGame("Computer wins!");
                }
                else
                {
                    EndGame("It's a tie!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error determining winner: " + ex.Message);
            }
        }
        
        // End the game
        private void EndGame(string message)
        {
            try
            {
                gameActive = false;
                playerTurn = false;
                gameTimer.Stop();
                
                // Update stats
                UpdateStats(message);
                
                statusLabel.Text = message;
                statusLabel.ForeColor = message.Contains("wins") ? Color.Green : Color.Red;
                
                // Disable buttons
                hitButton.Enabled = false;
                stayButton.Enabled = false;
                
                // Update UI
                UpdateUI();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error ending game: " + ex.Message);
            }
        }
        
        // Update the UI with current game state
        private void UpdateUI()
        {
            try
            {
                // Update score textboxes
                playerScoreTextBox.Text = playerScore.ToString();
                computerScoreTextBox.Text = computerScore.ToString();
                bonusPointsTextBox.Text = bonusPoints.ToString();
                gameRoundTextBox.Text = gameRound.ToString();
                
                // Update player hand display
                UpdateHandDisplay(playerHand, 0, 200);
                
                // Update computer hand display
                UpdateHandDisplay(computerHand, 200, 200);
                
                // Update stats
                playerWinsTextBox.Text = playerWins.ToString();
                computerWinsTextBox.Text = computerWins.ToString();
                tiesTextBox.Text = ties.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating UI: " + ex.Message);
            }
        }
        
        // Update hand display on UI
        private void UpdateHandDisplay(List<int> hand, int xStart, int yStart)
        {
            // This would normally update the visual representation of the cards
            // For simplicity, we'll just log it
            Console.WriteLine($"Hand at ({xStart}, {yStart}): {string.Join(", ", hand)}");
        }
        
        // Save current game state for undo functionality
        private void SaveGameState()
        {
            try
            {
                // In a real implementation, this would save the complete game state
                // For demonstration, we'll just save a simple snapshot
                gameStateStack.Add(new GameState
                {
                    PlayerHand = new List<int>(playerHand),
                    ComputerHand = new List<int>(computerHand),
                    PlayerScore = playerScore,
                    ComputerScore = computerScore,
                    BonusPoints = bonusPoints,
                    GameRound = gameRound,
                    PlayerTurn = playerTurn,
                    GameActive = gameActive
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving game state: " + ex.Message);
            }
        }
        
        // Undo last action
        private void Undo_Click(object sender, EventArgs e)
        {
            try
            {
                if (gameStateStack.Count > 0)
                {
                    GameState lastState = gameStateStack.Pop();
                    
                    // Restore game state
                    playerHand = lastState.PlayerHand;
                    computerHand = lastState.ComputerHand;
                    playerScore = lastState.PlayerScore;
                    computerScore = lastState.ComputerScore;
                    bonusPoints = lastState.BonusPoints;
                    gameRound = lastState.GameRound;
                    playerTurn = lastState.PlayerTurn;
                    gameActive = lastState.GameActive;
                    
                    // Update UI
                    UpdateUI();
                    
                    statusLabel.Text = "Action undone. Game restored.";
                }
                else
                {
                    statusLabel.Text = "No actions to undo.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error undoing action: " + ex.Message);
            }
        }
        
        // Update statistics
        private void UpdateStats(string result)
        {
            if (result.Contains("Player wins"))
                playerWins++;
            else if (result.Contains("Computer wins"))
                computerWins++;
            else if (result.Contains("tie"))
                ties++;
        }
        
        // Game timer tick event
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            // This could be used for animations or timed events
            // For now, we'll just update the display
            UpdateUI();
        }
        
        // Bonus card functionality
        private void UseBonusCard(int cardIndex)
        {
            try
            {
                if (cardIndex >= 0 && cardIndex < bonusCards.Count)
                {
                    // Apply bonus card effect
                    switch (bonusCards[cardIndex])
                    {
                        case "Double Points":
                            bonusPoints *= 2;
                            break;
                        case "Extra Card":
                            playerHand.Add(deck[0]);
                            deck.RemoveAt(0);
                            playerScore = CalculateScore(playerHand);
                            break;
                        case "Skip Turn":
                            // Computer skips turn
                            ComputerTurn();
                            break;
                    }
                    
                    // Remove used card
                    bonusCards.RemoveAt(cardIndex);
                    bonusPictureBoxes[cardIndex].Visible = false;
                    
                    // Update UI
                    UpdateUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error using bonus card: " + ex.Message);
            }
        }
        
        // Bonus card types
        private List<string> bonusCards = new List<string>
        {
            "Double Points", "Extra Card", "Skip Turn", "Lucky Draw"
        };
        
        // Bonus card picture boxes
        private List<PictureBox> bonusPictureBoxes = new List<PictureBox>();
        
        // Game state tracking
        private Stack<GameState> gameStateStack = new Stack<GameState>();
        
        // Game variables
        private List<int> deck = new List<int>();
        private List<int> playerHand = new List<int>();
        private List<int> computerHand = new List<int>();
        private int playerScore = 0;
        private int computerScore = 0;
        private int bonusPoints = 0;
        private int gameRound = 0;
        private bool playerTurn = true;
        private bool gameActive = false;
        
        // Statistics
        private int playerWins = 0;
        private int computerWins = 0;
        private int ties = 0;
        
        // Game state class
        private class GameState
        {
            public List<int> PlayerHand { get; set; }
            public List<int> ComputerHand { get; set; }
            public int PlayerScore { get; set; }
            public int ComputerScore { get; set; }
            public int BonusPoints { get; set; }
            public int GameRound { get; set; }
            public bool PlayerTurn { get; set; }
            public bool GameActive { get; set; }
        }
    }
} // End of namespace