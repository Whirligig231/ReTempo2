using PortAudioSharp;

namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        public BeatmapEditor()
        {
            InitializeComponent();
            Version.ConvertForm(this);
        }

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {
            
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(PortAudio.VersionInfo.versionText);
        }
    }
}
