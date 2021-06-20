namespace ElszabadultRobot
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Start = new System.Windows.Forms.Button();
            this.mapsizedecimal = new System.Windows.Forms.NumericUpDown();
            this.timelabel = new System.Windows.Forms.Label();
            this.Save = new System.Windows.Forms.Button();
            this.LoadGame = new System.Windows.Forms.Button();
            this.Pausebutton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.mapsizedecimal)).BeginInit();
            this.SuspendLayout();
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.Location = new System.Drawing.Point(979, 15);
            this.Start.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(100, 28);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // mapsizedecimal
            // 
            this.mapsizedecimal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.mapsizedecimal.Increment = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.mapsizedecimal.Location = new System.Drawing.Point(979, 50);
            this.mapsizedecimal.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mapsizedecimal.Maximum = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.mapsizedecimal.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.mapsizedecimal.Name = "mapsizedecimal";
            this.mapsizedecimal.Size = new System.Drawing.Size(100, 22);
            this.mapsizedecimal.TabIndex = 12;
            this.mapsizedecimal.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.mapsizedecimal.ValueChanged += new System.EventHandler(this.mapsize_ValueChanged);
            // 
            // timelabel
            // 
            this.timelabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.timelabel.AutoSize = true;
            this.timelabel.Location = new System.Drawing.Point(979, 84);
            this.timelabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.timelabel.Name = "timelabel";
            this.timelabel.Size = new System.Drawing.Size(24, 17);
            this.timelabel.TabIndex = 8;
            this.timelabel.Text = "00";
            // 
            // Save
            // 
            this.Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Save.Location = new System.Drawing.Point(983, 105);
            this.Save.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(100, 28);
            this.Save.TabIndex = 9;
            this.Save.Text = "Save";
            this.Save.UseVisualStyleBackColor = true;
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // LoadGame
            // 
            this.LoadGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadGame.Location = new System.Drawing.Point(983, 142);
            this.LoadGame.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoadGame.Name = "LoadGame";
            this.LoadGame.Size = new System.Drawing.Size(100, 28);
            this.LoadGame.TabIndex = 10;
            this.LoadGame.Text = "Load";
            this.LoadGame.UseVisualStyleBackColor = true;
            this.LoadGame.Click += new System.EventHandler(this.LoadGame_Click);
            // 
            // Pausebutton
            // 
            this.Pausebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Pausebutton.Location = new System.Drawing.Point(983, 178);
            this.Pausebutton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Pausebutton.Name = "Pausebutton";
            this.Pausebutton.Size = new System.Drawing.Size(96, 28);
            this.Pausebutton.TabIndex = 11;
            this.Pausebutton.Text = "|| ";
            this.Pausebutton.UseVisualStyleBackColor = true;
            this.Pausebutton.Click += new System.EventHandler(this.Pausebutton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 544);
            this.Controls.Add(this.Pausebutton);
            this.Controls.Add(this.LoadGame);
            this.Controls.Add(this.Save);
            this.Controls.Add(this.timelabel);
            this.Controls.Add(this.mapsizedecimal);
            this.Controls.Add(this.Start);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.RightToLeftLayout = true;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mapsizedecimal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.NumericUpDown mapsizedecimal;
        private System.Windows.Forms.Label timelabel;
        private System.Windows.Forms.Button Save;
        private System.Windows.Forms.Button LoadGame;
        private System.Windows.Forms.Button Pausebutton;
    }
}

