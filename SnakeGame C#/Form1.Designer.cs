namespace SnakeGame
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lostLabel = new System.Windows.Forms.Label();
            this.moveTimer = new System.Windows.Forms.Timer(this.components);
            this.resetLabel = new System.Windows.Forms.Label();
            this.pointsLabel = new System.Windows.Forms.Label();
            this.pausStart = new System.Windows.Forms.Label();
            this.time = new System.Windows.Forms.Label();
            this.clock = new System.Windows.Forms.Timer(this.components);
            this.power = new System.Windows.Forms.Timer(this.components);
            this.wallLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.speedLabel = new System.Windows.Forms.Label();
            this.foodTimer = new System.Windows.Forms.Label();
            this.foodLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.levelCount = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pointCount = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.highscoreLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Location = new System.Drawing.Point(165, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(400, 400);
            this.panel1.TabIndex = 0;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.Panel1_Paint);
            // 
            // lostLabel
            // 
            this.lostLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lostLabel.AutoSize = true;
            this.lostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lostLabel.ForeColor = System.Drawing.Color.Red;
            this.lostLabel.Location = new System.Drawing.Point(627, 305);
            this.lostLabel.Name = "lostLabel";
            this.lostLabel.Size = new System.Drawing.Size(0, 24);
            this.lostLabel.TabIndex = 0;
            // 
            // moveTimer
            // 
            this.moveTimer.Interval = 200;
            this.moveTimer.Tick += new System.EventHandler(this.MoveTimer_Tick);
            // 
            // resetLabel
            // 
            this.resetLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.resetLabel.AutoSize = true;
            this.resetLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.resetLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resetLabel.Location = new System.Drawing.Point(631, 385);
            this.resetLabel.Name = "resetLabel";
            this.resetLabel.Size = new System.Drawing.Size(133, 27);
            this.resetLabel.TabIndex = 1;
            this.resetLabel.Text = "STARTA OM";
            this.resetLabel.Visible = false;
            this.resetLabel.Click += new System.EventHandler(this.ResetLabel_Click);
            // 
            // pointsLabel
            // 
            this.pointsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pointsLabel.AutoSize = true;
            this.pointsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointsLabel.Location = new System.Drawing.Point(627, 341);
            this.pointsLabel.Name = "pointsLabel";
            this.pointsLabel.Size = new System.Drawing.Size(0, 24);
            this.pointsLabel.TabIndex = 2;
            // 
            // pausStart
            // 
            this.pausStart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pausStart.AutoSize = true;
            this.pausStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pausStart.Location = new System.Drawing.Point(664, 55);
            this.pausStart.Name = "pausStart";
            this.pausStart.Size = new System.Drawing.Size(48, 22);
            this.pausStart.TabIndex = 3;
            this.pausStart.Text = "Start";
            this.pausStart.Click += new System.EventHandler(this.PausStart_Click);
            // 
            // time
            // 
            this.time.AutoSize = true;
            this.time.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.time.Location = new System.Drawing.Point(48, 12);
            this.time.Name = "time";
            this.time.Size = new System.Drawing.Size(52, 31);
            this.time.TabIndex = 4;
            this.time.Text = "0:0";
            // 
            // clock
            // 
            this.clock.Enabled = true;
            this.clock.Interval = 1000;
            this.clock.Tick += new System.EventHandler(this.Clock_Tick);
            // 
            // power
            // 
            this.power.Interval = 1000;
            this.power.Tick += new System.EventHandler(this.Power_Tick);
            // 
            // wallLabel
            // 
            this.wallLabel.AutoSize = true;
            this.wallLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.wallLabel.Location = new System.Drawing.Point(126, 121);
            this.wallLabel.Name = "wallLabel";
            this.wallLabel.Size = new System.Drawing.Size(16, 17);
            this.wallLabel.TabIndex = 5;
            this.wallLabel.Text = "0";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(26, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(83, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Wall Timer :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 152);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Speed Timer :";
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(126, 152);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(16, 17);
            this.speedLabel.TabIndex = 7;
            this.speedLabel.Text = "0";
            // 
            // foodTimer
            // 
            this.foodTimer.AutoSize = true;
            this.foodTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foodTimer.Location = new System.Drawing.Point(12, 58);
            this.foodTimer.Name = "foodTimer";
            this.foodTimer.Size = new System.Drawing.Size(88, 17);
            this.foodTimer.TabIndex = 10;
            this.foodTimer.Text = "Food Timer :";
            // 
            // foodLabel
            // 
            this.foodLabel.AutoSize = true;
            this.foodLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.foodLabel.Location = new System.Drawing.Point(126, 58);
            this.foodLabel.Name = "foodLabel";
            this.foodLabel.Size = new System.Drawing.Size(24, 17);
            this.foodLabel.TabIndex = 9;
            this.foodLabel.Text = "10";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(56, 255);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 14;
            this.label1.Text = "Level :";
            // 
            // levelCount
            // 
            this.levelCount.AutoSize = true;
            this.levelCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.levelCount.Location = new System.Drawing.Point(126, 255);
            this.levelCount.Name = "levelCount";
            this.levelCount.Size = new System.Drawing.Size(16, 17);
            this.levelCount.TabIndex = 13;
            this.levelCount.Text = "0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(51, 224);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 17);
            this.label3.TabIndex = 12;
            this.label3.Text = "Points :";
            // 
            // pointCount
            // 
            this.pointCount.AutoSize = true;
            this.pointCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pointCount.Location = new System.Drawing.Point(126, 224);
            this.pointCount.Name = "pointCount";
            this.pointCount.Size = new System.Drawing.Size(16, 17);
            this.pointCount.TabIndex = 11;
            this.pointCount.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(593, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Highscore :";
            // 
            // highscoreLabel
            // 
            this.highscoreLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.highscoreLabel.AutoSize = true;
            this.highscoreLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.highscoreLabel.Location = new System.Drawing.Point(688, 96);
            this.highscoreLabel.Name = "highscoreLabel";
            this.highscoreLabel.Size = new System.Drawing.Size(18, 20);
            this.highscoreLabel.TabIndex = 16;
            this.highscoreLabel.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 451);
            this.Controls.Add(this.highscoreLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.levelCount);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pointCount);
            this.Controls.Add(this.foodTimer);
            this.Controls.Add(this.foodLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.speedLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.wallLabel);
            this.Controls.Add(this.time);
            this.Controls.Add(this.pausStart);
            this.Controls.Add(this.pointsLabel);
            this.Controls.Add(this.lostLabel);
            this.Controls.Add(this.resetLabel);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(815, 490);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Timer moveTimer;
        private System.Windows.Forms.Label lostLabel;
        private System.Windows.Forms.Label resetLabel;
        private System.Windows.Forms.Label pointsLabel;
        private System.Windows.Forms.Label pausStart;
        private System.Windows.Forms.Label time;
        private System.Windows.Forms.Timer clock;
        private System.Windows.Forms.Timer power;
        private System.Windows.Forms.Label wallLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.Label foodTimer;
        private System.Windows.Forms.Label foodLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label levelCount;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label pointCount;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label highscoreLabel;
    }
}

