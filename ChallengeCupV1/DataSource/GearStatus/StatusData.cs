using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupV1.DataSource.GearStatus
{
    public class StatusData : DependencyObject
    {
        /// <summary>
        /// Status data name
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(StatusData));

        /// <summary>
        /// Status value
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(StatusData), new PropertyMetadata(0.0));

        /// <summary>
        /// This calculater is used to calculate the Value
        /// </summary>
        private Func<List<double>, double> calculater;

        public StatusData(string name, Func<List<double>, double> cal)
        {
            Name = name;
            calculater = cal;
        }

        /// <summary>
        /// Use calculater to calculate Value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public double Calculate(List<double> input)
        {
            Value = calculater(input);
            return Value;
        }
    }
}
