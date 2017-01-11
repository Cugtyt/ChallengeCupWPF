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
    public class Data : DependencyObject
    {
        private static int times = 0;
        private static int capacity = 100;
        // y data set
        //Queue<double> yQueue = new Queue<double>(capacity);
        private double[] ySet = new double[capacity];
        private int writeIndex = 0;

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
        public Task Add(double newY)
        {
            times++;
#if DEBUG
            // Console.WriteLine("Data: Add(" + newY + ")");
#endif
            //if (yQueue.Count >= capacity)
            //{
            //    yQueue.Dequeue();
            //}
            //yQueue.Enqueue(newY);
            ySet[writeIndex++] = newY;
            writeIndex = writeIndex % capacity;
            if (times == 20)
            {
                Update();
                times = 0;
            }
            return null;
        }

        /// <summary>
        /// Add double list to ySet
        /// </summary>
        /// <param name="newYs"></param>
        /// <returns></returns>
        public async Task Add(List<double> newYs)
        {
            if (newYs.Count >= capacity)
            {
                //yQueue = new Queue<double>(newYs);
                ySet = newYs.ToArray();
                await Update();
                return;
            }
            for (int i = 0; i < newYs.Count; i++)
            {
                //yQueue.Enqueue(newYs[i]);
                ySet[writeIndex++] = newYs[i];
                writeIndex = writeIndex % capacity;
            }
            await Update();
        }

        /// <summary>
        /// Update Points when it's time to display
        /// </summary>
        public Task Update()
        {
#if DEBUG
            Console.WriteLine("Data: Update()");
#endif
            List<Point> pl = new List<Point>();
            //for (int i = 0; i < yQueue.Count; i++)
            //{
            //    pl.Add(new Point(i, yQueue.ElementAt(i)));
            //}
            for (int i = 0; i < capacity; i++)
            {
                pl.Add(new Point(i, ySet[i]));
            }
            Points.Collection.Clear();
            Points.AppendMany(pl);
            return null;
        }

        /// <summary>
        /// Transform yQueue to complex array
        /// </summary>
        /// <returns></returns>
        public Complex[] ToComplexArray()
        {
            //Complex[] com = new Complex[yQueue.Count];
            //for (int i = 0; i < yQueue.Count; i++)
            //{
            //    com[i] = new Complex(yQueue.ElementAt(i), 0);
            //}
            Complex[] com = new Complex[capacity];
            for (int i = 0; i < capacity; i++)
            {
                com[i] = new Complex(ySet[i], 0);
            }
            return com;
        }
    }
}
