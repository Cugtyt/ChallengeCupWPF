using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupWPF.DataUtils
{
    /// <summary>
    /// FFT data
    /// </summary>
    public static class FFT
    {
        /// <summary>
        /// Get data from ObservableDataSource<Point>[] and FFT them
        /// </summary>
        /// <param name="dataSource">data from ObservableDataSource<Point>[]</param>
        /// <returns>FFT result</returns>
        public static ObservableDataSource<Point>[] FFTForward(ObservableDataSource<Point>[] dataSource)
        {
            // Get points form dataSource
            List<Point>[] points = new List<Point>[dataSource.Length];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = dataSource[i].GetPoints().ToList();
            }

            // Convert points to complex
            Complex[][] data = new Complex[points.Length][];
            for (int i = 0; i < data.Length; i++)
            {
                for (int j = 0; j < points[i].Count; j++)
                {
                    data[i][j] = new Complex((float)points[i][j].Y, 0);
                }
            }

            // FFT
            ObservableDataSource<Point>[] fftResult = new ObservableDataSource<Point>[data.Length];
            for (int i = 0; i < fftResult.Length; i++)
            {
                fftResult[i] = new ObservableDataSource<Point>();
                Fourier.Forward(data[i]);
                fftResult[i].AppendMany(ConvertComplexToPoints(data[i]));
            }
            return fftResult;
        }

        /// <summary>
        /// Convert complex array to points list
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static List<Point> ConvertComplexToPoints(Complex[] data)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < data.Length; i++)
            {
                points.Add(new Point(data[i].Real, 0));
            }
            return points;
        }
    }
}
