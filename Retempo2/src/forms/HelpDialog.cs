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
    public partial class HelpDialog : Form
    {
        public HelpDialog()
        {
            InitializeComponent();
            Version.ConvertControl(this);
        }

        private void HelpDialog_Load(object sender, EventArgs e)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, "https://raw.githubusercontent.com/Whirligig231/ReTempo2/refs/heads/master/README.md");
                HttpResponseMessage response = client.Send(request);
                StreamReader reader = new StreamReader(response.Content.ReadAsStream());
                string markdownText = reader.ReadToEnd();
                if (markdownText.StartsWith("404"))
                    throw new Exception();
                HelpWebBrowser.DocumentText = Markdig.Markdown.ToHtml(markdownText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Couldn't load the online README! Check your Internet connection");
            }
        }
    }
}
