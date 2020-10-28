using System.Net.NetworkInformation;
using CongLibrary.Helper;

namespace Mu_Change_Server_IP
{
    /// <summary>
    /// Helper class to set networking configuration like IP address, DNS servers, etc.
    /// </summary>
    public class NetworkConfig
    {
        /// <summary>
        /// Set's a new IP Address and it's Submask of the local machine
        /// </summary>
        /// <param name="ipAddress">The IP Address</param>
        /// <param name="subnetMask">The Submask IP Address</param>
        /// <param name="gateway">The gateway.</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public static void SetIP(string NetworkName, string IP, string Netmask, string Gateway)
        {
            var Command = string.Format("netsh interface ip set address \"{0}\" static {1} {2} {3} 1",
                NetworkName, IP, Netmask, Gateway);
            WinHelper.cmd(Command);

        }

       

        public static void SetDNS(string NetworkName, string DNS1,string DNS2)
        {
            var Command = string.Format("netsh interface ipv4 set dns name=\"{0}\" static {1}",
                NetworkName, DNS1);
            WinHelper.cmd(Command);
            var Command1 = string.Format("netsh interface ipv4 add dns \"{0}\" address={1} index=2",
               NetworkName, DNS2);
            WinHelper.cmd(Command1);
        }
        public static NetworkItem GetNetworkItem()
        {
            NetworkItem NetworkItemObj = new NetworkItem();
            bool IsGetOne = false;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    NetworkItemObj.Name = ni.Name;
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            NetworkItemObj.IP = ip.Address.ToString();
                            IsGetOne = true;
                        }
                        // break for get one
                        if (IsGetOne) break;
                    }
                }
                // break for get one
                if (IsGetOne) break;
            }
            return NetworkItemObj;
        }
    }
    public class NetworkItem
    {
        public string Name;
        /// <summary>
        /// it mean IP v4 OK!
        /// </summary>
        public string IP;
    }
}
