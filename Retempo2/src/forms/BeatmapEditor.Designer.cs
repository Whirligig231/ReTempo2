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
            StopButton = new Button();
            OpenButton = new Button();
            AudioVis = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)AudioVis).BeginInit();
            SuspendLayout();
            // 
            // PlayButton
            // 
            PlayButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayButton.Image = Properties.Resources.play;
            PlayButton.Location = new Point(82, 12);
            PlayButton.Name = "PlayButton";
            PlayButton.Size = new Size(64, 64);
            PlayButton.TabIndex = 1;
            PlayButton.UseVisualStyleBackColor = true;
            PlayButton.Click += PlayButton_Click;
            // 
            // StopButton
            // 
            StopButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            StopButton.Image = Properties.Resources.stop;
            StopButton.Location = new Point(152, 12);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(64, 64);
            StopButton.TabIndex = 2;
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // OpenButton
            // 
            OpenButton.Image = Properties.Resources.open;
            OpenButton.Location = new Point(12, 12);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(64, 64);
            OpenButton.TabIndex = 0;
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // AudioVis
            // 
            AudioVis.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AudioVis.Location = new Point(12, 150);
            AudioVis.Name = "AudioVis";
            AudioVis.Size = new Size(600, 200);
            AudioVis.TabIndex = 3;
            AudioVis.TabStop = false;
            AudioVis.Paint += AudioVis_Paint;
            AudioVis.Resize += AudioVis_Resize;
            // 
            // BeatmapEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 441);
            Controls.Add(AudioVis);
            Controls.Add(OpenButton);
            Controls.Add(StopButton);
            Controls.Add(PlayButton);
            Name = "BeatmapEditor";
            Text = "Retempo 2 {VERSION} - Beatmap Editor";
            Load += BeatmapEditor_Load;
            MouseWheel += BeatmapEditor_MouseWheel;
            ((System.ComponentModel.ISupportInitialize)AudioVis).EndInit();
            ResumeLayout(false);
        }

        private void AudioVis_Paint1(object sender, PaintEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Button PlayButton;
        private Button StopButton;
        private Button OpenButton;
        private PictureBox AudioVis;
    }
}
