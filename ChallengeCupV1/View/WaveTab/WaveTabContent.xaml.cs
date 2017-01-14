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
    /// TabContent.xaml 的交互逻辑
    /// </summary>
    public partial class WaveTabContent : UserControl
    {
       
        public WaveTabContent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add double list to dataSource
        /// </summary>
        /// <param name="yList"></param>
        /// <returns></returns>
        public async Task AddPoints(List<double> yList)
        {
            await wavePlot.AddPoints(yList);
        }
    }
}
