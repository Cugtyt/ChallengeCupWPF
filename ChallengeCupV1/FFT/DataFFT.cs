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
        public static Task<Complex[]> Forward(Complex[] input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("DataFFT: Forward() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("DataFFT: Forward()");
            }
            return Task.Run(() =>
            {
#if DEBUG
                Console.WriteLine("DataFFT: Forward() -> Forward begins");
#endif
                Fourier.Forward(input);
#if DEBUG
                Console.WriteLine("DataFFT: Forward() -> Forward ends");
#endif
                return input;
            });
        }
    }
}
