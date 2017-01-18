using ChallengeCupV1.DataSource;
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
        public StatusTabContent()
        {
            InitializeComponent();
            var itemsource = new List<GearStatusData>();
            itemsource.Add(new GearStatusData());
            itemsource.Add(new GearStatusData());
            dataGrid.ItemsSource = itemsource;
        }
    }
}
