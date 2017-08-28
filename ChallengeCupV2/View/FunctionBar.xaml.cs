using ChallengeCupV2.DataSource;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace ChallengeCupV2.View
{
    /// <summary>
    /// FunctionColumn.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionBar : UserControl, IDisposable
    {
        /// <summary>
        /// Timer to set gratingdata in GratingDataContainer
        /// </summary>
        //private DispatcherTimer timer = new DispatcherTimer()
        //{
        //    Interval = TimeSpan.FromMilliseconds(50)
        //};

        //private FileInfo[] files;
        //private DirectoryInfo dir = new DirectoryInfo(SettingContainer.WaveDataDir);
        //private int index = 0;

        private Task udpTask;
        private CancellationTokenSource cts;
        private UDP.UDPRead udp = new UDP.UDPRead();
        public Button startBtn;
        //private UDP.UDPRead udp;


        public FunctionBar()
        {
            InitializeComponent();
            UserControlManager.Register(this, GetType().Name);
            startBtn = start;
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            //udp = new UDP.UDPRead();

            // Connect asked
            if ((string)connect.Content == "Connect")
            {
                cts = new CancellationTokenSource();
                udpTask = new Task(udp.Receive, cts.Token);
                udpTask.Start();
                connect.Content = "Disconnect";
            }
            // Connected cancled
            else
            {
                if ("Stop" == (string)start.Content)
                {
                    connect.Content = "Stop First";
                    return;
                }
                // Cancel UDP task
                cts.Cancel();
                //udp.Dispose();
                connect.Content = "Connect";
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Start asked
            if ("Start" == (string)start.Content)
            {
                if ((string)connect.Content == "Connect")
                {
                    start.Content = "Connect First";
                    return;
                }
                SetTimers(true);
                start.Content = "Stop";
            }
            // Start cancled
            else
            {
                SetTimers(false);
                start.Content = "Start";
            }
           
        }

        private void SetTimers(bool isEnabled)
        {
            WaveTab.WaveTabContent.Timer.IsEnabled
                = StateTab.StateTabContent.Timer.IsEnabled
                = ModelTab.WavelengthDisplay.Timer.IsEnabled
                = ModelTab.ModelTabContent.AutoRotationTimer.IsEnabled
                = isEnabled;
        }

        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            cts?.Cancel();
            SetTimers(false);
            udp.Dispose();
            //Dispose();
            Application.Current.Shutdown();
        }

        public void Dispose()
        {
            ((IDisposable)cts).Dispose();
        }
    }

}
