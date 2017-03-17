using ChallengeCupV2.DataSource;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ChallengeCupV2.View.ModelTab
{
    /// <summary>
    /// ParamDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class ParamDisplay : UserControl
    {
        /// <summary>
        /// Grid items source in which contains sampled wave length
        /// 
        /// As a set type like list etc. wants to be binded to view as data source, should be a 
        /// ObservableCollection or others more than row type, for example, list is not working
        /// fine, System.InvalidOperationException thrown, except the element of list be dependency
        /// object type, which means double should not be used directly, create a class like 
        /// XX{ propdp to create a dependency property to store double data }, it's work too.
        /// </summary>
        private ObservableCollection<double> waveLengthSource = new ObservableCollection<double>();
        
        /// <summary>
        /// Timer for update datagrid items source
        /// </summary>
        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(2),
        };
        public ParamDisplay()
        {
            InitializeComponent();
            waveLengthSource.Add(0.0);
            waveLength.ItemsSource = waveLengthSource;
            Timer.Tick += new EventHandler(updataSource);
            //Timer.IsEnabled = true;
        }

        /// <summary>
        /// Update items source of data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void updataSource(object sender, EventArgs e)
        {
#if DEBUG
            Console.WriteLine("this is updateSource");
#endif
            // Check data validation first
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            //waveLengthSource.Clear();
            //for (int i = 0; i < GratingDataContainer.Data.Length 
            //    && i < (UserControlManager.Get("InitPage") as InitPage).GetGratingNumber(); i++)
            //{
            //    double temp = 0;
            //    for (int j = 0; j < GratingDataContainer.Data[i].Count; j += samplingStep)
            //    {
            //        temp += GratingDataContainer.Data[i][j];
            //    }
            //    waveLengthSource.Add(temp * samplingStep / GratingDataContainer.Data[i].Count);
            //}
            //waveLengthSource.Add(0.0);
            Type modelType = (UserControlManager.Get("ModelTabContent") as ModelTabContent).Model.GetType();
            int ch = modelType.Equals(typeof(Models.Gear))
                ? 0 : modelType.Equals(typeof(Models.Bearing))
                ? 1 : modelType.Equals(typeof(Models.Shaft))
                ? 2 : 0;
            waveLengthSource.Clear();
            var dataClone = GratingDataContainer.Data;
            switch (ch)
            {
                case 0:
                    addAverage(dataClone, 0);
                    addAverage(dataClone, 1);
                    break;
                case 1:
                    addAverage(dataClone, 2);
                    break;
                case 2:
                    addAverage(dataClone, 3);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Add average wavelength to waveLengthSource
        /// </summary>
        /// <param name="dataClone"></param>
        /// <param name="ch"></param>
        private void addAverage(List<double>[][] dataClone, int ch)
        {
            foreach (var li in dataClone[ch])
            {
                if (li.Count > 0)
                {
                    waveLengthSource.Add(li.Average());
                }
            }
        }

        /// <summary>
        /// Auto increase row number when a new row added in datagrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void waveLength_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }

}
