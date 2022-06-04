using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace RemoSharpGUI
{
    public class NetworkConfig
    {
        public int index;
        public string name;
        public string IP;

        public NetworkConfig(int index,string name, string IP)
        {
            this.index = index;
            this.name = name;
            this.IP = IP;

        }

        public override string ToString()
        {
            return $"{this.index}  {this.name}  {this.IP}";
        }

        public static string GetNetworkListNames(List<NetworkConfig> networkConfigs, out string[] networkNames)
        {
            string names = "";
            string[] netNames = new string[networkConfigs.Count]; 
            for (int i = 0; i < networkConfigs.Count; i++)
            {
                NetworkConfig networkConfig = networkConfigs[i];
                names += networkConfig.ToString() + Environment.NewLine;
                netNames[i] = networkConfig.ToString();
            }
            networkNames = netNames;
            return names;
        }

        public static List<NetworkConfig> GetNetworkListDataFromPC()
        {
            List<NetworkConfig> networkConfigs = new List<NetworkConfig>();
            networkConfigs.Add(new NetworkConfig(0, "Local Server", "127.0.0.1"));
            int index = 1;
            foreach (NetworkInterface ni in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211 || ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                {
                    
                    string wifi_name = ni.Name;
                    string ipAddress = "";
                    foreach (UnicastIPAddressInformation ip in ni.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                                ipAddress = ip.Address.ToString();
                            
                        }
                    }
                    NetworkConfig networkConfig = new NetworkConfig(index, wifi_name, ipAddress);
                    networkConfigs.Add(networkConfig);
                    index++;
                }
            }

            return networkConfigs;
        }
    }
}
