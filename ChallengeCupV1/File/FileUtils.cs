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
        public static Task<List<double>[]> ReadWaveData(string filePath)
        {
#if DEBUG
            Console.WriteLine("FileUtils: ReadWaveDataAsync() -> file is " + filePath);
#endif
            if (!System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("FileUtils: ReadWaveDataAsync() -> file is not valid");
#endif
                return null;
            }
#if DEBUG
            Console.WriteLine("FileUtils: ReadWaveDataAsync() -> file is valid");
#endif
            string text;
            List<double>[] dataList = new List<double>[4];
            for (int i = 0; i < dataList.Length; i++)
            {
                dataList[i] = new List<double>();
            }

            return Task.Run(() =>
            {
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

                for (int i = 0; i < testResult.Length; i++)
                {
                    switch (i % 10)
                    {
                        case 6:
                            dataList[0].Add(double.Parse(testResult[i]));
                            break;
                        case 7:
                            dataList[1].Add(double.Parse(testResult[i]));
                            break;
                        case 8:
                            dataList[2].Add(double.Parse(testResult[i]));
                            break;
                        case 9:
                            dataList[3].Add(double.Parse(testResult[i]));
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

        public static Task<FileInfo[]> ReadGearLib(string gearDir)
        {
#if DEBUG
            if (!Directory.Exists(gearDir))
            {
                Console.WriteLine("FileUtils: ReadGearLib() -> directory is not vaild");
                return null;
            }
#endif
            return Task.Run(() =>
            {
                DirectoryInfo dire = new DirectoryInfo(gearDir);
                return dire.GetFiles();
            });
        }
    }
}
