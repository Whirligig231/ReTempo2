using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Retempo2.src.forms
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            Version.ConvertControl(this);
            int oldLeft = Title.Left;
            int oldWidth = Title.Width;
            int oldCenter = oldLeft + oldWidth / 2;
            Version.ConvertControl(Title);
            int newWidth = Title.Width;
            int newLeft = oldCenter - newWidth / 2;
            Title.Left = newLeft;
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
