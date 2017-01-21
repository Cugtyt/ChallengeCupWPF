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
using ChallengeCupWPF.DataUtils;
using System.Linq;
using System.Net.Sockets;
using System.Windows.Data;
using System.ComponentModel;

namespace ChallengeCupWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private ObservableDataSource<Point>[] dataSource = new ObservableDataSource<Point>[8];
        private DispatcherTimer timer = new DispatcherTimer();
        // If tcp is connected
        private bool enableTCPRead = false;
        // If FFT is working
        private bool enableFFT = false;
        // X dimension value
        private int x = 0;
        // Token to cancel
        private CancellationTokenSource cts;

        private int maxPointsCount = 150;

        //private DataSource ds = new DataSource();
        //private DependencyProperty dp;
        //private BindingBase binding;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set dataSource
            for (int i = 0; i < dataSource.Length; i++)
            {
                dataSource[i] = new ObservableDataSource<Point>();
                plotter.AddLineGraph(dataSource[i], Colors.Green, 2, " ");
            }
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.IsEnabled = true;
            plotter.Viewport.FitToView();
            plotter.AllowDrop = false;
            // Try use binding, not work yet
            //dp = DependencyProperty.Register("Points", typeof(DataSource), typeof(MainWindow));
            //binding = new Binding() { Source = ds, Path = new PropertyPath("Points") };

            //plotter.SetBinding(dp, binding);
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            if (enableTCPRead && TCPRead.TCPRead.IsConnected)
            {
                dataSource[0].AppendAsync(Dispatcher, new Point(x++, TCPRead.TCPRead.Data));
                if (dataSource[0].Collection.Count > maxPointsCount)
                {
                    //dataSource[0].Collection.Remove(dataSource[0].Collection.First());
                    dataSource[0].Collection.RemoveAt(0);
                }
            }
           
            // Not work yet
            // TODO: if enableFFT and data is from file ----do ...
            //       if ebableFFT and data is from tcp ----- append data to fft.data
            //if (enableFFT)
            //{

            //}
        }

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
                Console.WriteLine("MainWindow: OpenFile_Click() -> open file: " + openFile.FileName);
#endif
                dataSource.ClearDataSourceAll();
                
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

        private async void SaveFile_Click(object sender, RoutedEventArgs e)
        {

            // Doesn't Test yet
            // TODO: open SaveFileDialog and add path to write data.
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "txt file|*.txt";
            if (saveFile.ShowDialog() == true)
            {
#if DEBUG
                Console.WriteLine("MainWindow: SaveFile_Click() -> write to file: " + saveFile.FileName);
#endif
                // Not update data from tcp
                enableTCPRead = false;
                //await FileUtils.WriteData(dataSource.ToList().ToFloatList(), saveFile.FileName);
                await FileUtils.WriteData(dataSource.ToFlostListArray(), saveFile.FileName);
            }
        }

        private async void ConnectTCP_Click(object sender, RoutedEventArgs e)
        {
            enableTCPRead = !enableTCPRead;
            if (enableTCPRead)
            {
                try
                {
                    cts = new CancellationTokenSource();
                    await Task.Run(TCPRead.TCPRead.Read, cts.Token);
                }
                catch (Exception ex)
                {
#if DEBUG
                    Console.WriteLine("MainWindow: ConnectTCP_Click() -> " + ex);
#endif
                    cts?.Cancel();
                    TCPRead.TCPRead.IsConnected = false;
                    enableTCPRead = false;
                }
            }
            else
            {
                cts?.Cancel();
                TCPRead.TCPRead.IsConnected = false;
            }
        }

        private async void FFTForward_Click(object sender, RoutedEventArgs e)
        {
            // TODO: make this work
            if (dataSource[0].Collection.Count == 0)
            {
#if DEBUG
                Console.WriteLine("MainWindow: FFTForward_Click -> dataSource[0] is null");
#endif
                MessageBox.Show("No data");
                return;
            }
            FFT fft = null;
            try
            {
                fft = new FFT(dataSource[0].ToComplexList());
                bool finished = await fft.Forward();
                if (finished)
                {
                    dataSource.ClearDataSourceAll();
                    dataSource[0].AppendMany(fft.Data.ToPoints());
                }
            }
            catch (ArgumentException ae)
            {
#if DEBUG
                Console.WriteLine(ae);
#endif
                MessageBox.Show("No enough elements");
            }
        }

        public void Dispose()
        {
            cts.Dispose();
        }
    }

    //class DataSource : INotifyPropertyChanged
    //{
    //    List<Point> points = new List<Point>();

    //    public event PropertyChangedEventHandler PropertyChanged;

    //    public DataSource()
    //    {
    //        points.Add(new Point(1, 2));
    //        points.Add(new Point(3, 4));
    //        points.Capacity = 256;
    //    }

    //    public List<Point> Points
    //    {
    //        get
    //        {
    //            return points;
    //        }
    //        set
    //        {
    //            points = value;
    //            if (PropertyChanged != null)
    //            {
    //                PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Points"));
    //            }
    //        }
    //    }
    //}
}
