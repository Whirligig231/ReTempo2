namespace Retempo2
{
    public class EfficientMinMax
    {
        private List<float>[] channelValues; // Actual value of each datapoint
        private List<float>[] channelMins; // Minimum of each section
        private List<float>[] channelMaxs; // Maximum of each section
        private List<int> powerStartIndices; // Where each power of two starts in the channel list (offset by one, [0] -> 2^1)

        public EfficientMinMax(float[] data, int channels)
        {
            channelValues = new List<float>[channels];
            channelMins = new List<float>[channels];
            channelMaxs = new List<float>[channels];
            powerStartIndices = new List<int>();

            for (int c = 0; c < channels; c++)
            {
                List<float> thisChannel = new List<float>();
                for (int i = 0; i < data.Length / channels; i++)
                {
                    thisChannel.Add(data[i * channels + c]);
                }
                channelValues[c] = thisChannel;

                // Precompute min/max values!
                channelMins[c] = new List<float>();
                channelMaxs[c] = new List<float>();
                int numberOfPowers = data.Length / channels;
                numberOfPowers /= 2;
                int level = 0;
                if (c == 0)
                    powerStartIndices.Add(0);

                // Begin with the first layer
                for (int i = 0; i < numberOfPowers; i++)
                {
                    channelMaxs[c].Add(MathF.Max(channelValues[c][2 * i], channelValues[c][2 * i + 1]));
                    channelMins[c].Add(MathF.Min(channelValues[c][2 * i], channelValues[c][2 * i + 1]));
                }

                // The remaining layers
                while (numberOfPowers > 0)
                {
                    numberOfPowers /= 2;
                    level++;
                    if (c == 0)
                        powerStartIndices.Add(channelMins[0].Count);
                    int previousPSI = powerStartIndices[level - 1];
                    for (int i = 0; i < numberOfPowers;i++)
                    {
                        channelMaxs[c].Add(MathF.Max(channelMaxs[c][previousPSI + 2 * i], channelMaxs[c][previousPSI + 2 * i + 1]));
                        channelMins[c].Add(MathF.Min(channelMins[c][previousPSI + 2 * i], channelMins[c][previousPSI + 2 * i + 1]));
                    }
                }
            }
        }

        // Largest power of 2 dividing x
        public int V2(int x)
        {
            if (x == 0) return int.MaxValue;
            if ((x & 1) > 0) return 1;
            return V2(x >> 1) << 1;
        }

        // Note that maxI is inclusive, of course
        public float FindMax(int channel, int minI, int maxI)
        {
            int currentI = minI;
            float currentMax = channelValues[channel][minI];

            while (currentI < maxI)
            {
                int maxIncrement = V2(currentI);
                int increment = 1;
                int layer = 0;
                while (increment <= maxIncrement && currentI + increment <= maxI)
                {
                    increment <<= 1;
                    layer++;
                }
                increment >>= 1;
                layer--;
                if (layer == 0)
                {
                    currentMax = MathF.Max(currentMax, channelValues[channel][currentI]);
                }
                else
                {
                    int powerStartIndex = powerStartIndices[layer - 1];
                    currentMax = MathF.Max(currentMax, channelMaxs[channel][powerStartIndex + currentI / increment]);
                }
                currentI += increment;
            }

            return currentMax;
        }

        public float FindMin(int channel, int minI, int maxI)
        {
            int currentI = minI;
            float currentMin = channelValues[channel][minI];

            while (currentI < maxI)
            {
                int maxIncrement = V2(currentI);
                int increment = 1;
                int layer = 0;
                while (increment <= maxIncrement && currentI + increment <= maxI)
                {
                    increment <<= 1;
                    layer++;
                }
                increment >>= 1;
                layer--;
                if (layer == 0)
                {
                    currentMin = MathF.Min(currentMin, channelValues[channel][currentI]);
                }
                else
                {
                    int powerStartIndex = powerStartIndices[layer - 1];
                    currentMin = MathF.Min(currentMin, channelMins[channel][powerStartIndex + currentI / increment]);
                }
                currentI += increment;
            }

            return currentMin;
        }

        public int GetLength()
        {
            return channelValues[0].Count;
        }
    }
}
