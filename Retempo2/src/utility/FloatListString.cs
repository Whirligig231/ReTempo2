namespace Retempo2
{
    public static class FloatListString
    {
        public static string FloatsToString(IEnumerable<float> values)
        {
            string output = "";
            foreach (float value in values)
            {
                if (output != "")
                    output += " ";
                output += value.ToString();
            }
            return output;
        }

        public static float[] StringToFloats(string str)
        {
            List<float> output = new List<float>();
            string[] words = str.Split(' ');
            foreach (string word in words)
            {
                float val;
                if (float.TryParse(word, out val))
                    output.Add(val);
            }
            return output.ToArray();
        }
    }
}
