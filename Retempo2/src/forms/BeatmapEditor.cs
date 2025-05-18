using System.Diagnostics;
using System.Media;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using PortAudioSharp;
using Timer = System.Windows.Forms.Timer;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        private AudioStream aStream; // The internal audio stream
        private string? audioFname; // Name of the audio file
        private float[]? audioFileSamples; // The audio data
        private List<float>? beatmap; // List of times of beats, in seconds
        private EfficientMinMax? audioDataEmm; // Stores audio data for visuals
        private ArrayWithBeatsGenerator? generator; // Generates the audio stream
        private int[] playhead; // Frame number of playhead start and end
        private int startFrame, numFrames; // In the current window

        private Timer visualPlayTimer; // Timer to manage playback visuals
        // private Timer audioPlayTimer; // Timer to manage beat ticks
        private int currentBeatmapIndex; // Position of the playhead in the beatmap
        private int previousFrame; // Time of the previous frame
        // private SoundPlayer clickSound;
        private float[] clickSoundSamples; // Click sound

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

            visualPlayTimer = new Timer();
            visualPlayTimer.Interval = 20;
            visualPlayTimer.Tick += VisualPlayTimer_Tick;

            // audioPlayTimer = new Timer();
            // audioPlayTimer.Interval = 1;
            // audioPlayTimer.Tick += AudioPlayTimer_Tick;

            // clickSound = new SoundPlayer(new MemoryStream(Properties.Resources.click));

            // Load the click sound as an array
            clickSoundSamples = AudioFileHandling.LoadMFRFile(FilePaths.Include("click.wav")) ?? [];

            playhead = new int[2];
        }

        private void PlayAudio()
        {
            if (generator == null)
                return;
            generator.playheadStartFrame = playhead[0];
            previousFrame = playhead[0];
            currentBeatmapIndex = 0;
            aStream.Play();
            visualPlayTimer.Start();
            // audioPlayTimer.Start(); Legacy code
        }

        private void StopAudio()
        {
            aStream.Stop();
            visualPlayTimer.Stop();
            // audioPlayTimer.Stop(); Legacy code
            AudioVis.Refresh();
        }

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {

        }

        private void SeekToStart()
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

        private void SeekToEnd()
        {
            if (audioDataEmm == null)
                return;

            bool playing = aStream.IsPlaying();
            if (playing)
                StopAudio();

            playhead[0] = audioDataEmm.GetLength() - 1;
            playhead[1] = playhead[0];

            if (playing)
                PlayAudio();

            AudioVis.Refresh();
        }

        private void SeekToPrevFrame()
        {
            if (audioDataEmm == null)
                return;

            if (beatmap == null)
                return;

            if (aStream.IsPlaying())
                return;

            playhead[0]--;
            if (playhead[0] < 0)
                playhead[0] = 0;
            playhead[1] = playhead[0];

            AudioVis.Refresh();
        }

        private void SeekToNextFrame()
        {
            if (audioDataEmm == null)
                return;

            if (beatmap == null)
                return;

            if (aStream.IsPlaying())
                return;

            playhead[0]++;
            if (playhead[0] >= audioDataEmm.GetLength())
                playhead[0] = audioDataEmm.GetLength() - 1;
            playhead[1] = playhead[0];

            AudioVis.Refresh();
        }

        private void SeekToPrevBeat()
        {
            if (audioDataEmm == null)
                return;

            if (beatmap == null)
                return;

            if (aStream.IsPlaying())
                return;

            float playheadSeconds = (float)playhead[0] / sampleRate;
            int startIndex = GetBeatIndex(playheadSeconds);
            if (startIndex < 0)
            {
                playhead[0] = 0;
                playhead[1] = 0;
                return;
            }
            if (startIndex >= beatmap.Count || beatmap[startIndex] >= playheadSeconds)
                startIndex--;
            if (startIndex < 0)
            {
                playhead[0] = 0;
                playhead[1] = 0;
                return;
            }
            playhead[0] = (int)MathF.Floor(beatmap[startIndex] * sampleRate);
            playhead[1] = playhead[0];

            AudioVis.Refresh();
        }

        private void SeekToNextBeat()
        {
            if (audioDataEmm == null)
                return;

            if (beatmap == null)
                return;

            if (aStream.IsPlaying())
                return;

            float playheadSeconds = (float)playhead[0] / sampleRate;
            int startIndex = GetBeatIndex(playheadSeconds);
            if (startIndex >= beatmap.Count)
            {
                playhead[0] = audioDataEmm.GetLength() - 1;
                playhead[1] = playhead[0];
                return;
            }
            if (startIndex < 0 || beatmap[startIndex] <= playheadSeconds + 1.0f / sampleRate)
                startIndex++;
            if (startIndex >= beatmap.Count)
            {
                playhead[0] = audioDataEmm.GetLength() - 1;
                playhead[1] = playhead[0];
                return;
            }
            playhead[0] = (int)MathF.Floor(beatmap[startIndex] * sampleRate);
            playhead[1] = playhead[0];

            AudioVis.Refresh();
        }

        private void BeatmapEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                if (aStream.IsPlaying() && (e.Modifiers & Keys.Shift) == 0)
                    StopAudio();
                else
                    PlayAudio();
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right)
            {
                if (e.KeyCode == Keys.Left && (e.Modifiers & Keys.Shift) == 0)
                {
                    SeekToPrevFrame();
                }
                else if (e.KeyCode == Keys.Right && (e.Modifiers & Keys.Shift) == 0)
                {
                    SeekToNextFrame();
                }
                else if (e.KeyCode == Keys.Left)
                {
                    SeekToPrevBeat();
                }
                else
                {
                    SeekToNextBeat();
                }
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

        private void TrimBeatmap()
        {
            if (audioDataEmm == null)
                return;
            if (beatmap == null)
                return;
            for (int i = 0; i < beatmap.Count; i++)
            {
                if (beatmap[i] * sampleRate >= audioDataEmm.GetLength())
                {
                    beatmap.RemoveRange(i, beatmap.Count - i);
                    break;
                }
            }
        }

        private void ReplaceSample(string? fname)
        {
            if (fname == null)
                return;
            audioFname = fname;
            audioFileSamples = AudioFileHandling.LoadMFRFile(fname);
            if (audioFileSamples == null)
            {
                AudioVis.Refresh();
                return;
            }
            audioDataEmm = null;
            AudioVis.Refresh();
            audioDataEmm = new EfficientMinMax(audioFileSamples, 2); // TODO: Support for other numbers of channels?

            if (startFrame + numFrames > audioDataEmm.GetLength())
            {
                numFrames = audioDataEmm.GetLength() - startFrame;
            }

            aStream.Stop();
            if (beatmap == null)
                beatmap = [];
            else
                TrimBeatmap();
            generator = new ArrayWithBeatsGenerator(audioFileSamples, beatmap, clickSoundSamples);
            aStream.SetCallback(generator.Callback);

            playhead[0] = int.Min(playhead[0], audioDataEmm.GetLength() - 1);
            playhead[1] = int.Min(playhead[1], audioDataEmm.GetLength() - 1);

            AudioVis.Refresh();
        }

        private void LoadNewSample()
        {
            string? fname = DialogSupport.GetAudioFname();
            ReplaceSample(fname);
        }

        private void ReloadSample()
        {
            if (audioFname == null || !File.Exists(audioFname))
                LoadNewSample();
            else
                ReplaceSample(audioFname);
        }

        private void CreateNewDocument()
        {
            string? fname = DialogSupport.GetAudioFname();
            if (fname == null)
                return;
            ReplaceSample(fname);

            if (audioDataEmm == null)
                return;
            if (beatmap == null)
                return;
            beatmap.Clear();
            startFrame = 0;
            numFrames = audioDataEmm.GetLength();
            playhead[0] = 0;
            playhead[1] = 0;

            AudioVis.Refresh();
        }

        private void OpenButton_Click(object sender, EventArgs e)
        {
            LoadNewSample();
        }

        private void AudioVis_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Draw waveform
            Brush blue = new SolidBrush(Color.Blue);
            if (audioFileSamples != null)
                WaveformDrawing.DrawWaveform(g, blue, 0, 0, AudioVis.Width, AudioVis.Height, audioDataEmm, startFrame, numFrames);

            // Draw selection
            if (audioDataEmm != null)
            {
                Brush gray = new SolidBrush(Color.FromArgb(51, 0, 0, 0));
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

            // Draw beats
            if (audioDataEmm != null && beatmap != null)
            {
                Brush red = new SolidBrush(Color.FromArgb(153, 255, 0, 0));
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

        private void VisualPlayTimer_Tick(object? sender, EventArgs e)
        {
            AudioVis.Refresh();
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
            AutoTempoButton.Enabled = (playhead[1] > playhead[0]);
        }

        private void SeekStartButton_Click(object sender, EventArgs e)
        {
            SeekToStart();
        }

        private int GetBeatIndex(float startSeconds)
        {
            if (beatmap == null)
                return -1;
            int startIndex;
            if (beatmap.Count == 0)
                startIndex = 0;
            else
            {
                startIndex = BinarySearch.Closest(beatmap, startSeconds);
                if (beatmap[startIndex] < startSeconds)
                    startIndex++;
            }
            return startIndex;
        }

        private void DeleteBeatsInRange(float startSeconds, float lengthSeconds)
        {
            if (beatmap == null)
                return;
            int startIndex = GetBeatIndex(startSeconds);

            // Delete beats in the affected area
            while (startIndex < beatmap.Count && beatmap[startIndex] <= (startSeconds + lengthSeconds))
                beatmap.RemoveAt(startIndex);
        }

        private void FillManualTempoBeats(float beats)
        {
            if (beatmap == null)
                return;
            float startSeconds = (float)playhead[0] / sampleRate;
            float lengthSeconds = (float)(playhead[1] - playhead[0]) / sampleRate;

            DeleteBeatsInRange(startSeconds, lengthSeconds);

            // Make a list of new beats
            List<float> newBeats = new List<float>();
            for (int i = 0; i <= MathF.Floor(beats); i++)
            {
                newBeats.Add(((float)i / beats) * lengthSeconds + startSeconds);
            }

            int startIndex = GetBeatIndex(startSeconds);
            // Insert the new list
            beatmap.InsertRange(startIndex, newBeats);
            AudioVis.Refresh();
        }

        private void MapBeatsByTempo()
        {
            if (beatmap == null)
                return;
            // Get the suggested tempo
            float? suggestedBeats = null;
            if (beatmap.Count > 0)
            {
                float startSeconds = (float)playhead[0] / sampleRate;
                float endSeconds = (float)playhead[1] / sampleRate;
                int firstBeat = GetBeatIndex(startSeconds);
                int lastBeat = GetBeatIndex(endSeconds);
                if (lastBeat >= beatmap.Count || beatmap[lastBeat] > endSeconds)
                    lastBeat--;
                if (startSeconds <= beatmap[firstBeat] && firstBeat < lastBeat && beatmap[lastBeat] <= endSeconds)
                {
                    float lengthSeconds = endSeconds - startSeconds;
                    float beatLength = beatmap[lastBeat] - beatmap[firstBeat];
                    suggestedBeats = (lengthSeconds / beatLength) * (lastBeat - firstBeat);
                }
            }

            Form form = new ManualTempoDialog((float)(playhead[1] - playhead[0]) / sampleRate, suggestedBeats, FillManualTempoBeats);
            form.ShowDialog();
        }

        private void ManualTempoButton_Click(object sender, EventArgs e)
        {
            MapBeatsByTempo();
        }

        private void DetectBeats()
        {
            if (audioFileSamples == null)
                return;
            if (beatmap == null)
                return;
            float[] saveData = new float[(playhead[1] - playhead[0]) * 2];
            Array.Copy(audioFileSamples, playhead[0] * 2, saveData, 0, saveData.Length);

            float[]? beats = BeatrootWrapper.GetBeats(saveData);
            if (beats == null)
                return;

            float startSeconds = (float)playhead[0] / sampleRate;
            float lengthSeconds = (float)(playhead[1] - playhead[0]) / sampleRate;

            DeleteBeatsInRange(startSeconds, lengthSeconds);

            // Make a list of new beats
            List<float> newBeats = new List<float>();
            foreach (float beat in beats)
            {
                newBeats.Add(beat + startSeconds);
            }

            int startIndex = GetBeatIndex(startSeconds);
            // Insert the new list
            beatmap.InsertRange(startIndex, newBeats);
            AudioVis.Refresh();
        }

        private void AutoTempoButton_Click(object sender, EventArgs e)
        {
            DetectBeats();
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (audioDataEmm == null)
                return;

            if (beatmap == null)
                return;

            if (aStream.IsPlaying())
                return;

            playhead[0] = 0;
            playhead[1] = audioDataEmm.GetLength() - 1;
            ManualTempoButton.Enabled = (playhead[1] > playhead[0]);
            AutoTempoButton.Enabled = (playhead[1] > playhead[0]);
            AudioVis.Refresh();
        }

        private void playStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aStream.IsPlaying())
                StopAudio();
            else
                PlayAudio();
        }

        private void restartPlaybackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayAudio();
        }

        private void toStartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeekToStart();
        }

        private void toEndToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeekToEnd();
        }

        private void toPrevFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeekToPrevFrame();

        }

        private void toNextFrameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeekToNextFrame();
        }

        private void toPrevBeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeekToPrevBeat();
        }

        private void toNextBeatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SeekToNextBeat();
        }

        private void mapBeatsByTempoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MapBeatsByTempo();
        }

        private void detectBeatsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DetectBeats();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewDocument();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadNewSample();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReloadSample();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
