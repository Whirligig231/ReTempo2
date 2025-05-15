using PortAudioSharp;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        private AudioStream aStream;
        private float[]? audioFileSamples;
        private EfficientMinMax audioDataEmm;
        private int startFrame, numFrames;

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
            startFrame = 0;
            numFrames = audioDataEmm.GetLength();
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
                WaveformDrawing.DrawWaveform(g, b, 0, 0, AudioVis.Width, AudioVis.Height, audioDataEmm, startFrame, numFrames);
        }

        private void AudioVis_Resize(object sender, EventArgs e)
        {
            AudioVis.Refresh();
        }

        private void BeatmapEditor_MouseWheel(object sender, MouseEventArgs e)
        {
            if (audioDataEmm == null)
                return;

            // Get zoom factor
            float zoomFac = MathF.Pow(2.0f, -(float)e.Delta / 120.0f);
            // Get mouse X as a fraction
            Control panel = AudioVis;
            float xFrac = (float)(e.X - panel.Left) / panel.Width;
            // Get the corresponding frame
            float centerFrame = startFrame + numFrames * xFrac;
            // Set the new start and numFrames
            int newStart = (int)MathF.Round(centerFrame + (startFrame - centerFrame) * zoomFac);
            int newEnd = (int)MathF.Round(centerFrame + (startFrame + numFrames - centerFrame) * zoomFac);
            if (newStart < 0)
                newStart = 0;
            if (newEnd > audioDataEmm.GetLength())
                newEnd = audioDataEmm.GetLength();
            startFrame = newStart;
            numFrames = newEnd - newStart;

            AudioVis.Refresh();
        }
    }
}
