using System.Windows.Forms;

namespace BlackjackGame
{
    public class Dialog : Form
    {
        public Dialog()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 100);
            this.Name = "Dialog";
            this.Text = "Drawing Card...";
            this.StartPosition = FormStartPosition.CenterParent;

            Label messageLabel = new Label
            {
                Text = "Drawing a card for you...",
                Left = 20,
                Top = 20,
                Width = 260,
                Height = 30,
                AutoSize = false
            };
            this.Controls.Add(messageLabel);

            Button okButton = new Button
            {
                Text = "OK",
                Left = 110,
                Top = 60,
                Width = 80,
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
