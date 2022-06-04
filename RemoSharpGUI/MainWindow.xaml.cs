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


namespace RemoSharpGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static bool canvasSrv_Stay_Alive = true;
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
            this.IP_0_05.Text = ipNumbers[0]; 
            this.IP_0_06.Text = ipNumbers[0]; 
            this.IP_0_07.Text = ipNumbers[0]; 
            this.IP_0_08.Text = ipNumbers[0]; 
            this.IP_0_09.Text = ipNumbers[0];

            this.IP_1_00.Text = ipNumbers[1];
            this.IP_1_01.Text = ipNumbers[1];
            this.IP_1_02.Text = ipNumbers[1];
            this.IP_1_03.Text = ipNumbers[1];
            this.IP_1_04.Text = ipNumbers[1];
            this.IP_1_05.Text = ipNumbers[1];
            this.IP_1_06.Text = ipNumbers[1];
            this.IP_1_07.Text = ipNumbers[1];
            this.IP_1_08.Text = ipNumbers[1];
            this.IP_1_09.Text = ipNumbers[1];

            this.IP_2_00.Text = ipNumbers[2];
            this.IP_2_01.Text = ipNumbers[2];
            this.IP_2_02.Text = ipNumbers[2];
            this.IP_2_03.Text = ipNumbers[2];
            this.IP_2_04.Text = ipNumbers[2];
            this.IP_2_05.Text = ipNumbers[2];
            this.IP_2_06.Text = ipNumbers[2];
            this.IP_2_07.Text = ipNumbers[2];
            this.IP_2_08.Text = ipNumbers[2];
            this.IP_2_09.Text = ipNumbers[2];

            this.IP_3_00.Text = ipNumbers[3];
            this.IP_3_01.Text = ipNumbers[3];
            this.IP_3_02.Text = ipNumbers[3];
            this.IP_3_03.Text = ipNumbers[3];
            this.IP_3_04.Text = ipNumbers[3];
            this.IP_3_05.Text = ipNumbers[3];
            this.IP_3_06.Text = ipNumbers[3];
            this.IP_3_07.Text = ipNumbers[3];
            this.IP_3_08.Text = ipNumbers[3];
            this.IP_3_09.Text = ipNumbers[3];

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
        private void ServerButton00_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_00.Text + "."
                                     + this.IP_1_00.Text + "."
                                     + this.IP_2_00.Text + "."
                                     + this.IP_3_00.Text + ":"
                                     + this.Port_00.Text;


            if (this.ServerButton00.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server00_Stay_Alive = true;
                this.ServerButton00.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server00_Start(address));
                thread.Start();
            }
            else {
                server00_Stay_Alive = false;
                this.ServerButton00.Content = "Start Server";
                this.alive_server_count--;
            }

            if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;
        }

        private void ServerButton01_Click_1(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_01.Text + "."
                                     + this.IP_1_01.Text + "."
                                     + this.IP_2_01.Text + "."
                                     + this.IP_3_01.Text + ":"
                                     + this.Port_01.Text;


            if (this.ServerButton01.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server01_Stay_Alive = true;
                this.ServerButton01.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server01_Start(address));
                thread.Start();
            }
            else
            {
                server01_Stay_Alive = false;
                this.ServerButton01.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }

            if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

        }

        private void ServerButton02_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_02.Text + "."
                                     + this.IP_1_02.Text + "."
                                     + this.IP_2_02.Text + "."
                                     + this.IP_3_02.Text + ":"
                                     + this.Port_02.Text;


            if (this.ServerButton02.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server02_Stay_Alive = true;
                this.ServerButton02.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server02_Start(address));
                thread.Start();
            }
            else
            {
                server02_Stay_Alive = false;
                this.ServerButton02.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton03_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_03.Text + "."
                                     + this.IP_1_03.Text + "."
                                     + this.IP_2_03.Text + "."
                                     + this.IP_3_03.Text + ":"
                                     + this.Port_03.Text;


            if (this.ServerButton03.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server03_Stay_Alive = true;
                this.ServerButton03.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server03_Start(address));
                thread.Start();
            }
            else
            {
                server03_Stay_Alive = false;
                this.ServerButton03.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton04_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_04.Text + "."
                                      + this.IP_1_04.Text + "."
                                      + this.IP_2_04.Text + "."
                                      + this.IP_3_04.Text + ":"
                                      + this.Port_04.Text;


            if (this.ServerButton04.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server04_Stay_Alive = true;
                this.ServerButton04.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server04_Start(address));
                thread.Start();
            }
            else
            {
                server04_Stay_Alive = false;
                this.ServerButton04.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton05_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_05.Text + "."
                                      + this.IP_1_05.Text + "."
                                      + this.IP_2_05.Text + "."
                                      + this.IP_3_05.Text + ":"
                                      + this.Port_05.Text;


            if (this.ServerButton05.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server05_Stay_Alive = true;
                this.ServerButton05.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server05_Start(address));
                thread.Start();
            }
            else
            {
                server05_Stay_Alive = false;
                this.ServerButton05.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton06_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_06.Text + "."
                                     + this.IP_1_06.Text + "."
                                     + this.IP_2_06.Text + "."
                                     + this.IP_3_06.Text + ":"
                                     + this.Port_06.Text;


            if (this.ServerButton06.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server06_Stay_Alive = true;
                this.ServerButton06.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server06_Start(address));
                thread.Start();
            }
            else
            {
                server06_Stay_Alive = false;
                this.ServerButton06.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton07_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_07.Text + "."
                                      + this.IP_1_07.Text + "."
                                      + this.IP_2_07.Text + "."
                                      + this.IP_3_07.Text + ":"
                                      + this.Port_07.Text;


            if (this.ServerButton07.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server07_Stay_Alive = true;
                this.ServerButton07.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server07_Start(address));
                thread.Start();
            }
            else
            {
                server07_Stay_Alive = false;
                this.ServerButton07.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton08_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_08.Text + "."
                                      + this.IP_1_08.Text + "."
                                      + this.IP_2_08.Text + "."
                                      + this.IP_3_08.Text + ":"
                                      + this.Port_08.Text;


            if (this.ServerButton08.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server08_Stay_Alive = true;
                this.ServerButton08.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server08_Start(address));
                thread.Start();
            }
            else
            {
                server08_Stay_Alive = false;
                this.ServerButton08.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void ServerButton09_Click(object sender, RoutedEventArgs e)
        {
            string address = "ws://" + this.IP_0_09.Text + "."
                                      + this.IP_1_09.Text + "."
                                      + this.IP_2_09.Text + "."
                                      + this.IP_3_09.Text + ":"
                                      + this.Port_09.Text;


            if (this.ServerButton09.Content.Equals("Start Server"))
            {
                this.alive_server_count++;
                server09_Stay_Alive = true;
                this.ServerButton09.Content = "Stop Server";
                this.NetSel.IsEnabled = false;
                Thread thread = new Thread(() => Server09_Start(address));
                thread.Start();
            }
            else
            {
                server09_Stay_Alive = false;
                this.ServerButton09.Content = "Start Server";
                this.alive_server_count--;

                if (alive_server_count == 0) this.NetSel.IsEnabled = true; else this.NetSel.IsEnabled = false;

            }
        }

        private void Server00_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server00_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server01_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server01_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server02_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server02_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server03_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server03_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server04_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server04_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server05_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server05_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server06_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server06_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server07_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server07_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server08_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server08_Stay_Alive) { }

            wssv.Stop();
        }

        private void Server09_Start(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharp>("/RemoSharp");

            wssv.Start();

            while (server09_Stay_Alive) { }

            wssv.Stop();
        }

        private void CopyButton00_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_00.Text + "." 
                                  + this.IP_1_00.Text + "." 
                                  + this.IP_2_00.Text + "." 
                                  + this.IP_3_00.Text + ":" 
                                  + this.Port_00.Text + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton01_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_01.Text
                            + "." + this.IP_1_01.Text 
                            + "." + this.IP_2_01.Text
                            + "." + this.IP_3_01.Text
                            + ":" + this.Port_01.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton02_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_02.Text
                            + "." + this.IP_1_02.Text
                            + "." + this.IP_2_02.Text
                            + "." + this.IP_3_02.Text
                            + ":" + this.Port_02.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton03_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_03.Text
                            + "." + this.IP_1_03.Text
                            + "." + this.IP_2_03.Text
                            + "." + this.IP_3_03.Text
                            + ":" + this.Port_03.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton04_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_04.Text
                            + "." + this.IP_1_04.Text
                            + "." + this.IP_2_04.Text
                            + "." + this.IP_3_04.Text
                            + ":" + this.Port_04.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton05_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_05.Text
                            + "." + this.IP_1_05.Text
                            + "." + this.IP_2_05.Text
                            + "." + this.IP_3_05.Text
                            + ":" + this.Port_05.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton06_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_06.Text
                            + "." + this.IP_1_06.Text
                            + "." + this.IP_2_06.Text
                            + "." + this.IP_3_06.Text
                            + ":" + this.Port_06.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton07_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_07.Text
                            + "." + this.IP_1_07.Text
                            + "." + this.IP_2_07.Text
                            + "." + this.IP_3_07.Text
                            + ":" + this.Port_07.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton08_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_08.Text
                            + "." + this.IP_1_08.Text
                            + "." + this.IP_2_08.Text
                            + "." + this.IP_3_08.Text
                            + ":" + this.Port_08.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CopyButton09_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://" + this.IP_0_09.Text
                            + "." + this.IP_1_09.Text
                            + "." + this.IP_2_09.Text
                            + "." + this.IP_3_09.Text
                            + ":" + this.Port_09.Text
                            + "/RemoSharp";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

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
            this.IP_0_05.Text = IP_0;
            this.IP_0_06.Text = IP_0;
            this.IP_0_07.Text = IP_0;
            this.IP_0_08.Text = IP_0;
            this.IP_0_09.Text = IP_0;

            this.IP_1_00.Text = IP_1;
            this.IP_1_01.Text = IP_1;
            this.IP_1_02.Text = IP_1;
            this.IP_1_03.Text = IP_1;
            this.IP_1_04.Text = IP_1;
            this.IP_1_05.Text = IP_1;
            this.IP_1_06.Text = IP_1;
            this.IP_1_07.Text = IP_1;
            this.IP_1_08.Text = IP_1;
            this.IP_1_09.Text = IP_1;

            this.IP_2_00.Text = IP_2;
            this.IP_2_01.Text = IP_2;
            this.IP_2_02.Text = IP_2;
            this.IP_2_03.Text = IP_2;
            this.IP_2_04.Text = IP_2;
            this.IP_2_05.Text = IP_2;
            this.IP_2_06.Text = IP_2;
            this.IP_2_07.Text = IP_2;
            this.IP_2_08.Text = IP_2;
            this.IP_2_09.Text = IP_2;

            this.IP_3_00.Text = IP_3;
            this.IP_3_01.Text = IP_3;
            this.IP_3_02.Text = IP_3;
            this.IP_3_03.Text = IP_3;
            this.IP_3_04.Text = IP_3;
            this.IP_3_05.Text = IP_3;
            this.IP_3_06.Text = IP_3;
            this.IP_3_07.Text = IP_3;
            this.IP_3_08.Text = IP_3;
            this.IP_3_09.Text = IP_3;

            this.PC_ID.Text = IP_3;
            this.PC_ADDRESS.Text = IP_2;

            //this.Address00.Text = "ws://" + this.IP_0_00.Text + "." + this.IP_1_00.Text + "." + this.IP_2_00.Text + "." + this.IP_3_00.Text + ":" + this.Port_00.Text + "/RemoSharp";
            //this.Address01.Text = "ws://" + this.IP_0_01.Text + "." + this.IP_1_01.Text + "." + this.IP_2_01.Text + "." + this.IP_3_01.Text + ":" + this.Port_01.Text + "/RemoSharp";
            //this.Address02.Text = "ws://" + this.IP_0_02.Text + "." + this.IP_1_02.Text + "." + this.IP_2_02.Text + "." + this.IP_3_02.Text + ":" + this.Port_02.Text + "/RemoSharp";
            //this.Address03.Text = "ws://" + this.IP_0_03.Text + "." + this.IP_1_03.Text + "." + this.IP_2_03.Text + "." + this.IP_3_03.Text + ":" + this.Port_03.Text + "/RemoSharp";
            //this.Address04.Text = "ws://" + this.IP_0_04.Text + "." + this.IP_1_04.Text + "." + this.IP_2_04.Text + "." + this.IP_3_04.Text + ":" + this.Port_04.Text + "/RemoSharp";
            //this.Address05.Text = "ws://" + this.IP_0_05.Text + "." + this.IP_1_05.Text + "." + this.IP_2_05.Text + "." + this.IP_3_05.Text + ":" + this.Port_05.Text + "/RemoSharp";
            //this.Address06.Text = "ws://" + this.IP_0_06.Text + "." + this.IP_1_06.Text + "." + this.IP_2_06.Text + "." + this.IP_3_06.Text + ":" + this.Port_06.Text + "/RemoSharp";
            //this.Address07.Text = "ws://" + this.IP_0_07.Text + "." + this.IP_1_07.Text + "." + this.IP_2_07.Text + "." + this.IP_3_07.Text + ":" + this.Port_07.Text + "/RemoSharp";
            //this.Address08.Text = "ws://" + this.IP_0_08.Text + "." + this.IP_1_08.Text + "." + this.IP_2_08.Text + "." + this.IP_3_08.Text + ":" + this.Port_08.Text + "/RemoSharp";
            //this.Address09.Text = "ws://" + this.IP_0_09.Text + "." + this.IP_1_09.Text + "." + this.IP_2_09.Text + "." + this.IP_3_09.Text + ":" + this.Port_09.Text + "/RemoSharp";

        }

        private void OnStartupServerStart(object sender, RoutedEventArgs e)
        {
            string address = "ws://127.0.0.1:18580";


            if (this.canvasBoundStartServer.Content.Equals("Start Server"))
            {
                canvasSrv_Stay_Alive = true;
                this.canvasBoundStartServer.Content = "Stop Server";
                Thread thread = new Thread(() => CanvasBoundsWS_Server(address));
                thread.Start();
            }
            else
            {
                canvasSrv_Stay_Alive = false;
                this.canvasBoundStartServer.Content = "Start Server";
            }
        }

        private void canvasBoundCopyAddress_Click(object sender, RoutedEventArgs e)
        {
            string name = "ws://127.0.0.1:18580/RemoSharpCanvasBounds";
            Exception threadEx = null;
            Thread staThread = new Thread(
              delegate ()
              {
                  try
                  {
                      System.Windows.Forms.Clipboard.SetText(name);
                  }

                  catch (Exception ex)
                  {
                      threadEx = ex;
                  }
              });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
        }

        private void CanvasBoundsWS_Server(string address)
        {

            WebSocketServer wssv = new WebSocketServer(address);

            wssv.AddWebSocketService<RemoSharpCanvasBounds>("/RemoSharpCanvasBounds");

            if (wssv.IsListening) wssv.Stop();
            wssv.Start();

            while (canvasSrv_Stay_Alive) { }

            wssv.Stop();
        }

        private void TurnOffServers(object sender, System.ComponentModel.CancelEventArgs e)
        {
            canvasSrv_Stay_Alive = false;
            server00_Stay_Alive = false;
            server01_Stay_Alive = false;
            server02_Stay_Alive = false;
            server03_Stay_Alive = false;
            server04_Stay_Alive = false;
            server05_Stay_Alive = false;
            server06_Stay_Alive = false;
            server07_Stay_Alive = false;
            server08_Stay_Alive = false;
            server09_Stay_Alive = false;
            Thread.Sleep(200);
    }

        private void OnStartupServerStart(object sender, EventArgs e)
        {
            string address = "ws://127.0.0.1:18580";


            if (this.canvasBoundStartServer.Content.Equals("Start Server"))
            {
                canvasSrv_Stay_Alive = true;
                this.canvasBoundStartServer.Content = "Stop Server";
                Thread thread = new Thread(() => CanvasBoundsWS_Server(address));
                thread.Start();
            }
            else
            {
                canvasSrv_Stay_Alive = false;
                this.canvasBoundStartServer.Content = "Start Server";
            }
        }
    }
}
