using PortAudioSharp;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        public AudioStream aStream;

        public BeatmapEditor()
        {
            InitializeComponent();
            Version.ConvertForm(this);
            aStream = new AudioStream();
        }

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            aStream.Play();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            aStream.Stop();
        }
    }
}
