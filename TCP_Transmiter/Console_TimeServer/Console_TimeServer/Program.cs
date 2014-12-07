using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Net;

namespace Console_TimeServer
{
    class Program
    {
        private static Socket server_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static List<Socket> client_socket = new List<Socket>();
        private static byte[] buffer = new byte[2048];

        private static void SetupServer()
        {
            Console.WriteLine("The server is setting now.....");
            server_socket.Bind(new IPEndPoint(IPAddress.Any,100));
            server_socket.Listen(5); //just for 5s
            server_socket.BeginAccept(new AsyncCallback(AcceptCallback),null);
        }

        private static void AcceptCallback(IAsyncResult AR)
        {
            Socket socket = server_socket.EndAccept(AR);
            client_socket.Add(socket);
            Console.WriteLine("Clinet Connected");
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            server_socket.BeginAccept(new AsyncCallback(AcceptCallback), null);
        }

        private static void ReceiveCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            int received = socket.EndReceive(AR);
            byte[] data_buffer = new byte[received];
            Array.Copy(buffer, data_buffer, received);

            String text = Encoding.ASCII.GetString(data_buffer);
            Console.WriteLine("Message received: " + text);

            string respons = string.Empty;
            if (text.ToLower() != "get time")
            {
                respons = "Invaild Request";
            }
            else 
            {
                respons = DateTime.Now.ToLongTimeString();
            }
            byte[] data = Encoding.ASCII.GetBytes(respons);
            socket.BeginSend(data, 0, data.Length, SocketFlags.None, new AsyncCallback(SendCallback), socket);
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
        }


        private static void SendCallback(IAsyncResult AR)
        {
            Socket socket = (Socket)AR.AsyncState;
            socket.EndSend(AR);
        }
        static void Main(string[] args)
        {
            Console.Title = "Server";
            SetupServer();
            Console.ReadLine();
        }

    }
}
