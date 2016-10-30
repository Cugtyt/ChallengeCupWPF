using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChallengeCupWPF.DataUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System.Windows;
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