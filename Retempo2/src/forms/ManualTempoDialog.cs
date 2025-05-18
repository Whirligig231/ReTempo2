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
    }
}
