namespace Retempo2
{
    partial class ManualTempoDialog
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
            SecondsLabel = new Label();
            BeatsBox = new TextBox();
            BPMBox = new TextBox();
            BPMLabel = new Label();
            BeatsLabel = new Label();
            CancelButton = new Button();
            OKButton = new Button();
            SuspendLayout();
            // 
            // SecondsLabel
            // 
            SecondsLabel.Anchor = AnchorStyles.Top;
            SecondsLabel.AutoSize = true;
            SecondsLabel.Location = new Point(105, 9);
            SecondsLabel.Name = "SecondsLabel";
            SecondsLabel.Size = new Size(135, 15);
            SecondsLabel.TabIndex = 0;
            SecondsLabel.Text = "Selection: ??.??? seconds";
            // 
            // BeatsBox
            // 
            BeatsBox.Location = new Point(52, 50);
            BeatsBox.Name = "BeatsBox";
            BeatsBox.Size = new Size(57, 23);
            BeatsBox.TabIndex = 1;
            // 
            // BPMBox
            // 
            BPMBox.Location = new Point(235, 50);
            BPMBox.Name = "BPMBox";
            BPMBox.Size = new Size(57, 23);
            BPMBox.TabIndex = 2;
            // 
            // BPMLabel
            // 
            BPMLabel.AutoSize = true;
            BPMLabel.Location = new Point(247, 76);
            BPMLabel.Name = "BPMLabel";
            BPMLabel.Size = new Size(32, 15);
            BPMLabel.TabIndex = 3;
            BPMLabel.Text = "BPM";
            // 
            // BeatsLabel
            // 
            BeatsLabel.AutoSize = true;
            BeatsLabel.Location = new Point(63, 76);
            BeatsLabel.Name = "BeatsLabel";
            BeatsLabel.Size = new Size(35, 15);
            BeatsLabel.TabIndex = 4;
            BeatsLabel.Text = "Beats";
            // 
            // CancelButton
            // 
            CancelButton.Location = new Point(257, 126);
            CancelButton.Name = "CancelButton";
            CancelButton.Size = new Size(75, 23);
            CancelButton.TabIndex = 5;
            CancelButton.Text = "Cancel";
            CancelButton.UseVisualStyleBackColor = true;
            CancelButton.Click += CancelButton_Click;
            // 
            // OKButton
            // 
            OKButton.Location = new Point(176, 126);
            OKButton.Name = "OKButton";
            OKButton.Size = new Size(75, 23);
            OKButton.TabIndex = 6;
            OKButton.Text = "OK";
            OKButton.UseVisualStyleBackColor = true;
            // 
            // ManualTempoDialog
            // 
            AcceptButton = OKButton;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(344, 161);
            Controls.Add(OKButton);
            Controls.Add(CancelButton);
            Controls.Add(BeatsLabel);
            Controls.Add(BPMLabel);
            Controls.Add(BPMBox);
            Controls.Add(BeatsBox);
            Controls.Add(SecondsLabel);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ManualTempoDialog";
            Text = "Map Beats by Tempo";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label SecondsLabel;
        private TextBox BeatsBox;
        private TextBox BPMBox;
        private Label BPMLabel;
        private Label BeatsLabel;
        private Button CancelButton;
        private Button OKButton;
    }
}