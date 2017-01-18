using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupV1.DataSource
{
    public class GearStatusData : DependencyObject
    {
        public double Item1
        {
            get { return (double)GetValue(Item1Property); }
            set { SetValue(Item1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Item1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Item1Property =
            DependencyProperty.Register("Item1", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));

        public double Item2
        {
            get { return (double)GetValue(Item2Property); }
            set { SetValue(Item2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Item2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Item2Property =
            DependencyProperty.Register("Item2", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));



        public double Item3
        {
            get { return (double)GetValue(Item3Property); }
            set { SetValue(Item3Property, value); }
        }

        // Using a DependencyProperty as the backing store for Item3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Item3Property =
            DependencyProperty.Register("Item3", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));



        public double Item4
        {
            get { return (double)GetValue(Item4Property); }
            set { SetValue(Item4Property, value); }
        }

        // Using a DependencyProperty as the backing store for Item4.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Item4Property =
            DependencyProperty.Register("Item4", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));


    }
}
