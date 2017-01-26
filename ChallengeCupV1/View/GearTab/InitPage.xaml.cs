using ChallengeCupV1.DataSource;
using ChallengeCupV1.GearLib;
using ChallengeCupV1.Reflection;
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
        /// <summary>
        /// ComboBox items source, name collection of gear in gear lib
        /// </summary>
        ObservableCollection<string> gearLibSource = new ObservableCollection<string>();
        /// <summary>
        /// ComboBox items source, collection of grating number
        /// </summary>
        ObservableCollection<int> gratingNumberSource = new ObservableCollection<int>() { 0, 1, 2, 3, 4 };

        public InitPage()
        {
            InitializeComponent();

            #region Init ComboBox
            // Search in GearLib to set items for gearSelectComboBox
            var classes = ReflectionUtils.IsNotInterfaceFilter(ReflectionUtils.IsInterfaceFilter(
                ReflectionUtils.GetClassList("ChallengeCupV1.GearLib"), typeof(IGear)), typeof(IGrating));

            foreach (var c in classes)
            {
                gearLibSource.Add(c.Name);
            }
            gearSelectComboBox.ItemsSource = gearLibSource;
            gearSelectComboBox.SelectedIndex = 0;
            // Set grating number for gratingNumberComboBox
            gratingNumberComboBox.ItemsSource = gratingNumberSource;
            gratingNumberComboBox.SelectedIndex = 0;
            #endregion
        }

        /// <summary>
        /// Apply all setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_Click(object sender, RoutedEventArgs e)
        {
            Visibility = Visibility.Hidden;
            var parent = (VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(
                VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this)))) as GearTabContent);
            parent.ShowHidden();
            parent.UpdateGear();
        }

        public int GetGearIndex()
        {
            // Indexs of gear in lib start from 1
            return gearSelectComboBox.SelectedIndex + 1;
        }

        public int GetGratingNumber()
        {
            return gratingNumberComboBox.SelectedIndex;
        }
    }
}
