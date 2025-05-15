using System.Runtime.InteropServices;
using NAudio.Vorbis;
using NAudio.Wave;

namespace Retempo2
{
    public static class AudioFileLoad
    {
        public static float[]? LoadMFRFile(string fname)
        {
            if (!File.Exists(fname))
                return null;

            WaveStream reader;

            bool isOgg = false;
            if (Path.GetExtension(fname) == ".ogg" || Path.GetExtension(fname) == ".OGG")
            {
                isOgg = true;
                try
                {
                    reader = new VorbisWaveReader(fname);
                }
                catch (COMException e)
                {
                    MessageBox.Show("This audio file is invalid.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            else
            {
                try
                {
                    reader = new MediaFoundationReader(fname);
                }
                catch (COMException e)
                {
                    MessageBox.Show("This audio file is invalid.", null, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            ISampleProvider isp = reader.ToSampleProvider();
            long lengthToRead = reader.Length / 2;
            // Unknown why OGG does this
            if (isOgg)
            {
                lengthToRead /= 2;
            }
            float[] buffer = new float[lengthToRead];
            isp.Read(buffer, 0, buffer.Length);
            return buffer;
        }
    }
}