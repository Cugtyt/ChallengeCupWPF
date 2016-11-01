using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupWPF.TCPRead
{
    public class TCPRead
    {
        private static int port = 5000;
        private static string server = "127.0.0.1";
        // Store data from tcp
        //public static float[] data = new float[100];
        public static float data;
        // Whether tcp is connected
        public static bool isConnected = false;
        // Array index to write
        public static int index = 0;

        public static Task Read()
        {
            try
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

                        isConnected = true;
                        while (isConnected)
                        {
                            Byte[] recv = new Byte[4];
                            stream.Read(recv, 0, recv.Length);
                            data = BitConverter.ToSingle(recv, 0);
                            //data[index] = BitConverter.ToSingle(recv, 0);
                            //Console.WriteLine("TCPRead recv " + data[index]);
                            Task.Delay(10);
                            //index = (++index) % data.Length;
                        }
                        isConnected = false;
                    }
                }
            }
            catch (SocketException e)
            {
                MessageBox.Show("无连接");
                Console.WriteLine(e);
            }
            catch (Exception e)
            {
                isConnected = false;
                Console.WriteLine(e);
            }
            return null;
        }

    }
}
