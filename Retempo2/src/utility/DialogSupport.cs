namespace Retempo2
{
    public static class DialogSupport
    {
        public static string? GetAudioOpenFname()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Audio files(*.aac;*.mp3;*.m4a;*.wav;*.ogg)|*.AAC;*.MP3;*.M4A;*.WAV;*.OGG|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
                return ofd.FileName;
            else
                return null;
        }

        public static string? GetBeatmapOpenFname()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Beatmap files(*.rtbm)|*.RTBM|All files (*.*)|*.*";
            ofd.RestoreDirectory = true;
            DialogResult res = ofd.ShowDialog();
            if (res == DialogResult.OK)
                return ofd.FileName;
            else
                return null;
        }

        public static string? GetBeatmapSaveFname()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Beatmap files(*.rtbm)|*.RTBM|All files (*.*)|*.*";
            sfd.RestoreDirectory = true;
            DialogResult res = sfd.ShowDialog();
            if (res == DialogResult.OK)
                return sfd.FileName;
            else
                return null;
        }

        // 0 = cancel
        // 1 = proceed
        // 2 = save first
        public static int DirtyDialog()
        {
            DialogResult result = MessageBox.Show("Unsaved changes--would you like to save?", null, MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
                return 2;
            if (result == DialogResult.No)
                return 1;
            return 0;
        }
    }
}