using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BlackjackGame
{
    public partial class MainWindow : Window
    {
        private Card[] deckCards;
        private Player playerCharacter;
        private Player computer;
        private Dictionary<int, Bitmap> cardImages;
        private int[] bonusValues;
        private bool newGameFlag = false;

        public MainWindow()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InitializeGame()
        {
            deckCards = new Card[52];
            playerCharacter = new Player(false);
            computer = new Player(true);
            bonusValues = new int[4];
            cardImages = new Dictionary<int, Bitmap>();

            LoadCardImages();
        }

        private void LoadCardImages()
        {
            string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

            if (!Directory.Exists(resourcesPath))
            {
                var statusLabel = this.FindControl<TextBlock>("StatusLabel");
                statusLabel.Text = "Resources directory not found!";
                return;
            }

            // Load all card images with proper index mapping
            for (int i = 0; i < CardDeck.DeckCards.Length; i++)
            {
                Card card = CardDeck.DeckCards[i];
                string imageFile = GetImageFileForCard(card, resourcesPath);

                if (File.Exists(imageFile))
                {
                    try
                    {
                        cardImages[i] = new Bitmap(imageFile);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"Error loading image {imageFile}: {ex.Message}");
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"Image file not found: {imageFile}");
                }
            }
        }

        private string GetImageFileForCard(Card card, string resourcesPath)
        {
            string abbrev = card.AbreviatedName.ToUpper();

            // Handle cards with invalid abbreviated names (bonus cards, blank cards)
            if (abbrev.Length < 2 || abbrev == " ")
            {
                return Path.Combine(resourcesPath, "back_blank.bmp");
            }

            // Handle special joker cards first
            if (abbrev == "JO" || abbrev == "JT")
            {
                return Path.Combine(resourcesPath, abbrev == "JO" ? "joker_one.bmp" : "joker_two.bmp");
            }

            // Map abbreviated name to image filename for regular cards
            string valueStr = abbrev[0] switch
            {
                'A' => "ace",
                '2' => "two",
                '3' => "three",
                '4' => "four",
                '5' => "five",
                '6' => "six",
                '7' => "seven",
                '8' => "eight",
                '9' => "nine",
                'T' => "ten",
                'J' => "jack",
                'Q' => "queen",
                'K' => "king",
                _ => "unknown"
            };

            string suitStr = abbrev[1] switch
            {
                'H' => "heart",
                'D' => "diamond",
                'S' => "spade",
                'C' => "club",
                _ => "heart"
            };

            string filename = $"{valueStr}_{suitStr}.bmp";
            return Path.Combine(resourcesPath, filename);
        }

        private string GetSuitString(int type)
        {
            return type switch
            {
                1 => "heart",
                2 => "diamond",
                3 => "spade",
                4 => "club",
                _ => "heart"
            };
        }

        private Bitmap GetCardImage(int index)
        {
            if (cardImages.ContainsKey(index))
            {
                return cardImages[index];
            }
            return null;
        }

        private void DisplayCards()
        {
            int x = playerCharacter.GetNumCards();

            if (x == 1)
                return;

            var images = new[] {
                this.FindControl<Image>("Card1"),
                this.FindControl<Image>("Card2"),
                this.FindControl<Image>("Card3"),
                this.FindControl<Image>("Card4"),
                this.FindControl<Image>("Card5"),
                this.FindControl<Image>("Card6"),
                this.FindControl<Image>("Card7"),
                this.FindControl<Image>("Card8"),
                this.FindControl<Image>("Card9"),
                this.FindControl<Image>("Card10"),
                this.FindControl<Image>("Card11")
            };

            for (int i = 0; i < images.Length; i++)
            {
                if (i + 2 <= x)
                {
                    images[i].Source = GetCardImage(playerCharacter.GetCardIndex(i + 1));
                }
            }
        }

        private void ClearCards()
        {
            var cardDrawn = this.FindControl<Image>("CardDrawn");
            var images = new[] {
                this.FindControl<Image>("Card1"),
                this.FindControl<Image>("Card2"),
                this.FindControl<Image>("Card3"),
                this.FindControl<Image>("Card4"),
                this.FindControl<Image>("Card5"),
                this.FindControl<Image>("Card6"),
                this.FindControl<Image>("Card7"),
                this.FindControl<Image>("Card8"),
                this.FindControl<Image>("Card9"),
                this.FindControl<Image>("Card10"),
                this.FindControl<Image>("Card11")
            };

            var bonusImages = new[] {
                this.FindControl<Image>("BonusCard1"),
                this.FindControl<Image>("BonusCard2"),
                this.FindControl<Image>("BonusCard3"),
                this.FindControl<Image>("BonusCard4")
            };

            foreach (var img in images)
            {
                img.Source = null;
            }

            // Load back card image
            string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            string backCardPath = Path.Combine(resourcesPath, "back_bj.bmp");
            if (File.Exists(backCardPath))
            {
                cardDrawn.Source = new Bitmap(backCardPath);
            }

            foreach (var img in bonusImages)
            {
                img.Source = null;
                img.IsVisible = false;
            }
        }

        private void DisplayBonus()
        {
            int playerScore = playerCharacter.GetTotalScore();

            var bonusImages = new[] {
                this.FindControl<Image>("BonusCard1"),
                this.FindControl<Image>("BonusCard2"),
                this.FindControl<Image>("BonusCard3"),
                this.FindControl<Image>("BonusCard4")
            };

            string resourcesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");
            string bonusCardPath = Path.Combine(resourcesPath, "back_bj.bmp");

            if (playerScore >= 15 && File.Exists(bonusCardPath))
            {
                bonusImages[0].IsVisible = true;
                bonusValues[0] = new Random().Next(55, 65);
                bonusImages[0].Source = new Bitmap(bonusCardPath);
            }

            if (playerScore >= 30 && File.Exists(bonusCardPath))
            {
                bonusImages[1].IsVisible = true;
                bonusValues[1] = new Random().Next(55, 65);
                bonusImages[1].Source = new Bitmap(bonusCardPath);
            }

            if (playerScore >= 45 && File.Exists(bonusCardPath))
            {
                bonusImages[2].IsVisible = true;
                bonusValues[2] = new Random().Next(55, 65);
                bonusImages[2].Source = new Bitmap(bonusCardPath);
            }

            if (playerScore >= 60 && File.Exists(bonusCardPath))
            {
                bonusImages[3].IsVisible = true;
                bonusValues[3] = new Random().Next(55, 65);
                bonusImages[3].Source = new Bitmap(bonusCardPath);
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

        private void OnNewGame(object sender, RoutedEventArgs e)
        {
            newGameFlag = true;

            playerCharacter.NewGame();
            computer.NewGame();

            ClearCards();
            var statusLabel = this.FindControl<TextBlock>("StatusLabel");
            statusLabel.Text = "New Game Started";

            Shuffle();

            playerCharacter.Deal(deckCards, 2);
            computer.Deal(deckCards, 2);

            DisplayCards();

            var playerScoreBox = this.FindControl<TextBlock>("PlayerScoreTextBox");
            var computerScoreBox = this.FindControl<TextBlock>("ComputerScoreTextBox");
            playerScoreBox.Text = playerCharacter.GetTotalScore().ToString();
            computerScoreBox.Text = computer.GetTotalScore().ToString();

            var hitButton = this.FindControl<Button>("HitButton");
            var stayButton = this.FindControl<Button>("StayButton");
            hitButton.IsEnabled = true;
            stayButton.IsEnabled = true;
        }

        private async void OnHit(object sender, RoutedEventArgs e)
        {
            if (!newGameFlag)
                return;

            if (playerCharacter.GetNumCards() < 12)
            {
                var dialog = new DrawCardWindow();
                await dialog.ShowDialog<bool>(this);

                playerCharacter.Deal(deckCards, 1);
                DisplayCards();

                int score = playerCharacter.CountCards();
                var playerScoreBox = this.FindControl<TextBlock>("PlayerScoreTextBox");
                playerScoreBox.Text = score.ToString();

                if (score >= 21)
                {
                    playerCharacter.SetStay(true);
                }

                DisplayBonus();
            }
        }

        private async void OnStay(object sender, RoutedEventArgs e)
        {
            if (!newGameFlag)
                return;

            playerCharacter.SetStay(true);
            var hitButton = this.FindControl<Button>("HitButton");
            var stayButton = this.FindControl<Button>("StayButton");
            hitButton.IsEnabled = false;
            stayButton.IsEnabled = false;

            // Small delay for visual effect
            await Task.Delay(500);

            ComputerTurn();
            DetermineWinner();
        }

        private void ComputerTurn()
        {
            while (!computer.Turn())
            {
                computer.Deal(deckCards, 1);
                var computerScoreBox = this.FindControl<TextBlock>("ComputerScoreTextBox");
                computerScoreBox.Text = computer.CountCards().ToString();
                System.Threading.Thread.Sleep(500);
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

            var statusLabel = this.FindControl<TextBlock>("StatusLabel");
            statusLabel.Text = result;

            var playerWinBox = this.FindControl<TextBlock>("PlayerWinTextBox");
            var computerWinBox = this.FindControl<TextBlock>("ComputerWinTextBox");
            var noneWinBox = this.FindControl<TextBlock>("NoneWinsTextBox");

            playerWinBox.Text = playerCharacter.GetGamesWon().ToString();
            computerWinBox.Text = computer.GetGamesWon().ToString();
            noneWinBox.Text = playerCharacter.GetDrawCount().ToString();

            newGameFlag = false;
        }

        private void OnUndo(object sender, RoutedEventArgs e)
        {
            if (newGameFlag && playerCharacter.GetNumCards() > 0)
            {
                playerCharacter.Undo();
                DisplayCards();
                var playerScoreBox = this.FindControl<TextBlock>("PlayerScoreTextBox");
                playerScoreBox.Text = playerCharacter.CountCards().ToString();
            }
        }

        private async void OnAbout(object sender, RoutedEventArgs e)
        {
            var aboutWindow = new AboutWindow();
            await aboutWindow.ShowDialog<bool>(this);
        }

        private async void OnHighScores(object sender, RoutedEventArgs e)
        {
            var highScoreWindow = new HighScoreWindow();
            await highScoreWindow.ShowDialog<bool>(this);
        }

        private void OnExit(object sender, RoutedEventArgs e)
        {
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

            var lifetime = Application.Current?.ApplicationLifetime;
            if (lifetime != null)
            {
                dynamic d = lifetime;
                d.Shutdown();
            }
        }
    }
}
