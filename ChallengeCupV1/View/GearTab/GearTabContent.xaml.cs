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

namespace ChallengeCupV1.View.GearTab
{
    /// <summary>
    /// GearTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class GearTabContent : UserControl
    {
        Gear selectedGear;

        public GearTabContent()
        {
            InitializeComponent();
        }

        private void gear1_Selected(object sender, RoutedEventArgs e)
        {
            selectedGear = Gear.G1;
        }

        private void gear2_Selected(object sender, RoutedEventArgs e)
        {
            selectedGear = Gear.G2;
        }

        private void gear3_Selected(object sender, RoutedEventArgs e)
        {
            selectedGear = Gear.G3;
        }

        private void gear4_Selected(object sender, RoutedEventArgs e)
        {
            selectedGear = Gear.G4;
        }

    }

    enum Gear
    {
        G1, G2, G3, G4
    }
}
