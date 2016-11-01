using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace ChallengeCupWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableDataSource<Point>[] dataSource = new ObservableDataSource<Point>[8];
        private DispatcherTimer timer = new DispatcherTimer();
        // Enable TCPRead if ConnectTCP menu clicked
        private bool enableTCPRead = false;
        // X dimension value
        private int x = 0;
        // Token to cancel
        private CancellationTokenSource cts = new CancellationTokenSource();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataSource[i] = new ObservableDataSource<Point>();
                plotter.AddLineGraph(dataSource[i], Colors.Green, 2, " ");
            }
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.IsEnabled = true;
            plotter.Viewport.FitToView();
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            if (enableTCPRead && TCPRead.TCPRead.isConnected)
            {
                dataSource[0].AppendAsync(Dispatcher, new Point(x++, TCPRead.TCPRead.data));
                //dataSource[0].AppendAsync(Dispatcher, new Point(x++, TCPRead.TCPRead.data[10]));
                //Task.Run(AddTCPDataToDataSource, cts.Token);
            }
        }

        /// <summary>
        /// Add tcp data to dataSource
        /// </summary>
        //private Task AddTCPDataToDataSource()
        //{
            //            for (int i = 0; i < TCPRead.TCPRead.data.Length; i++)
            //            {
            //                dataSource[0].AppendAsync(Dispatcher, new Point(x++, TCPRead.TCPRead.data[i]));
            //#if DEBUG
            //                Console.WriteLine("dataSource[0] append " + TCPRead.TCPRead.data[i]);
            //#endif
            //            }
            //dataSource[0].AppendAsync(Dispatcher, new Point(x++, TCPRead.TCPRead.data[10]));
            //return null;
        //}

        /// <summary>
        /// Open file and read data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (enableTCPRead)
            {
                enableTCPRead = false;
            }
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "txt file|*.txt";
            if (openFile.ShowDialog() == true)
            {
#if DEBUG
                Console.WriteLine("open file: " + openFile.FileName);
#endif
                ClearDataSourceAll();
                // Read form file
                List<float>[] dataList = await FileUtils.ReadDataAsync(openFile.FileName);

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

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Get data to write
            //await FileUtils.WriteData(dataList, @"C:\Users\Daniel\Desktop\write.txt");
        }

        private void ConnectTCP_Click(object sender, RoutedEventArgs e)
        {
            enableTCPRead = !enableTCPRead;
            if (enableTCPRead)
            {
                x = 0;
                ClearDataSourceAll();
                Task.Run(TCPRead.TCPRead.Read, cts.Token);
            }
            else
            {
                cts?.Cancel();
                TCPRead.TCPRead.isConnected = false;
            }
        }

        /// <summary>
        /// Clear dataSource[index]
        /// </summary>
        /// <param name="index"></param>
        private void ClearDataSource(int index)
        {
            if (dataSource[index].Collection != null)
            {
                dataSource[index].Collection.Clear();
            }
        }

        /// <summary>
        /// Clear dataSource array
        /// </summary>
        private void ClearDataSourceAll()
        {
            for (int i = 0; i < dataSource.Length; i++)
            {
                ClearDataSource(i);
            }
        }
    }
}
