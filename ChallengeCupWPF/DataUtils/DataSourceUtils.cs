using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Windows;

namespace ChallengeCupWPF.DataUtils
{
    public static class DataSourceUtils
    {
        /// <summary>
        /// Convert ObservableDataSource<T>[] to List<T>[]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        public static List<T>[] ToListArray<T>(this ObservableDataSource<T>[] dataSource)
        {
            List<T>[] dataList = new List<T>[dataSource.Length];
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataList[i] = dataSource[i].Collection.ToList();
            }
            return dataList;
        }

        #region ClearDataSource
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
        /// <seealso cref="ClearDataSource{T}(ObservableDataSource{T}[], int)"/>
        public static void ClearDataSourceAll<T>(this ObservableDataSource<T>[] dataSource)
        {
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataSource?.ClearDataSource(i);
            }
        }
        #endregion

        #region ToFlostList
        /// <summary>
        /// Convert all Y part in ObservableDataSource<Point>.Collection  
        /// to  List<float>
        /// </summary>
        /// <param name="dataSource">ObservableDataSource<Point> to be converted</param>
        /// <returns></returns>
        public static List<float> ToFlostList(this ObservableDataSource<Point> dataSource)
        {
            List<float> result = new List<float>();
            for (int i = 0; i < dataSource.Collection.Count; i++)
            {
                result.Add((float)dataSource.Collection[i].Y);
            }
            return result;
        }

        /// <summary>
        /// Convert ObservableDataSource<Point>[] to array of float list
        /// via extension method of ObservableDataSource{Point}
        /// </summary>
        /// <param name="dataSource">ObservableDataSource<Point>[] to be converted</param>
        /// <returns></returns>
        /// <seealso cref="ToFlostList(ObservableDataSource{Point})"/>
        public static List<float>[] ToFlostListArray(this ObservableDataSource<Point>[] dataSource)
        {
            List<float>[] fl = new List<float>[dataSource.Length];
            for (int i = 0; i < dataSource.Length; i++)
            {
                fl[i] = dataSource[i].ToFlostList();
            }
            return fl;
        }
        #endregion

        #region Complex array and float list coverter
        /// <summary>
        /// Convert ObservableDataSource<Point> to list of complex
        /// real part of complex is point's y
        /// imag parr of complex is 0
        /// </summary>
        /// <param name="dataSource"></param>
        /// <returns></returns>
        /// <seealso cref="ToFlostList(ObservableDataSource{Point})"/>
        public static List<Complex> ToComplexList(this ObservableDataSource<Point> dataSource)
        {
            List<float> floatList = dataSource.ToFlostList();
            List<Complex> complexList = new List<Complex>();
            for (int i = 0; i < floatList.Count; i++)
            {
                complexList[i] = new Complex(floatList[i], 0);
            }
            return complexList;
        }

        /// <summary>
        /// Convert complex array to points list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<Point> ToPoints(this Complex[] data)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < data.Length; i++)
            {
                points.Add(new Point(data[i].Real, 0));
            }
            return points;
        }
        #endregion
    }
}
