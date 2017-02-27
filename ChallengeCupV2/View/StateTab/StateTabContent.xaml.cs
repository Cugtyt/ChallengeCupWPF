using ChallengeCupV2.DataSource;
using ChallengeCupV2.DataSource.GearState;
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

namespace ChallengeCupV2.View.StateTab
{
    /// <summary>
    /// StateTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class StateTabContent : UserControl
    {
        /// <summary>
        /// Data source of data grid to show 
        /// </summary>
        private StateDataContainer stateDataSource = new StateDataContainer();
        /// <summary>
        /// Timer to set interval of calculating state data
        /// </summary>
        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(2),
        };

        public StateTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, this.GetType().Name);
            dataGrid.ItemsSource = stateDataSource.StateData;
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
            //stateDataSource.Calculate();
#if DEBUG
            Console.WriteLine("StateTabContent: calculateParam");
#endif
            stateDataSource.Update();
        }

        /// <summary>
        /// Generate state data file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void generateReport_Click(object sender, RoutedEventArgs e)
        {
            File.FileUtils.GenerateStateReportFile(SettingContainer.StateReportDir, stateDataSource.StateData);
        }
    }
}
