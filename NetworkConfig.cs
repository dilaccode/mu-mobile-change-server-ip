using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

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
        public void SetIP(string ipAddress, string subnetMask, string gateway)
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(managementObject => (bool)managementObject["IPEnabled"]))
                    {
                        using (var newIP = managementObject.GetMethodParameters("EnableStatic"))
                        {
                            // Set new IP address and subnet if needed
                            if ((!String.IsNullOrEmpty(ipAddress)) || (!String.IsNullOrEmpty(subnetMask)))
                            {
                                if (!String.IsNullOrEmpty(ipAddress))
                                {
                                    newIP["IPAddress"] = new[] { ipAddress };
                                }

                                if (!String.IsNullOrEmpty(subnetMask))
                                {
                                    newIP["SubnetMask"] = new[] { subnetMask };
                                }

                                managementObject.InvokeMethod("EnableStatic", newIP, null);
                            }

                            // Set mew gateway if needed
                            if (!String.IsNullOrEmpty(gateway))
                            {
                                using (var newGateway = managementObject.GetMethodParameters("SetGateways"))
                                {
                                    newGateway["DefaultIPGateway"] = new[] { gateway };
                                    newGateway["GatewayCostMetric"] = new[] { 1 };
                                    managementObject.InvokeMethod("SetGateways", newGateway, null);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Set's the DNS Server of the local machine
        /// </summary>
        /// <param name="nic">NIC address, NetworkInterface.GetAllNetworkInterfaces()</param>
        /// <param name="dnsServers">Comma seperated list of DNS server addresses</param>
        /// <remarks>Requires a reference to the System.Management namespace</remarks>
        public void SetNameservers(string nic, string dnsServers)
        {
            using (var networkConfigMng = new ManagementClass("Win32_NetworkAdapterConfiguration"))
            {
                using (var networkConfigs = networkConfigMng.GetInstances())
                {
                    foreach (var managementObject in networkConfigs.Cast<ManagementObject>().Where(objMO => (bool)objMO["IPEnabled"] && objMO["Caption"].Equals(nic)))
                    {
                        using (var newDNS = managementObject.GetMethodParameters("SetDNSServerSearchOrder"))
                        {
                            newDNS["DNSServerSearchOrder"] = dnsServers.Split(',');
                            managementObject.InvokeMethod("SetDNSServerSearchOrder", newDNS, null);
                        }
                    }
                }
            }
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
    public class NetworkItem {
        public string Name;
        /// <summary>
        /// it mean IP v4 OK!
        /// </summary>
        public string IP;
    }
}
