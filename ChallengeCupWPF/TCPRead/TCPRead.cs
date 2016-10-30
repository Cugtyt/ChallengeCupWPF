using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupWPF.TCPRead
{
    public class TCPRead
    {
        private static int port = 5000;
        private static string server = "127.0.0.1";
        public static float data;
        public static bool isConnect = true;

        public static void Read()
        {
            using (TcpClient client = new TcpClient(server, port))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    // DO NOT REMOVE, FOR TEST
                    // Read data once, Tested successfully
                    // Uncommit this code block whiling testing
                    // -----------test code block begins------------
                    //Byte[] recv = new Byte[4];
                    //stream.Read(recv, 0, recv.Length);
                    //bytes = BitConverter.ToSingle(recv, 0);
                    // -----------test code block ends------------

                    while (isConnect)
                    {
                        Byte[] recv = new Byte[4];
                        stream.Read(recv, 0, recv.Length);
                        data = BitConverter.ToSingle(recv, 0);
                        Console.WriteLine(data);
                        if (data == -1000f)
                        {
                            isConnect = false;
                        }
                    }
                }
            }
        }

    }
}
