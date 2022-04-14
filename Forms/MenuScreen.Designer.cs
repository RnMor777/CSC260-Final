namespace CSC260_Final {
    partial class Start {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.NameLabel = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.Label();
            this.Play = new System.Windows.Forms.Button();
            this.Instructions = new System.Windows.Forms.Button();
            this.Settings = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::CSC260_Final.Properties.Resources.StartBishop;
            this.pictureBox2.Location = new System.Drawing.Point(750, 107);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(279, 412);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::CSC260_Final.Properties.Resources.StartKnight;
            this.pictureBox1.Location = new System.Drawing.Point(12, 133);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(297, 395);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // NameLabel
            // 
            this.NameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NameLabel.Location = new System.Drawing.Point(806, 522);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(251, 32);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "© By Ryan Morganti 2022";
            // 
            // Title
            // 
            this.Title.Font = new System.Drawing.Font("Microsoft Sans Serif", 60F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(294, 33);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(505, 110);
            this.Title.TabIndex = 3;
            this.Title.Text = "C# Chess";
            this.Title.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Play
            // 
            this.Play.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Play.Location = new System.Drawing.Point(439, 203);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(226, 53);
            this.Play.TabIndex = 4;
            this.Play.Text = "Play Game";
            this.Play.UseVisualStyleBackColor = true;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Instructions
            // 
            this.Instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Instructions.Location = new System.Drawing.Point(439, 277);
            this.Instructions.Name = "Instructions";
            this.Instructions.Size = new System.Drawing.Size(226, 53);
            this.Instructions.TabIndex = 5;
            this.Instructions.Text = "Instructions";
            this.Instructions.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.Settings.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Settings.Location = new System.Drawing.Point(439, 347);
            this.Settings.Name = "Settings";
            this.Settings.Size = new System.Drawing.Size(226, 53);
            this.Settings.TabIndex = 6;
            this.Settings.Text = "Settings";
            this.Settings.UseVisualStyleBackColor = true;
            // 
            // Quit
            // 
            this.Quit.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quit.Location = new System.Drawing.Point(439, 421);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(226, 53);
            this.Quit.TabIndex = 7;
            this.Quit.Text = "Exit";
            this.Quit.UseVisualStyleBackColor = true;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // Start
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1075, 584);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.Settings);
            this.Controls.Add(this.Instructions);
            this.Controls.Add(this.Play);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Start";
            this.Text = "MorgantiChess";
            this.Load += new System.EventHandler(this.MenuScreen_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label Title;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Instructions;
        private System.Windows.Forms.Button Settings;
        private System.Windows.Forms.Button Quit;
    }
}