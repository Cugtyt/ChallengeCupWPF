using ChallengeCupV2.DataSource;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private int maxGratingNumber = 6;

        private UdpClient udpClient = new UdpClient(10000);
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

        public UDPRead()
        {
            for (int i = 0; i < dataBuffer.Length; i++)
            {
                dataBuffer[i] = new List<double>[maxGratingNumber];
                for (int j = 0; j < dataBuffer[i].Length; j++)
                {
                    dataBuffer[i][j] = new List<double>();
                }
            }
        }

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
            //parse input
            string[] parseResult = input.Split();
            // Add to buffer
            int spaceCount = 0;
            int ch = 0;
            int grating = 0;
            for (int i = 0; i < parseResult.Length; i++)
            {
                if (parseResult[i].Equals(""))
                {
                    spaceCount++;
                    if (spaceCount > 1)
                    {
                        break;
                    }
                    continue;
                }
                if (spaceCount == 1)
                {
                    ++ch;
                    grating = 0;
#if DEBUG
                    Debug.Assert(ch < 4);
                    Debug.Assert(grating < 6);
#endif
                }
                dataBuffer[ch][grating++].Add(double.Parse(parseResult[i]));
                spaceCount = 0;
            }
            // If count of buffer is enough, update 
            if (dataBuffer[0][0].Count >= 1000)
            {
                GratingDataContainer.UpdateData(dataBuffer);
                for (int i = 0; i < dataBuffer.Length; i++)
                {
                    for (int j = 0; j < dataBuffer[i].Length; j++)
                    {
                        dataBuffer[i][j].Clear();
                    }
                }
            }
        }
    }
}
