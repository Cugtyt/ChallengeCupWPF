using ChallengeCupV2.DataSource;
using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.ViewportRestrictions;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ChallengeCupV2.View.WaveTab
{
    /// <summary>
    /// WavePlot.xaml 的交互逻辑
    /// </summary>
    public partial class WavePlot : UserControl
    {
        /// <summary>
        /// Data source to show in chart
        /// </summary>
        private WavePoints dataSource = new WavePoints();
        private double end;
        private double start;

        public WavePlot()
        {
            InitializeComponent();
            UserControlManager.Register(this, this.GetType().Name);
            // Set axes range
            //ViewportAxesRangeRestriction restr = new ViewportAxesRangeRestriction();
            // Set mouse cannot change view of plotter
            plotter.Children.Remove(plotter.MouseNavigation);
            plotter.Children.Remove(plotter.KeyboardNavigation);

            //DataContext = dataSource;
            line.DataSource = dataSource.Points;
            
        }

        /// <summary>
        /// Add double list to dataSource in time domain
        /// </summary>
        /// <param name="yList"></param>
        /// <returns></returns>
        public void AddTimePoints(List<double> yList)
        {
            if (yList.Count == 0)
            {
                dataSource.CleanAll();
                return;
            }
            
            UpdateYRange(yList[0] - 0.07, yList[0] + 0.07);
            xAxis.Visibility = Visibility.Hidden;
            yAxis.Visibility = Visibility.Visible;
            yAxis.Width = 74;
            dataSource.Add(yList);
        }
        
        /// <summary>
        /// Add complex array to dataSource in freq domain
        /// </summary>
        /// <param name="com"></param>
        /// <returns></returns>
        public void AddFreqPoints(Complex[] com)
        {
            if (com == null || com.Length == 0)
            {
                dataSource.CleanAll();
                return;
            }
            plotter.Viewport.Restrictions.Clear();
            xAxis.Visibility = Visibility.Visible;
            yAxis.Visibility = Visibility.Hidden;
            yAxis.Width = 10;
            dataSource.FromComplexArray(com);
        }

        /// <summary>
        /// Set vertical title
        /// </summary>
        /// <param name="title"></param>
        public void SetYTitle(string title)
        {
            verticalTitle.Content = title ?? throw new ArgumentNullException("WavePlot: SetYTitle()");
        }

        /// <summary>
        /// Update y range that from min to max
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void UpdateYRange(double min, double max)
        {
            if (plotter.Viewport.Restrictions.Count > 0 && Math.Abs(start - min) < 0.1)
            {
                return;
            }
            ViewportAxesRangeRestriction restr = new ViewportAxesRangeRestriction()
            {
                //restr.YRange = new DisplayRange(
                //    SettingContainer.MinYWavePlotTimeDomain,
                //    SettingContainer.MaxYWavePlotTimeDomain);
                YRange = new DisplayRange(start = min, end = max)
            };
            //restr.YRange = new DisplayRange(
            //    1556,
            //    1556.5);
            //plotter.Viewport.AutoFitToView = true;
            plotter.Viewport.Restrictions.Clear();
            plotter.Viewport.Restrictions.Add(restr);
        }

        public void Clean()
        {
            dataSource.CleanAll();
        }
    }

    /// <summary>
    /// Set Axes Range
    /// </summary>
    class ViewportAxesRangeRestriction : IViewportRestriction
    {
        public DisplayRange XRange = null;
        public DisplayRange YRange = null;

        public event EventHandler Changed;

        public Rect Apply(Rect oldVisible, Rect newVisible, Viewport2D viewport)
        {
            if (XRange != null)
            {
                newVisible.X = XRange.Start;
                newVisible.Width = XRange.End - XRange.Start;
            }

            if (YRange != null)
            {
                newVisible.Y = YRange.Start;
                newVisible.Height = YRange.End - YRange.Start;
            }
            return newVisible;
        }
    }

    /// <summary>
    /// Display range
    /// </summary>
    class DisplayRange
    {
        public double Start { get; set; }
        public double End { get; set; }
        public DisplayRange(double start, double end)
        {
            Start = start;
            End = end;
        }
    }

}
