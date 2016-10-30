using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeCupWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupWPF.Tests
{
    [TestClass()]
    public class FileUtilsTests
    {
        private string readFilePath = @"C:\Users\Daniel\Desktop\read.txt";
        private string writeFilePath = @"C:\Users\Daniel\Desktop\write.txt";
        private string test = "1.5\t1.4\t\n2.1\t2.8\t\n";

        [TestMethod()]
        public void ReadDataAsyncTest()
        {
            List<float>[] data = FileUtils.ReadDataAsync(readFilePath).Result;
            Assert.AreEqual(ConvertDataToString(data), test);
        }

        [TestMethod()]
        public void WriteDataTest()
        {
            // Get data from read file first
            List<float>[] data = FileUtils.ReadDataAsync(readFilePath).Result;
            // Write to file
            FileUtils.WriteData(data, writeFilePath);
            // Read again
            data = FileUtils.ReadDataAsync(readFilePath).Result;
            Assert.AreEqual(ConvertDataToString(data), test);
        }

        /// <summary>
        /// Convert arrary of float list to string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string ConvertDataToString(List<float>[] data)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                foreach (var item in data[i])
                {
                    sb.Append(item + "\t");
                }
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}