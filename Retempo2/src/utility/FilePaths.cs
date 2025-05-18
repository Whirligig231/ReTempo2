namespace Retempo2
{
    public static class FilePaths
    {
        public static string AppData(string fname)
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string fullDir = Path.Combine(appData, "Retempo2\\");
            if (!Directory.Exists(fullDir))
            {
                Directory.CreateDirectory(fullDir);
            }

            return Path.Combine(fullDir, fname);
        }

        public static string Vamp(string fname)
        {
            string? myDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (myDir == null)
            {
                throw new Exception("Where is the app? Unclear");
            }
            string fullDir = Path.Combine(myDir, "vamp\\");
            return Path.Combine(fullDir, fname);
        }
    }
}
