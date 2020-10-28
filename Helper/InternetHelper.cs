using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CongLibrary.Helper
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
        public static bool IsRightIPv4AndGateway(string IPv4,string Gateway) {
            //
            var IPv4Array = IPv4.Trim().Split('.');
            var GatewayArray = Gateway.Trim().Split('.');
            return IPv4Array[0].Equals(GatewayArray[0])
                && IPv4Array[1].Equals(GatewayArray[1])
                && IPv4Array[2].Equals(GatewayArray[2]);
        }
    }
}
