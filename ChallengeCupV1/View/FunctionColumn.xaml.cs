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
                connect.Content = "断开";
            }
            // Connected cancled
            else
            {
                connect.Content = "连接";
            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            // Start asked
            if ("开始" == (string)start.Content)
            {
                start.Content = "停止";
                WaveTab.WaveTabContent.Timer.IsEnabled = true;
            }
            // Start cancled
            else
            {
                start.Content = "停止";
                WaveTab.WaveTabContent.Timer.IsEnabled = false;
            }
           
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }

}
