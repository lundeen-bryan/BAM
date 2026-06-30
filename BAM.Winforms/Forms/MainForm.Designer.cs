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
            this.TestAddTenButton = new System.Windows.Forms.Button();
            this.TestResultTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TestAddTenButton
            // 
            this.TestAddTenButton.Location = new System.Drawing.Point(164, 116);
            this.TestAddTenButton.Name = "TestAddTenButton";
            this.TestAddTenButton.Size = new System.Drawing.Size(75, 23);
            this.TestAddTenButton.TabIndex = 0;
            this.TestAddTenButton.Text = "TestAddTen";
            this.TestAddTenButton.UseVisualStyleBackColor = true;
            // 
            // TestResultTextBox
            // 
            this.TestResultTextBox.Location = new System.Drawing.Point(164, 173);
            this.TestResultTextBox.Name = "TestResultTextBox";
            this.TestResultTextBox.Size = new System.Drawing.Size(100, 20);
            this.TestResultTextBox.TabIndex = 1;
            this.TestResultTextBox.Text = "TestResult";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.TestResultTextBox);
            this.Controls.Add(this.TestAddTenButton);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button TestAddTenButton;
        private System.Windows.Forms.TextBox TestResultTextBox;
    }
}