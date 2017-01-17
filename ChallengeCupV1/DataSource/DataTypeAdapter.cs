using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource
{
    public static class DataTypeAdapter
    {
        /// <summary>
        /// Convert double list to Complex array
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Complex[] ToComplex(this List<double> data)
        {
            Complex[] com = new Complex[data.Count];
            for (int i = 0; i < data.Count; i++)
            {
                com[i] = new Complex(data[i], 0);
            }
            return com;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<double> ToDoubleList(this Complex[] data)
        {
            List<double> list = new List<double>();
            for (int i = 0; i < data.Length; i++)
            {
                list[i] = data[i].Real;
            }
            return list;
        }
    }
}
