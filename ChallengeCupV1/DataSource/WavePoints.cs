using Microsoft.Research.DynamicDataDisplay.DataSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
    public class WavePoints : DependencyObject
    {
        private int capacity;
        private double[] ySet;

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

        /// <summary>
        /// Initial all points as (0, 0)
        /// </summary>
        public WavePoints(int capacity = 200)
        {
            Points = new ObservableDataSource<Point>();
            this.capacity = capacity;
            ySet = new double[capacity];
        }

        /// <summary>
        /// Add double list to ySet
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Add(List<double> input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("WavePoints: Add() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("WavePoints: Add()");
            }
            for (int i = 0; i < input.Count && i < capacity; i++)
            {
                ySet[i] = input[i];
            }
            await Update();
        }

        /// <summary>
        /// Update Points when it's time to display
        /// </summary>
        public Task Update()
        {
            List<Point> pl = new List<Point>();
            for (int i = 0; i < ySet.Length; i++)
            {
                pl.Add(new Point(i, ySet[i]));
            }
            Points.Collection.Clear();
            Points.AppendMany(pl);
            return null;
        }

        /// <summary>
        /// Transform complex array to ySet
        /// </summary>
        /// <returns></returns>
        public void FromComplexArray(Complex[] input)
        {
            if (input == null)
            {
#if DEBUG
                Console.WriteLine("WavePoints: FromComplexArray() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("WavePoints: FromComplexArray()");
            }
            List<Point> pl = new List<Point>();
            for (int i = 0; i < input.Length && i < 200; i++)
            {
                pl.Add(new Point(i * 2e5 / 200, Math.Abs(input[i].Real)));
            }
            Points.Collection.Clear();
            Points.AppendMany(pl);
        }
    }
}
