using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mu_Change_Server_IP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            /// change IP
            WriteLog("Old IP: "+NetworkConfig.GetNetworkItem().IP);
            WriteLog("Input new IP: ");
            //NetworkConfig.SetIP();
        }

        private void WriteLog(string TextNew)
        {
            LogText.Text = string.Format("{0}\n[root ple]# {1}", LogText.Text,TextNew);

        }

        private void LogText_TextChanged(object sender, EventArgs e)
        {
            // auto scroll bottom
            LogText.SelectionStart = LogText.Text.Length;
            LogText.ScrollToCaret();
        }

        private void InputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // do some thing process input text
                WriteLog("You recentlly input: " + InputText.Text);
                InputText.Clear();
                InputText.Focus();
            }
        }
    }
}
