using Microsoft.Research.DynamicDataDisplay;
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

namespace ChallengeCupV1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer = new DispatcherTimer();
        string directoryPath = @"C:\debug\FBG解调系统数据文件\数据文件\2017-01-11\temp\";
        FileInfo[] files;
        static int fileIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            // Test read data from file
            // test passed
            //var data = File.FileUtils
            //    .ReadDataAsync(@"C:\debug\FBG解调系统数据文件\数据文件\2017-01-11\temp\PeaksAll 2017-01-11 15-46-17 02-Wednesday.txt")
            //    .Result;
            //wave1.AddPoints(data[0]);
            //wave2.AddPoints(data[1]);
            //wave3.AddPoints(data[2]);
            //wave4.AddPoints(data[3]);
            var dire = new DirectoryInfo(directoryPath);
            files = dire.GetFiles();
#if DEBUG
            Console.WriteLine("files name list");
            //for (int i = 0; i < files.Length; i++)
            //{
            //    Console.WriteLine(files[i].Name);
            //}
#endif
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += new EventHandler(AnimatedPlot);
            timer.IsEnabled = true;
        }

        private async void AnimatedPlot(object sender, EventArgs e)
        {
            if (fileIndex < files.Length)
            {
                var data = await File.FileUtils
                .ReadDataAsync(directoryPath + files[fileIndex++].Name);
                //wave1.AddPoints(data[0]);
                //wave2.AddPoints(data[1]);
                //wave3.AddPoints(data[2]);
                //wave4.AddPoints(data[3]);
                //wave.AddPoints(data[0]);
                //((View.WaveTab.TabContent)wave.Content).wavePlot.AddPoints(data[0]);
                //wave.wavePlot.AddPoints(data[0]);
                wave.AddPoints(data[0]);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Set window size max
            WindowState = WindowState.Normal;
            WindowStyle = WindowStyle.None;
            ResizeMode = ResizeMode.NoResize;
            Topmost = true;

            Left = 0.0;
            Top = 0.0;
            Width = SystemParameters.PrimaryScreenWidth;
            Height = SystemParameters.PrimaryScreenHeight;
        }
    }
}
