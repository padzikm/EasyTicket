namespace Server
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
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.mailBody = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.stopChecking = new System.Windows.Forms.Button();
            this.StartChecking = new System.Windows.Forms.Button();
            this.frequencyTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Interval = 10000;
            this.timer.Tick += new System.EventHandler(this.TimerTick);
            // 
            // mailBody
            // 
            this.mailBody.Location = new System.Drawing.Point(12, 26);
            this.mailBody.Name = "mailBody";
            this.mailBody.Size = new System.Drawing.Size(359, 355);
            this.mailBody.TabIndex = 1;
            this.mailBody.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(253, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(118, 28);
            this.button1.TabIndex = 2;
            this.button1.Text = "Send Mail to all users";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.SendMailClick);
            // 
            // stopChecking
            // 
            this.stopChecking.Location = new System.Drawing.Point(397, 109);
            this.stopChecking.Name = "stopChecking";
            this.stopChecking.Size = new System.Drawing.Size(250, 62);
            this.stopChecking.TabIndex = 3;
            this.stopChecking.Text = "Stop Checking Offers";
            this.stopChecking.UseVisualStyleBackColor = true;
            this.stopChecking.Click += new System.EventHandler(this.stopChecking_Click);
            // 
            // StartChecking
            // 
            this.StartChecking.Enabled = false;
            this.StartChecking.Location = new System.Drawing.Point(397, 26);
            this.StartChecking.Name = "StartChecking";
            this.StartChecking.Size = new System.Drawing.Size(250, 62);
            this.StartChecking.TabIndex = 4;
            this.StartChecking.Text = "Start Checking Offers";
            this.StartChecking.UseVisualStyleBackColor = true;
            this.StartChecking.Click += new System.EventHandler(this.StartCheckingClick);
            // 
            // frequencyTextBox
            // 
            this.frequencyTextBox.Location = new System.Drawing.Point(535, 198);
            this.frequencyTextBox.Name = "frequencyTextBox";
            this.frequencyTextBox.Size = new System.Drawing.Size(42, 20);
            this.frequencyTextBox.TabIndex = 5;
            this.frequencyTextBox.Text = "1";
            this.frequencyTextBox.TextChanged += new System.EventHandler(this.frequencyTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(397, 201);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(132, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Checking frequency (mins)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 427);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.frequencyTextBox);
            this.Controls.Add(this.StartChecking);
            this.Controls.Add(this.stopChecking);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.mailBody);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Panel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1Closing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.RichTextBox mailBody;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button stopChecking;
        private System.Windows.Forms.Button StartChecking;
        private System.Windows.Forms.TextBox frequencyTextBox;
        private System.Windows.Forms.Label label1;
    }
}

