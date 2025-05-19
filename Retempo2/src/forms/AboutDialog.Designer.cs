namespace Retempo2.src.forms
{
    partial class AboutDialog
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
            OKButton = new Button();
            Title = new Label();
            Description = new Label();
            SuspendLayout();
            // 
            // OKButton
            // 
            OKButton.Anchor = AnchorStyles.Top;
            OKButton.Location = new Point(138, 146);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 0;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            OKButton.Click += OKButton_Click;
            // 
            // Title
            // 
            Title.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            Title.AutoSize = true;
            Title.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Title.Location = new Point(70, 9);
            Title.Name = "Title";
            Title.Size = new Size(210, 25);
            Title.TabIndex = 1;
            Title.Text = "ReTempo 2 {VERSION}";
            Title.TextAlign = ContentAlignment.TopCenter;
            // 
            // Description
            // 
            Description.Anchor = AnchorStyles.Top;
            Description.AutoSize = true;
            Description.Location = new Point(14, 51);
            Description.Name = "Description";
            Description.Size = new Size(323, 45);
            Description.TabIndex = 2;
            Description.Text = "ReTempo 2 is (c) Whirligig Studios, 2025\r\n\r\nLicensed under the GNU GPL v2; see COPYING.txt for details";
            Description.TextAlign = ContentAlignment.TopCenter;
            // 
            // AboutDialog
            // 
            AcceptButton = OKButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(351, 181);
            Controls.Add(Description);
            Controls.Add(Title);
            Controls.Add(OKButton);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "AboutDialog";
            StartPosition = FormStartPosition.CenterParent;
            Text = "About ReTempo 2 {VERSION}";
            Load += AboutDialog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button OKButton;
        private Label Title;
        private Label Description;
    }
}