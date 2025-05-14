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
            
            if (startAudioFrame + framesPerBlock >= totalAudioFrames)
            {
                Array.Copy(samples, startAudioFrame * channels, output, 0, (totalAudioFrames - startAudioFrame) * channels);
                framesPerBlock -= (totalAudioFrames - startAudioFrame);
                Array.Copy(samples, 0, output, (totalAudioFrames - startAudioFrame) * channels, framesPerBlock * channels);
            }
            else
            {
                Array.Copy(samples, startAudioFrame * channels, output, 0, framesPerBlock * channels);
            }

            return output;
        }
    }
}
