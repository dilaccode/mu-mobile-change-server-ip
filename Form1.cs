using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using CongLibrary.CustomControls;
using CongLibrary.Helper;
using MySql.Data.MySqlClient;
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
        private PopupFeedback PopupFeedbackObj;
        private string NewIP = "";
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

            NetworkWork();

            ChangeIPServerWork(NewIP);

            ChangeIPServerMySql(NewIP);

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
                    LogError(string.Format("Item {0} = {1} is not IPv4 format. Check /Data/Network.json .", Item.Key, Item.Value));
                    return; // END work here
                }
                Log(string.Format("Check {0} = {1} is OK.", Item.Key, Item.Value));
            }
            // check right IPv4 vs Gateway
            if (!InternetHelper.IsRightIPv4AndGateway(NetworkDictionary["IP"], NetworkDictionary["Gateway"]))
            {
                LogError("IPv4 not same Gateway, ex SAME.SAME.SAME.DIFFER .");
                return; // END work here
            }

            //
            NewIP = NetworkDictionary["IP"];
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
            //Thread.Sleep(1000);

            Log("Done. New IP For Verify: " + NetworkConfig.GetNetworkItem().IP);
            Log("My production performance/effeitivelyale this Yearly >>>");
            //
            Log(WinHelper.cmd("ipconfig /all"));
        }
        #endregion >>> Network <<<

        #region >>> Change IP Server <<<
        private void ChangeIPServerWork(string NewIP)
        {
            // read Network config
            var NetworkJsonText = File.ReadAllText("Data\\ServerPathFilesChangeIP.json");
            List<string> ListPathFileConfig = JsonConvert.DeserializeObject<List<string>>(NetworkJsonText);
            //
            if (ListPathFileConfig.Count == 0)
            {
                LogError("list file path empty.");
                return; // END work here
            }
            // get old server ip
            var PathFileVersion = ListPathFileConfig[0];
            var FileVersionText = File.ReadAllText(PathFileVersion);
            Log(FileVersionText);
            var match = Regex.Match(FileVersionText, @"\b(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3})\b");
            string OldIP = "";
            if (match.Success)
            {
                OldIP = match.Captures[0].ToString();
                Log("Old IP on files: "+ OldIP);
            }
            else
            {
                LogError("Find Old IP failed.");
                return; // END work here
            }
            //
            foreach (var PathFileConfig in ListPathFileConfig)
            {
                var FileText = File.ReadAllText(PathFileConfig);
                //
                Log(" - " + PathFileConfig);
               int Count = Regex.Matches(FileText, OldIP).Count;
                Log(" --> Replace " + Count + " positions");
                //
                var FileTextNew = FileText.Replace(OldIP, NewIP);
                File.WriteAllText(PathFileConfig, FileTextNew);
            }
        }
        private void ChangeIPServerMySql(string NewIP)
        {
            var Query = string.Format("update {0} set ip='{1}'",
                "t_server_info", NewIP);
            ConnectDatabaseAndReplace("mu_kuafu", Query);
            Query = string.Format("update {0} set ServerURL='{1}'",
                "t_serverdata", NewIP);
            ConnectDatabaseAndReplace("mu_serverinfo", Query);
        }

        private void ConnectDatabaseAndReplace(string Database, string Query)
        {
            MySqlConnection connection;
            string server = "localhost";
            string database = Database;
            string uid = "root";
            string password = "123456";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            try
            {
                using (var con = new MySqlConnection { ConnectionString = connectionString })
                {
                    using (var command = new MySqlCommand { Connection = con })
                    {
                        Log("Connect MySQL success. Databse = " + database);
                        con.Open();
                        command.CommandText = Query;
                        var CountResult = command.ExecuteNonQuery();
                        if (CountResult > 0)
                        {
                            Log(string.Format("Replace success {0} IP in {1}.", CountResult, database));
                        }
                        else
                        {
                            LogError("No result update database: " + CountResult + " row");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                LogError("Can not connect MySql");
                LogError(e.Message);
            }
        }
        #endregion >>> Change IP Server <<<
    }
}
