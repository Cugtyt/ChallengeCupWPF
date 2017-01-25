using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource
{
    public static class GratingDataContainer
    {
        public static int MaxLength = 1000;

        public static List<double>[] Data;

        public static Task<List<double>[]> GetDataFrom(string[] data)
        {
            return Task.Run(() =>
            {
                lock (typeof(GratingDataContainer))
                {

                    try
                    {
                        Data = new List<double>[int.Parse(data[1]) + 1];
                        for (int i = 0; i < Data.Length; i++)
                        {
                            Data[i] = new List<double>();
                        }
                    }
                    catch (Exception)
                    {
#if DEBUG
                        Console.WriteLine("File formate is illegal.");
#endif
                        throw new Exception("File formate is illegal.");
                    }

                    for (int i = 0; i < data.Length && i < MaxLength * 10; i++)
                    {
                        switch (i % 10)
                        {
                            case 5:
                                Data[0].Add(double.Parse(data[i]));
                                break;
                            case 6:
                                Data[1].Add(double.Parse(data[i]));
                                break;
                            case 7:
                                Data[2].Add(double.Parse(data[i]));
                                break;
                            case 8:
                                Data[3].Add(double.Parse(data[i]));
                                break;
                            case 9:
                                Data[4].Add(double.Parse(data[i]));
                                break;
                            default:
                                break;
                        }
                    }
                }
                return Data;
            });
        }
    }
}
