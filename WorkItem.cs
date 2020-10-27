using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Mu_Change_Server_IP
{
    public class WorkItem
    {
        public WorkItem(bool IsWork, string Text, string Data) {
            this.IsWork = IsWork;
            this.Text = Text;
            this.Data = Data;
        }
        public bool IsWork;
        public string Text;
        public string Data;
    }
}
