using ChallengeCupV1.DataSource;
using ChallengeCupV1.DataSource.FFT;
using System;
using System.Collections.Generic;
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

namespace ChallengeCupV1.View.WaveTab
{
    /// <summary>
    /// TabContent.xaml 的交互逻辑
    /// </summary>
    public partial class WaveTabContent : UserControl
    {
        CH selectedCH = CH.CH1;
        Grating selectedGrating = Grating.G1;
        Domain selectedDomain = Domain.Time;

        public WaveTabContent()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Add double list to dataSource
        /// </summary>
        /// <param name="yList"></param>
        /// <returns></returns>
        public async Task AddPoints(List<double> yList)
        {
            await wavePlot.AddPoints(yList);
        }

        /// <summary>
        /// Add double list array to dataSource
        /// added index of array is determined by value of selectedCH
        /// </summary>
        /// <param name="yListArray"></param>
        /// <returns></returns>
        public async Task AddPoints(List<double>[] yListArray)
        {
            var selected = (int)Enum.Parse(typeof(CH), selectedCH.ToString());
            switch (selectedDomain)
            {
                case Domain.Time:
                    await wavePlot.AddPoints(yListArray[selected]);
                    break;
                case Domain.Frequency:
                    await wavePlot.AddPoints(
                        DataFFT.Forward(yListArray[selected].ToComplex()).Result
                        .ToDoubleList());
                    break;
                default:
                    break;
            }
            
        }
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