using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
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
            InputDNS1,
            InputDNS2,
            SetIPv4,
        }
        private Dictionary<TASK, WorkItem> ListWork = new Dictionary<TASK, WorkItem>() {
            {TASK.InputIP,new WorkItem(false,"Input IP, ex 192.168.1.44","")},
            {TASK.InputNetmask, new WorkItem(false,"Input Netmask, ex 255.255.255.0","")},
            {TASK.InputGateway, new WorkItem(false,"Input Gateway ex 192.168.1.1","")},
            {TASK.InputDNS1, new WorkItem(false,"Input DNS1, ex 8.8.8.8","")},
            {TASK.InputDNS2, new WorkItem(false,"Input DNS2, ex 8.8.4.4","")},
            {TASK.SetIPv4, new WorkItem(false,"Set IP, DNS","")},
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
            var IsDoneInputIP = ListWork[TASK.InputDNS2].IsWork;
            var IsNotSetIPv4 = !ListWork[TASK.SetIPv4].IsWork;
            if (IsDoneInputIP && IsNotSetIPv4)
            {
                NetworkConfig.SetIP(
                    NetworkItemObj.Name,
                    ListWork[TASK.InputIP].Data,
                    ListWork[TASK.InputNetmask].Data,
                    ListWork[TASK.InputGateway].Data
                    );
                NetworkConfig.SetDNS(
                    NetworkItemObj.Name,
                    ListWork[TASK.InputDNS1].Data,
                    ListWork[TASK.InputDNS2].Data
                    );
                WriteLog("Done. New IP For Verify: " + NetworkConfig.GetNetworkItem().IP);
                WriteLog("Bonus Yearly >>>");
                // wait some for win update
                Thread.Sleep(3000);
                //
                WriteLog(WinHelper.cmd("ipconfig"));
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
            else if (!ListWork[TASK.InputDNS1].IsWork)
            {
                var WorkItem = ListWork[TASK.InputDNS1];
                WorkItem.IsWork = true;
                WorkItem.Data = InputText.Text;
                WriteLog(WorkItem.Data);
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputDNS2].IsWork)
            {
                var WorkItem = ListWork[TASK.InputDNS2];
                WorkItem.IsWork = true;
                WorkItem.Data = InputText.Text;
                WriteLog(WorkItem.Data);
                ProcessNextWork();
            }
            // some command home make
            else if (InputText.Text.ToLower().Trim() == "clear") {
                LogText.Clear();
            }
            else // normal cmd
            {
                WriteLog(WinHelper.cmd(InputText.Text));
            }
        }


        public Form1()
        {
            InitializeComponent();
            // fix bug mouse wheel on input
            this.InputText.MouseWheel += new MouseEventHandler(this.InputText_MouseWheel);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkItemObj = new NetworkItem();
            /// change IP
            NetworkItemObj = NetworkConfig.GetNetworkItem();
            WriteLog(NetworkItemObj.Name+", IP: " + NetworkItemObj.IP);
            ProcessNextWork();
        }

        private void WriteLog(string TextNew)
        {
            LogText.Text = string.Format("{0}\n[root ple]# {1}", LogText.Text, TextNew);

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
        private void LogText_KeyDown(object sender, KeyEventArgs e)
        {
            InputText.Focus();
        }
        private void LogText_TextChanged(object sender, EventArgs e)
        {
            // auto scroll bottom
            LogText.SelectionStart = LogText.Text.Length;
            LogText.ScrollToCaret();
        }

        private void InputText_MouseWheel(object sender, EventArgs e)
        {
            // move mouse to Log Text
            this.Cursor = new Cursor(Cursor.Current.Handle);
            Cursor.Position = new Point(Cursor.Position.X - 0, Cursor.Position.Y - 100);
            Cursor.Clip = new Rectangle(this.Location, this.Size);
            //
            LogText.Focus();
        }
    }
}
