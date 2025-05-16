using System.Media;
using PortAudioSharp;
using Timer = System.Windows.Forms.Timer;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        private AudioStream aStream; // The internal audio stream
        private float[]? audioFileSamples; // The audio data
        private List<float>? beatmap; // List of times of beats, in seconds
        private EfficientMinMax? audioDataEmm; // Stores audio data for visuals
        private SimpleArrayGenerator? sag; // Generates the audio stream
        private int playhead; // Frame number of playhead
        private int startFrame, numFrames; // In the current window

        private Timer playTimer; // Timer to manage playback visuals and beat ticks
        private int currentBeatmapIndex; // Position of the playhead in the beatmap
        private int previousFrame; // Time of the previous frame
        private SoundPlayer clickSound;

        private const float sampleRate = 44100.0f; // TODO

        private void PlayAudio()
        {
            if (sag == null)
                return;
            sag.playheadStartFrame = playhead;
            previousFrame = playhead;
            currentBeatmapIndex = 0;
            aStream.Play();
            playTimer.Start();
        }

        private void StopAudio()
        {
            aStream.Stop();
            playTimer.Stop();
            AudioVis.Refresh();
        }

        public BeatmapEditor()
        {
            InitializeComponent();
            Version.ConvertForm(this);
            aStream = new AudioStream();
            AudioVisScroll.Maximum = 0;

            playTimer = new Timer();
            playTimer.Interval = 20;
            playTimer.Tick += PlayTimer_Tick;

            clickSound = new SoundPlayer(new MemoryStream(Properties.Resources.click));
        }

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            PlayAudio();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopAudio();
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
            audioDataEmm = null;
            AudioVis.Refresh();
            audioDataEmm = new EfficientMinMax(audioFileSamples, 2); // TODO: Support for other numbers of channels?
            startFrame = 0;
            numFrames = audioDataEmm.GetLength();
            aStream.Stop();
            sag = new SimpleArrayGenerator(audioFileSamples);
            aStream.SetCallback(sag.Callback);

            playhead = 0;

            // Debug: generate a test beatmap
            beatmap = [0.0f, 0.5f, 1.0f, 1.5f, 2.0f, 2.5f, 3.0f, 3.5f];

            AudioVis.Refresh();
        }

        private void AudioVis_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw waveform
            Brush blue = new SolidBrush(Color.Blue);
            if (audioFileSamples != null)
                WaveformDrawing.DrawWaveform(g, blue, 0, 0, AudioVis.Width, AudioVis.Height, audioDataEmm, startFrame, numFrames);

            // Draw beats
            if (audioDataEmm != null && beatmap != null)
            {
                Brush red = new SolidBrush(Color.Red);
                Pen redPen = new Pen(red);
                for (int i = 0; i < beatmap.Count; i++)
                {
                    // TODO: What needs to change to support sample rates besides 44.1k?
                    float frame = beatmap[i] * sampleRate;
                    if (frame < startFrame)
                        continue;
                    if (frame >= startFrame + numFrames)
                        break;

                    float frac = (float)(frame - startFrame) / numFrames;
                    int fracPixel = (int)MathF.Floor(frac * AudioVis.Width);
                    g.DrawLine(redPen, fracPixel, 0, fracPixel, AudioVis.Height);
                }
            }

            // Draw playhead (selected)
            if (audioDataEmm != null)
            {
                Brush black = new SolidBrush(Color.Black);
                Pen blackPen = new Pen(black);
                if (playhead >= startFrame && playhead < startFrame + numFrames)
                {
                    float frac = (float)(playhead - startFrame) / numFrames;
                    int fracPixel = (int)MathF.Floor(frac * AudioVis.Width);
                    g.DrawLine(blackPen, fracPixel, 0, fracPixel, AudioVis.Height);
                }
            }

            // Draw play line
            if (aStream.IsPlaying() && audioDataEmm != null)
            {
                Brush green = new SolidBrush(Color.Green);
                Pen greenPen = new Pen(green);
                int currentFrame = (int)((aStream.NumFramesPlayed() + playhead) % audioDataEmm.GetLength());
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
            if (audioDataEmm == null)
                return;
            startFrame = e.NewValue;
            if (startFrame + numFrames >= audioDataEmm.GetLength())
                startFrame = audioDataEmm.GetLength() - numFrames;
            AudioVis.Refresh();
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            AudioVis.Refresh();

            // Check for beat
            if (audioDataEmm == null || beatmap == null)
                return;
            int currentFrame = (int)((aStream.NumFramesPlayed() + playhead) % audioDataEmm.GetLength());
            while (currentBeatmapIndex < beatmap.Count && beatmap[currentBeatmapIndex] < (float)previousFrame / sampleRate)
                currentBeatmapIndex++;
            if (currentBeatmapIndex >= beatmap.Count)
                return;
            if (beatmap[currentBeatmapIndex] < (float)currentFrame / sampleRate)
                clickSound.Play();
            previousFrame = currentFrame;
        }

        private void AudioVis_MouseClick(object sender, MouseEventArgs e)
        {
            // TODO: is there any context where we want to click with no beatmap, not even an empty beatmap?
            if (beatmap == null)
                return;

            // Get the frame clicked on
            Control panel = AudioVis;
            float xFrac = (float)(e.X) / panel.Width;
            // Get the corresponding frame
            float mouseFrame = startFrame + numFrames * xFrac;
            
            // Snap to beats!
            // Check if there's a beat nearby
            float mouseSeconds = mouseFrame / sampleRate;
            int closestBeat = BinarySearch.Closest(beatmap, mouseSeconds);
            int snapBeat = closestBeat;
            if (snapBeat >= 0)
            {
                float snapBeatSeconds = beatmap[snapBeat];
                float diffSeconds = MathF.Abs(mouseSeconds - snapBeatSeconds);
                float diffFrames = diffSeconds * sampleRate;
                float diffFrac = diffFrames / numFrames;
                float diffPixels = diffFrac * panel.Width;

                if (diffPixels <= 5)
                {
                    mouseSeconds = snapBeatSeconds;
                    mouseFrame = snapBeatSeconds * sampleRate;
                }
                else
                    snapBeat = -1;
            }

            if (e.Button == MouseButtons.Left)
            {
                // Left button: place playhead
                bool playing = aStream.IsPlaying();
                if (playing)
                    StopAudio();

                playhead = (int)MathF.Floor(mouseFrame);

                if (playing)
                    PlayAudio();
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Right button: add/remove beat
                if (snapBeat >= 0)
                    beatmap.RemoveAt(snapBeat);
                else
                {
                    int newIndex = closestBeat;
                    if (beatmap[newIndex] < mouseSeconds)
                        newIndex++;
                    beatmap.Insert(newIndex, mouseSeconds);
                }
            }

                AudioVis.Refresh();
        }

        private void SeekStartButton_Click(object sender, EventArgs e)
        {
            bool playing = aStream.IsPlaying();
            if (playing)
                StopAudio();

            playhead = 0;

            if (playing)
                PlayAudio();

            AudioVis.Refresh();
        }
    }
}
