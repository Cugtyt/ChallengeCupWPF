using ChallengeCupV1.DataSource.GearState;
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
//        public static Task<List<double>[]> ReadWaveData(string filePath, int max = 1000)
//        {
//            if (filePath == null)
//            {
//#if DEBUG
//                Console.WriteLine("FileUtils: ReadWaveData() -> Illegal input, argument can not be null.");
//#endif
//                throw new ArgumentNullException("FileUtils: ReadWaveData()");
//            }
            
//            if (!System.IO.File.Exists(filePath))
//            {
//#if DEBUG
//                Console.WriteLine("FileUtils: ReadWaveData() -> file is not valid");
//#endif
//                throw new FileNotFoundException("FileUtils: ReadWaveData() -> file " + filePath + "is not valid.");
//            }
//#if DEBUG
//            Console.WriteLine("FileUtils: ReadWaveData() -> file is valid");
//#endif
//            //List<double>[] dataList = new List<double>[4];
//            //for (int i = 0; i < dataList.Length; i++)
//            //{
//            //    dataList[i] = new List<double>();
//            //}
//            return Task.Run(() =>
//            {
//                string text;
//                // begin to read data form file
//                using (StreamReader reader = new StreamReader(filePath))
//                {
//                    reader.ReadLine();
//                    reader.ReadLine();
//                    reader.ReadLine();
//                    text = reader.ReadToEnd();
//                }
//                var temp = text.Substring(0, text.LastIndexOf('\n'));
//                string[] testResult = text.Replace("\r", " ").Replace("\t", " ").Replace("\n", " ")
//                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

//                List<double>[] dataList;
//                try
//                {
//                    dataList = new List<double>[int.Parse(testResult[1]) + 1];
//                    for (int i = 0; i < dataList.Length; i++)
//                    {
//                        dataList[i] = new List<double>();
//                    }
//                }
//                catch (Exception)
//                {
//#if DEBUG
//                    Console.WriteLine("File formate is illegal.");
//#endif
//                    throw new Exception("File formate is illegal.");
//                }

//                for (int i = 0; i < testResult.Length - 20 && i < max * 10; i++)
//                {
//                    switch (i % 10)
//                    {
//                        case 5:
//                            dataList[0].Add(double.Parse(testResult[i]));
//                            break;
//                        case 6:
//                            dataList[1].Add(double.Parse(testResult[i]));
//                            break;
//                        case 7:
//                            dataList[2].Add(double.Parse(testResult[i]));
//                            break;
//                        case 8:
//                            dataList[3].Add(double.Parse(testResult[i]));
//                            break;
//                        case 9:
//                            dataList[4].Add(double.Parse(testResult[i]));
//                            break;
//                        default:
//                            break;
//                    }
//                }
//                return dataList;
//            });

//        }

        /// <summary>
        /// Read data form file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>data from file</returns>
//        public static Task<string[]> ReadDataFromFile(string filePath)
//        {
//            if (filePath == null)
//            {
//#if DEBUG
//                Console.WriteLine("FileUtils: ReadWaveData() -> Illegal input, argument can not be null.");
//#endif
//                throw new ArgumentNullException("FileUtils: ReadWaveData()");
//            }

//            if (!System.IO.File.Exists(filePath))
//            {
//#if DEBUG
//                Console.WriteLine("FileUtils: ReadWaveData() -> file is not valid");
//#endif
//                throw new FileNotFoundException("FileUtils: ReadWaveData() -> file " + filePath + "is not valid.");
//            }
//#if DEBUG
//            Console.WriteLine("FileUtils: ReadWaveData() -> file is valid");
//#endif
//            //List<double>[] dataList = new List<double>[4];
//            //for (int i = 0; i < dataList.Length; i++)
//            //{
//            //    dataList[i] = new List<double>();
//            //}
//            return Task.Run(() =>
//            {
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
//            });
//        }

        public static string[] ReadDataFromFile(string filePath)
        {
            if (filePath == null)
            {
#if DEBUG
                Console.WriteLine("FileUtils: ReadWaveData() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("FileUtils: ReadWaveData()");
            }
            
            if (!System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("FileUtils: ReadWaveData() -> file is not valid");
#endif
                throw new FileNotFoundException("FileUtils: ReadWaveData() -> file " + filePath + "is not valid.");
            }
#if DEBUG
            Console.WriteLine("FileUtils: ReadWaveData() -> file is valid");
#endif
            //List<double>[] dataList = new List<double>[4];
            //for (int i = 0; i < dataList.Length; i++)
            //{
            //    dataList[i] = new List<double>();
            //}
            //return Task.Run(() =>
            //{
            string text;
            // begin to read data form file
            using (StreamReader reader = new StreamReader(filePath))
            {
                reader.ReadLine();
                reader.ReadLine();
                reader.ReadLine();
                text = reader.ReadToEnd();
            }
            text = text.Substring(0, text.LastIndexOf('\n'));
            return text.Replace("\r", " ").Replace("\t", " ").Replace("\n", " ")
            .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //});
        }

        /// <summary>
        /// Remove file
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
//        public static Task RemoveFile(string filePath)
//        {
//            if (filePath == null)
//            {
//#if DEBUG
//                Console.WriteLine("FileUtils: RemoveFile() -> Illegal input, argument can not be null.");
//#endif
//                throw new ArgumentNullException("FileUtils: RemoveFile()");
//            }

//            if (!System.IO.File.Exists(filePath))
//            {
//#if DEBUG
//                Console.WriteLine("FileUtils: RemoveFile() -> file is not exit");
//#endif
//                throw new FileNotFoundException("FileUtils: RemoveFile() -> file " + filePath + "is not valid.");
//            }
//            // File exits
//            try
//            {
//                System.IO.File.Delete(filePath);
//#if DEBUG
//                Console.WriteLine("FileUtils: RemoveFile() -> remove file done");
//#endif
//            }
//            catch (IOException e)
//            {
//                Console.WriteLine(e.Message);
//            }

//            return null;
//        }

        /// <summary>
        /// Remove all files from start to end, end will not be deleted.
        /// </summary>
        /// <param name="info">FileInfo array</param>
        /// <returns></returns>
        public static void RemoveFileAll(FileInfo[] info, int start, int end)
        {
            if (info == null || end <= start)
            {
                return;
            }

            for (int i = start; i < end; i++)
            {
                Console.WriteLine("now " + i);
                info[i].Delete();
            }
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
        /// Generate state report to file
        /// File name is named by GearStateData + time stamp
        /// </summary>
        /// <param name="dirPath"></param>
        /// <param name="datalist"></param>
        /// <returns></returns>
        public static void GenerateStateReportFile(string dirPath, ICollection<StateDataTemplate> datalist)
        {
            #region Generate and check if the file path created by time is valid, it shoule always be vaild
            if (dirPath == null)
            {
#if DEBUG
                Console.WriteLine("FileUtils: GenerateStateReportFile() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("FileUtils: GenerateStateReportFile()");
            }

#if DEBUG
            Console.WriteLine("FileUtils: GenerateStateReportFile() -> dir is " + dirPath);
#endif
            DateTime timeStamp = DateTime.Now;
            string filePath = dirPath + "GearStateData" + timeStamp.ToString("yyyyMMddHHmmssff") + ".txt";
            if (System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("FileUtils: GenerateStateReportFile() -> file is not valid, IT CANNOT HAPPENED!!!");
#endif
                throw new Exception("FileUtils: GenerateStateReportFile() -> file exits.");
            }
#if DEBUG
            Console.WriteLine("FileUtils: GenerateStateReportFile() -> filePath: " + filePath);
#endif
            #endregion

            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("This is a gear state data file generated by CCV1.\nTime: "
                + timeStamp + "\n{0, -20}{1, -20}{2, -20}{3, -20}\n", "Grating", "StatePARM", "Value", "Unit"));
            foreach (var d in datalist)
            {
                sb.Append(string.Format("{0, -20}{1, -20}{2, -20}{3, -20}\n", d.GratingID, d.Name, d.Value, d.Unit));
            }
#if DEBUG
            Console.WriteLine(sb);
#endif
            FileStream fs = new FileStream(filePath, FileMode.Create);
            using (StreamWriter writer = new StreamWriter(fs))
            {
                writer.Write(sb.ToString());
#if DEBUG
                Console.WriteLine("write done");
#endif
            }
            
        }

    }
}
