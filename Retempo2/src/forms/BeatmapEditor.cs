using PortAudioSharp;
using Timer = System.Windows.Forms.Timer;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        private AudioStream aStream;
        private float[]? audioFileSamples;
        private EfficientMinMax audioDataEmm;
        private int startFrame, numFrames; // In the current window
        private Timer playTimer;

        public BeatmapEditor()
        {
            InitializeComponent();
            Version.ConvertForm(this);
            aStream = new AudioStream();
            AudioVisScroll.Maximum = 0;

            playTimer = new Timer();
            playTimer.Interval = 20;
            playTimer.Tick += PlayTimer_Tick;
        }

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            aStream.Play();
            playTimer.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            aStream.Stop();
            playTimer.Stop();
            AudioVis.Refresh();
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
            Brush blue = new SolidBrush(Color.Blue);
            if (audioFileSamples != null)
                WaveformDrawing.DrawWaveform(g, blue, 0, 0, AudioVis.Width, AudioVis.Height, audioDataEmm, startFrame, numFrames);

            if (aStream.IsPlaying() && audioDataEmm != null)
            {
                Brush green = new SolidBrush(Color.Green);
                Pen greenPen = new Pen(green);
                int currentFrame = (int)(aStream.NumFramesPlayed() % audioDataEmm.GetLength());
                if (currentFrame >= startFrame && currentFrame < startFrame + numFrames)
                {
                    float frac = (float)(currentFrame - startFrame) / numFrames;
                    int fracPixel = (int)MathF.Floor(frac * AudioVis.Width);
                    g.DrawLine(greenPen, fracPixel, 0, fracPixel, AudioVis.Height);
                }
            }
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
            if (newEnd == newStart)
                newEnd++;
            if (newStart < 0)
                newStart = 0;
            if (newEnd > audioDataEmm.GetLength())
                newEnd = audioDataEmm.GetLength();
            if (newEnd == newStart)
                newStart--;
            startFrame = newStart;
            numFrames = newEnd - newStart;

            AudioVis.Refresh();

            AudioVisScroll.Maximum = audioDataEmm.GetLength();
            AudioVisScroll.LargeChange = numFrames;
            AudioVisScroll.Value = newStart;
        }

        private void AudioVisScroll_Scroll(object sender, ScrollEventArgs e)
        {
            startFrame = e.NewValue;
            if (startFrame + numFrames >= audioDataEmm.GetLength())
                startFrame = audioDataEmm.GetLength() - numFrames;
            AudioVis.Refresh();
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            AudioVis.Refresh();
        }
    }
}
