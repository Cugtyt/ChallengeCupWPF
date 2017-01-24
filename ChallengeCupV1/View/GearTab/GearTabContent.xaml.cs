using ChallengeCupV1.GearLib;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChallengeCupV1.View.GearTab
{
    /// <summary>
    /// GearTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class GearTabContent : UserControl
    { 
        private IGear gear;

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
            gear = GearFactory.GetGear(initPage.GetGearIndex(), initPage.GetGratingNumber());
            gearContainer.Children.Clear();
            gearContainer.Children.Add(gear as UserControl);
        }

        public void ShowSettingBtn()
        {
            setting.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Gear model zoom in and zoom out while mouse wheeling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gearContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            gear.Zoom(e.Delta);
        }

        private void gearContainer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gear.MouseUp();
        }

        private void gearContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                gear.Reset();
            }
            gear.MouseDown(e);
        }

        private void gearContainer_MouseMove(object sender, MouseEventArgs e)
        {
            gear.MouseMove();
        }

        private void gearContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            gear.MouseUp();
        }
    }

}
