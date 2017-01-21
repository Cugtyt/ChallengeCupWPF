using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
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
        private static string gearLibPath = File.FileUtils.GetRootPath() + @"\View\GearTab\Gears";

        public GearTabContent()
        {
            InitializeComponent();
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            initPage.Visibility = Visibility.Visible;
            setting.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Choose gear to show in GearTabControl
        /// Use reflection to get gear in lib
        /// </summary>
        public void UpdateGear()
        {
            gear.Children.Add(Assembly.GetExecutingAssembly()
                .CreateInstance("ChallengeCupV1.View.GearTab.Gears.Gear" + 
                (int)Enum.Parse(typeof(Gear), SelectedGear.ToString())) as UserControl);
        }

        public void ShowSettingBtn()
        {
            setting.Visibility = Visibility.Visible;
        }
    }

    public enum Gear
    {
        G1 = 1, G2, G3, G4
    }
}
