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
        private NetworkItem NetworkItemObj;
        private enum TASK
        {
            InputIP,
            InputNetmask,
            InputGateway,
            InputDNS,
            SetIPv4,
        }
        private Dictionary<TASK, WorkItem> ListWork = new Dictionary<TASK, WorkItem>() {
            {TASK.InputIP,new WorkItem(false,"Input IP, ex 192.168.1.44","")},
            {TASK.InputNetmask, new WorkItem(false,"Input Netmask, ex 255.255.255.0","")},
            {TASK.InputGateway, new WorkItem(false,"Input Gateway ex 192.168.1.1","")},
            {TASK.InputDNS, new WorkItem(false,"Input DNS, ex 8.8.8.8,8.8.4.4 Use comma ',' for 2 items","")},
            {TASK.SetIPv4, new WorkItem(false,"Set IPv4","")},
        };
        private void ProcessNextWork()
        {
            foreach (KeyValuePair<TASK, WorkItem> Work in ListWork)
            {
                var WorkItem = ListWork[Work.Key];
                var IsNotWork = !WorkItem.IsWork;
                if (IsNotWork)
                {
                    WriteLog(WorkItem.Text + " :");
                    break;
                }
            }

            // set new IP
            var IsDoneInputIP = ListWork[TASK.InputDNS].IsWork;
            var IsNotSetIPv4 = !ListWork[TASK.SetIPv4].IsWork;
            if (IsDoneInputIP && IsNotSetIPv4)
            {
                WriteLog(ListWork[TASK.InputDNS].Data);
                NetworkConfig.SetIP(
                    ListWork[TASK.InputIP].Data,
                    ListWork[TASK.InputNetmask].Data,
                    ListWork[TASK.InputGateway].Data
                    );
                NetworkConfig.SetDNS(
                    NetworkItemObj.Name,
                    ListWork[TASK.InputDNS].Data
                     );
                WriteLog("Done. New IP For Verify: " + NetworkConfig.GetNetworkItem().IP);
            }
        }
        private void WorkWithInput(string InputData)
        {
            // new ip
            if (!ListWork[TASK.InputIP].IsWork)
            {
                var WorkItem = ListWork[TASK.InputIP];
                WorkItem.IsWork = true;
                WorkItem.Data = InputText.Text;
                WriteLog(WorkItem.Data);
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputNetmask].IsWork)
            {
                var WorkItem = ListWork[TASK.InputNetmask];
                WorkItem.IsWork = true;
                WorkItem.Data = InputText.Text;
                WriteLog(WorkItem.Data);
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputGateway].IsWork)
            {
                var WorkItem = ListWork[TASK.InputGateway];
                WorkItem.IsWork = true;
                WorkItem.Data = InputText.Text;
                WriteLog(WorkItem.Data);
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputDNS].IsWork)
            {
                var WorkItem = ListWork[TASK.InputDNS];
                WorkItem.IsWork = true;
                WorkItem.Data = InputText.Text;
                WriteLog(WorkItem.Data);
                ProcessNextWork();
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkItemObj = new NetworkItem();
            /// change IP
            NetworkItemObj = NetworkConfig.GetNetworkItem();
            WriteLog("Old IP: " + NetworkItemObj.IP);
            ProcessNextWork();
        }

        private void WriteLog(string TextNew)
        {
            LogText.Text = string.Format("{0}\n[root ple]# {1}", LogText.Text, TextNew);

        }

        private void LogText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                InputText.Focus();
            }
        }
        private void InputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // do some thing process input text

                /// work with input data
                WorkWithInput(InputText.Text);


                // reset
                InputText.Clear();
                InputText.Focus();
            }
        }

        private void LogText_TextChanged(object sender, EventArgs e)
        {
            // auto scroll bottom
            LogText.SelectionStart = LogText.Text.Length;
            LogText.ScrollToCaret();
        }


    }
}
