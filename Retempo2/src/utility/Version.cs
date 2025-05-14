namespace Retempo2
{
    public static class Version
    {
        public static string Get()
        {
            return "\u03B1.0.2";
        }

        public static string ConvertString(string value)
        {
            return value.Replace("{VERSION}", Get());
        }

        public static void ConvertForm(Form form)
        {
            form.Text = form.Text.Replace("{VERSION}", Get());
        }
    }
}