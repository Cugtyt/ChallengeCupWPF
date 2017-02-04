using ChallengeCupV1.DataSource;
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

namespace ChallengeCupV1.View.GearTab
{
    /// <summary>
    /// ParamDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class ParamDisplay : UserControl
    {
        private int samplingStep = 100;
        //private List<WaveLength> waveLengthSource = new List<WaveLength>();


        //public List<double> waveLengthSource
        //{
        //    get { return (List<double>)GetValue(waveLengthSourceProperty); }
        //    set { SetValue(waveLengthSourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for waveLengthSource.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty waveLengthSourceProperty =
        //    DependencyProperty.Register("waveLengthSource", typeof(List<double>), typeof(ParamDisplay));
        private ObservableCollection<double> waveLengthSource = new ObservableCollection<double>();


        public static DispatcherTimer Timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(2),
        };
        public ParamDisplay()
        {
            InitializeComponent();
            //waveLengthSource = new List<double>();
            waveLengthSource.Add(0.0);
            waveLength.ItemsSource = waveLengthSource;
            //waveLengthSource.Add(new WaveLength() { ID = 1, Value = 100 });
            //waveLengthSource.Add(new WaveLength() { ID = 2, Value = 100 });
            //waveLengthSource.Add(new WaveLength() { ID = 3, Value = 100 });
            //waveLengthSource[2].Value = 200;
            Timer.Tick += new EventHandler(updataSource);
            //Timer.IsEnabled = true;
        }

        private void updataSource(object sender, EventArgs e)
        {
            Console.WriteLine("this is updateSource");
            waveLengthSource.Clear();
            for (int i = 1; i < GratingDataContainer.Data.Length; i++)
            {
                double temp = 0;
                for (int j = 0; j < GratingDataContainer.Data[i].Count; j += samplingStep)
                {
                    temp += GratingDataContainer.Data[i][j];
                }
                waveLengthSource.Add(temp * samplingStep / GratingDataContainer.Data[i].Count);
            }
            //waveLengthSource.Add(0.0);
        }

        private void waveLength_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }

    class WaveLength : DependencyObject
    {
        //public int ID
        //{
        //    get { return (int)GetValue(IDProperty); }
        //    set { SetValue(IDProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ID.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty IDProperty =
        //    DependencyProperty.Register("ID", typeof(int), typeof(WaveLength), new PropertyMetadata(0));



        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(WaveLength), new PropertyMetadata(0.0));


    }
}
