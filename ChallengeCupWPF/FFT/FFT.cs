using MathNet.Numerics.IntegralTransforms;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupWPF.DataUtils
{
    /// <summary>
    /// FFT data
    /// </summary>
    public class FFT
    {
        public Complex[] Data { get; private set; } = new Complex[200];

        // TODO: flush GUI in real-time
        // Replacing all data in ObservableDataSource<Point>[] costs time
        // and data can not be updated while forwarding

        /// <summary>
        /// Set Data
        /// 
        /// If length of param data bigger than 512,
        /// copy last 512 element to Data
        /// </summary>
        /// <param name="data"></param>
        //public FFT(Complex[] data)
        //{
        //    Data = new Complex[data.Length > 512 ? 512 : data.Length];
        //    for (int i = 0; i < Data.Length; i++)
        //    {
        //        Data[i] = new Complex((double)data[i].Real, 0);
        //    }

        //    //if (data.Length <= 512)
        //    //{
        //    //    //data.CopyTo(Data, 0);
        //    //    Data = data;
        //    //}
        //    //else
        //    //{
        //    //    Data = new Complex[512];
        //    //    for (int i = 0; i < Data.Length; i++)
        //    //    {
        //    //        Data[i] = new Complex();
        //    //    }
        //    //    data.CopyTo(Data, data.Length - 512);
        //    //}
        //}


        public FFT(List<Complex> list)
        {
            
            if (list == null)
            {
#if DEBUG
                Console.WriteLine("FFT cstr: list is null");
#endif
                throw new NullReferenceException();
            }
            // Make sure list contains not less than 512 elements
            if (list.Count >= Data.Length)
            {
                for (int i = 0; i < Data.Length; i++)
                {
                    Data[i] = new Complex();
                }
                list.CopyTo(Data, list.Count - Data.Length);
            }
            else
            {
#if DEBUG
                Console.WriteLine("FFT cstr: not enough elements");
#endif
                throw new ArgumentException("Elements less than 512");
            }
        }

        public void Append(Complex com)
        {
        }

        /// <summary>
        /// Forward Complex[] Data
        /// </summary>
        /// <returns></returns>
        public Task<bool> Forward()
        {
            return Task.Run(() =>
            {
#if DEBUG
                System.Console.WriteLine("Forward begins");
#endif
                Fourier.Forward(Data);
#if DEBUG
                System.Console.WriteLine("Forward ends");
#endif
                return true;
            });
        }

    }
}
