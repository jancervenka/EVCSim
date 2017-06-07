using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Windows;

namespace EVC
{
    public class EVCServer
    {
        private TcpListener Server;
        IPAddress IPAddr;
    
        public EVCServer(int portNumber, string ipAddr)
        {

            try
            {
                IPAddr = IPAddress.Parse(ipAddr);
            }
            catch
            {
                MessageBox.Show("Invalid IP range", "Value Error");
                Application.Current.Shutdown();
            }

            Server = new TcpListener(IPAddr, portNumber);

        }

        public void Start(List<MessageContainer> smData, List<MessageContainer> paData)
        {

            byte[] clientResponse = new byte[1];
            int smIndx = 0;
            
            try
            {
                Console.WriteLine("STARTING THE SERVER...");
                Server.Start();
                TcpClient client = Server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                while (true)
                {
                    try
                    {
                        stream.Read(clientResponse, 0, 1);
                        Console.WriteLine("DMI RESPONSE: {0}", BitConverter.ToString(clientResponse));

                        if (clientResponse[0] == 0xFE && smIndx == smData.Count)
                        {
                            Console.WriteLine("END");
                            stream.Write(new byte[] { 0xFF }, 0, 1);
                            client.Close();
                            Server.Stop();
                            break;
                        }

                        if (clientResponse[0] == 0xFF)
                        {
                            Console.WriteLine("SEND PA");

                            List<byte> paDataJoined = new List<byte>();
                            paDataJoined.AddRange(paData[0].Msg);
                            paDataJoined.AddRange(paData[1].Msg);
                            paDataJoined.AddRange(paData[2].Msg);
                            stream.Write(paDataJoined.ToArray(), 0, paDataJoined.Count());
                        }
                        else if (clientResponse[0] == 0xFE)
                        {
                            stream.Write(smData[smIndx].Msg.ToArray(), 0, smData[smIndx].Msg.Count());
                            Console.WriteLine("SEND GM");
                            smIndx += 1;
                        }
                    }
                    catch (IOException e)
                    {
                        Console.WriteLine("IOException: {0}", e);
                        MessageBox.Show("Stream IO failure", "Value Error");
                        client.Close();
                        Server.Stop();
                        break;
                    }
                }
            }
            catch (SocketException e)
            {
               // Console.WriteLine("SocketException: {0}", e);
                MessageBox.Show("Connection failure", "Value Error");
                Server.Stop();
            }
            catch (IOException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                MessageBox.Show("IOException", "Value Error");
                Server.Stop();
            }

        }
    }
}
