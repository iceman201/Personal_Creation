using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Console_TimeClient
{
    class Program
    {
        private static Socket client_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


        private static void WhileSend()
        {
            while (true)
            {
                Console.Write("Enter message: ");
                string request = Console.ReadLine();
                byte[] buffer = Encoding.ASCII.GetBytes(request);
                client_socket.Send(buffer);

                byte[] receiving_buffer = new byte[2046];
                int receiving = client_socket.Receive(receiving_buffer);
                byte[] data = new byte[receiving];
                Array.Copy(receiving_buffer, data, receiving);
                Console.WriteLine("Received: " + Encoding.ASCII.GetString(data));
            }
        }

        private static void WhileConnect()
        {
            int counter = 0;
            while (!client_socket.Connected)
            {
                try
                {
                    counter++;
                    client_socket.Connect(IPAddress.Loopback, 100); // port number is 100
                }
                catch (SocketException)
                {
                    Console.WriteLine("Number of Connection: " + counter.ToString());
                }
            }
        }
        
        static void Main(string[] args)
        {
            Console.Title = "Client";
            WhileConnect();
            WhileSend();
            Console.ReadLine();
        }
    }
}
