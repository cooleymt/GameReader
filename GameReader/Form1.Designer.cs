namespace GameReader
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
            this.foundText = new System.Windows.Forms.TextBox();
            this.volumeNum = new System.Windows.Forms.NumericUpDown();
            this.rateNum = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pausePlayBtn = new System.Windows.Forms.Button();
            this.stopVoiceBtn = new System.Windows.Forms.Button();
            this.statusLbl = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.volumeNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateNum)).BeginInit();
            this.SuspendLayout();
            // 
            // foundText
            // 
            this.foundText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.foundText.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.foundText.ForeColor = System.Drawing.SystemColors.Window;
            this.foundText.Location = new System.Drawing.Point(112, 35);
            this.foundText.Multiline = true;
            this.foundText.Name = "foundText";
            this.foundText.ReadOnly = true;
            this.foundText.Size = new System.Drawing.Size(444, 226);
            this.foundText.TabIndex = 3;
            // 
            // volumeNum
            // 
            this.volumeNum.BackColor = System.Drawing.Color.White;
            this.volumeNum.Location = new System.Drawing.Point(60, 63);
            this.volumeNum.Name = "volumeNum";
            this.volumeNum.Size = new System.Drawing.Size(46, 20);
            this.volumeNum.TabIndex = 4;
            this.volumeNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // rateNum
            // 
            this.rateNum.Location = new System.Drawing.Point(60, 89);
            this.rateNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.rateNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this.rateNum.Name = "rateNum";
            this.rateNum.Size = new System.Drawing.Size(46, 20);
            this.rateNum.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Volume";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(24, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(30, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Rate";
            // 
            // pausePlayBtn
            // 
            this.pausePlayBtn.Enabled = false;
            this.pausePlayBtn.Location = new System.Drawing.Point(181, 278);
            this.pausePlayBtn.Name = "pausePlayBtn";
            this.pausePlayBtn.Size = new System.Drawing.Size(75, 23);
            this.pausePlayBtn.TabIndex = 8;
            this.pausePlayBtn.Text = "Pause";
            this.pausePlayBtn.UseVisualStyleBackColor = true;
            this.pausePlayBtn.Click += new System.EventHandler(this.pausePlayBtn_Click);
            // 
            // stopVoiceBtn
            // 
            this.stopVoiceBtn.Enabled = false;
            this.stopVoiceBtn.Location = new System.Drawing.Point(405, 278);
            this.stopVoiceBtn.Name = "stopVoiceBtn";
            this.stopVoiceBtn.Size = new System.Drawing.Size(75, 23);
            this.stopVoiceBtn.TabIndex = 10;
            this.stopVoiceBtn.Text = "Stop";
            this.stopVoiceBtn.UseVisualStyleBackColor = true;
            this.stopVoiceBtn.Click += new System.EventHandler(this.stopVoiceBtn_Click);
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.ForeColor = System.Drawing.Color.White;
            this.statusLbl.Location = new System.Drawing.Point(301, 9);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(52, 13);
            this.statusLbl.TabIndex = 11;
            this.statusLbl.Text = "Initializing";
            this.statusLbl.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(692, 408);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.stopVoiceBtn);
            this.Controls.Add(this.pausePlayBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rateNum);
            this.Controls.Add(this.volumeNum);
            this.Controls.Add(this.foundText);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.volumeNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateNum)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox foundText;
        private System.Windows.Forms.NumericUpDown volumeNum;
        private System.Windows.Forms.NumericUpDown rateNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button pausePlayBtn;
        private System.Windows.Forms.Button stopVoiceBtn;
        private System.Windows.Forms.Label statusLbl;
    }
}

