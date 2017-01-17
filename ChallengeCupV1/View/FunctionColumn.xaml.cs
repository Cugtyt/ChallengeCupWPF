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
        bool ConnectBtnPressed = false;
        bool StartBtnPressed = false;

        public FunctionColumn()
        {
            InitializeComponent();
        }

        private void connect_Click(object sender, RoutedEventArgs e)
        {
            ConnectBtnPressed = !ConnectBtnPressed;
            // Connect asked
            if (ConnectBtnPressed)
            {

            }
            // Connected cancled
            else
            {

            }
        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            StartBtnPressed = !StartBtnPressed;
            // Start asked
            if (StartBtnPressed)
            {

            }
            // Start cancled
            else
            {

            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
