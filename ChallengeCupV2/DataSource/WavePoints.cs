using ChallengeCupV2.DataSource.GearState;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ChallengeCupV2.DataSource
{
    /// <summary>
    /// Data class
    /// 
    /// Contains data to show in chart
    /// </summary>
    public class WavePoints : DependencyObject
    {
        /// <summary>
        /// Capacity of ySet
        /// </summary>
        private int capacity = 500;
        /// <summary>
        /// All the ys of points
        /// </summary>
        //private List<double> ySet = new List<double>();
        private List<Point> ps = new List<Point>();
        /// <summary>
        /// Points is the final points set to show in chart
        /// </summary>
        public ObservableDataSource<Point> Points
        {
            get
            {
                return (ObservableDataSource<Point>)GetValue(PointsProperty);
            }
            set
            {
                SetValue(PointsProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableDataSource<Point>), typeof(WavePoints));

        public WavePoints()
        {
            Points = new ObservableDataSource<Point>();
        }

        /// <summary>
        /// Add double list to ySet
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Add(List<double> input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("WavePoints: Add() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("WavePoints: Add()");
            }
            ps.Clear();
            //Points.Collection.Clear();
            for (int i = 0; i < input.Count && i < capacity; i++)
            {
                //ySet[i] = input[i];
                ps.Add(new Point(i, input[i]));
            }
            //Update();
            Points.Collection.Clear();
            Points.AppendMany(ps);
            
        }

        /// <summary>
        /// Update Points when it's time to display
        /// </summary>
        //public void Update()
        //{
        //    List<Point> pl = new List<Point>();
        //    for (int i = 0; i < ySet.Count; i++)
        //    {
        //        pl.Add(new Point(i, ySet[i]));
        //    }
        //    Points.Collection.Clear();
        //    Points.AppendMany(pl);
        //}

        /// <summary>
        /// Transform complex array to ySet
        /// </summary>
        public void FromComplexArray(Complex[] input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("WavePoints: FromComplexArray() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("WavePoints: FromComplexArray()");
            }
            ps.Clear();
            for (int i = 1; i < input.Length / 2 && i < 230; i++)
            {
                ps.Add(new Point(i * StateConstantParam.DemodulationFrequency / input.Length, 
                    Math.Abs(input[i].Real)));
            }
            Points.Collection.Clear();
            Points.AppendMany(ps);
        }

        public void CleanAll()
        {
            Points.Collection.Clear();
        }
    }
}
