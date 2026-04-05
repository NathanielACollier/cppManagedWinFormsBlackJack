using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace blackjack
{
    public partial class AboutBox : Form
    {
        private const string BJ_VERSION = "2.0";
        private const string BJ_LASTCHANGE = "Sunday, October 23, 2005";
        private StringBuilder buffer;

        public AboutBox()
        {
            buffer = new StringBuilder();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.aboutbox_ok = new System.Windows.Forms.Button();
            this.version_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.last_change = new System.Windows.Forms.Label();
            this.card5 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.card5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();

            // aboutbox_ok
            this.aboutbox_ok.Location = new System.Drawing.Point(448, 272);
            this.aboutbox_ok.Name = "aboutbox_ok";
            this.aboutbox_ok.TabIndex = 0;
            this.aboutbox_ok.Text = "ok";
            this.aboutbox_ok.Click += new System.EventHandler(this.aboutbox_ok_Click);

            // version_label
            this.version_label.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.version_label.Location = new System.Drawing.Point(56, 24);
            this.version_label.Name = "version_label";
            this.version_label.Size = new System.Drawing.Size(416, 32);
            this.version_label.TabIndex = 1;
            this.version_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.version_label.Text = "BlackJack Version " + BJ_VERSION;

            // label1
            this.label1.Font = new System.Drawing.Font("Arial Black", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(248, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Written by: Nathaniel Collier";

            // label2
            this.label2.Location = new System.Drawing.Point(136, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(240, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Console Version Created on Wednesday, August 24, 2005, 2:41:39 PM";

            // label3
            this.label3.Location = new System.Drawing.Point(136, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(240, 32);
            this.label3.TabIndex = 4;
            this.label3.Text = "Visual C++ .net version created on Monday, October 17, 2005, 3:02:42 PM";

            // last_change
            this.last_change.Location = new System.Drawing.Point(136, 200);
            this.last_change.Name = "last_change";
            this.last_change.Size = new System.Drawing.Size(240, 23);
            this.last_change.TabIndex = 5;
            this.last_change.Text = "Last Change: " + BJ_LASTCHANGE;

            // card5
            this.card5.Location = new System.Drawing.Point(392, 120);
            this.card5.Name = "card5";
            this.card5.Size = new System.Drawing.Size(71, 96);
            this.card5.TabIndex = 23;
            this.card5.TabStop = false;

            // pictureBox1
            this.pictureBox1.Location = new System.Drawing.Point(48, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 96);
            this.pictureBox1.TabIndex = 22;
            this.pictureBox1.TabStop = false;

            // label4
            this.label4.Location = new System.Drawing.Point(136, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(240, 23);
            this.label4.TabIndex = 5;
            this.label4.Text = "C# WinForms version created 2026";

            // AboutBox
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.ForestGreen;
            this.ClientSize = new System.Drawing.Size(528, 320);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.card5);
            this.Controls.Add(this.last_change);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.version_label);
            this.Controls.Add(this.aboutbox_ok);
            this.Name = "AboutBox";
            this.Text = "About BlackJack";
            ((System.ComponentModel.ISupportInitialize)(this.card5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                card5?.Dispose();
                pictureBox1?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void aboutbox_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private System.Windows.Forms.Button aboutbox_ok;
        private System.Windows.Forms.Label version_label;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label last_change;
        private System.Windows.Forms.PictureBox card5;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
    }
}
