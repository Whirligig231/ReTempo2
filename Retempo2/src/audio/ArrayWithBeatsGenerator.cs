namespace Retempo2
{
    public class ArrayWithBeatsGenerator
    {
        private float[] samples;
        private List<float> beats;
        private float[] clickSamples;

        public int playheadStartFrame;

        public ArrayWithBeatsGenerator(float[] sampleArray, List<float> beatArray, float[] clickSampleArray)
        {
            samples = sampleArray;
            beats = beatArray;
            clickSamples = clickSampleArray;
        }

        public float[] Callback(double sampleRate, int channels, long startGlobalFrame, int framesPerBlock)
        {
            float[] output = new float[framesPerBlock * channels];
            int totalAudioFrames = samples.Length / channels;

            float[] thisFrame = new float[channels];
            int currentFrame = (int)((startGlobalFrame + playheadStartFrame) % totalAudioFrames);
            int clickFrame = -1;
            int currentBeat = 0;

            while (currentBeat < beats.Count && beats[currentBeat] < (float)currentFrame / 44100.0f)
                currentBeat++;

            if (currentBeat > 0)
            {
                int lastBeatFrame = (int)MathF.Floor(beats[currentBeat - 1] * 44100.0f); // When did the tick start?
                clickFrame = currentFrame - lastBeatFrame;
                if (clickFrame >= clickSamples.Length / channels)
                    clickFrame = -1;
            }

            for (int frameNumber = 0; frameNumber < framesPerBlock; frameNumber++)
            {
                float currentSeconds = (float)currentFrame / 44100.0f; // TODO sample rate?
                float nextSeconds = (float)(currentFrame + 1) / 44100.0f;

                while (currentBeat < beats.Count && beats[currentBeat] < nextSeconds)
                {
                    clickFrame = 0;
                    currentBeat++;
                }

                if (clickFrame >= clickSamples.Length / channels)
                    clickFrame = -1;

                Array.Clear(thisFrame, 0, channels);

                if (clickFrame >= 0)
                {
                    for (int i = 0; i < channels; i++)
                        thisFrame[i] += clickSamples[clickFrame * channels + i];
                }

                for (int i = 0; i < channels; i++)
                    thisFrame[i] += samples[currentFrame * channels + i];

                Array.Copy(thisFrame, 0, output, frameNumber * channels, channels);

                currentFrame++;
                if (clickFrame >= 0)
                    clickFrame++;

                if (currentFrame >= totalAudioFrames)
                {
                    currentFrame = 0;
                    currentBeat = 0;
                }
            }

            return output;
        }
    }
}
