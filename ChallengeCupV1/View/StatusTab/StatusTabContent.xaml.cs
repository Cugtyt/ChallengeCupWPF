using ChallengeCupV1.DataSource;
using ChallengeCupV1.DataSource.GearStatus;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace ChallengeCupV1.View.StatusTab
{
    /// <summary>
    /// StatusTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class StatusTabContent : UserControl
    {
        //public List<GearStatusData> ItemsSource = new List<GearStatusData>();
        //{
        //    get { return (List<GearStatusData>)GetValue(ItemSourceProperty); }
        //    set { SetValue(ItemSourceProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for ItemSource.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty ItemSourceProperty =
        //    DependencyProperty.Register("ItemSource", typeof(List<GearStatusData>), typeof(StatusTabContent));

        public List<StatusData> statusData;


        public StatusTabContent()
        {
            InitializeComponent();
            initStatusData();
            //ItemsSource.Add(new GearStatusData());
            //ItemsSource.Add(new GearStatusData());
            //dataGrid.ItemsSource = ItemsSource;
            //ItemsSource[0].Frequency = 5.0;
            dataGrid.ItemsSource = statusData;
        }

        private void initStatusData()
        {
            statusData = new List<StatusData>();
            statusData.Add(new StatusData("Stress", StatusCalculater.StressCalculater));
            statusData.Add(new StatusData("Strain", StatusCalculater.StressCalculater));
            statusData.Add(new StatusData("Temperature", StatusCalculater.TemperatureCalculater));
            statusData.Add(new StatusData("Frequency", StatusCalculater.FrequencyCalculater));
        }

        private void generateReport_Click(object sender, RoutedEventArgs e)
        {
           
        }
    }
}
