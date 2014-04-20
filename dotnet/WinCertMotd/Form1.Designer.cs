namespace WinCertMotd
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
            this.quitButton = new System.Windows.Forms.Button();
            this.certText = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // quitButton
            // 
            this.quitButton.Location = new System.Drawing.Point(187, 156);
            this.quitButton.Name = "quitButton";
            this.quitButton.Size = new System.Drawing.Size(75, 23);
            this.quitButton.TabIndex = 0;
            this.quitButton.Text = "OK";
            this.quitButton.UseVisualStyleBackColor = true;
            this.quitButton.Click += new System.EventHandler(this.quitButton_Click);
            // 
            // certText
            // 
            this.certText.AllowWebBrowserDrop = false;
            this.certText.IsWebBrowserContextMenuEnabled = false;
            this.certText.Location = new System.Drawing.Point(12, 12);
            this.certText.MinimumSize = new System.Drawing.Size(20, 20);
            this.certText.Name = "certText";
            this.certText.Size = new System.Drawing.Size(425, 138);
            this.certText.TabIndex = 1;
            this.certText.WebBrowserShortcutsEnabled = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(449, 191);
            this.Controls.Add(this.certText);
            this.Controls.Add(this.quitButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CERT C Secure Coding Standard MOTD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button quitButton;
        private System.Windows.Forms.WebBrowser certText;
    }
}

