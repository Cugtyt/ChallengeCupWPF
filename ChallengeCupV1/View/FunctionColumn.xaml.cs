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

namespace ChallengeCupV1.View
{
    /// <summary>
    /// FunctionColumn.xaml 的交互逻辑
    /// </summary>
    public partial class FunctionColumn : UserControl
    {
        public FunctionColumn()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            
            // Connect asked
            if ((string)connect.Content == "连接")
            {
                WaveTab.WaveTabContent.Timer.IsEnabled = true;
                connect.Content = "断开";
            }
            // Connected cancled
            else
            {
                WaveTab.WaveTabContent.Timer.IsEnabled = false;
                connect.Content = "连接";
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Start asked
            if ("开始" == (string)start.Content)
            {
                WaveTab.WaveTabContent.IsDisplaying = true;
                start.Content = "停止";
            }
            // Start cancled
            else
            {
                WaveTab.WaveTabContent.IsDisplaying = false;
                start.Content = "开始";
            }
           
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
