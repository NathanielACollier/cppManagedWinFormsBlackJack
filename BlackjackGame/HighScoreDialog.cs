using System.Collections.Generic;
using System.Windows.Forms;

namespace BlackjackGame
{
    public class HighScoreDialog : Form
    {
        private TextBox scoresTextBox;

        public HighScoreDialog()
        {
            InitializeComponent();
            LoadHighScores();
        }

        private void LoadHighScores()
        {
            List<HighScoreEntry> scores = HighScoreManager.LoadHighScores();
            string scoreTable = HighScoreManager.GetHighScoreTable(scores);
            scoresTextBox.Text = scoreTable;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 450);
            this.Name = "HighScoreDialog";
            this.Text = "SV Blackjack High Score List";
            this.StartPosition = FormStartPosition.CenterParent;

            // Header Labels
            Label dateLabel = new Label
            {
                Text = "Date",
                Left = 0,
                Top = 8,
                Width = 176,
                Height = 32,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold)
            };

            Label nameLabel = new Label
            {
                Text = "Player Name",
                Left = 176,
                Top = 8,
                Width = 176,
                Height = 32,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold)
            };

            Label playerScoreLabel = new Label
            {
                Text = "Player Score",
                Left = 352,
                Top = 8,
                Width = 176,
                Height = 32,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold)
            };

            Label computerScoreLabel = new Label
            {
                Text = "Computer Score",
                Left = 528,
                Top = 8,
                Width = 176,
                Height = 32,
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold)
            };

            this.Controls.Add(dateLabel);
            this.Controls.Add(nameLabel);
            this.Controls.Add(playerScoreLabel);
            this.Controls.Add(computerScoreLabel);

            // Scores Display TextBox
            scoresTextBox = new TextBox
            {
                Left = 31,
                Top = 55,
                Width = 659,
                Height = 330,
                ReadOnly = true,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical,
                Font = new System.Drawing.Font("Courier New", 10F)
            };
            this.Controls.Add(scoresTextBox);

            // OK Button
            Button okButton = new Button
            {
                Text = "OK",
                Left = 300,
                Top = 395,
                Width = 100,
                Height = 30,
                DialogResult = DialogResult.OK
            };
            okButton.Click += (s, e) => this.Close();
            this.Controls.Add(okButton);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
