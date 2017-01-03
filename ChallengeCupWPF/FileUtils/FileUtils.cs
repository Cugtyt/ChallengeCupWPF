using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupWPF
{
    public static class FileUtils
    {
        /// <summary>
        /// read data form file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>data from file</returns>
        public static Task<List<float>[]> ReadDataAsync(string filePath)
        {
#if DEBUG
            Console.WriteLine("file is " + filePath);
#endif
            if (!File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("file is not valid");
#endif
                return null;
            }
#if DEBUG
            Console.WriteLine("file is valid");
#endif

            int chanelNumber;
            string text;
            return Task.Run(() =>
            {
                // begin to read data form file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // read chanels' number
                    chanelNumber = int.Parse(reader.ReadLine());
                    // initial chanels[]
                    // read all data form file
                    text = reader.ReadToEnd();
                }
#if DEBUG
                Console.WriteLine("chanel number is " + chanelNumber);
#endif
                List<float>[] chanels = new List<float>[chanelNumber];
                for (int i = 0; i < chanels.Length; i++)
                {
                    chanels[i] = new List<float>();
                }
                #region SpiltData
                string[] data = text.Replace("\r\n", " ").Replace("\t", " ")
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
#if DEBUG
                foreach (var d in data)
                {
                    Console.WriteLine(d);
                }
#endif
                // parse and add data to chanels
                for (int i = 0; i < data.Length; i++)
                {
                    chanels[i % chanels.Length].Add(float.Parse(data[i]));
                }
                #endregion
                return chanels;
            });
        }

        /// <summary>
        /// write data to file
        /// </summary>
        /// <param name="data">data to write</param>
        /// <param name="filePath">file path</param>
        public static Task WriteData(List<float>[] data, string filePath)
        {
            if (data == null || data[0] == null)
            {
#if DEBUG
                Console.WriteLine("Data to write is empty");
#endif
                return null;
            }
#if DEBUG
            Console.WriteLine("write to file " + filePath);
            Console.WriteLine("chanel number is " + data.Length);
#endif
            Task.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(data.Length + "\n");
                for (int i = 0; i < data[0].Count; i++)
                {
                    foreach (var item in data)
                    {
                        sb.Append(item[i] + "\t");
                    }
                    sb.AppendLine();
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // write number of chanels to file
                    //writer.WriteLine(data.Length);
                    // write data
                    //for (int i = 0; i < data[0].Count; i++)
                    //{
                    //    foreach (var item in data)
                    //    {
                    //        writer.Write(item[i] + "\t");
                    //    }
                    //    writer.WriteLine();
                    //}
                    writer.Write(sb);
#if DEBUG
                    Console.WriteLine("write done");
#endif
                }
            });
            return null;
        }
    }
}
