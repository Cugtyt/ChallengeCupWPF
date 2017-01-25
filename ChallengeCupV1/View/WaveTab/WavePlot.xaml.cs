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
        DataSource.WavePoints dataSource = new DataSource.WavePoints();

        public WavePlot()
        {
            InitializeComponent();
            plotter.Children.Remove(plotter.MouseNavigation);
            plotter.Children.Remove(plotter.KeyboardNavigation);
            //DataContext = dataSource;
            line.DataSource = dataSource.Points;
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

        public void SetYTitle(string title)
        {
            if (title == null)
            {
#if DEBUG
                Console.WriteLine("WavePlot: SetYTitle() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("WavePlot: SetYTitle()");
            }
            verticalTitle.Content = title;
        }
    }
}
