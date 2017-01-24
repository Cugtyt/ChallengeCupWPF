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
            dataSource.FromComplexArray(com);
            return null;
        }
    }
}
