using System.Runtime.InteropServices;
using NAudio.Wave;

namespace Retempo2
{
    public static class AudioFileLoad
    {
        public static float[]? LoadMFRFile(string fname)
        {
            if (!File.Exists(fname))
                return null;
            MediaFoundationReader reader;
            try
            {
                reader = new MediaFoundationReader(fname);
            } catch (COMException e)
            {
                MessageBox.Show("This audio file is invalid.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            ISampleProvider isp = reader.ToSampleProvider();
            float[] buffer = new float[reader.Length / 2];
            isp.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}