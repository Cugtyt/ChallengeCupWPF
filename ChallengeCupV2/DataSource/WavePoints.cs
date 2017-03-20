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
        private int capacity;
        /// <summary>
        /// All the ys of points
        /// </summary>
        private double[] ySet;
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

        public WavePoints(int capacity = 300)
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
        public void Add(List<double> input)
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
            Update();
        }

        /// <summary>
        /// Update Points when it's time to display
        /// </summary>
        public void Update()
        {
            List<Point> pl = new List<Point>();
            for (int i = 0; i < ySet.Length; i++)
            {
                pl.Add(new Point(i, ySet[i]));
            }
            Points.Collection.Clear();
            Points.AppendMany(pl);
            //Points = new ObservableDataSource<Point>(pl);
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
            for (int i = 0; i < input.Length / 2; i++)
            {
                pl.Add(new Point(i * StateConstantParam.DemodulationFrequency / input.Length, 
                    Math.Abs(input[i].Real) * 2));
            }
            Points.Collection.Clear();
            Points.AppendMany(pl);
        }
    }
}
