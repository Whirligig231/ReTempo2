namespace Retempo2.src.forms
{
    partial class HelpDialog
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
            HelpWebBrowser = new WebBrowser();
            SuspendLayout();
            // 
            // HelpWebBrowser
            // 
            HelpWebBrowser.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            HelpWebBrowser.Location = new Point(12, 12);
            HelpWebBrowser.Name = "HelpWebBrowser";
            HelpWebBrowser.Size = new Size(440, 577);
            HelpWebBrowser.TabIndex = 0;
            // 
            // HelpDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(464, 601);
            Controls.Add(HelpWebBrowser);
            Name = "HelpDialog";
            Text = "ReTempo 2 {VERSION} - Help";
            Load += HelpDialog_Load;
            ResumeLayout(false);
        }

        #endregion

        private WebBrowser HelpWebBrowser;
    }
}