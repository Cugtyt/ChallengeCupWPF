using ChallengeCupV2.DataSource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV2.UDP
{
    /// <summary>
    /// Receive data from UDP and update 
    /// </summary>
    public class UDPRead
    {
        /// <summary>
        /// Buffer data received from UDP and update data in GratingDataContainer
        /// </summary>
        private List<double>[][] dataBuffer = new List<double>[4][];

        private UdpClient udpClient = new UdpClient(10000);
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);
        /// <summary>
        /// Receive data from UDP and updata data in GratingDataContainer
        /// </summary>
        public void Receive()
        {
            while (true)
            {
                string tempRecv = Encoding.ASCII.GetString(
                    udpClient.Receive(ref endPoint));
                if (tempRecv != null)
                {
                    updateData(tempRecv);
                }
            }
        }

        /// <summary>
        /// Parse string input and add result to dataBuffer
        /// </summary>
        /// <param name="input"></param>
        private void updateData(string input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("UDPRead: updateData() -> input is null");
#endif
                return;
            }
            //parse input to double list
            string[] parseResult = input.Split();
            
            // Add to buffer

            // If count of buffer is enough, update 
            if (dataBuffer[0][0].Count >= 500)
            {
                GratingDataContainer.UpdateData(dataBuffer);
            }
        }
    }
}
