﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RemoSharpBroadcasterGUI
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

        public static void ProcessGH_DocumentAndSendGenerateData(string openPath, string finalPath, string RemoSharpLibraryGUID, string RemoSharpNickName,
                                                                    double minX, double maxX, double minY, double maxY)
        {
            CheckForDirectoryAndFileExistance(openPath);
            CheckForDirectoryAndFileExistance(finalPath);


            string fileContent = "";

            while (true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openPath))
                    {
                        fileContent += sr.ReadToEnd();
                    }
                    break;
                }
                catch { }
            }

            XmlDocument document = new XmlDocument();
            document.LoadXml(fileContent);
            string docObjectPaths = "";
            string objectCountPath = FindObjectCountXMLPath(document, out docObjectPaths);

            var objectCount = Convert.ToInt32(document.SelectSingleNode(objectCountPath).InnerText);

            XmlNode objNodes = document.SelectSingleNode(docObjectPaths);


            for (int i = objectCount - 1; i > -1; i--)
            {

                //Console.WriteLine(i);
                XmlNode node = objNodes.ChildNodes[i];
                var attributeNode = node.SelectSingleNode("chunks/chunk/chunks");
                var nameNode = node.SelectSingleNode("chunks/chunk/items");
                var libNode = node.SelectSingleNode("items");

                bool outOfBounds = CleanOutOfBoundComponents(attributeNode, minX, maxX, minY, maxY);
                bool isNickNameRemoSharp = IsComponentNickNameRemoSharp(nameNode, RemoSharpNickName);
                bool isOfRemoSharp = IsComponentOfRemoSharp(libNode, RemoSharpLibraryGUID);

                if (outOfBounds || isNickNameRemoSharp || isOfRemoSharp) objNodes.RemoveChild(node);

            }

            int newObjectCount = document.SelectSingleNode(docObjectPaths).ChildNodes.Count;

            for (int i = newObjectCount - 1; i > -1; i--)
            {
                XmlNode node = objNodes.ChildNodes[i];
                node.Attributes["index"].InnerText = i.ToString();
            }

            int newCount = newObjectCount;

            string newCountStr = newCount.ToString();
            document.SelectSingleNode(objectCountPath).InnerText = newCountStr;
            objNodes.Attributes["count"].InnerText = newCountStr;

            while (true)
            {
                try
                {
                    document.Save(finalPath);
                    //Thread.Sleep(100);
                    break;
                }
                catch { }
            }
            //return document.InnerXml;
        }
        private static string FindObjectCountXMLPath(XmlDocument document, out string docObjectsPath)
        {
            int index = 1;
            string path = "";
            for (int i = 0; i < 10; i++)
            {
                path = "Archive/chunks[1]/chunk[1]/chunks[1]/chunk[" + index + "]/items/item/@name";
                if (document.SelectSingleNode(path).InnerText.Equals("ObjectCount")) break;
                index++;
            }
            docObjectsPath = "Archive/chunks[1]/chunk[1]/chunks[1]/chunk[" + index + "]/chunks";
            return "Archive/chunks[1]/chunk[1]/chunks[1]/chunk[" + index + "]/items/item";
        }

        private static bool CleanOutOfBoundComponents(XmlNode attributeNode, double minX, double maxX, double minY, double maxY)
        {
            bool deleteThisNode = false;

            foreach (XmlNode subNode in attributeNode.ChildNodes)
            {
                string subNodeXml = subNode.InnerXml;
                string subNodeName = subNode.Attributes["name"].InnerText;

                if (subNodeName == "Attributes")
                {
                    if (string.IsNullOrEmpty(subNodeXml)) continue;
                    var subNodeXML = subNode.SelectSingleNode("items").ChildNodes;
                    foreach (XmlNode subSubNode in subNodeXML)
                    {
                        if (subSubNode.Attributes["name"].InnerText == "Pivot")
                        {
                            double pivotX = Convert.ToDouble(subSubNode.SelectSingleNode("X").InnerText);
                            double pivotY = Convert.ToDouble(subSubNode.SelectSingleNode("Y").InnerText);
                            bool isInside = pivotX > minX &&
                                            pivotX < maxX &&
                                            pivotY > minY &&
                                            pivotY < maxY;

                            if (!isInside) deleteThisNode = true;

                            //Console.ReadKey();
                        }

                    }
                    break;
                }

            }

            return deleteThisNode;
        }

        private static void CheckForDirectoryAndFileExistance(string path)
        {
            bool directoryExists = Directory.Exists(Path.GetDirectoryName(path));
            bool fileExists = File.Exists(path);

            if (!directoryExists) Directory.CreateDirectory(Path.GetDirectoryName(path));
            if (!fileExists) File.Create(path);
        }

        private static bool IsComponentNickNameRemoSharp(XmlNode attributeNode, string nickName)
        {
            //string subNodeXmlText = attributeNode.InnerText;
            bool deleteThisComponent = false;

            foreach (XmlNode node in attributeNode.ChildNodes)
            {
                if (string.IsNullOrEmpty(node.InnerXml) || string.IsNullOrEmpty(node.InnerText)) continue;
                string objectNickNameAttribute = node.Attributes["name"].InnerText;
                string objectNickName = node.InnerXml;
                if (objectNickNameAttribute.Equals("NickName") &&
                   objectNickName.Contains(nickName))
                {
                    deleteThisComponent = true;
                    break;
                }
            }
            //if (subNodeXmlText.Contains(libraryGUID) || subNodeXmlText.Contains(nickName)) return true;
            return deleteThisComponent;
        }

        private static bool IsComponentOfRemoSharp(XmlNode libNode, string remoSharpLibraryGUID)
        {
            bool deleteThisComponent = false;

            foreach (XmlNode node in libNode.ChildNodes)
            {
                if (string.IsNullOrEmpty(node.InnerXml) || string.IsNullOrEmpty(node.InnerText)) continue;
                string objectNickNameAttribute = node.Attributes["name"].InnerText;
                string objectNickName = node.InnerXml;
                if (objectNickNameAttribute.Equals("Lib") &&
                   objectNickName.Contains(remoSharpLibraryGUID))
                {
                    deleteThisComponent = true;
                    break;
                }
            }

            return deleteThisComponent;
        }

        public class VisibleBounds
        {
            public int topLeftCornerX;
            public int topLeftCornerY;
            public int visibleAreaWidth;
            public int visibleAreaHeight;
            public string ID;

            public VisibleBounds(string coordinatesCSV)
            {
                string[] csv = coordinatesCSV.Split(',');
                double topLeftCornerX = Convert.ToDouble(csv[0]);
                double topLeftCornerY = Convert.ToDouble(csv[1]);
                double visibleAreaWidth = Convert.ToDouble(csv[2]);
                double visibleAreaHeight = Convert.ToDouble(csv[3]);
                string ID = csv[csv.Length - 1];

                this.topLeftCornerX = (int)topLeftCornerX;
                this.topLeftCornerY = (int)topLeftCornerY;
                this.visibleAreaWidth = (int)visibleAreaWidth;
                this.visibleAreaHeight = (int)visibleAreaHeight;
                this.ID = ID;
            }
        }
    }
}
