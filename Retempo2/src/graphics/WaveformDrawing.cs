namespace Retempo2
{
    public static class WaveformDrawing
    {
        // TODO: Use memoization of some form to improve performance here. Unacceptably slow when resizing the window for a long song
        // Idea: store min/max for each channel and for each power-of-two grouping, as a sort of "mipmap"
        // This should only require linear extra storage and improve performance to logarithmic
        public static void DrawWaveform(Graphics g, Brush b, int left, int top, int width, int height, float[] data, int channels)
        {
            int amplitude = height / 2 / channels;

            for (int c = 0; c < channels; c++)
            {
                bool[] hitMe = new bool[width];
                int[] maxima = new int[width];
                int[] minima = new int[width];
                for (int i = 0; i < data.Length / channels; i++)
                {
                    int xPos = (int)MathF.Floor((float)i / (data.Length / channels) * width);
                    int yPos = (int)MathF.Round(data[i * channels + c] * amplitude);
                    if (!hitMe[xPos])
                    {
                        hitMe[xPos] = true;
                        maxima[xPos] = yPos;
                        minima[xPos] = yPos;
                    }
                    else
                    {
                        if (yPos > maxima[xPos])
                            maxima[xPos] = yPos;
                        if (yPos < minima[xPos])
                            minima[xPos] = yPos;
                    }
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
