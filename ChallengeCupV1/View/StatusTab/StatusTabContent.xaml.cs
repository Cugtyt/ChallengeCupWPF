using ChallengeCupV1.DataSource;
using ChallengeCupV1.DataSource.GearStatus;
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

namespace ChallengeCupV1.View.StatusTab
{
    /// <summary>
    /// StatusTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class StatusTabContent : UserControl
    {
        /// <summary>
        /// Data source of data grid to show 
        /// </summary>
        private StatusDataContainer statusDataSource = new StatusDataContainer();
        /// <summary>
        /// Timer to set interval of calculating status data
        /// </summary>
        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(2),
        };

        public StatusTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, this.GetType().Name);
            dataGrid.ItemsSource = statusDataSource.StatusData;
            Timer.Tick += new EventHandler(calculateParam);
            //Timer.IsEnabled = true;
        }

        /// <summary>
        /// Calculate event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calculateParam(object sender, EventArgs e)
        {
            statusDataSource.Calculate();
        }

        /// <summary>
        /// Generate status data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateReport_Click(object sender, RoutedEventArgs e)
        {
            File.FileUtils.GenerateStatusReportFile(SettingContainer.StatusReportDir, statusDataSource.StatusData);
        }
    }
}
