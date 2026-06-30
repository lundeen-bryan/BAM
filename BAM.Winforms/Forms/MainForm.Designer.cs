namespace BAM.Winforms.Forms
{
    partial class MainForm
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
            this.TestAddButton = new System.Windows.Forms.Button();
            this.TestMainLedTextBox = new System.Windows.Forms.TextBox();
            this.TestMemoryLedTextBox = new System.Windows.Forms.TextBox();
            this.TestTapeListBox = new System.Windows.Forms.ListBox();
            this.TestMemoryGroupBox = new System.Windows.Forms.GroupBox();
            this.TestMainGroupBox = new System.Windows.Forms.GroupBox();
            this.TestMemoryGroupBox.SuspendLayout();
            this.TestMainGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // TestAddButton
            // 
            this.TestAddButton.Location = new System.Drawing.Point(193, 129);
            this.TestAddButton.Name = "TestAddButton";
            this.TestAddButton.Size = new System.Drawing.Size(75, 23);
            this.TestAddButton.TabIndex = 0;
            this.TestAddButton.Text = "TestAddTen";
            this.TestAddButton.UseVisualStyleBackColor = true;
            // 
            // TestMainLedTextBox
            // 
            this.TestMainLedTextBox.Location = new System.Drawing.Point(6, 19);
            this.TestMainLedTextBox.Name = "TestMainLedTextBox";
            this.TestMainLedTextBox.Size = new System.Drawing.Size(100, 20);
            this.TestMainLedTextBox.TabIndex = 1;
            this.TestMainLedTextBox.Text = "TestMainLed";
            // 
            // TestMemoryLedTextBox
            // 
            this.TestMemoryLedTextBox.Location = new System.Drawing.Point(6, 19);
            this.TestMemoryLedTextBox.Name = "TestMemoryLedTextBox";
            this.TestMemoryLedTextBox.Size = new System.Drawing.Size(100, 20);
            this.TestMemoryLedTextBox.TabIndex = 2;
            this.TestMemoryLedTextBox.Text = "TestMemoryLed";
            // 
            // TestTapeListBox
            // 
            this.TestTapeListBox.FormattingEnabled = true;
            this.TestTapeListBox.Location = new System.Drawing.Point(101, 170);
            this.TestTapeListBox.Name = "TestTapeListBox";
            this.TestTapeListBox.Size = new System.Drawing.Size(266, 238);
            this.TestTapeListBox.TabIndex = 3;
            // 
            // TestMemoryGroupBox
            // 
            this.TestMemoryGroupBox.Controls.Add(this.TestMemoryLedTextBox);
            this.TestMemoryGroupBox.Location = new System.Drawing.Point(79, 66);
            this.TestMemoryGroupBox.Name = "TestMemoryGroupBox";
            this.TestMemoryGroupBox.Size = new System.Drawing.Size(123, 57);
            this.TestMemoryGroupBox.TabIndex = 4;
            this.TestMemoryGroupBox.TabStop = false;
            this.TestMemoryGroupBox.Text = "TestMemory";
            // 
            // TestMainGroupBox
            // 
            this.TestMainGroupBox.Controls.Add(this.TestMainLedTextBox);
            this.TestMainGroupBox.Location = new System.Drawing.Point(296, 66);
            this.TestMainGroupBox.Name = "TestMainGroupBox";
            this.TestMainGroupBox.Size = new System.Drawing.Size(131, 57);
            this.TestMainGroupBox.TabIndex = 5;
            this.TestMainGroupBox.TabStop = false;
            this.TestMainGroupBox.Text = "TestMain";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 450);
            this.Controls.Add(this.TestMainGroupBox);
            this.Controls.Add(this.TestMemoryGroupBox);
            this.Controls.Add(this.TestTapeListBox);
            this.Controls.Add(this.TestAddButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.TestMemoryGroupBox.ResumeLayout(false);
            this.TestMemoryGroupBox.PerformLayout();
            this.TestMainGroupBox.ResumeLayout(false);
            this.TestMainGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button TestAddButton;
        private System.Windows.Forms.TextBox TestMainLedTextBox;
        private System.Windows.Forms.TextBox TestMemoryLedTextBox;
        private System.Windows.Forms.ListBox TestTapeListBox;
        private System.Windows.Forms.GroupBox TestMemoryGroupBox;
        private System.Windows.Forms.GroupBox TestMainGroupBox;
    }
}