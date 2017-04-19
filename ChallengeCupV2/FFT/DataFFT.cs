using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.IntegralTransforms;

namespace ChallengeCupV2.FFT
{
    public class DataFFT
    {

        private static Complex[] temp;
        /// <summary>
        /// Forward Complex[] data
        /// </summary>
        /// <returns></returns>
//        public static Task<Complex[]> ForwardAsync(Complex[] input)
//        {
//            if (input == null)
//            {
//#if DEBUG
//                Console.WriteLine("DataFFT: Forward() -> Illegal input, argument can not be null.");
//#endif
//                throw new ArgumentNullException("DataFFT: Forward()");
//            }
//            return Task.Run(() =>
//            {
//#if DEBUG
//                Console.WriteLine("DataFFT: Forward() -> Forward begins");
//#endif
//                Fourier.Forward(input);
//#if DEBUG
//                Console.WriteLine("DataFFT: Forward() -> Forward ends");
//#endif
//                return input;
//            });
//        }

        public static void Forward(Complex[] input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("DataFFT: Forward() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("DataFFT: Forward()");
            }
#if DEBUG
            Console.WriteLine("DataFFT: Forward() -> Forward begins");
#endif
            Fourier.Forward(input);
#if DEBUG
            Console.WriteLine("DataFFT: Forward() -> Forward ends");
#endif
        }

        public static Complex[] Forward(List<double> input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("DataFFT: Forward() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("DataFFT: Forward()");
            }
#if DEBUG
            Console.WriteLine("DataFFT: Forward() -> Forward begins");
#endif
            temp = new Complex[input.Count];
            for (int i = 0; i < input.Count; i++)
            {
                temp[i] = new Complex(input[i], 0);
            }
            Fourier.Forward(temp);
#if DEBUG
            Console.WriteLine("DataFFT: Forward() -> Forward ends");
#endif
            return temp;
        }
    }
}
