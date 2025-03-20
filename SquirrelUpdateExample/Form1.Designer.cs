
namespace SquirrelUpdateExample
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
            this.updateButton = new System.Windows.Forms.Button();
            this.VersionKeyLabel = new System.Windows.Forms.Label();
            this.VersionValueLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(28, 118);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(173, 45);
            this.updateButton.TabIndex = 0;
            this.updateButton.Text = "Check for Updates";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // VersionKeyLabel
            // 
            this.VersionKeyLabel.AutoSize = true;
            this.VersionKeyLabel.Location = new System.Drawing.Point(47, 60);
            this.VersionKeyLabel.Name = "VersionKeyLabel";
            this.VersionKeyLabel.Size = new System.Drawing.Size(82, 13);
            this.VersionKeyLabel.TabIndex = 2;
            this.VersionKeyLabel.Text = "Current Version:";
            // 
            // VersionValueLabel
            // 
            this.VersionValueLabel.AutoSize = true;
            this.VersionValueLabel.Location = new System.Drawing.Point(135, 60);
            this.VersionValueLabel.Name = "VersionValueLabel";
            this.VersionValueLabel.Size = new System.Drawing.Size(31, 13);
            this.VersionValueLabel.TabIndex = 3;
            this.VersionValueLabel.Text = "0.0.0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(240, 206);
            this.Controls.Add(this.VersionValueLabel);
            this.Controls.Add(this.VersionKeyLabel);
            this.Controls.Add(this.updateButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button updateButton;
        private System.Windows.Forms.Label VersionKeyLabel;
        private System.Windows.Forms.Label VersionValueLabel;
    }
}

