namespace Retempo2
{
    public static class Version
    {
        public static string Get()
        {
            return "\u03B1.0.8";
        }

        public static string ConvertString(string value)
        {
            return value.Replace("{VERSION}", Get());
        }

        public static void ConvertControl(Control control)
        {
            control.Text = control.Text.Replace("{VERSION}", Get());
        }
    }
}