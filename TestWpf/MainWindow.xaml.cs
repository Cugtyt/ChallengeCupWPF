using DynamicDataDisplay.Markers.DataSources;
using System;
using System.Collections.Generic;
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
using System.Collections;

namespace TestWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            line.DataSource = new DataSource();
        }
    }

    public class DataSource : PointDataSourceBase
    {
        protected override IEnumerable GetDataCore(DataSourceEnvironment environment)
        {
            return new List<Point>() { new Point(0, 0), new Point(10, 100) };

        }
    }
}
