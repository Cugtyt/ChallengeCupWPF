using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Numerics;
using MathNet.Numerics.IntegralTransforms;

namespace ChallengeCupWPF.DataUtils.Tests
{
    [TestClass()]
    public class FFTTests
    {
        [TestMethod()]
        public void FFTForwardTest()
        {
            Complex[] c = new Complex[100];
            for (int i = 0; i < c.Length; i++)
            {
                c[i] = new Complex(Math.Sin((double)i / 3), 0);
            }
            TestFFT(c);
            //TODO add test case
        }

        private void TestFFT(Complex[] c)
        {
            Fourier.Forward(c);
            for (int i = 0; i < c.Length; i++)
            {
                Console.WriteLine(c[i].Real);
            }
        }
    }
}