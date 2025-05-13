namespace Retempo2
{
    public partial class BeatmapEditor : Form
    {
        public BeatmapEditor()
        {
            InitializeComponent();
        }

        private void BeatmapEditor_Load(object sender, EventArgs e)
        {

        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Hand.Play();
        }
    }
}
