using PortAudioSharp;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        private AudioStream aStream;
        private float[]? audioFileSamples;
        private EfficientMinMax audioDataEmm;

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

        private void OpenButton_Click(object sender, EventArgs e)
        {
            string? fname = DialogSupport.GetAudioFname();
            if (fname == null)
                return;
            audioFileSamples = AudioFileLoad.LoadMFRFile(fname);
            if (audioFileSamples == null)
            {
                AudioVis.Refresh();
                return;
            }
            audioDataEmm = new EfficientMinMax(audioFileSamples, 2); // TODO: Support for other numbers of channels?
            AudioVis.Refresh();
            aStream.Stop();
            SimpleArrayGenerator sag = new SimpleArrayGenerator(audioFileSamples);
            aStream.SetCallback(sag.Callback);
        }

        private void AudioVis_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush b = new SolidBrush(Color.Blue);
            if (audioFileSamples != null)
                WaveformDrawing.DrawWaveform(g, b, 0, 0, AudioVis.Width, AudioVis.Height, audioDataEmm, 2);
        }

        private void AudioVis_Resize(object sender, EventArgs e)
        {
            AudioVis.Refresh();
        }
    }
}
