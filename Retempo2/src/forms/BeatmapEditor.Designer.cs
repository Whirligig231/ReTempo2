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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeatmapEditor));
            PlayButton = new NonSelectableButton();
            StopButton = new NonSelectableButton();
            OpenButton = new NonSelectableButton();
            AudioVis = new PictureBox();
            AudioVisScroll = new HScrollBar();
            SeekStartButton = new NonSelectableButton();
            ManualTempoButton = new NonSelectableButton();
            AutoTempoButton = new NonSelectableButton();
            MenuStrip = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            newToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator = new ToolStripSeparator();
            saveToolStripMenuItem = new ToolStripMenuItem();
            saveAsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            importToolStripMenuItem = new ToolStripMenuItem();
            reloadToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            editToolStripMenuItem = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            cutToolStripMenuItem = new ToolStripMenuItem();
            copyToolStripMenuItem = new ToolStripMenuItem();
            pasteToolStripMenuItem = new ToolStripMenuItem();
            deleteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            selectAllToolStripMenuItem = new ToolStripMenuItem();
            playheadToolStripMenuItem = new ToolStripMenuItem();
            playStopToolStripMenuItem = new ToolStripMenuItem();
            restartPlaybackToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            toStartToolStripMenuItem = new ToolStripMenuItem();
            toEndToolStripMenuItem = new ToolStripMenuItem();
            toPrevFrameToolStripMenuItem = new ToolStripMenuItem();
            toNextFrameToolStripMenuItem = new ToolStripMenuItem();
            toPrevBeatToolStripMenuItem = new ToolStripMenuItem();
            toNextBeatToolStripMenuItem = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            mapBeatsByTempoToolStripMenuItem = new ToolStripMenuItem();
            detectBeatsToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            showREADMEToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)AudioVis).BeginInit();
            MenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // PlayButton
            // 
            PlayButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            PlayButton.Image = Properties.Resources.play;
            PlayButton.Location = new Point(82, 27);
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
            StopButton.Location = new Point(152, 27);
            StopButton.Name = "StopButton";
            StopButton.Size = new Size(64, 64);
            StopButton.TabIndex = 2;
            StopButton.UseVisualStyleBackColor = true;
            StopButton.Click += StopButton_Click;
            // 
            // OpenButton
            // 
            OpenButton.Image = Properties.Resources.open;
            OpenButton.Location = new Point(12, 27);
            OpenButton.Name = "OpenButton";
            OpenButton.Size = new Size(64, 64);
            OpenButton.TabIndex = 0;
            OpenButton.UseVisualStyleBackColor = true;
            OpenButton.Click += OpenButton_Click;
            // 
            // AudioVis
            // 
            AudioVis.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AudioVis.Location = new Point(12, 97);
            AudioVis.Name = "AudioVis";
            AudioVis.Size = new Size(600, 200);
            AudioVis.TabIndex = 3;
            AudioVis.TabStop = false;
            AudioVis.Paint += AudioVis_Paint;
            AudioVis.MouseDown += AudioVis_MouseDown;
            AudioVis.MouseMove += AudioVis_MouseMove;
            AudioVis.MouseUp += AudioVis_MouseUp;
            AudioVis.Resize += AudioVis_Resize;
            // 
            // AudioVisScroll
            // 
            AudioVisScroll.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AudioVisScroll.Location = new Point(12, 300);
            AudioVisScroll.Name = "AudioVisScroll";
            AudioVisScroll.Size = new Size(600, 17);
            AudioVisScroll.TabIndex = 4;
            AudioVisScroll.Scroll += AudioVisScroll_Scroll;
            // 
            // SeekStartButton
            // 
            SeekStartButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            SeekStartButton.Image = Properties.Resources.seekstart;
            SeekStartButton.Location = new Point(222, 27);
            SeekStartButton.Name = "SeekStartButton";
            SeekStartButton.Size = new Size(64, 64);
            SeekStartButton.TabIndex = 3;
            SeekStartButton.UseVisualStyleBackColor = true;
            SeekStartButton.Click += SeekStartButton_Click;
            // 
            // ManualTempoButton
            // 
            ManualTempoButton.Enabled = false;
            ManualTempoButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ManualTempoButton.Image = Properties.Resources.metronome_manual;
            ManualTempoButton.Location = new Point(292, 27);
            ManualTempoButton.Name = "ManualTempoButton";
            ManualTempoButton.Size = new Size(64, 64);
            ManualTempoButton.TabIndex = 5;
            ManualTempoButton.UseVisualStyleBackColor = true;
            ManualTempoButton.Click += ManualTempoButton_Click;
            // 
            // AutoTempoButton
            // 
            AutoTempoButton.Enabled = false;
            AutoTempoButton.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            AutoTempoButton.Image = (Image)resources.GetObject("AutoTempoButton.Image");
            AutoTempoButton.Location = new Point(362, 27);
            AutoTempoButton.Name = "AutoTempoButton";
            AutoTempoButton.Size = new Size(64, 64);
            AutoTempoButton.TabIndex = 6;
            AutoTempoButton.UseVisualStyleBackColor = true;
            AutoTempoButton.Click += AutoTempoButton_Click;
            // 
            // MenuStrip
            // 
            MenuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, playheadToolStripMenuItem, toolsToolStripMenuItem, helpToolStripMenuItem });
            MenuStrip.Location = new Point(0, 0);
            MenuStrip.Name = "MenuStrip";
            MenuStrip.Size = new Size(624, 24);
            MenuStrip.TabIndex = 7;
            MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { newToolStripMenuItem, openToolStripMenuItem, toolStripSeparator, saveToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator2, importToolStripMenuItem, reloadToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // newToolStripMenuItem
            // 
            newToolStripMenuItem.Image = (Image)resources.GetObject("newToolStripMenuItem.Image");
            newToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            newToolStripMenuItem.Name = "newToolStripMenuItem";
            newToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.N;
            newToolStripMenuItem.Size = new Size(198, 22);
            newToolStripMenuItem.Text = "&New...";
            newToolStripMenuItem.Click += newToolStripMenuItem_Click;
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Image = (Image)resources.GetObject("openToolStripMenuItem.Image");
            openToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.O;
            openToolStripMenuItem.Size = new Size(198, 22);
            openToolStripMenuItem.Text = "&Open...";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(195, 6);
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Image = (Image)resources.GetObject("saveToolStripMenuItem.Image");
            saveToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.S;
            saveToolStripMenuItem.Size = new Size(198, 22);
            saveToolStripMenuItem.Text = "&Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // saveAsToolStripMenuItem
            // 
            saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            saveAsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.S;
            saveAsToolStripMenuItem.Size = new Size(198, 22);
            saveAsToolStripMenuItem.Text = "Save &As...";
            saveAsToolStripMenuItem.Click += saveAsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(195, 6);
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            importToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.I;
            importToolStripMenuItem.Size = new Size(198, 22);
            importToolStripMenuItem.Text = "&Import Sample...";
            importToolStripMenuItem.Click += importToolStripMenuItem_Click;
            // 
            // reloadToolStripMenuItem
            // 
            reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            reloadToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.R;
            reloadToolStripMenuItem.Size = new Size(198, 22);
            reloadToolStripMenuItem.Text = "&Reload Sample";
            reloadToolStripMenuItem.Click += reloadToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(195, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.W;
            exitToolStripMenuItem.Size = new Size(198, 22);
            exitToolStripMenuItem.Text = "&Close Window";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // editToolStripMenuItem
            // 
            editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { undoToolStripMenuItem, redoToolStripMenuItem, toolStripSeparator3, cutToolStripMenuItem, copyToolStripMenuItem, pasteToolStripMenuItem, deleteToolStripMenuItem, toolStripSeparator4, selectAllToolStripMenuItem });
            editToolStripMenuItem.Name = "editToolStripMenuItem";
            editToolStripMenuItem.Size = new Size(39, 20);
            editToolStripMenuItem.Text = "&Edit";
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Z;
            undoToolStripMenuItem.Size = new Size(164, 22);
            undoToolStripMenuItem.Text = "&Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Y;
            redoToolStripMenuItem.Size = new Size(164, 22);
            redoToolStripMenuItem.Text = "&Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(161, 6);
            // 
            // cutToolStripMenuItem
            // 
            cutToolStripMenuItem.Image = (Image)resources.GetObject("cutToolStripMenuItem.Image");
            cutToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            cutToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.X;
            cutToolStripMenuItem.Size = new Size(164, 22);
            cutToolStripMenuItem.Text = "Cu&t";
            cutToolStripMenuItem.Click += cutToolStripMenuItem_Click;
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Image = (Image)resources.GetObject("copyToolStripMenuItem.Image");
            copyToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.C;
            copyToolStripMenuItem.Size = new Size(164, 22);
            copyToolStripMenuItem.Text = "&Copy";
            copyToolStripMenuItem.Click += copyToolStripMenuItem_Click;
            // 
            // pasteToolStripMenuItem
            // 
            pasteToolStripMenuItem.Image = (Image)resources.GetObject("pasteToolStripMenuItem.Image");
            pasteToolStripMenuItem.ImageTransparentColor = Color.Magenta;
            pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            pasteToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.V;
            pasteToolStripMenuItem.Size = new Size(164, 22);
            pasteToolStripMenuItem.Text = "&Paste";
            pasteToolStripMenuItem.Click += pasteToolStripMenuItem_Click;
            // 
            // deleteToolStripMenuItem
            // 
            deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            deleteToolStripMenuItem.ShortcutKeys = Keys.Delete;
            deleteToolStripMenuItem.Size = new Size(164, 22);
            deleteToolStripMenuItem.Text = "&Delete";
            deleteToolStripMenuItem.Click += deleteToolStripMenuItem_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(161, 6);
            // 
            // selectAllToolStripMenuItem
            // 
            selectAllToolStripMenuItem.Name = "selectAllToolStripMenuItem";
            selectAllToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.A;
            selectAllToolStripMenuItem.Size = new Size(164, 22);
            selectAllToolStripMenuItem.Text = "Select &All";
            selectAllToolStripMenuItem.Click += selectAllToolStripMenuItem_Click;
            // 
            // playheadToolStripMenuItem
            // 
            playheadToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { playStopToolStripMenuItem, restartPlaybackToolStripMenuItem, toolStripSeparator5, toStartToolStripMenuItem, toEndToolStripMenuItem, toPrevFrameToolStripMenuItem, toNextFrameToolStripMenuItem, toPrevBeatToolStripMenuItem, toNextBeatToolStripMenuItem });
            playheadToolStripMenuItem.Name = "playheadToolStripMenuItem";
            playheadToolStripMenuItem.Size = new Size(67, 20);
            playheadToolStripMenuItem.Text = "&Playhead";
            // 
            // playStopToolStripMenuItem
            // 
            playStopToolStripMenuItem.Name = "playStopToolStripMenuItem";
            playStopToolStripMenuItem.Size = new Size(171, 22);
            playStopToolStripMenuItem.Text = "&Play/Stop";
            playStopToolStripMenuItem.Click += playStopToolStripMenuItem_Click;
            // 
            // restartPlaybackToolStripMenuItem
            // 
            restartPlaybackToolStripMenuItem.Name = "restartPlaybackToolStripMenuItem";
            restartPlaybackToolStripMenuItem.Size = new Size(171, 22);
            restartPlaybackToolStripMenuItem.Text = "&Restart Playback";
            restartPlaybackToolStripMenuItem.Click += restartPlaybackToolStripMenuItem_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(168, 6);
            // 
            // toStartToolStripMenuItem
            // 
            toStartToolStripMenuItem.Name = "toStartToolStripMenuItem";
            toStartToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Left;
            toStartToolStripMenuItem.Size = new Size(171, 22);
            toStartToolStripMenuItem.Text = "To &Start";
            toStartToolStripMenuItem.Click += toStartToolStripMenuItem_Click;
            // 
            // toEndToolStripMenuItem
            // 
            toEndToolStripMenuItem.Name = "toEndToolStripMenuItem";
            toEndToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Right;
            toEndToolStripMenuItem.Size = new Size(171, 22);
            toEndToolStripMenuItem.Text = "To &End";
            toEndToolStripMenuItem.Click += toEndToolStripMenuItem_Click;
            // 
            // toPrevFrameToolStripMenuItem
            // 
            toPrevFrameToolStripMenuItem.Name = "toPrevFrameToolStripMenuItem";
            toPrevFrameToolStripMenuItem.Size = new Size(171, 22);
            toPrevFrameToolStripMenuItem.Text = "To &Prev Frame";
            toPrevFrameToolStripMenuItem.Click += toPrevFrameToolStripMenuItem_Click;
            // 
            // toNextFrameToolStripMenuItem
            // 
            toNextFrameToolStripMenuItem.Name = "toNextFrameToolStripMenuItem";
            toNextFrameToolStripMenuItem.Size = new Size(171, 22);
            toNextFrameToolStripMenuItem.Text = "To &Next Frame";
            toNextFrameToolStripMenuItem.Click += toNextFrameToolStripMenuItem_Click;
            // 
            // toPrevBeatToolStripMenuItem
            // 
            toPrevBeatToolStripMenuItem.Name = "toPrevBeatToolStripMenuItem";
            toPrevBeatToolStripMenuItem.Size = new Size(171, 22);
            toPrevBeatToolStripMenuItem.Text = "To Prev Bea&t";
            toPrevBeatToolStripMenuItem.Click += toPrevBeatToolStripMenuItem_Click;
            // 
            // toNextBeatToolStripMenuItem
            // 
            toNextBeatToolStripMenuItem.Name = "toNextBeatToolStripMenuItem";
            toNextBeatToolStripMenuItem.Size = new Size(171, 22);
            toNextBeatToolStripMenuItem.Text = "To Next &Beat";
            toNextBeatToolStripMenuItem.Click += toNextBeatToolStripMenuItem_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mapBeatsByTempoToolStripMenuItem, detectBeatsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // mapBeatsByTempoToolStripMenuItem
            // 
            mapBeatsByTempoToolStripMenuItem.Name = "mapBeatsByTempoToolStripMenuItem";
            mapBeatsByTempoToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.M;
            mapBeatsByTempoToolStripMenuItem.Size = new Size(238, 22);
            mapBeatsByTempoToolStripMenuItem.Text = "&Map Beats by Tempo...";
            mapBeatsByTempoToolStripMenuItem.Click += mapBeatsByTempoToolStripMenuItem_Click;
            // 
            // detectBeatsToolStripMenuItem
            // 
            detectBeatsToolStripMenuItem.Name = "detectBeatsToolStripMenuItem";
            detectBeatsToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.B;
            detectBeatsToolStripMenuItem.Size = new Size(238, 22);
            detectBeatsToolStripMenuItem.Text = "&Detect Beats";
            detectBeatsToolStripMenuItem.Click += detectBeatsToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { showREADMEToolStripMenuItem, toolStripSeparator6, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // showREADMEToolStripMenuItem
            // 
            showREADMEToolStripMenuItem.Name = "showREADMEToolStripMenuItem";
            showREADMEToolStripMenuItem.ShortcutKeys = Keys.F1;
            showREADMEToolStripMenuItem.Size = new Size(171, 22);
            showREADMEToolStripMenuItem.Text = "Show &README";
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(168, 6);
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(171, 22);
            aboutToolStripMenuItem.Text = "&About...";
            // 
            // BeatmapEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 322);
            Controls.Add(AutoTempoButton);
            Controls.Add(ManualTempoButton);
            Controls.Add(SeekStartButton);
            Controls.Add(AudioVisScroll);
            Controls.Add(AudioVis);
            Controls.Add(OpenButton);
            Controls.Add(StopButton);
            Controls.Add(PlayButton);
            Controls.Add(MenuStrip);
            KeyPreview = true;
            MainMenuStrip = MenuStrip;
            Name = "BeatmapEditor";
            Text = "Retempo 2 {VERSION} - Beatmap Editor";
            FormClosing += BeatmapEditor_FormClosing;
            Load += BeatmapEditor_Load;
            KeyDown += BeatmapEditor_KeyDown;
            MouseWheel += BeatmapEditor_MouseWheel;
            ((System.ComponentModel.ISupportInitialize)AudioVis).EndInit();
            MenuStrip.ResumeLayout(false);
            MenuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox AudioVis;
        private HScrollBar AudioVisScroll;
        private NonSelectableButton ManualTempoButton;
        private NonSelectableButton PlayButton;
        private NonSelectableButton StopButton;
        private NonSelectableButton OpenButton;
        private NonSelectableButton SeekStartButton;
        private NonSelectableButton AutoTempoButton;
        private MenuStrip MenuStrip;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem saveAsToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem editToolStripMenuItem;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem cutToolStripMenuItem;
        private ToolStripMenuItem copyToolStripMenuItem;
        private ToolStripMenuItem pasteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem selectAllToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem reloadToolStripMenuItem;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private ToolStripMenuItem playheadToolStripMenuItem;
        private ToolStripMenuItem playStopToolStripMenuItem;
        private ToolStripMenuItem restartPlaybackToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem toStartToolStripMenuItem;
        private ToolStripMenuItem toEndToolStripMenuItem;
        private ToolStripMenuItem toPrevFrameToolStripMenuItem;
        private ToolStripMenuItem toNextFrameToolStripMenuItem;
        private ToolStripMenuItem toPrevBeatToolStripMenuItem;
        private ToolStripMenuItem toNextBeatToolStripMenuItem;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem mapBeatsByTempoToolStripMenuItem;
        private ToolStripMenuItem detectBeatsToolStripMenuItem;
        private ToolStripMenuItem showREADMEToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator6;
    }
}
