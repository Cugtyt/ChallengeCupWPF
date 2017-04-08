using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV2.DataSource
{
    public static class GratingDataContainer
    {
        /// <summary>
        /// A flag stands if data is ready
        /// 
        /// Data is read from input, if input is null or input is invalid, 
        /// Data will be null, if some method read Data, there will be exception,
        /// so, each method that read data from Data should check this value first.
        /// </summary>
        public static bool IsDataReady = false;
        /// <summary>
        /// MaxLength limits length every list of double in Data array
        /// </summary>
        private static int maxLength = 512;
        /// <summary>
        /// Stores all data after parsing input data
        /// </summary>
        //public static List<double>[] Data;
        private static int chMax = 4;
        private static int gratingMax = 6;
        // Do not set array length here, exception will thrown in UDPRead for init problem 
        // !!!!
        public static List<double>[][] Data;
        public static double?[][] RefLen;

        static GratingDataContainer()
        {
            Data = new List<double>[chMax][];
            RefLen = new double?[chMax][];
            for (int i = 0; i < chMax; i++)
            {
                Data[i] = new List<double>[gratingMax];
                RefLen[i] = new double?[gratingMax];
                for (int j = 0; j < gratingMax; j++)
                {
                    Data[i][j] = new List<double>();
                    RefLen[i][j] = null;
                }
            }
        }

        /// <summary>
        /// Get data from a string array input,
        /// there should be infos of length of data array in input
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
//        public static Task<List<double>[]> GetDataFrom(string[] data)
//        {
//            if (data == null)
//            {
//#if DEBUG
//                Console.WriteLine("GratingDataContainer: GetDataFrom() -> Illegal input, argument can not be null.");
//#endif
//                throw new ArgumentNullException("GratingDataContainer: GetDataFrom()");
//            }
//            return Task.Run(() =>
//            {
//                lock (typeof(GratingDataContainer))
//                {

//                    try
//                    {
//                        Data = new List<double>[int.Parse(data[1]) + 1];
//                        for (int i = 0; i < Data.Length; i++)
//                        {
//                            Data[i] = new List<double>();
//                        }
//                    }
//                    catch (Exception)
//                    {
//#if DEBUG
//                        Console.WriteLine("File formate is illegal.");
//#endif
//                        throw new Exception("File formate is illegal.");
//                    }

//                    for (int i = 0; i < data.Length && i < MaxLength * 10; i++)
//                    {
//                        switch (i % 10)
//                        {
//                            case 5:
//                                Data[0].Add(double.Parse(data[i]));
//                                break;
//                            case 6:
//                                Data[1].Add(double.Parse(data[i]));
//                                break;
//                            case 7:
//                                Data[2].Add(double.Parse(data[i]));
//                                break;
//                            case 8:
//                                Data[3].Add(double.Parse(data[i]));
//                                break;
//                            case 9:
//                                Data[4].Add(double.Parse(data[i]));
//                                break;
//                            default:
//                                break;
//                        }
//                    }
//                }
//                return Data;
//            });
//        }

//        public static void GetDataFrom(string[] data)
//        {
//            // Input data is invalid, set IsDataReady and return
//            if (data == null)
//            {
//#if DEBUG
//                Console.WriteLine("GratingDataContainer: GetDataFrom() -> Illegal input, argument can not be null.");
//#endif
//                IsDataReady = false;
//                return;
//            }
//            // Input data is valid, read and set Data
//            IsDataReady = false;
//            lock (typeof(GratingDataContainer))
//            {
//                int len = int.Parse(data[1]) + int.Parse(data[2]) + int.Parse(data[3]) + int.Parse(data[4]);
//                try
//                {
//                    Data = new List<double>[len];
//                    for (int i = 0; i < Data.Length; i++)
//                    {
//                        Data[i] = new List<double>();
//                    }
//                }
//                catch (Exception)
//                {
//#if DEBUG
//                    Console.WriteLine("File formate is illegal.");
//#endif
//                    throw new Exception("File formate is illegal.");
//                }

//                for (int i = 0; i < data.Length && i < MaxLength * 10; i++)
//                {
//                    //switch (i % 10)
//                    //{
//                    //    //case 5:
//                    //    //    Data[0].Add(double.Parse(data[i]));
//                    //    //    break;
//                    //    case 6:
//                    //        Data[0].Add(double.Parse(data[i]));
//                    //        break;
//                    //    case 7:
//                    //        Data[1].Add(double.Parse(data[i]));
//                    //        break;
//                    //    case 8:
//                    //        Data[2].Add(double.Parse(data[i]));
//                    //        break;
//                    //    case 9:
//                    //        Data[3].Add(double.Parse(data[i]));
//                    //        break;
//                    //    default:
//                    //        break;
//                    //}
//                    if (i % (6 + len) > 5)
//                    {
//                        Data[i % (6 + len) - 6].Add(double.Parse(data[i]));
//                    }
//                }
//                IsDataReady = true;
//            }
//        }


        /// <summary>
        /// Updata data from input data
        /// </summary>
        /// <param name="data"></param>
        public static void UpdateData(List<double>[][] data)
        {
            // Input data is invalid, set IsDataReady and return
            if (data == null)
            {
#if DEBUG
                Console.WriteLine("GratingDataContainer: GetDataFrom() -> Illegal input, argument can not be null.");
#endif
                //IsDataReady = false;
                return;
            }
            IsDataReady = false;
            // Input data is valid, read and set Data
#if DEBUG
                Debug.Assert(data.Length == chMax);
                Debug.Assert(data[0].Length == gratingMax);
#endif
            lock (Data)
            {
                // Copy
                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = 0; j < data[i].Length; j++)
                    {
                        // Ensure that count is small than input data and not more than maxLength
                        if (Data[i][j].Count > data[i][j].Count && Data[i][j].Count >= maxLength)
                        {
                            Data[i][j].RemoveRange(0, data[i][j].Count);
                        }
                        Data[i][j].AddRange(data[i][j]);
                        if (Data[i][j].Count > 0)
                        {
                            RefLen[i][j] = Data[i][j].Average();

                        }
                        //else
                        //{
                        //    RefLen[i][j] = null;
                        //}
                    }
                }
                IsDataReady = Data[0][0].Count >= maxLength 
                    || Data[1][0].Count >= maxLength
                    || Data[2][0].Count >= maxLength
                    || Data[3][0].Count >= maxLength;
            }
        }
    }
}
