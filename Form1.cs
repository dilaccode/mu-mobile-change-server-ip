using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using CongLibrary.CustomControls;
using CongLibrary.Helper;
using Newtonsoft.Json;

namespace Mu_Change_Server_IP
{
    public partial class Form1 : Form
    {
        #region >>> Variable <<<
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
        private Point LastMousePoition = new Point(0, 0);
        private bool IsCopyInputText = false;
        private Dictionary<TASK, WorkItem> ListWork = new Dictionary<TASK, WorkItem>() {
            {TASK.InputIP,new WorkItem(false,"Input IP, ex 192.168.1.44","")},
            {TASK.InputNetmask, new WorkItem(false,"Input Netmask, ex 255.255.255.0","")},
            {TASK.InputGateway, new WorkItem(false,"Input Gateway ex 192.168.1.1","")},
            {TASK.InputDNS1, new WorkItem(false,"Input DNS1, ex 8.8.8.8","")},
            {TASK.InputDNS2, new WorkItem(false,"Input DNS2, ex 8.8.4.4","")},
            {TASK.SetIPv4, new WorkItem(false,"Set IP, DNS","")},
        };
        PopupFeedback PopupFeedbackObj;
        #endregion >>> Variable <<<

        #region >>> Work <<<
        private void WorkWithInput(TextBox InputTextSender)
        {
            string InputData = InputTextSender.Text;
            // set false if you set data for InputText
            bool IsClearInputText = true;
            
            // some command make
            if (InputData.ToLower().Trim() == "clear")
            {
                LogText.Clear();
            }
            else if (InputData.ToLower().Trim() == "restart")
            {
                Log("restart");
                Application.Restart();
            }
            else if (InputData.ToLower().Trim() == "exit")
            {
                Log("exit");
                Application.Exit();
            }
            else // normal cmd
            {
                Log(InputData);
                Log(WinHelper.cmd(InputData));
            }

            // Clear Input
            if (IsClearInputText)
            {
                InputTextSender.Clear();
            }
        }
        private void Log(string TextNew)
        {
            LogText.Text = string.Format("{0}\n[root ple]# {1}", LogText.Text, TextNew);

        }
        private void LogError(string TextNew)
        {
            LogText.Text = string.Format("{0}\n\n[root ple]# ERROR: {1}\n", LogText.Text, TextNew);

        }
        #endregion >>> Work <<<

        #region >>> Form <<<
        public Form1()
        {
            InitializeComponent();
            // fix bug mouse wheel on input
            this.InputText.MouseWheel += new MouseEventHandler(this.InputText_MouseWheel);

            // start text, command here
            LogText.Text = "[root ple]# hi... commands: clear, restart, exit\n";

            // Popup Feedback
            Size RealSoftSize = new Size(this.Size.Width,
                this.InputText.Top + this.InputText.Height);
            PopupFeedbackObj = new PopupFeedback(
                     PopupFeedbackPanel, PopupFeedbackText, RealSoftSize);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            NetworkItemObj = new NetworkItem();
            /// change IP
            NetworkItemObj = NetworkConfig.GetNetworkItem();
            Log(NetworkItemObj.Name + ", IP: " + NetworkItemObj.IP);
            //ProcessNextWork();

            NetworkWork();
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
        private void LogText_MouseDown(object sender, MouseEventArgs e)
        {
            LastMousePoition = new Point(e.X, e.Y);
            //
            IsCopyInputText = false;
        }

        private void InputText_MouseDown(object sender, MouseEventArgs e)
        {
            LastMousePoition = new Point(
                e.X + InputText.Left,
                e.Y + InputText.Top
                );
            IsCopyInputText = true;
        }
        private void InputText_TextChanged(object sender, EventArgs e)
        {

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
        private void InputText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // do some thing process input text

                /// work with input data
                WorkWithInput(InputText);

            }
        }

        private void CopyMenuItem_Click(object sender, EventArgs e)
        {
            if (IsCopyInputText)
            {
                InputText.Copy();
                IsCopyInputText = false;
            }
            else
                LogText.Copy();
            // UX feedback
            Point FeedbackShowPosition = new Point(
                LastMousePoition.X + CopyMenuItem.Width,
                LastMousePoition.Y
                );
            PopupFeedbackObj.Show("Copied", FeedbackShowPosition);
        }

        private void PasteMenuItem_Click(object sender, EventArgs e)
        {
            InputText.Text += Clipboard.GetText();
            InputText.MoveCursorToEnd();

            // UX feedback
            Point FeedbackShowPosition = new Point(
                LastMousePoition.X + PasteMenuItem.Width,
                LastMousePoition.Y + PasteMenuItem.Height
                );
            PopupFeedbackObj.Show("Pasted", FeedbackShowPosition);
        }

        #endregion >>> Form <<<

        #region >>> Network <<<
        private void NetworkWork()
        {
            // read Network config
            var NetworkJsonText = File.ReadAllText("Data\\Network.json");
            Dictionary<string, string> NetworkDictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(NetworkJsonText);
            // check right IPv4 format
            foreach (KeyValuePair<string, string> Item in NetworkDictionary)
            {
                bool IsOK = InternetHelper.IsIPv4(Item.Value.Trim());
                if (!IsOK)
                {
                    LogError(string.Format("Item {0} = {1} is not IPv4 format. Check /Data/Network.json .",Item.Key,Item.Value));
                    return; // END work here
                }
                Log(string.Format("Check {0} = {1} is OK.", Item.Key, Item.Value));
            }
            // check right IPv4 vs Gateway
            if(!InternetHelper.IsRightIPv4AndGateway(NetworkDictionary["IP"], NetworkDictionary["Gateway"]))
            {
                LogError("IPv4 not same Gateway, ex SAME.SAME.SAME.DIFFER .");
                return; // END work here
            }

            // set data
            NetworkConfig.SetIP(
                    NetworkItemObj.Name,
                    NetworkDictionary["IP"],
                    NetworkDictionary["Netmask"],
                    NetworkDictionary["Gateway"]
                    );
            NetworkConfig.SetDNS(
                NetworkItemObj.Name,
                NetworkDictionary["DNS1"],
                NetworkDictionary["DNS2"]
                );

            // wait some for win update
            Thread.Sleep(1000);

            Log("Done. New IP For Verify: " + NetworkConfig.GetNetworkItem().IP);
            Log("My production performance/effeitivelyale this Yearly >>>");
            //
            Log(WinHelper.cmd("ipconfig /all"));
        }
        #endregion >>> Network <<<
    }
}
