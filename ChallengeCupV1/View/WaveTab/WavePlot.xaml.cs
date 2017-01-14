using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChallengeCupV1.View.WaveTab
{
    /// <summary>
    /// WavePlot.xaml 的交互逻辑
    /// </summary>
    public partial class WavePlot : UserControl
    {
        DataSource.Data dataSource = new DataSource.Data();
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
        public void change()
        {
#if DEBUG
            Console.WriteLine("Wave: change()");
#endif
            dataSource.Add(50);
            dataSource.Add(80);
            dataSource.Add(100);
            dataSource.Update();
        }

        /// <summary>
        /// Add double list to dataSource
        /// </summary>
        /// <param name="yList"></param>
        /// <returns></returns>
        public async Task AddPoints(List<double> yList)
        {
            await dataSource.Add(yList);
        }
    }
}
