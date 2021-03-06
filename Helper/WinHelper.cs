﻿using System.Diagnostics;

namespace CongLibrary.Helper
{
    class WinHelper
    {
        public static string cmd(string Command) {
            Process myProcess = new Process();
            myProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            myProcess.StartInfo.CreateNoWindow = true;
            myProcess.StartInfo.UseShellExecute = false;
            myProcess.StartInfo.FileName = "cmd.exe";
            myProcess.StartInfo.Arguments = "/c " + Command;
            // for get output
            myProcess.StartInfo.RedirectStandardOutput = true;
            //myProcess.EnableRaisingEvents = true;
            //myProcess.Exited += new EventHandler(process_Exited);
            myProcess.Start();

            string output = myProcess.StandardOutput.ReadToEnd();
            myProcess.WaitForExit();
            return output;
        }
    }
}
