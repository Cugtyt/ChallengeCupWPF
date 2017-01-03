using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChallengeCupV1.DataSource
{
    /// <summary>
    /// Data class
    /// 
    /// Contains data to show in chart
    /// </summary>
    public class Data : DependencyObject
    {

        int capacity = 200;
        public ObservableDataSource<Point> Points
        {
            get { return (ObservableDataSource<Point>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableDataSource<Point>), typeof(Data));


        
        //public PointCollection Points
        //{
        //    get { return (PointCollection)GetValue(PointsProperty); }
        //    set { SetValue(PointsProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty PointsProperty =
        //    DependencyProperty.Register("Points", typeof(PointCollection), typeof(Data), new PropertyMetadata(null));



        /// <summary>
        /// Initial all points as (0, 0)
        /// </summary>
        public Data()
        {
            Points = new ObservableDataSource<Point>();
            List<Point> pl = new List<Point>();
            for (int i = 0; i < capacity; i++)
            {
                pl.Add(new Point(i, i + 1));
            }
            Points.AppendMany(pl);
        }

    }
}
