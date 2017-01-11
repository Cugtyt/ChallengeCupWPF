using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.File
{
    class FileUtils
    {
        /// <summary>
        /// read data form file
        /// </summary>
        /// <param name="filePath">file path</param>
        /// <returns>data from file</returns>
        public static Task<List<double>[]> ReadDataAsync(string filePath)
        {
#if DEBUG
            Console.WriteLine("file is " + filePath);
#endif
            if (!System.IO.File.Exists(filePath))
            {
#if DEBUG
                Console.WriteLine("file is not valid");
#endif
                return null;
            }
#if DEBUG
            Console.WriteLine("file is valid");
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
    }
}
