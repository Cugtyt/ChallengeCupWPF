using ChallengeCupV2.DataSource;
using ChallengeCupV2.Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace ChallengeCupV2.View.ModelTab
{
    /// <summary>
    /// ModelTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class ModelTabContent : UserControl
    {
        public IModel Model = new Gear();
        private int mouseClickCount;
        private DispatcherTimer doubleClickTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(300)
        };
        public static DispatcherTimer AutoRotationTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(10)
        };

        public ModelTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, GetType().Name);
            updateModel();
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
                if (Model != null)
                {
                    ModelAction.AutoRotation(Model);
                }
            };
        }

        private void updateModel()
        {
            modelContainer?.Children.Clear();
            modelContainer?.Children.Add(Model as UserControl);
        }

        /// <summary>
        /// Model zoom in and zoom out while mouse wheeling
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelContainer_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Model.Zoom(e.Delta);
            AutoRotationTimer.IsEnabled = false;
        }

        /// <summary>
        /// When mouse up, call model's MouseUp()
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelContainer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Model.MouseUp();
        }

        /// <summary>
        /// When mouse down, there are two situation,
        /// one is double click to reset model view,
        /// antoher is rotate model view.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelContainer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mouseClickCount += 1;
            //doubleClickTimer.Tick += (s, e1) => { doubleClickTimer.IsEnabled = false; mouseClickCount = 0; };
            doubleClickTimer.IsEnabled = true;
            if (mouseClickCount % 2 == 0)
            {
                doubleClickTimer.IsEnabled = false;
                mouseClickCount = 0;
                Model.ResetView();
                AutoRotationTimer.IsEnabled = true;
            }
            else
            {
                Model.MouseDown(e);
                AutoRotationTimer.IsEnabled = false;
            }
        }

        /// <summary>
        /// Call model's Rotate() while mouse moving
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelContainer_MouseMove(object sender, MouseEventArgs e)
        {
            Model.Rotate();
        }

        /// <summary>
        /// Call model's MouseUp() when mouse leave current view part
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void modelContainer_MouseLeave(object sender, MouseEventArgs e)
        {
            Model.MouseUp();
            AutoRotationTimer.IsEnabled = true;
        }


        #region Set rotate style

        private void vertical_Click(object sender, RoutedEventArgs e)
        {
            Model.SetRotateStyle(ModelAction.RotateStyle.vertical);
        }

        private void horizontal_Click(object sender, RoutedEventArgs e)
        {
            Model.SetRotateStyle(ModelAction.RotateStyle.Horizontal);
        }
        #endregion

        #region Update model

        private void gearModel_Selected(object sender, RoutedEventArgs e)
        {
            Model = new Gear();
            updateModel();
            wavelengthDisplay?.UpdataSource(null, null);
        }

        private void bearingModel_Selected(object sender, RoutedEventArgs e)
        {
            Model = new Bearing();
            updateModel();
            wavelengthDisplay?.UpdataSource(null, null);
        }

        private void shaftModel_Selected(object sender, RoutedEventArgs e)
        {
            Model = new Shaft();
            updateModel();
            wavelengthDisplay?.UpdataSource(null, null);
        }
        #endregion
    }

}
