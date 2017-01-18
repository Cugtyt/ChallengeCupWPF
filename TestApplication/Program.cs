using MathNet.Numerics.IntegralTransforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder("before");
            Complex[] com = new Complex[100];
            for (int i = 0; i < com.Length; i++)
            {
                com[i] = new Complex(Math.Sin(i / 20.0), 0);
                sb.Append(com[i].Real + "\t\t");
            }
            sb.Append("\nafter");
            Fourier.Forward(com);
            for (int i = 0; i < com.Length; i++)
            {
                sb.Append(com[i].Real + "\t" + com[i].Imaginary + "\t");
            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\Daniel\Desktop\write.txt"))
            {
                // write number of chanels to file
                //writer.WriteLine(data.Length);
                // write data
                //for (int i = 0; i < data[0].Count; i++)
                //{
                //    foreach (var item in data)
                //    {
                //        writer.Write(item[i] + "\t");
                //    }
                //    writer.WriteLine();
                //}
                writer.Write(sb);
            }
        }
    }
}
