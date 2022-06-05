using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WebSocketSharp;
using WebSocketSharp.Server;
using System.Threading;


namespace RemoSharpBroadcasterGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static readonly string RemoSharpLibraryGUID = "a1d80423-e6e0-49f5-8514-de158ae1193a";
        public static readonly string RemoSharpNickName = "RemoSharp";

        public static bool server00_Stay_Alive = true;
        public static bool server01_Stay_Alive = true;
        public static bool server02_Stay_Alive = true;
        public static bool server03_Stay_Alive = true;
        public static bool server04_Stay_Alive = true;
        public static bool server05_Stay_Alive = true;
        public static bool server06_Stay_Alive = true;
        public static bool server07_Stay_Alive = true;
        public static bool server08_Stay_Alive = true;
        public static bool server09_Stay_Alive = true;
        public List<NetworkConfig> availableNetworks;
        public int alive_server_count = 0;
        public MainWindow()
        {
            this.alive_server_count = 0;
            InitializeComponent();
            var networkList = NetworkConfig.GetNetworkListDataFromPC();
            this.availableNetworks = networkList;
            string[] networkListNamesForDropDown;
            string networkListNames = NetworkConfig.GetNetworkListNames(networkList, out networkListNamesForDropDown);
            this.NetList.Content = networkListNames;
            this.NetSel.ItemsSource = networkListNamesForDropDown;

            int addressIndex = 0;
            for (int i = 0; i < networkList.Count; i++)
            {
                if (networkList[i].name.ToUpper().Contains("WIFI") || networkList[i].name.ToUpper().Contains("WI-FI"))
                {
                    this.NetSel.Text = networkList[i].ToString();
                    addressIndex = i;
                }
            }
            string[] ipNumbers = networkList[addressIndex].IP.Split('.');
            this.IP_0_00.Text = ipNumbers[0]; 
            this.IP_0_01.Text = ipNumbers[0]; 
            this.IP_0_02.Text = ipNumbers[0]; 
            this.IP_0_03.Text = ipNumbers[0]; 
            this.IP_0_04.Text = ipNumbers[0]; 


            this.IP_1_00.Text = ipNumbers[1];
            this.IP_1_01.Text = ipNumbers[1];
            this.IP_1_02.Text = ipNumbers[1];
            this.IP_1_03.Text = ipNumbers[1];
            this.IP_1_04.Text = ipNumbers[1];


            this.IP_2_00.Text = ipNumbers[2];
            this.IP_2_01.Text = ipNumbers[2];
            this.IP_2_02.Text = ipNumbers[2];
            this.IP_2_03.Text = ipNumbers[2];
            this.IP_2_04.Text = ipNumbers[2];


            this.PC_ID.Text = ipNumbers[3];
            this.PC_ADDRESS.Text = ipNumbers[2];
            //this.Address00.Text = "ws://" + this.IP_0_00.Text + "." + this.IP_1_00.Text + "." + this.IP_2_00.Text + "." + this.IP_3_00.Text + ":" + this.Port_00.Text  + "/RemoSharp";
            //this.Address01.Text = "ws://" + this.IP_0_01.Text + "." + this.IP_1_01.Text + "." + this.IP_2_01.Text + "." + this.IP_3_01.Text + ":" + this.Port_01.Text  + "/RemoSharp";
            //this.Address02.Text = "ws://" + this.IP_0_02.Text + "." + this.IP_1_02.Text + "." + this.IP_2_02.Text + "." + this.IP_3_02.Text + ":" + this.Port_02.Text  + "/RemoSharp";
            //this.Address03.Text = "ws://" + this.IP_0_03.Text + "." + this.IP_1_03.Text + "." + this.IP_2_03.Text + "." + this.IP_3_03.Text + ":" + this.Port_03.Text  + "/RemoSharp";
            //this.Address04.Text = "ws://" + this.IP_0_04.Text + "." + this.IP_1_04.Text + "." + this.IP_2_04.Text + "." + this.IP_3_04.Text + ":" + this.Port_04.Text  + "/RemoSharp";
            //this.Address05.Text = "ws://" + this.IP_0_05.Text + "." + this.IP_1_05.Text + "." + this.IP_2_05.Text + "." + this.IP_3_05.Text + ":" + this.Port_05.Text  + "/RemoSharp";
            //this.Address06.Text = "ws://" + this.IP_0_06.Text + "." + this.IP_1_06.Text + "." + this.IP_2_06.Text + "." + this.IP_3_06.Text + ":" + this.Port_06.Text  + "/RemoSharp";
            //this.Address07.Text = "ws://" + this.IP_0_07.Text + "." + this.IP_1_07.Text + "." + this.IP_2_07.Text + "." + this.IP_3_07.Text + ":" + this.Port_07.Text  + "/RemoSharp";
            //this.Address08.Text = "ws://" + this.IP_0_08.Text + "." + this.IP_1_08.Text + "." + this.IP_2_08.Text + "." + this.IP_3_08.Text + ":" + this.Port_08.Text  + "/RemoSharp";
            //this.Address09.Text = "ws://" + this.IP_0_09.Text + "." + this.IP_1_09.Text + "." + this.IP_2_09.Text + "." + this.IP_3_09.Text + ":" + this.Port_09.Text  + "/RemoSharp";
            
        }

        public class RemoSharp : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Sessions.Broadcast(e.Data);
            }
        }

        public class RemoSharpCanvasBounds : WebSocketBehavior
        {
            protected override void OnMessage(MessageEventArgs e)
            {
                Sessions.Broadcast(e.Data);
            }
        }

        // how to pass arguments into threaded methods
        //https://stackoverflow.com/questions/1195896/threadstart-with-parameters
        
            

        

        private void NetSel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = NetSel.SelectedIndex;
            string[] IP = this.availableNetworks[index].IP.Split('.');
            string IP_0 = IP[0];
            string IP_1 = IP[1];
            string IP_2 = IP[2];
            string IP_3 = IP[3];

            this.IP_0_00.Text = IP_0;
            this.IP_0_01.Text = IP_0;
            this.IP_0_02.Text = IP_0;
            this.IP_0_03.Text = IP_0;
            this.IP_0_04.Text = IP_0;

            this.IP_1_00.Text = IP_1;
            this.IP_1_01.Text = IP_1;
            this.IP_1_02.Text = IP_1;
            this.IP_1_03.Text = IP_1;
            this.IP_1_04.Text = IP_1;

            this.IP_2_00.Text = IP_2;
            this.IP_2_01.Text = IP_2;
            this.IP_2_02.Text = IP_2;
            this.IP_2_03.Text = IP_2;
            this.IP_2_04.Text = IP_2;

            this.PC_ID.Text = IP_3;
            this.PC_ADDRESS.Text = IP_2;

        }
        private void TurnOffServers(object sender, System.ComponentModel.CancelEventArgs e)
        {
            server00_Stay_Alive = false;
            server01_Stay_Alive = false;
            server02_Stay_Alive = false;
            server03_Stay_Alive = false;
            server04_Stay_Alive = false;
            Thread.Sleep(200);
    }

        private void ConnectButton00_Click(object s, RoutedEventArgs ea)
        {

            string address = "";
            if (this.radioButton_0_1.IsChecked.Value)
            {
                address = this.Full_Address_0.Text;
            }
            else
            {
                if (string.IsNullOrEmpty(this.IP_3_00.Text)) 
                {
                    System.Windows.Forms.MessageBox.Show("Please Specify a correct IP address.");
                    return;
                }
                else
                {
                    address = "ws://" + this.IP_0_00.Text + "."
                                      + this.IP_1_00.Text + "."
                                      + this.IP_2_00.Text + "."
                                      + this.IP_3_00.Text + ":"
                                      + this.Port_00.Text + "/RemoSharp";
                }
            }

            if (this.ConnectButton00.Content.Equals("Connect"))
            {
                server00_Stay_Alive = true;
                this.ConnectButton00.Content = "Disconnect";
                Thread thread = new Thread(() => ConnectToServer0(address));
                thread.Start();
            }
            else
            {
                server00_Stay_Alive = false;
                this.ConnectButton00.Content = "Connect";
            }

            
        }

        private void ConnectButton01_Click(object s, RoutedEventArgs ea)
        {

            string address = "";
            if (this.radioButton_1_1.IsChecked.Value)
            {
                address = this.Full_Address_1.Text;
            }
            else
            {
                if (string.IsNullOrEmpty(this.IP_3_01.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Please Specify a correct IP address.");
                    return;
                }
                else
                {
                    address = "ws://" + this.IP_0_01.Text + "."
                                      + this.IP_1_01.Text + "."
                                      + this.IP_2_01.Text + "."
                                      + this.IP_3_01.Text + ":"
                                      + this.Port_01.Text + "/RemoSharp";
                }
            }

            if (this.ConnectButton01.Content.Equals("Connect"))
            {
                server01_Stay_Alive = true;
                this.ConnectButton01.Content = "Disconnect";
                Thread thread = new Thread(() => ConnectToServer1(address));
                thread.Start();
            }
            else
            {
                server01_Stay_Alive = false;
                this.ConnectButton01.Content = "Connect";
            }


        }

        private void ConnectButton02_Click(object s, RoutedEventArgs ea)
        {

            string address = "";
            if (this.radioButton_2_1.IsChecked.Value)
            {
                address = this.Full_Address_2.Text;
            }
            else
            {
                if (string.IsNullOrEmpty(this.IP_3_02.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Please Specify a correct IP address.");
                    return;
                }
                else
                {
                    address = "ws://" + this.IP_0_02.Text + "."
                                      + this.IP_1_02.Text + "."
                                      + this.IP_2_02.Text + "."
                                      + this.IP_3_02.Text + ":"
                                      + this.Port_02.Text + "/RemoSharp";
                }
            }

            if (this.ConnectButton02.Content.Equals("Connect"))
            {
                server02_Stay_Alive = true;
                this.ConnectButton02.Content = "Disconnect";
                Thread thread = new Thread(() => ConnectToServer2(address));
                thread.Start();
            }
            else
            {
                server02_Stay_Alive = false;
                this.ConnectButton02.Content = "Connect";
            }


        }

        private void ConnectButton03_Click(object s, RoutedEventArgs ea)
        {

            string address = "";
            if (this.radioButton_3_1.IsChecked.Value)
            {
                address = this.Full_Address_3.Text;
            }
            else
            {
                if (string.IsNullOrEmpty(this.IP_3_03.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Please Specify a correct IP address.");
                    return;
                }
                else
                {
                    address = "ws://" + this.IP_0_03.Text + "."
                                      + this.IP_1_03.Text + "."
                                      + this.IP_2_03.Text + "."
                                      + this.IP_3_03.Text + ":"
                                      + this.Port_03.Text + "/RemoSharp";
                }
            }

            if (this.ConnectButton03.Content.Equals("Connect"))
            {
                server03_Stay_Alive = true;
                this.ConnectButton03.Content = "Disconnect";
                Thread thread = new Thread(() => ConnectToServer3(address));
                thread.Start();
            }
            else
            {
                server03_Stay_Alive = false;
                this.ConnectButton03.Content = "Connect";
            }


        }

        private void ConnectButton04_Click(object s, RoutedEventArgs ea)
        {

            string address = "";
            if (this.radioButton_4_1.IsChecked.Value)
            {
                address = this.Full_Address_4.Text;
            }
            else
            {
                if (string.IsNullOrEmpty(this.IP_3_04.Text))
                {
                    System.Windows.Forms.MessageBox.Show("Please Specify a correct IP address.");
                    return;
                }
                else
                {
                    address = "ws://" + this.IP_0_04.Text + "."
                                      + this.IP_1_04.Text + "."
                                      + this.IP_2_04.Text + "."
                                      + this.IP_3_04.Text + ":"
                                      + this.Port_04.Text + "/RemoSharp";
                }
            }

            if (this.ConnectButton04.Content.Equals("Connect"))
            {
                server04_Stay_Alive = true;
                this.ConnectButton04.Content = "Disconnect";
                Thread thread = new Thread(() => ConnectToServer4(address));
                thread.Start();
            }
            else
            {
                server04_Stay_Alive = false;
                this.ConnectButton04.Content = "Connect";
            }


        }

        private void SendTester(WebSocket client, bool keepSending) 
        {
            
            
        }

        private void ConnectToServer0(string address)
        {
            try
            {
                using (WebSocket boundsClient = new WebSocket(address))
                {
                    boundsClient.OnMessage += (object sender, MessageEventArgs e) =>
                    {
                        try
                        {
                            var bounds = new NetworkConfig.VisibleBounds(e.Data);
                            string finalPath = @"C:\temp\RemoSharp\finalTempFile" + bounds.ID + ".ghx";
                            string openPath = @"C:\temp\RemoSharp\openTempFile" + bounds.ID + ".ghx";

                            NetworkConfig.ProcessGH_DocumentAndSendGenerateData(openPath, finalPath, RemoSharpLibraryGUID, RemoSharpNickName,
                            bounds.topLeftCornerX,
                            bounds.topLeftCornerX + bounds.visibleAreaWidth,
                            bounds.topLeftCornerY,
                            bounds.topLeftCornerY + bounds.visibleAreaHeight);

                        }
                        catch
                        { }
                    };

                    boundsClient.Connect();
                    boundsClient.Send("Hello World");
                    // int i = 0;
                    while (boundsClient.IsAlive && server00_Stay_Alive) 
                    {
                        // boundsClient.Send("hi " + i);
                        // Thread.Sleep(100);
                        // i++;
                    }

                    try
                    {
                        boundsClient.Close();
                        System.Windows.Forms.MessageBox.Show("(_ 1 _) Collaborator Connection Closed!");
                    }
                    catch { }

                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot Connect To Server with the Current Address");
            }
        }

        private void ConnectToServer1(string address)
        {
            try
            {
                using (WebSocket boundsClient = new WebSocket(address))
                {
                    boundsClient.OnMessage += (object sender, MessageEventArgs e) =>
                    {
                        try
                        {
                            var bounds = new NetworkConfig.VisibleBounds(e.Data);
                            string finalPath = @"C:\temp\RemoSharp\finalTempFile" + bounds.ID + ".ghx";
                            string openPath = @"C:\temp\RemoSharp\openTempFile" + bounds.ID + ".ghx";

                            NetworkConfig.ProcessGH_DocumentAndSendGenerateData(openPath, finalPath, RemoSharpLibraryGUID, RemoSharpNickName,
                            bounds.topLeftCornerX,
                            bounds.topLeftCornerX + bounds.visibleAreaWidth,
                            bounds.topLeftCornerY,
                            bounds.topLeftCornerY + bounds.visibleAreaHeight);

                        }
                        catch
                        { }
                    };

                    boundsClient.Connect();
                    boundsClient.Send("Hello World");
                    // int i = 0;

                    while (boundsClient.IsAlive && server01_Stay_Alive)
                    {

                        // boundsClient.Send("hi " + i);
                        // Thread.Sleep(100);
                        // i++;
                    }

                    try
                    {
                        boundsClient.Close();
                        System.Windows.Forms.MessageBox.Show("(_ 2 _) Collaborator Connection Closed!");
                    }
                    catch { }

                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot Connect To Server with the Current Address");
            }
        }

        private void ConnectToServer2(string address)
        {
            try
            {
                using (WebSocket boundsClient = new WebSocket(address))
                {
                    boundsClient.OnMessage += (object sender, MessageEventArgs e) =>
                    {
                        try
                        {
                            var bounds = new NetworkConfig.VisibleBounds(e.Data);
                            string finalPath = @"C:\temp\RemoSharp\finalTempFile" + bounds.ID + ".ghx";
                            string openPath = @"C:\temp\RemoSharp\openTempFile" + bounds.ID + ".ghx";

                            NetworkConfig.ProcessGH_DocumentAndSendGenerateData(openPath, finalPath, RemoSharpLibraryGUID, RemoSharpNickName,
                            bounds.topLeftCornerX,
                            bounds.topLeftCornerX + bounds.visibleAreaWidth,
                            bounds.topLeftCornerY,
                            bounds.topLeftCornerY + bounds.visibleAreaHeight);

                        }
                        catch
                        { }
                    };

                    boundsClient.Connect();
                    boundsClient.Send("Hello World");

                    // int i = 0;
                    while (boundsClient.IsAlive && server02_Stay_Alive)
                    {
                        // boundsClient.Send("hi " + i);
                        // Thread.Sleep(100);
                        // i++;
                    }

                    try
                    {
                        boundsClient.Close();
                        System.Windows.Forms.MessageBox.Show("(_ 3 _) Collaborator Connection Closed!");

                    }
                    catch { }

                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot Connect To Server with the Current Address");
            }
        }

        private void ConnectToServer3(string address)
        {
            try
            {
                using (WebSocket boundsClient = new WebSocket(address))
                {
                    boundsClient.OnMessage += (object sender, MessageEventArgs e) =>
                    {
                        try
                        {
                            var bounds = new NetworkConfig.VisibleBounds(e.Data);
                            string finalPath = @"C:\temp\RemoSharp\finalTempFile" + bounds.ID + ".ghx";
                            string openPath = @"C:\temp\RemoSharp\openTempFile" + bounds.ID + ".ghx";

                            NetworkConfig.ProcessGH_DocumentAndSendGenerateData(openPath, finalPath, RemoSharpLibraryGUID, RemoSharpNickName,
                            bounds.topLeftCornerX,
                            bounds.topLeftCornerX + bounds.visibleAreaWidth,
                            bounds.topLeftCornerY,
                            bounds.topLeftCornerY + bounds.visibleAreaHeight);

                        }
                        catch
                        { }
                    };

                    boundsClient.Connect();
                    boundsClient.Send("Hello World");

                    // int i = 0;
                    while (boundsClient.IsAlive && server03_Stay_Alive)
                    {
                        // boundsClient.Send("hi " + i);
                        // Thread.Sleep(100);
                        // i++;
                    }

                    try
                    {
                        boundsClient.Close();
                        System.Windows.Forms.MessageBox.Show("(_ 4 _) Collaborator Connection Closed!");
                    }
                    catch { }

                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot Connect To Server with the Current Address");
            }
        }

        private void ConnectToServer4(string address)
        {
            try
            {
                using (WebSocket boundsClient = new WebSocket(address))
                {
                    boundsClient.OnMessage += (object sender, MessageEventArgs e) =>
                    {
                        try
                        {
                            var bounds = new NetworkConfig.VisibleBounds(e.Data);
                            string finalPath = @"C:\temp\RemoSharp\finalTempFile" + bounds.ID + ".ghx";
                            string openPath = @"C:\temp\RemoSharp\openTempFile" + bounds.ID + ".ghx";

                            NetworkConfig.ProcessGH_DocumentAndSendGenerateData(openPath, finalPath, RemoSharpLibraryGUID, RemoSharpNickName,
                            bounds.topLeftCornerX,
                            bounds.topLeftCornerX + bounds.visibleAreaWidth,
                            bounds.topLeftCornerY,
                            bounds.topLeftCornerY + bounds.visibleAreaHeight);

                        }
                        catch
                        { }
                    };

                    boundsClient.Connect();
                    boundsClient.Send("Hello World");

                    //int i = 0;
                    while (boundsClient.IsAlive && server04_Stay_Alive)
                    {
                        // boundsClient.Send("hi " + i);
                        // Thread.Sleep(100);
                        // i++;
                    }

                    try
                    {
                        boundsClient.Close();
                        System.Windows.Forms.MessageBox.Show("(_ 5 _) Collaborator Connection Closed!");
                    }
                    catch { }

                }
            }
            catch
            {
                System.Windows.Forms.MessageBox.Show("Cannot Connect To Server with the Current Address");
            }
        }
    }
}
