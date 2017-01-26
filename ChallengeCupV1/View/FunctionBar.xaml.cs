using ChallengeCupV1.DataSource;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ChallengeCupV1.View
{
    /// <summary>
    /// FunctionColumn.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionBar : UserControl
    {
        /// <summary>
        /// Timer to set gratingdata in GratingDataContainer
        /// </summary>
        private DispatcherTimer timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };

        private FileInfo[] files;
        private int index = 0;

        public FunctionBar()
        {
            InitializeComponent();
            timer.Tick += new EventHandler(ReadDataFromFile);
        }

        private async void ReadDataFromFile(object sender, EventArgs e)
        {
            //-----------------------
            // Remove all files in dir
            //File.FileUtils.RemoveFileAll(files, 0, files.Length - 1);
            //-------------------------
            if (index < files.Length)
            {
                //GratingDataContainer.Data = await File.FileUtils.ReadWaveData(
                //    SettingDataContainer.WaveDataDir + files[index++].Name);
                await GratingDataContainer.GetDataFrom(
                    File.FileUtils.ReadDataFromFile(
                        SettingContainer.WaveDataDir + files[index++].Name).Result);
            }
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            
            // Connect asked
            if ((string)connect.Content == "连接")
            {
                //WaveTab.WaveTabContent.Timer.IsEnabled = true;
                var dir = new DirectoryInfo(SettingContainer.WaveDataDir);
                files = dir.GetFiles();
                timer.IsEnabled = true;
                connect.Content = "断开";
            }
            // Connected cancled
            else
            {
                timer.IsEnabled = false;
                connect.Content = "连接";
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Start asked
            if ("开始" == (string)start.Content)
            {
                if ((string)connect.Content == "连接")
                {
                    start.Content = "请先连接";
                    return;
                }
                WaveTab.WaveTabContent.Timer.IsEnabled = true;
                StatusTab.StatusTabContent.Timer.IsEnabled = true;
                //WaveTab.WaveTabContent.IsDisplaying = true;
                start.Content = "停止";
            }
            // Start cancled
            else
            {
                WaveTab.WaveTabContent.Timer.IsEnabled = false;
                StatusTab.StatusTabContent.Timer.IsEnabled = false;
                //WaveTab.WaveTabContent.IsDisplaying = false;
                start.Content = "开始";
            }
           
        }

        /// <summary>
        /// Close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
