namespace Retempo2
{
    public static class WaveformDrawing
    {
        // TODO: Use memoization of some form to improve performance here. Unacceptably slow when resizing the window for a long song
        // Idea: store min/max for each channel and for each power-of-two grouping, as a sort of "mipmap"
        // This should only require linear extra storage and improve performance to logarithmic
        public static void DrawWaveform(Graphics g, Brush b, int left, int top, int width, int height, EfficientMinMax? emm, int startFrame, int frameCount)
        {
            if (emm == null)
                return;

            int channels = emm.GetChannels();
            int amplitude = height / 2 / channels;

            for (int c = 0; c < channels; c++)
            {
                bool[] hitMe = new bool[width];
                int[] maxima = new int[width];
                int[] minima = new int[width];

                for (int xPos = 0; xPos < width; xPos++)
                {
                    int minI = (int)MathF.Floor((float)xPos / width * frameCount + startFrame);
                    int maxI = (int)MathF.Floor((float)(xPos + 1) / width * frameCount + startFrame);
                    float minVal = emm.FindMin(c, minI, maxI);
                    float maxVal = emm.FindMax(c, minI, maxI);
                    minima[xPos] = (int)MathF.Round(minVal * amplitude);
                    maxima[xPos] = (int)MathF.Round(maxVal * amplitude);
                }

                List<PointF> points = new List<PointF>();
                for (int x = 0; x < width; x++)
                {
                    points.Add(new PointF(x, amplitude * (2 * c + 1) - maxima[x]));
                }
                for (int x = width - 1; x >= 0; x--)
                {
                    points.Add(new PointF(x, amplitude * (2 * c + 1) - minima[x]));
                }

                g.FillPolygon(b, points.ToArray());
            }
        }
    }
}
