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

        static int capacity = 200;
        Queue<double> y = new Queue<double>(capacity);

        public ObservableDataSource<Point> Points
        {
            get { return (ObservableDataSource<Point>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableDataSource<Point>), typeof(Data));

        /// <summary>
        /// Initial all points as (0, 0)
        /// </summary>
        public Data()
        {
            Points = new ObservableDataSource<Point>();
            //List<Point> pl = new List<Point>();
            //y.Enqueue(5);
            //y.Enqueue(10);
            //y.Enqueue(50);
            //for (int i = 0; i < capacity; i++)
            //{
            //    pl.Add(new Point(i, i * 2));
            //}
            //for (int i = 0; i < y.Count; i++)
            //{
            //    pl.Add(new Point(i, y.ElementAt(i)));
            //}
            //Points.AppendMany(pl);
        }
        
        /// <summary>
        /// Add new y value
        /// </summary>
        /// <param name="newY"></param>
        public void Add(double newY)
        {
            if (y.Count == capacity)
            {
                y.Dequeue();
            }
            y.Enqueue(newY);
        }

        /// <summary>
        /// Update Points when it's time to display
        /// </summary>
        public void Update()
        {
            List<Point> pl = new List<Point>();
            for (int i = 0; i < y.Count; i++)
            {
                pl.Add(new Point(i, y.ElementAt(i)));
            }
            Points.Collection.Clear();
            Points.AppendMany(pl);
        }
    }
}
