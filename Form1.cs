using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using CongLibrary.CustomControls;
using CongLibrary.Helper;

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

            // set input data , and make more correct with IP
            var IsDoneInputPreviousStep = ListWork[TASK.InputNetmask].IsWork;
            var IsNotInputGateWay = !ListWork[TASK.InputGateway].IsWork;
            if (IsDoneInputPreviousStep && IsNotInputGateWay)
            {
                var IPArray = ListWork[TASK.InputIP].Data.Trim().Split('.');
                InputText.Text = string.Format("{0}.{1}.{2}.",
                    IPArray[0], IPArray[1], IPArray[2]);
                InputText.MoveCursorToEnd();

                //InputText.Focus();
                //InputText.SelectionStart = InputText.Text.Length;
            }

            // set new Data
            IsDoneInputPreviousStep = ListWork[TASK.InputDNS2].IsWork;
            var IsNotSetIPv4 = !ListWork[TASK.SetIPv4].IsWork;
            if (IsDoneInputPreviousStep && IsNotSetIPv4)
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

                // wait some for win update
                Thread.Sleep(1000);

                WriteLog("Done. New IP For Verify: " + NetworkConfig.GetNetworkItem().IP);
                WriteLog("Bonus Yearly >>>");
                //
                WriteLog(WinHelper.cmd("ipconfig /all"));
            }
        }
        private void WorkWithInput(TextBox InputTextSender)
        {
            string InputData = InputTextSender.Text;
            // set false if you set data for InputText
            bool IsClearInputText = true;
            // new ip
            if (!ListWork[TASK.InputIP].IsWork)
            {
                // check correct data
                if (!InternetHelper.IsIPv4(InputData))
                {
                    WriteLogError(InputData + " is wrong format IPv4");
                }
                else // correct format IPv4
                {
                    var WorkItem = ListWork[TASK.InputIP];
                    WorkItem.IsWork = true;
                    WorkItem.Data = InputData;
                    WriteLog(WorkItem.Data);
                }
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputNetmask].IsWork)
            {
                // check correct data
                if (!InternetHelper.IsIPv4(InputData))
                {
                    WriteLogError(InputData + " is wrong format IPv4");
                }
                else // correct format IPv4
                {
                    var WorkItem = ListWork[TASK.InputNetmask];
                    WorkItem.IsWork = true;
                    WorkItem.Data = InputData;
                    WriteLog(WorkItem.Data);

                    // for add data next step
                    IsClearInputText = false;
                }
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputGateway].IsWork)
            {
                // check correct data
                if (!InternetHelper.IsIPv4(InputData))
                {
                    WriteLogError(InputData + " is wrong format IPv4");

                    // for add data next step
                    IsClearInputText = false;
                }
                else // correct format IPv4
                {
                    var WorkItem = ListWork[TASK.InputGateway];
                    WorkItem.IsWork = true;
                    WorkItem.Data = InputData;
                    WriteLog(WorkItem.Data);
                }
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputDNS1].IsWork)
            {
                // check correct data
                if (!InternetHelper.IsIPv4(InputData))
                {
                    WriteLogError(InputData + " is wrong format IPv4");
                }
                else // correct format IPv4
                {
                    var WorkItem = ListWork[TASK.InputDNS1];
                    WorkItem.IsWork = true;
                    WorkItem.Data = InputData;
                    WriteLog(WorkItem.Data);
                }
                ProcessNextWork();
            }
            else if (!ListWork[TASK.InputDNS2].IsWork)
            {
                // check correct data
                if (!InternetHelper.IsIPv4(InputData))
                {
                    WriteLogError(InputData + " is wrong format IPv4");
                }
                else // correct format IPv4
                {
                    var WorkItem = ListWork[TASK.InputDNS2];
                    WorkItem.IsWork = true;
                    WorkItem.Data = InputData;
                    WriteLog(WorkItem.Data);
                }
                ProcessNextWork();
            }
            // some command home make
            else if (InputData.ToLower().Trim() == "clear")
            {
                LogText.Clear();
            }
            else if (InputData.ToLower().Trim() == "restart")
            {
                WriteLog("restart");
                Application.Restart();
            }
            else if (InputData.ToLower().Trim() == "exit")
            {
                WriteLog("exit");
                Application.Exit();
            }
            else // normal cmd
            {
                WriteLog(InputData);
                WriteLog(WinHelper.cmd(InputData));
            }

            // Clear Input
            if (IsClearInputText)
            {
                InputTextSender.Clear();
            }
        }
        private void WriteLog(string TextNew)
        {
            LogText.Text = string.Format("{0}\n[root ple]# {1}", LogText.Text, TextNew);

        }
        private void WriteLogError(string TextNew)
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
            WriteLog(NetworkItemObj.Name + ", IP: " + NetworkItemObj.IP);
            ProcessNextWork();

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
    }
}
