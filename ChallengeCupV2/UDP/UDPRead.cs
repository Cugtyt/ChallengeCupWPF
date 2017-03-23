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
        private int maxLen = 100;

        private UdpClient udpClient = new UdpClient(60);
        private IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 100);

        public UDPRead()
        {
            // To active GratingDataContainer static ctr
            GratingDataContainer.IsDataReady = false;
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
            string[] results = input.Split();
            // Add to buffer
            int ch1Count = int.Parse(results[0]);
            int ch2Count = int.Parse(results[1]);
            int ch3Count = int.Parse(results[2]);
            int ch4Count = int.Parse(results[3]);
            addResult(results, 4, 0, ch1Count);
            addResult(results, 4 + ch1Count, 1, ch2Count);
            addResult(results, 4 + ch1Count + ch2Count, 2, ch3Count);
            addResult(results, 4 + ch1Count + ch2Count + ch3Count, 3, ch4Count);
            // If count of buffer is enough, update 
            if (dataBuffer[0][0].Count >= maxLen || dataBuffer[1][0].Count >= maxLen
                || dataBuffer[2][0].Count >= maxLen | dataBuffer[3][0].Count >= maxLen)
            {
                GratingDataContainer.UpdateData(dataBuffer);
#if DEBUG
                Console.WriteLine("udp update data in grating data container.");
#endif
                for (int i = 0; i < dataBuffer.Length; i++)
                {
                    for (int j = 0; j < dataBuffer[i].Length; j++)
                    {
                        dataBuffer[i][j].Clear();
                    }
                }
            }
        }

        private void addResult(string[] results, int start, int ch, int len)
        {
            for (int i = 0; i < len; i++)
            {
                dataBuffer[ch][i].Add(double.Parse(results[start + i]));
            }
        }
    }
}
