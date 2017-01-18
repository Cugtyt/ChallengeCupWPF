using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.IntegralTransforms;

namespace ChallengeCupV1.FFT
{
    public class DataFFT
    {
        /// <summary>
        /// Forward Complex[] data
        /// </summary>
        /// <returns></returns>
        public static Task<Complex[]> Forward(Complex[] data)
        {
#if DEBUG
            Console.WriteLine("DataFFT: Forward()");
#endif
            return Task.Run(() =>
            {
#if DEBUG
                Console.WriteLine("Forward begins");
#endif
                Fourier.Forward(data);
#if DEBUG
                Console.WriteLine("Forward ends");
#endif
                return data;
            });
        }
    }
}
