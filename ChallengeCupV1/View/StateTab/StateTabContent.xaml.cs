using ChallengeCupV1.DataSource;
using ChallengeCupV1.DataSource.GearState;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Threading;

namespace ChallengeCupV1.View.StateTab
{
    /// <summary>
    /// StatusTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class StateTabContent : UserControl
    {
        /// <summary>
        /// Data source of data grid to show 
        /// </summary>
        private StateDataContainer statusDataSource = new StateDataContainer();
        /// <summary>
        /// Timer to set interval of calculating status data
        /// </summary>
        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(2),
        };

        public StateTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, this.GetType().Name);
            dataGrid.ItemsSource = statusDataSource.StateData;
            Timer.Tick += calculateParam;
            //Timer.IsEnabled = true;
        }

        /// <summary>
        /// Calculate event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateParam(object sender, EventArgs e)
        {
            //statusDataSource.Calculate();
#if DEBUG
            Console.WriteLine("StateTabContent: calculateParam");
#endif
            statusDataSource.Update();
        }

        /// <summary>
        /// Generate status data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateReport_Click(object sender, RoutedEventArgs e)
        {
            File.FileUtils.GenerateStateReportFile(SettingContainer.StatusReportDir, statusDataSource.StateData);
        }
    }
}
