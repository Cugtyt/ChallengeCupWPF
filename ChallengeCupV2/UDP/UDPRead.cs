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
    public class UDPRead : IDisposable
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
            for (int i = 0; i < len && i < maxGratingNumber; i++)
            {
                dataBuffer[ch][i].Add(double.Parse(results[start + i]));
            }
        }

        public void Dispose()
        {
            udpClient.Close();
            ((IDisposable)udpClient).Dispose();
        }
//        class Program
//        {
//            static int count;
//            static void Main(string[] args)
//            {
//                UdpClient udpClient = new UdpClient(100);
//                IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 60);
//                double a1 = 0.01;
//                double a2 = 1545.5;
//                double a3 = 1545.6;
//                Random rd = new Random();
//                int count = 0;

//                #region Test Case 1
//                while (true)
//                {
//                    double ran = rd.NextDouble() / 100;
//                    double sina1 = Math.Sin(a1) / 100;
//                    Byte[] message = Encoding.ASCII.GetBytes(string.Format("2 1 1 0 {1} {2} {1} {2}", sina1 + ran, a2 + ran, a3 + ran));
//                    udpClient.Send(message, message.Length, endPoint);
//                    //Thread.Sleep(1);
//                    Console.Write(++count >= 6000 ? DateTime.Now.ToString() + $"\t{count}\n" : "");
//                    count %= 6000;
//                    a1 += 0.08;
//                }
//                //UdpClient udpClient = new UdpClient(60);
//                //IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 100);
//                //count = 0;
//                //while (true)
//                //{
//                //    string tempRecv = Encoding.ASCII.GetString(
//                //        udpClient.Receive(ref endPoint));
//                //    if (tempRecv != null)
//                //    {
//                //        count++;
//                //        if (count > 500)
//                //        {
//                //            Console.WriteLine(count);
//                //            count = 0;
//                //        }
//                //    }
//                //}
//                #endregion
//                #region Test Case 2
//                //var data = GetDataFrom(ReadDataFromFile(@"C:\debug\FBG解调系统数据文件\数据文件\2017-04-05\0_12_70\PeaksAll 2017-04-05 22-43-34 14-Wednesday.txt"));
//                //int index = 0;
//                //while (true)
//                //{
//                //    Byte[] message = Encoding.ASCII.GetBytes(string.Format("0 1 0 0 {0}", data[0][index++]));
//                //    index %= data[0].Count();
//                //    udpClient.Send(message, message.Length, endPoint);
//                //    Thread.Sleep(1);
//                //    Console.Write(++count >= 6000 ? DateTime.Now.ToString() + $"\t{count}\n" : "");
//                //    count %= 6000;
//                //}
//                #endregion
//            }

//            public static string[] ReadDataFromFile(string filePath)
//            {
//                if (filePath == null)
//                {
//#if DEBUG
//                Console.WriteLine("FileUtils: ReadWaveData() -> Illegal input, argument can not be null.");
//#endif
//                    throw new ArgumentNullException("FileUtils: ReadWaveData()");
//                }

//                if (!System.IO.File.Exists(filePath))
//                {
//#if DEBUG
//                Console.WriteLine("FileUtils: ReadWaveData() -> file is not valid");
//#endif
//                    throw new FileNotFoundException("FileUtils: ReadWaveData() -> file " + filePath + "is not valid.");
//                }
//#if DEBUG
//            Console.WriteLine("FileUtils: ReadWaveData() -> file is valid");
//#endif
//                //List<double>[] dataList = new List<double>[4];
//                //for (int i = 0; i < dataList.Length; i++)
//                //{
//                //    dataList[i] = new List<double>();
//                //}
//                //return Task.Run(() =>
//                //{
//                string text;
//                // begin to read data form file
//                using (StreamReader reader = new StreamReader(filePath))
//                {
//                    reader.ReadLine();
//                    reader.ReadLine();
//                    reader.ReadLine();
//                    text = reader.ReadToEnd();
//                }
//                text = text.Substring(0, text.LastIndexOf('\n'));
//                return text.Replace("\r", " ").Replace("\t", " ").Replace("\n", " ")
//                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
//                //});
//            }

//            public static List<double>[] GetDataFrom(string[] data)
//            {
//                List<double>[] Data;
//                int len;
//                try
//                {
//                    len = int.Parse(data[1]) + int.Parse(data[2]) + int.Parse(data[3]) + int.Parse(data[4]);
//                    Data = new List<double>[len];
//                    for (int i = 0; i < Data.Length; i++)
//                    {
//                        Data[i] = new List<double>();
//                    }
//                }
//                catch (Exception)
//                {
//                    throw new Exception("File formate is illegal.");
//                }

//                for (int i = 0; i < data.Length; i++)
//                {
//                    if (i % (6 + len) > 5)
//                    {
//                        Data[i % (6 + len) - 6].Add(double.Parse(data[i]));
//                    }
//                }
//                return Data;
//            }
//        }
    }
}
