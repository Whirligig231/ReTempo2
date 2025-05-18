using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Retempo2
{
    public partial class ManualTempoDialog : Form
    {
        private float seconds; // Number of seconds in use
        private string beatsText = ""; // Number of beats
        private string bpmText = ""; // BPM value

        private bool ignoreChanged = false; // Ignore text changes

        public ManualTempoDialog(float numSeconds)
        {
            InitializeComponent();
            seconds = numSeconds;

            SecondsLabel.Text = "Selection: " + seconds.ToString("0.000") + " seconds";
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BeatsBox_TextChanged(object sender, EventArgs e)
        {
            if (ignoreChanged)
                return;

            ignoreChanged = true;
            if (BeatsBox.Text == "")
            {
                beatsText = "";
                bpmText = "";
                BPMBox.Text = "";
                ignoreChanged = false;
                return;
            }

            float beats;
            if (!float.TryParse(BeatsBox.Text, out beats))
            {
                BeatsBox.Text = beatsText;
                BeatsBox.Select(BeatsBox.Text.Length, 0);
            }
            else
            {
                float bpm = beats / (seconds / 60.0f);
                beatsText = BeatsBox.Text;
                bpmText = bpm.ToString();
                BPMBox.Text = bpmText;
            }

            ignoreChanged = false;
        }

        private void BPMBox_TextChanged(object sender, EventArgs e)
        {
            if (ignoreChanged)
                return;

            ignoreChanged = true;
            if (BPMBox.Text == "")
            {
                beatsText = "";
                bpmText = "";
                BeatsBox.Text = "";
                ignoreChanged = false;
                return;
            }

            float bpm;
            if (!float.TryParse(BPMBox.Text, out bpm))
            {
                BPMBox.Text = bpmText;
                BPMBox.Select(BPMBox.Text.Length, 0);
            }
            else
            {
                float beats = bpm * (seconds / 60.0f);
                bpmText = BPMBox.Text;
                beatsText = beats.ToString();
                BeatsBox.Text = beatsText;
            }

            ignoreChanged = false;
        }
    }
}
