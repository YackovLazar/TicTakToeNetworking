namespace Game
{
    partial class Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Game));
            label1 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            button4 = new System.Windows.Forms.Button();
            button5 = new System.Windows.Forms.Button();
            button6 = new System.Windows.Forms.Button();
            button7 = new System.Windows.Forms.Button();
            button8 = new System.Windows.Forms.Button();
            button9 = new System.Windows.Forms.Button();
            LoadGame = new System.Windows.Forms.Button();
            SaveGame = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            label1.Location = new System.Drawing.Point(229, 64);
            label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(77, 19);
            label1.TabIndex = 0;
            label1.Text = "Your Turn!";
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(89, 135);
            button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(104, 98);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += Button1_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(89, 236);
            button2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(104, 98);
            button2.TabIndex = 2;
            button2.UseVisualStyleBackColor = true;
            button2.Click += Button2_Click;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(89, 342);
            button3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(104, 98);
            button3.TabIndex = 3;
            button3.UseVisualStyleBackColor = true;
            button3.Click += Button3_Click;
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(201, 135);
            button4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(104, 98);
            button4.TabIndex = 4;
            button4.UseVisualStyleBackColor = true;
            button4.Click += Button4_Click;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(201, 236);
            button5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(104, 98);
            button5.TabIndex = 5;
            button5.UseVisualStyleBackColor = true;
            button5.Click += Button5_Click;
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(201, 342);
            button6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(104, 98);
            button6.TabIndex = 6;
            button6.UseVisualStyleBackColor = true;
            button6.Click += Button6_Click;
            // 
            // button7
            // 
            button7.Location = new System.Drawing.Point(313, 135);
            button7.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button7.Name = "button7";
            button7.Size = new System.Drawing.Size(104, 98);
            button7.TabIndex = 9;
            button7.UseVisualStyleBackColor = true;
            button7.Click += Button7_Click;
            // 
            // button8
            // 
            button8.Location = new System.Drawing.Point(313, 236);
            button8.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button8.Name = "button8";
            button8.Size = new System.Drawing.Size(104, 98);
            button8.TabIndex = 8;
            button8.UseVisualStyleBackColor = true;
            button8.Click += Button8_Click;
            // 
            // button9
            // 
            button9.Location = new System.Drawing.Point(312, 342);
            button9.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            button9.Name = "button9";
            button9.Size = new System.Drawing.Size(104, 98);
            button9.TabIndex = 7;
            button9.UseVisualStyleBackColor = true;
            button9.Click += Button9_Click;
            // 
            // LoadGame
            // 
            LoadGame.Location = new System.Drawing.Point(23, 22);
            LoadGame.Name = "LoadGame";
            LoadGame.Size = new System.Drawing.Size(91, 23);
            LoadGame.TabIndex = 10;
            LoadGame.Text = "Load Game";
            LoadGame.UseVisualStyleBackColor = true;
            LoadGame.Click += LoadGame_Clicked<string>;
            // 
            // SaveGame
            // 
            SaveGame.Location = new System.Drawing.Point(415, 22);
            SaveGame.Name = "SaveGame";
            SaveGame.Size = new System.Drawing.Size(92, 23);
            SaveGame.TabIndex = 11;
            SaveGame.Text = "Save Game";
            SaveGame.UseVisualStyleBackColor = true;
            SaveGame.Click += SaveGame_Clicked;
            // 
            // Game
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.PowderBlue;
            ClientSize = new System.Drawing.Size(536, 554);
            Controls.Add(SaveGame);
            Controls.Add(LoadGame);
            Controls.Add(button7);
            Controls.Add(button8);
            Controls.Add(button9);
            Controls.Add(button6);
            Controls.Add(button5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(label1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            Name = "Game";
            Text = "TTTGame";
            FormClosing += Game_FormClosing;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button LoadGame;
        private System.Windows.Forms.Button SaveGame;
    }
}