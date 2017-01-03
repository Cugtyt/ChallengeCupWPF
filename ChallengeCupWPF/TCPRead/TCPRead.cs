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
        public static float Data;
        // Whether tcp is connected
        public static bool IsConnected = false;
        // Array index to write
        //public static int index = 0;

        public static Task Read()
        {
            TcpClient client = null; ;
            NetworkStream stream = null;
            try
            {
                client = new TcpClient(server, port);
                stream = client.GetStream();
                // DO NOT REMOVE, FOR TEST
                // Read data once, Tested successfully
                // Uncommit this code block while testing
                // -----------test code block begins------------
                //Byte[] recv = new Byte[4];
                //stream.Read(recv, 0, recv.Length);
                //bytes = BitConverter.ToSingle(recv, 0);
                // -----------test code block ends------------

                IsConnected = true;
                Byte[] recv = new Byte[4];
                while (IsConnected)
                {
                    stream.Read(recv, 0, recv.Length);
                    Data = BitConverter.ToSingle(recv, 0);
                    //data[index] = BitConverter.ToSingle(recv, 0);
                    //Console.WriteLine("TCPRead recv " + data[index]);
                    Task.Delay(10);
                    //index = (++index) % data.Length;
                }
                IsConnected = false;
            }
            catch (SocketException e)
            {
                IsConnected = false;
                MessageBox.Show("No server");
#if DEBUG
                Console.WriteLine("TCPRead.Read() " + e);
#endif
            }
            catch (Exception e)
            {
                IsConnected = false;
                MessageBox.Show("Unknown exception");
#if DEBUG
                Console.WriteLine("TCPRead.Read() " + e);
#endif
            }
            finally
            {
                stream?.Close();
                client?.Close();
            }
            return null;
        }

    }
}
