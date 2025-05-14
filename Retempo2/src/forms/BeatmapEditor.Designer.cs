namespace Retempo2
{
    partial class BeatmapEditor
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            PlayButton = new Button();
            SuspendLayout();
            // 
            // PlayButton
            // 
            PlayButton.Anchor = AnchorStyles.None;
            PlayButton.Location = new Point(264, 208);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(96, 24);
            PlayButton.TabIndex = 0;
            PlayButton.Text = "Play Sound";
            PlayButton.UseVisualStyleBackColor = true;
            PlayButton.Click += PlayButton_Click;
            // 
            // BeatmapEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(PlayButton);
            Name = "BeatmapEditor";
            Text = "Retempo 2 {VERSION} - Beatmap Editor";
            Load += BeatmapEditor_Load;
            ResumeLayout(false);
        }

        #endregion

        private Button PlayButton;
    }
}
