namespace Retempo2
{
    public static class DialogSupport
    {
        public static string? GetAudioFname()
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
    }
}