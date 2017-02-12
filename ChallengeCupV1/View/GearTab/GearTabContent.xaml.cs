using ChallengeCupV1.DataSource;
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
using System.Windows.Threading;

namespace ChallengeCupV1.View.GearTab
{
    /// <summary>
    /// GearTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class GearTabContent : UserControl
    { 
        private IGear gear;
        private int mouseClickCount;
        private DispatcherTimer doubleClickTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(300)
        };
        public static DispatcherTimer AutoRotationTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(10)
        };

        public GearTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, GetType().Name);
            doubleClickTimer.Tick += (s, e1) => 
            {
                doubleClickTimer.IsEnabled = false;
                mouseClickCount = 0;
            };
            AutoRotationTimer.Tick += (s, e1) => 
            {
                if ("Start" ==  (string)(UserControlManager.Get("FunctionBar") as FunctionBar)
                                .start.Content)
                {
                    return;
                }
                if (gear != null)
                {
                    GearAction.AutoRotation(gear);
                }
            };
        }

        private void setting_Click(object sender, RoutedEventArgs e)
        {
            initPage.Visibility = Visibility.Visible;
            setting.Visibility = Visibility.Hidden;
            sideBar.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// Choose gear to show in GearTabControl
        /// </summary>
        public void UpdateGear()
        {
            gear = GearFactory.GetGear(initPage.GetGearIndex(), initPage.GetGratingNumber());
            // Set rotate style, cause new gear's rotate style may not be set
            gear.SetRotateStyle(horizontal.IsChecked.Value ? 
                GearAction.RotateStyle.Horizontal : GearAction.RotateStyle.vertical);
            gearContainer.Children.Clear();
            gearContainer.Children.Add(gear as UserControl);
            AutoRotationTimer.IsEnabled = true;
        }

        /// <summary>
        /// Show hidden things
        /// </summary>
        public void ShowHidden()
        {
            setting.Visibility = Visibility.Visible;
            sideBar.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Gear model zoom in and zoom out while mouse wheeling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gearContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            gear.Zoom(e.Delta);
            AutoRotationTimer.IsEnabled = false;
        }

        /// <summary>
        /// When mouse up, call gear's MouseUp()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gearContainer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gear.MouseUp();
        }

        /// <summary>
        /// When mouse down, there are two situation,
        /// one is double click to reset gear view,
        /// antoher is rotate gear view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gearContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseClickCount += 1;
            //doubleClickTimer.Tick += (s, e1) => { doubleClickTimer.IsEnabled = false; mouseClickCount = 0; };
            doubleClickTimer.IsEnabled = true;
            if (mouseClickCount % 2 == 0)
            {
                doubleClickTimer.IsEnabled = false;
                mouseClickCount = 0;
                gear.ResetView();
                AutoRotationTimer.IsEnabled = true;
            }
            else
            {
                gear.MouseDown(e);
                AutoRotationTimer.IsEnabled = false;
            }
        }

        /// <summary>
        /// Call gear's Rotate() while mouse moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gearContainer_MouseMove(object sender, MouseEventArgs e)
        {
            gear.Rotate();
        }

        /// <summary>
        /// Call gear's MouseUp() when mouse leave current view part
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gearContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            gear.MouseUp();
            AutoRotationTimer.IsEnabled = true;
        }


        #region Set rotate style

        private void vertical_Click(object sender, RoutedEventArgs e)
        {
            gear.SetRotateStyle(GearAction.RotateStyle.vertical);
        }

        private void horizontal_Click(object sender, RoutedEventArgs e)
        {
            gear.SetRotateStyle(GearAction.RotateStyle.Horizontal);
        }
        #endregion

    }

}
