using System.Diagnostics;

namespace Retempo2
{
    public static class BeatrootWrapper
    {
        public static float[]? GetBeats(float[] sampleData)
        {
            string inFname = FilePaths.AppData("temp.wav");
            try
            {
                AudioFileHandling.SaveWavFile(inFname, sampleData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to save the sample data");
                return null;
            }
            string outFname = FilePaths.AppData("temp.txt");
            string exeFname = FilePaths.Vamp("VampSimpleHost.exe");
            string args = "beatroot-vamp:beatroot \"" + inFname + "\" -o \"" + outFname + "\"";

            Process p = new Process();
            p.StartInfo = new ProcessStartInfo(exeFname, args);
            p.StartInfo.CreateNoWindow = true;
            try
            {
                p.Start();
                p.WaitForExit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to run Beatroot");
                return null;
            }

            if (!File.Exists(outFname))
            {
                MessageBox.Show("Beatroot returned no data");
                return null;
            }

            List<float> beats = new List<float>();

            try
            {
                using StreamReader reader = new StreamReader(outFname);
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    string lineTrim = line.Trim();
                    string lineSub = lineTrim.Substring(0, lineTrim.IndexOf(':'));
                    try
                    {
                        float beat = float.Parse(lineSub);
                        beats.Add(beat);
                    }
                    catch (Exception ex)
                    {
                        // Let it be, might be a blank line
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to load Beatroot data");
                return null;
            }

            try
            {
                File.Delete(inFname);
                File.Delete(outFname);
            }
            catch (Exception ex)
            {
                // Let it be
            }

            return beats.ToArray();
        }
    }
}
