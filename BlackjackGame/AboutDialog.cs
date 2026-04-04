using System.Windows.Forms;

namespace BlackjackGame
{
    public class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 250);
            this.Name = "AboutDialog";
            this.Text = "About Blackjack";
            this.StartPosition = FormStartPosition.CenterParent;

            // Version Label
            Label versionLabel = new Label
            {
                Text = "Version 2.0",
                Left = 20,
                Top = 20,
                Width = 360,
                Height = 30,
                Font = new System.Drawing.Font("Arial", 14, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(versionLabel);

            // Description
            Label descriptionLabel = new Label
            {
                Text = "Blackjack Game\n\nA card game where you try to beat the computer by getting 21 or getting closer to 21 than the computer.",
                Left = 20,
                Top = 60,
                Width = 360,
                Height = 100,
                AutoSize = false
            };
            this.Controls.Add(descriptionLabel);

            // Last Change Label
            Label lastChangeLabel = new Label
            {
                Text = "Last Updated: Sunday, October 23, 2005",
                Left = 20,
                Top = 170,
                Width = 360,
                Height = 30
            };
            this.Controls.Add(lastChangeLabel);

            // OK Button
            Button okButton = new Button
            {
                Text = "OK",
                Left = 150,
                Top = 210,
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
