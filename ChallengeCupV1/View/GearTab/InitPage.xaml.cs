using ChallengeCupV1.DataSource;
using ChallengeCupV1.GearLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
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
    /// InitPage.xaml 的交互逻辑
    /// </summary>
    public partial class InitPage : UserControl
    {
        ObservableCollection<string> gearLibSource = new ObservableCollection<string>();
        ObservableCollection<int> gratingNumberSource = new ObservableCollection<int>() { 1, 2, 3, 4 };

        public InitPage()
        {
            InitializeComponent();
            // Search in GearLib to set items for gearSelectComboBox
            var classes = Reflection.ReflectionUtils.GetClassList("ChallengeCupV1.GearLib", typeof(IGear));
            foreach (var c in classes)
            {
                gearLibSource.Add(c.Name);
            }
            gearSelectComboBox.ItemsSource = gearLibSource;
            gearSelectComboBox.SelectedItem = classes[0];
            // Set grating number for gratingNumberComboBox
            gratingNumberComboBox.ItemsSource = gratingNumberSource;
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            var parent = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(
                VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this)))) as GearTabContent);
            parent.ShowSettingBtn();
            parent.UpdateGear();
        }

        public int GetGearIndex()
        {
            return gearSelectComboBox.SelectedIndex + 1;
        }

        public int GetGratingNumber()
        {
            return gratingNumberComboBox.SelectedIndex + 1;
        }

        //#region Select gear
        //private void gear1_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.SelectedGear = Gear.G1;
        //}

        //private void gear2_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.SelectedGear = Gear.G2;
        //}

        //private void gear3_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.SelectedGear = Gear.G3;
        //}

        //private void gear4_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.SelectedGear = Gear.G4;
        //}
        //#endregion

        //#region Select grating number
        //private void num1_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.GratingNumber = 1;
        //}

        //private void num2_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.GratingNumber = 2;
        //}

        //private void num3_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.GratingNumber = 3;
        //}

        //private void num4_Selected(object sender, RoutedEventArgs e)
        //{
        //    GearTabContent.GratingNumber = 4;
        //}
        //#endregion
    }
}
