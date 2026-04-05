using System;
using System.Windows.Forms;

namespace blackjack
{
    public partial class HighScoreForm : Form
    {
        public HighScoreForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "High Scores";
            this.ClientSize = new System.Drawing.Size(500, 400);
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.Name = "HighScoreForm";
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.ComponentModel.Container components = null;
    }
}
