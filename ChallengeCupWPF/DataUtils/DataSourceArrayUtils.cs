using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace ChallengeCupWPF.DataUtils
{
    public static class DataSourceArrayUtils
    {
        /// <summary>
        ///  Convert ObservableDataSource<T>[] to List<T>[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static List<T>[] ToList<T>(this ObservableDataSource<T>[] dataSource)
        {
            List<T>[] dataList = new List<T>[dataSource.Length];
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataList[i] = dataSource[i].Collection.ToList();
            }
            return dataList;
        }

        /// <summary>
        /// Clear dataSource[index]
        /// </summary>
        /// <param name="index"></param>
        public static void ClearDataSource<T>(this ObservableDataSource<T>[] dataSource, int index)
        {
            if (dataSource[index].Collection != null)
            {
                dataSource[index].Collection.Clear();
            }
        }

        /// <summary>
        /// Clear dataSource array
        /// </summary>
        public static void ClearDataSourceAll<T>(this ObservableDataSource<T>[] dataSource)
        {
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataSource.ClearDataSource(i);
            }
        }

        /// <summary>
        /// Convert Ys in List<Point>[] to List<float>[]
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static List<float>[] ToFloatList(this List<Point>[] points)
        {
            List<float>[] result = new List<float>[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                result[i] = new List<float>();
                for (int j = 0; j < points[i].Count; j++)
                {
                    result[i].Add((float)points[i][j].Y);
                }
            }
            return result;
        }
    }
}
