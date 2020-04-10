using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MusicBeePlugin
{
    public partial class ConfigPanel : UserControl
    {
        public ConfigPanel()
        {
            InitializeComponent();
        }

        public string KgMid { get => textBox1.Text; set => textBox1.Text = value; }

        private void ConfigPanel_Load(object sender, EventArgs e)
        {
            label2.Text = LocalizationManager.ConfigPanelKgMidDescription;
        }
    }
}
