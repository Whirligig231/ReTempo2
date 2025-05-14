using NAudio.Wave;

namespace Retempo2
{
    public static class AudioFileLoad
    {
        public static float[]? LoadMFRFile(string fname)
        {
            if (!File.Exists(fname))
                return null;
            MediaFoundationReader reader = new MediaFoundationReader(fname);
            ISampleProvider isp = reader.ToSampleProvider();
            float[] buffer = new float[reader.Length / 2];
            isp.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}