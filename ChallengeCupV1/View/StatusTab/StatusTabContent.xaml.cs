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
        private StatusDataContainer statusDataSource = new StatusDataContainer();
        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(2),
        };

        public StatusTabContent()
        {
            InitializeComponent();
            dataGrid.ItemsSource = statusDataSource.StatusData;
            Timer.Tick += new EventHandler(calculateParam);
            //Timer.IsEnabled = true;
        }

        private void calculateParam(object sender, EventArgs e)
        {
            statusDataSource.Calculate();
        }

        private void generateReport_Click(object sender, RoutedEventArgs e)
        {
            File.FileUtils.GenerateStatusReportFile(SettingDataContainer.StatusReportDir, statusDataSource.StatusData);
        }
    }
}
