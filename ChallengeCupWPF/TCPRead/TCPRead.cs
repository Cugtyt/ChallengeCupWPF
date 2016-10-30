using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupWPF.TCPRead
{
    class TCPRead
    {
        private static int portNum = 13;
        private static string hostName = "host.contoso.com";
        //private static bool isStoped = true;

        public static void Read(List<float> dataList)
        {
            // This is demo
            // Change it while using
            try
            {
                TcpClient client = new TcpClient(hostName, portNum);
                NetworkStream ns = client.GetStream();
                byte[] bytes = new byte[1024];
                int bytesRead = ns.Read(bytes, 0, bytes.Length);
                Console.WriteLine(Encoding.ASCII.GetString(bytes, 0, bytesRead));
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }

    //public class TcpTimeServer {

    //private const int portNum = 13;

    //public static int Main(String[] args) {
    //    bool done = false;

    //    TcpListener listener = new TcpListener(portNum);

    //    listener.Start();

    //    while (!done) {
    //        Console.Write("Waiting for connection...");
    //        TcpClient client = listener.AcceptTcpClient();

    //        Console.WriteLine("Connection accepted.");
    //        NetworkStream ns = client.GetStream();

    //        byte[] byteTime = Encoding.ASCII.GetBytes(DateTime.Now.ToString());

    //        try {
    //            ns.Write(byteTime, 0, byteTime.Length);
    //            ns.Close();
    //            client.Close();
    //        } catch (Exception e) {
    //            Console.WriteLine(e.ToString());
    //        }
    //    }

    //    listener.Stop();

    //    return 0;
    //}

//}
}
