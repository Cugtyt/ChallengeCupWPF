using ChallengeCupV2.DataSource;
using ChallengeCupV2.DataSource.GearState;
using ChallengeCupV2.FFT;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
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

namespace ChallengeCupV2.View.WaveTab
{
    /// <summary>
    /// TabContent.xaml 的交互逻辑
    /// </summary>
    public partial class WaveTabContent : UserControl
    {
        //public static bool IsDisplaying = false;
        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(50)
        };
        //private FileInfo[] files;
        //private int fileIndex = 0;

        CH selectedCH = CH.CH1;
        Grating selectedGrating = Grating.G1;
        Domain selectedDomain = Domain.Time;


        public WaveTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, this.GetType().Name);
            //var dire = new DirectoryInfo(directoryPath);
            //var dir = new DirectoryInfo(SettingDataContainer.WaveDataDir);
            //files = dir.GetFiles();

            //#if DEBUG
            //            Console.WriteLine("WaveTabContent:WaveTabContent() -> files name list");
            //            //for (int i = 0; i < files.Length; i++)
            //            //{
            //            //    Console.WriteLine(files[i].Name);
            //            //}
            //#endif
            Timer.Tick += new EventHandler(AnimatedPlot);
            //timer.IsEnabled = true;
        }

        private void AnimatedPlot(object sender, EventArgs e)
        {
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            // Check data validation first
            var selectedCH = (int)Enum.Parse(typeof(CH), this.selectedCH.ToString());
            var selectedGrating = (int)Enum.Parse(typeof(Grating), this.selectedGrating.ToString());
            switch (selectedDomain)
            {
                case Domain.Time:
#if DEBUG
                    //Console.WriteLine("WaveTabContent:AnimatedPlot() -> add time domain points");
#endif
                    lock (this)
                    {
                        wavePlot.AddTimePoints(GratingDataContainer.Data[selectedCH][selectedGrating]);
                    }
                    break;
                case Domain.Frequency:
#if DEBUG
                    Console.WriteLine("WaveTabContent:AnimatedPlot() -> add frequency domain points");
#endif
                    //await wavePlot.AddFreqPoints(DataFFT.Forward(yListArray[selected].ToComplex()).Result);
                    //wavePlot.AddFreqPoints(DataFFT.ForwardAsync(
                    //    (from y in GratingDataContainer.Data[selected]
                    //     select new Complex(y, 0))
                    //     .ToArray())
                    //    .Result);
                    if (StateCalculator.FFTResults[selectedCH].Count == 0)
                    {
                        return;
                    }
                    lock (this)
                    {
                        wavePlot.AddFreqPoints(StateCalculator.FFTResults[selectedCH][selectedGrating]);
                    }
                    break;
                default:
                    break;
            }
        }

        #region Grating selected

        private void g1_Selected(object sender, RoutedEventArgs e)
        {
            selectedGrating = Grating.G1;
        }

        private void g2_Selected(object sender, RoutedEventArgs e)
        {
            selectedGrating = Grating.G2;
        }

        private void g3_Selected(object sender, RoutedEventArgs e)
        {
            selectedGrating = Grating.G3;
        }

        private void g4_Selected(object sender, RoutedEventArgs e)
        {
            selectedGrating = Grating.G4;
        }
        #endregion
        #region Time or Freq selected

        private void time_Selected(object sender, RoutedEventArgs e)
        {
            selectedDomain = Domain.Time;
            if (wavePlot == null)
            {
                return;
            }
            wavePlot.SetYTitle("WaveLength");
        }

        private void freq_Selected(object sender, RoutedEventArgs e)
        {
            selectedDomain = Domain.Frequency;
            if (wavePlot == null)
            {
                return;
            }
            wavePlot.SetYTitle("");
        }
        #endregion
        #region CH selected

        private void ch1_Selected(object sender, RoutedEventArgs e)
        {
            selectedCH = CH.CH1;
        }

        private void ch2_Selected(object sender, RoutedEventArgs e)
        {
            selectedCH = CH.CH2;
        }

         private void ch3_Selected(object sender, RoutedEventArgs e)
        {
            selectedCH = CH.CH3;
        }

        private void ch4_Selected(object sender, RoutedEventArgs e)
        {
            selectedCH = CH.CH4;
        }
        #endregion
    }
}

enum CH
{
    CH1, CH2, CH3, CH4
}

enum Grating
{
    G1, G2, G3, G4
}

enum Domain
{
    Time, Frequency
}