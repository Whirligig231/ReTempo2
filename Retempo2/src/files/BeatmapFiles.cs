namespace Retempo2
{
    public static class BeatmapFiles
    {
        public static void SaveBeatmapToFile(string fname, float[] samples, float[] beats)
        {
            using (FileStream fs = File.Create(fname))
            {
                using (BinaryWriter bw = new BinaryWriter(fs))
                {
                    bw.Write('R');
                    bw.Write('T');
                    bw.Write('B');
                    bw.Write('M');
                    bw.Write(1);
                    bw.Write(samples.Length);
                    foreach (float sample in samples)
                    {
                        bw.Write(sample);
                    }
                    bw.Write(beats.Length);
                    foreach (float beat in beats)
                    {
                        bw.Write(beat);
                    }
                }
            }
        }

        public static void LoadBeatmapFromFile(string fname, out float[] samples, out float[] beats)
        {
            List<float> sampleList = new List<float>();
            List<float> beatList = new List<float>();

            using (FileStream fs = File.Open(fname, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    if (br.ReadChar() != 'R')
                        throw new IOException();
                    if (br.ReadChar() != 'T')
                        throw new IOException();
                    if (br.ReadChar() != 'B')
                        throw new IOException();
                    if (br.ReadChar() != 'M')
                        throw new IOException();
                    if (br.ReadInt32() != 1)
                        throw new IOException();
                    int numSamples = br.ReadInt32();
                    for (int i = 0; i < numSamples; i++)
                    {
                        sampleList.Add(br.ReadSingle());
                    }
                    int numBeats = br.ReadInt32();
                    for (int i = 0;i < numBeats; i++)
                    {
                        beatList.Add(br.ReadSingle());
                    }
                }
            }

            samples = sampleList.ToArray();
            beats = beatList.ToArray();
        }
    }
}
