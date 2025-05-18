using System.Diagnostics;
using System.Media;
using System.Windows.Forms;
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
        private int[] playhead; // Frame number of playhead start and end
        private int startFrame, numFrames; // In the current window

        private Timer playTimer; // Timer to manage playback visuals and beat ticks
        private int currentBeatmapIndex; // Position of the playhead in the beatmap
        private int previousFrame; // Time of the previous frame
        private SoundPlayer clickSound;

        private const float sampleRate = 44100.0f; // TODO

        private int draggingBeatIndex = -1; // Index of the currently dragged beat
        private bool dragBeatHasMoved = false; // Has the currently dragged beat moved?
        private bool dragBeatIsRemovable = false; // Should we remove the dragged beat?

        private int draggingPlayheadIndex = -1; // Index of the currently dragged playhead
        private bool dragPlayheadHasMoved = false; // Has the currently dragged playhead moved?

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

            playhead = new int[2];
        }

        private void PlayAudio()
        {
            if (sag == null)
                return;
            sag.playheadStartFrame = playhead[0];
            previousFrame = playhead[0];
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

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {

        }

        private void BeatmapEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (aStream.IsPlaying())
                    StopAudio();
                else
                    PlayAudio();
            }
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

            playhead[0] = 0;
            playhead[1] = 0;

            // Beatmap starts empty
            beatmap = [];

            AudioVis.Refresh();
        }

        private void AudioVis_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw selection
            if (audioDataEmm != null)
            {
                Brush gray = new SolidBrush(Color.LightGray);
                int selectStart = int.Max(playhead[0], startFrame);
                int selectEnd = int.Min(playhead[1], startFrame + numFrames - 1);
                if (selectStart < startFrame + numFrames && selectEnd >= startFrame)
                {
                    float fracA = (float)(selectStart - startFrame) / numFrames;
                    int fracAPixel = (int)MathF.Floor(fracA * AudioVis.Width);
                    float fracB = (float)(selectEnd - startFrame) / numFrames;
                    int fracBPixel = (int)MathF.Floor(fracB * AudioVis.Width);
                    g.FillRectangle(gray, fracAPixel, 0, fracBPixel - fracAPixel, AudioVis.Height);
                }
            }

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
                if (playhead[0] >= startFrame && playhead[0] < startFrame + numFrames)
                {
                    float frac = (float)(playhead[0] - startFrame) / numFrames;
                    int fracPixel = (int)MathF.Floor(frac * AudioVis.Width);
                    g.DrawLine(blackPen, fracPixel, 0, fracPixel, AudioVis.Height);
                }
            }

            // Draw play line
            if (aStream.IsPlaying() && audioDataEmm != null)
            {
                Brush green = new SolidBrush(Color.Green);
                Pen greenPen = new Pen(green);
                int currentFrame = (int)((aStream.NumFramesPlayed() + playhead[0]) % audioDataEmm.GetLength());
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

        private void PlayTimer_Tick(object? sender, EventArgs e)
        {
            AudioVis.Refresh();

            // Check for beat
            if (audioDataEmm == null || beatmap == null)
                return;
            int currentFrame = (int)((aStream.NumFramesPlayed() + playhead[0]) % audioDataEmm.GetLength());
            while (currentBeatmapIndex < beatmap.Count && beatmap[currentBeatmapIndex] < (float)previousFrame / sampleRate)
                currentBeatmapIndex++;
            if (currentBeatmapIndex >= beatmap.Count)
                return;
            if (beatmap[currentBeatmapIndex] < (float)currentFrame / sampleRate)
                clickSound.Play();
            previousFrame = currentFrame;
        }

        private bool WithinSnapThresholdFrames(float framesA, float framesB)
        {
            Control panel = AudioVis;

            float diffFrames = MathF.Abs(framesA - framesB);
            float diffFrac = diffFrames / numFrames;
            float diffPixels = diffFrac * panel.Width;
            return (diffPixels <= 5);
        }

        private bool WithinSnapThresholdSeconds(float secondsA, float secondsB)
        {
            Control panel = AudioVis;

            float diffSeconds = MathF.Abs(secondsA - secondsB);
            float diffFrames = diffSeconds * sampleRate;
            float diffFrac = diffFrames / numFrames;
            float diffPixels = diffFrac * panel.Width;
            return (diffPixels <= 5);
        }

        private float GetFrameFromPixels(float pixels)
        {
            Control panel = AudioVis;
            float xFrac = (float)(pixels) / panel.Width;
            // Get the corresponding frame
            float frameNo = startFrame + numFrames * xFrac;
            if (frameNo < startFrame)
                frameNo = startFrame;
            if (frameNo >= startFrame + numFrames)
                frameNo = startFrame + numFrames - 1;
            return frameNo;
        }

        private void ReinsertIntoBeatmap(ref int index)
        {
            if (beatmap == null || beatmap.Count == 0)
                return;
            // Make sure that the beatmap is still in order after something moved
            while (index > 0 && beatmap[index] < beatmap[index - 1])
            {
                (beatmap[index], beatmap[index - 1]) = (beatmap[index - 1], beatmap[index]);
                index--;
            }
            while (index < beatmap.Count - 1 && beatmap[index] > beatmap[index + 1])
            {
                (beatmap[index], beatmap[index + 1]) = (beatmap[index + 1], beatmap[index]);
                index++;
            }
        }

        private void AudioVis_MouseDown(object sender, MouseEventArgs e)
        {
            // TODO: is there any context where we want to click with no beatmap, not even an empty beatmap?
            if (beatmap == null)
                return;

            // Disable when playing
            if (aStream.IsPlaying())
                return;

            // Get the corresponding frame
            float mouseFrame = GetFrameFromPixels(e.X);

            // Snap to beats!
            // Check if there's a beat nearby
            float mouseSeconds = mouseFrame / sampleRate;
            int closestBeat = BinarySearch.Closest(beatmap, mouseSeconds);
            int snapBeat = closestBeat;
            if (snapBeat >= 0)
            {
                float snapBeatSeconds = beatmap[snapBeat];

                if (WithinSnapThresholdSeconds(mouseSeconds, snapBeatSeconds))
                {
                    mouseSeconds = snapBeatSeconds;
                    mouseFrame = snapBeatSeconds * sampleRate;
                }
                else
                    snapBeat = -1;
            }

            draggingBeatIndex = -1;
            dragBeatHasMoved = false;
            draggingPlayheadIndex = -1;
            dragPlayheadHasMoved = false;

            if (e.Button == MouseButtons.Left)
            {
                if (WithinSnapThresholdFrames(mouseFrame, playhead[1]))
                    draggingPlayheadIndex = 1;
                else if (WithinSnapThresholdFrames(mouseFrame, playhead[0]))
                    draggingPlayheadIndex = 0;
                else
                {
                    playhead[0] = (int)MathF.Floor(mouseFrame);
                    playhead[1] = playhead[0];
                    draggingPlayheadIndex = 1;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                // Right button: add/remove beat
                if (snapBeat >= 0)
                {
                    draggingBeatIndex = snapBeat;
                    dragBeatIsRemovable = true;
                }
                else
                {
                    int newIndex = closestBeat;
                    if (newIndex < 0)
                        newIndex = 0;
                    else if (beatmap[newIndex] < mouseSeconds)
                        newIndex++;
                    beatmap.Insert(newIndex, mouseSeconds);
                    draggingBeatIndex = newIndex;
                    dragBeatIsRemovable = false;
                }
            }

            AudioVis.Refresh();
        }

        private void AudioVis_MouseMove(object sender, MouseEventArgs e)
        {
            if (beatmap == null)
                return;

            // Disable when playing
            if (aStream.IsPlaying())
                return;

            if (draggingBeatIndex >= 0)
            {
                float mouseFrame = GetFrameFromPixels(e.X);
                float mouseSeconds = mouseFrame / sampleRate;

                if (!WithinSnapThresholdSeconds(mouseSeconds, beatmap[draggingBeatIndex]))
                    dragBeatHasMoved = true;

                if (dragBeatHasMoved)
                {
                    beatmap[draggingBeatIndex] = mouseSeconds;
                    ReinsertIntoBeatmap(ref draggingBeatIndex);
                }

                AudioVis.Refresh();
            }

            if (draggingPlayheadIndex >= 0)
            {
                float mouseFrame = GetFrameFromPixels(e.X);
                float mouseSeconds = mouseFrame / sampleRate;
                int closestBeat = BinarySearch.Closest(beatmap, mouseSeconds);
                int snapBeat = closestBeat;
                if (snapBeat >= 0)
                {
                    float snapBeatSeconds = beatmap[snapBeat];

                    if (WithinSnapThresholdSeconds(mouseSeconds, snapBeatSeconds))
                    {
                        mouseSeconds = snapBeatSeconds;
                        mouseFrame = snapBeatSeconds * sampleRate;
                    }
                    else
                        snapBeat = -1;
                }

                if (!WithinSnapThresholdFrames(mouseFrame, playhead[draggingPlayheadIndex]))
                    dragPlayheadHasMoved = true;

                if (dragPlayheadHasMoved)
                {
                    playhead[draggingPlayheadIndex] = (int)MathF.Floor(mouseFrame);
                    if (playhead[1] < playhead[0])
                    {
                        (playhead[0], playhead[1]) = (playhead[1], playhead[0]);
                        draggingPlayheadIndex = 1 - draggingPlayheadIndex;
                    }
                }

                AudioVis.Refresh();
            }
        }

        private void AudioVis_MouseUp(object sender, MouseEventArgs e)
        {
            if (beatmap == null)
                return;

            // Disable when playing
            if (aStream.IsPlaying())
                return;

            if (draggingBeatIndex >= 0)
            {
                if (dragBeatIsRemovable && !dragBeatHasMoved)
                    beatmap.RemoveAt(draggingBeatIndex);
                draggingBeatIndex = -1;

                AudioVis.Refresh();
            }

            if (draggingPlayheadIndex >= 0)
            {
                draggingPlayheadIndex = -1;
                AudioVis.Refresh();
            }

            ManualTempoButton.Enabled = (playhead[1] > playhead[0]);
        }

        private void SeekStartButton_Click(object sender, EventArgs e)
        {
            bool playing = aStream.IsPlaying();
            if (playing)
                StopAudio();

            playhead[0] = 0;
            playhead[1] = 0;

            if (playing)
                PlayAudio();

            AudioVis.Refresh();
        }

        private void ManualTempoButton_Click(object sender, EventArgs e)
        {
            Form form = new ManualTempoDialog((float)(playhead[1] - playhead[0]) / sampleRate);
            form.ShowDialog();
        }
    }
}
