using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ChallengeCupV1.View.WaveTab
{
    /// <summary>
    /// WavePlot.xaml 的交互逻辑
    /// </summary>
    public partial class WavePlot : UserControl
    {
        DataSource.WaveData dataSource = new DataSource.WaveData();

        public WavePlot()
        {
#if DEBUG
            Console.WriteLine("Wave: Wave()");
#endif
            InitializeComponent();
            DataContext = dataSource;
        }

        /// <summary>
        /// Test OK
        /// add test data to dataSource, it can display
        /// time: 2017年1月10日17:16:41
        /// </summary>
//        public void change()
//        {
//#if DEBUG
//            Console.WriteLine("Wave: change()");
//#endif
//            dataSource.Add(50);
//            dataSource.Add(80);
//            dataSource.Add(100);
//            dataSource.Update();
//        }

        /// <summary>
        /// Add double list to dataSource
        /// </summary>
        /// <param name="yList"></param>
        /// <returns></returns>
        public async Task AddTimePoints(List<double> yList)
        {
            await dataSource.Add(yList);
        }
        
        public Task AddFreqPoints(Complex[] com)
        {
            // test begin
            //Complex[] test = new Complex[200];
            //for (int i = 0; i < test.Length; i++)
            //{
            //    test[i] = new Complex(Math.Sin(i), 0);
            //}
            //dataSource.FromComplexArray(DataFFT.Forward(test).Result);
            // test end
            dataSource.FromComplexArray(com);
            return null;
        }
    }
}
