using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ChallengeCupWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableDataSource<Point>[] dataSource = new ObservableDataSource<Point>[8];
        //private PerformanceCounter cpuPerformance = new PerformanceCounter();
        private DispatcherTimer timer = new DispatcherTimer();
        private List<float>[] dataList;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            //cpuPerformance.CategoryName = "Processor";
            //cpuPerformance.CounterName = "% Processor Time";
            //cpuPerformance.InstanceName = "_Total";

            //double x = i;
            //double y = cpuPerformance.NextValue();

            //Point point = new Point(x, y);
            //dataSource.AppendAsync(base.Dispatcher, point);
            //cpuUsageText.Text = String.Format("{0:0}%", y);
            //i++;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataSource[i] = new ObservableDataSource<Point>();
                plotter.AddLineGraph(dataSource[i], Colors.Green, 2, " ");
            }
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.IsEnabled = true;
            plotter.Viewport.FitToView();
        }

        /// <summary>
        /// Open file and read data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt file|*.txt";
            if (openFile.ShowDialog() == true)
            {
#if DEBUG
                Console.WriteLine("open file: " + openFile.FileName);
#endif
                // Read form file
                dataList = await FileUtils.ReadDataAsync(openFile.FileName);
                // Add data to dataSource
                for (int i = 0; i < dataList.Length; i++)
                {
                    for (int j = 0; j < dataList[i].Count; j++)
                    {
                        dataSource[i].AppendAsync(base.Dispatcher, new Point(j, dataList[i][j]));
                    }
                }
            }
        }

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            await FileUtils.WriteData(dataList, "c:\\");
        }
    }
}
