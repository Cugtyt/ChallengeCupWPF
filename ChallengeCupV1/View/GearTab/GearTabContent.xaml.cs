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
        public static Gear SelectedGear = Gear.G1;
        public static int GratingNumber = 1;
        private static string gearLibPath = @"\View\GearTab\Gears";

        public GearTabContent()
        {
            InitializeComponent();
            gear3D.Children.Add(new Gears.Gear1());
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            initPage.Visibility = Visibility.Visible;
            setting.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Choose gear to show in GearTabControl
        /// </summary>
        public void UpdateGear()
        {
            // TODO: update gear dynamicly
            int index = (int)Enum.Parse(typeof(Gear), SelectedGear.ToString());
            //File.FileUtils.ReadGearLib()
            
        }

        public void ShowSettingBtn()
        {
            setting.Visibility = Visibility.Visible;
        }
    }

    public enum Gear
    {
        G1, G2, G3, G4
    }
}
