namespace Retempo2
{
    public class SimpleArrayGenerator
    {
        private float[] samples;

        public SimpleArrayGenerator(float[] sampleArray)
        {
            samples = sampleArray;
        }

        public float[] Callback(double sampleRate, int channels, long startGlobalFrame, int framesPerBlock)
        {
            float[] output = new float[framesPerBlock * channels];
            int totalAudioFrames = samples.Length / channels;
            int startAudioFrame = (int)(startGlobalFrame % totalAudioFrames);

            int outputStart = 0;

            while (startAudioFrame + framesPerBlock >= totalAudioFrames)
            {
                Array.Copy(samples, startAudioFrame * channels, output, outputStart, (totalAudioFrames - startAudioFrame) * channels);
                framesPerBlock -= (totalAudioFrames - startAudioFrame);
                outputStart += (totalAudioFrames - startAudioFrame) * channels;
                startAudioFrame = 0;
            }
            Array.Copy(samples, startAudioFrame * channels, output, outputStart, framesPerBlock * channels);

            return output;
        }
    }
}
