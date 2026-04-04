using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;

namespace BlackjackGame
{
    public partial class HighScoreWindow : Window
    {
        public HighScoreWindow()
        {
            InitializeComponent();
            LoadHighScores();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void LoadHighScores()
        {
            List<HighScoreEntry> scores = HighScoreManager.LoadHighScores();
            string scoreTable = HighScoreManager.GetHighScoreTable(scores);

            var scoresTextBox = this.FindControl<TextBox>("ScoresTextBox");
            scoresTextBox.Text = scoreTable;
        }

        private void OnClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
