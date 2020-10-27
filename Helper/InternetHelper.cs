using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Mu_Change_Server_IP.Helper
{
    class InternetHelper
    {
        public static bool IsIPv4(string IPv4) {
            if (String.IsNullOrWhiteSpace(IPv4))
            {
                return false;
            }

            string[] splitValues = IPv4.Split('.');
            if (splitValues.Length != 4)
            {
                return false;
            }

            byte tempForParsing;

            return splitValues.All(r => byte.TryParse(r, out tempForParsing));
        }
    }
}
