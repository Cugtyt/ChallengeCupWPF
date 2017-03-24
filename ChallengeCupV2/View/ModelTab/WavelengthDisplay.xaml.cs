using ChallengeCupV2.DataSource;
using ChallengeCupV2.DataSource.GearState;
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
    /// WavelengthDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class WavelengthDisplay : UserControl
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
        public WavelengthDisplay()
        {
            InitializeComponent();
            //waveLengthSource.Add(0.0);
            waveLength.ItemsSource = waveLengthSource;
            Timer.Tick += new EventHandler(UpdataSource);
            //Timer.IsEnabled = true;
        }

        /// <summary>
        /// Update items source of data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdataSource(object sender, EventArgs e)
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
            //Type modelType = (UserControlManager.Get("ModelTabContent") as ModelTabContent).Model.GetType();
            //int ch = modelType.Equals(typeof(Models.Gear))
            //    ? 0 : modelType.Equals(typeof(Models.Bearing))
            //    ? 1 : modelType.Equals(typeof(Models.Shaft))
            //    ? 2 : 0;
            var model = (UserControlManager.Get("ModelTabContent") as ModelTabContent).Model;
            int ch = model is Models.Gear
                ? 1 : model is Models.Bearing
                ? 2 : model is Models.Shaft
                ? 3 : 0;
            waveLengthSource.Clear();
            switch (ch)
            {
                // For gear average wavelength
                case 1:
                    waveLengthSource.Add(StateCalculator.GetAve(ch, 1));
                    waveLengthSource.Add(StateCalculator.GetAve(ch, 2));
                    break;
                // For shaft average wavelength
                case 2:
                    waveLengthSource.Add(StateCalculator.GetAve(ch, 1));
                    break;
                // For bearing average wavelength
                case 3:
                    waveLengthSource.Add(StateCalculator.GetAve(ch, 1));
                    break;
                default:
                    break;
            }
            //var dataClone = GratingDataContainer.Data;
            //List<double> temp = new List<double>();
            //switch (ch)
            //{
            //    case 0:
            //        addAverage(dataClone, 0, temp);
            //        addAverage(dataClone, 1, temp);
            //        break;
            //    case 1:
            //        addAverage(dataClone, 2, temp);
            //        break;
            //    case 2:
            //        addAverage(dataClone, 3, temp);
            //        break;
            //    default:
            //        break;
            //}
            //waveLength.ItemsSource = new ObservableCollection<double>(temp);
        }

        /// <summary>
        /// Add average wavelength to waveLengthSource
        /// </summary>
        /// <param name="dataClone"></param>
        /// <param name="ch"></param>
        //private void addAverage(List<double>[][] dataClone, int ch, List<double> temp)
        //{
        //    //foreach (var li in dataClone[ch])
        //    //{
        //    //    if (li.Count > 0)
        //    //    {
        //    //        temp.Add(li.Average());
        //    //    }
        //    //}
        //    for (int i = 0; i < dataClone[ch].Length; i++)
        //    {
        //        if (dataClone[ch][i].Count > 0)
        //        {
        //            temp.Add(dataClone[ch][i].Average());
        //        }
        //    }
        //}

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
