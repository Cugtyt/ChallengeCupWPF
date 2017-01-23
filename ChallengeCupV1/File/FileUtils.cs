using ChallengeCupV1.DataSource.GearStatus;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.File
{
    public static class FileUtils
    {
        /// <summary>
        /// Read data form file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>data from file</returns>
        public static Task<List<double>[]> ReadWaveData(string filePath, int max = 10000)
        {
#if DEBUG
            Console.WriteLine("FileUtils: ReadWaveData() -> file is " + filePath);
#endif
            if (!System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("FileUtils: ReadWaveData() -> file is not valid");
#endif
                throw new Exception("File is not valid.");
            }
#if DEBUG
            Console.WriteLine("FileUtils: ReadWaveData() -> file is valid");
#endif
            //List<double>[] dataList = new List<double>[4];
            //for (int i = 0; i < dataList.Length; i++)
            //{
            //    dataList[i] = new List<double>();
            //}
            return Task.Run(() =>
            {
                string text;
                // begin to read data form file
                using (StreamReader reader = new StreamReader(filePath))
                {
                    reader.ReadLine();
                    reader.ReadLine();
                    reader.ReadLine();
                    text = reader.ReadToEnd();
                }
                string[] testResult = text.Replace("\r", " ").Replace("\t", " ").Replace("\n", " ")
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                List<double>[] dataList;
                try
                {
                    dataList = new List<double>[int.Parse(testResult[1]) + 1];
                    for (int i = 0; i < dataList.Length; i++)
                    {
                        dataList[i] = new List<double>();
                    }
                }
                catch (Exception)
                {
#if DEBUG
                    Console.WriteLine("File formate is illegal.");
#endif
                    throw new Exception("File formate is illegal.");
                }

                for (int i = 0; i < testResult.Length && i < max; i++)
                {
                    switch (i % 10)
                    {
                        case 5:
                            dataList[0].Add(double.Parse(testResult[i]));
                            break;
                        case 6:
                            dataList[1].Add(double.Parse(testResult[i]));
                            break;
                        case 7:
                            dataList[2].Add(double.Parse(testResult[i]));
                            break;
                        case 8:
                            dataList[3].Add(double.Parse(testResult[i]));
                            break;
                        case 9:
                            dataList[4].Add(double.Parse(testResult[i]));
                            break;
                        default:
                            break;
                    }
                }

#if DEBUG
                //for (int i = 0; i < dataList[3].Count; i++)
                //{
                //    for (int j = 0; j < dataList.Length; j++)
                //    {
                //        Console.WriteLine(dataList[j][i]);
                //    }
                //}
#endif
                return dataList;
            });

        }

        /// <summary>
        /// Remove file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Task RemoveFile(string filePath)
        {
#if DEBUG
            Console.WriteLine("FileUtils: RemoveFile() -> file is " + filePath);
#endif
            if (!System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("FileUtils: RemoveFile() -> file is not exit");
#endif
                return null;
            }
            // File exits
            try
            {
                System.IO.File.Delete(filePath);
#if DEBUG
                Console.WriteLine("FileUtils: RemoveFile() -> remove file done");
#endif
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }

        /// <summary>
        /// Remove all files
        /// </summary>
        /// <param name="info">FileInfo array</param>
        /// <returns></returns>
        public static Task RemoveFileAll(FileInfo[] info)
        {
#if DEBUG
            Console.WriteLine("FileUtils: RemoveFileAll()");
#endif
            for (int i = 0; i < info.Length; i++)
            {
                info[i].Delete();
            }
            return null;
        }

        /// <summary>
        /// Get project root path
        /// </summary>
        /// <returns></returns>
        public static string GetRootPath()
        {
            string BaseDirectoryPath = AppDomain.CurrentDomain.BaseDirectory;
            string rootPath = BaseDirectoryPath.Substring(0, BaseDirectoryPath.LastIndexOf("\\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            rootPath = rootPath.Substring(0, rootPath.LastIndexOf("\\"));
            return rootPath;
        }

        /// <summary>
        /// Generate status report to file
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="datalist"></param>
        /// <returns></returns>
        public static Task GenerateStatusReportFile(string dirPath, List<StatusData> datalist)
        {
            #region Generate and check if the file path created by time is valid, it shoule always be vaild

#if DEBUG
            Console.WriteLine("FileUtils: GenerateStatusReportFile() -> dir is " + dirPath);
#endif
            string timeStamp = DateTime.Now.ToString();
            string filePath = dirPath + @"\" + "GearStatusData" + timeStamp + ".txt";
            if (!System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("FileUtils: ReadWaveDataAsync() -> file is not valid, IT CANNOT HAPPENED!!!");
#endif
                return null;
            }
#if DEBUG
            Console.WriteLine("FileUtils: GenerateStatusReportFile() -> filePath: " + filePath);
#endif
            #endregion

            return Task.Run(() =>
            {

                StringBuilder sb = new StringBuilder();
                sb.Append(string.Format("This is a gear status data file generated by CCV1.\n Time: " 
                    + timeStamp + "\n{0:-20}Value\n", "StatusPARM"));
                foreach (var d in datalist)
                {
                    sb.Append(string.Format("{0:-20}{1}", d.Name, d.Value));
                }
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.Write(sb);
#if DEBUG
                    Console.WriteLine("write done");
#endif
                }
            });
        }
    }
}
