using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupV1.DataSource.GearStatus
{
    /// <summary>
    /// StatusDataTemplate hold infos of status data
    /// </summary>
    public class StatusDataTemplate : DependencyObject
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
            DependencyProperty.Register("Name", typeof(string), typeof(StatusDataTemplate));



        /// <summary>
        /// Status data unit
        /// </summary>
        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Unit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(StatusDataTemplate));



        /// <summary>
        /// Average value of  status value set
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(StatusDataTemplate), new PropertyMetadata(0.0));


        /// <summary>
        /// Status value set
        /// </summary>
        public IEnumerable<double> ValueSet
        {
            get { return (IEnumerable<double>)GetValue(ValueSetProperty); }
            set { SetValue(ValueSetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueSet.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueSetProperty =
            DependencyProperty.Register("ValueSet", typeof(IEnumerable<double>), typeof(StatusDataTemplate));





        /// <summary>
        /// This calculater is used to calculate the Value
        /// </summary>
        private Func<IEnumerable<double>> calculater;

        public StatusDataTemplate(string name, Func<IEnumerable<double>> cal, string unit)
        {
            if (name == null || cal == null || unit == null)
            {
#if DEBUG
                Console.WriteLine("StatusDataTemplate: StatusDataTemplate() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("StatusDataTemplate: StatusDataTemplate()");
            }
            Name = name;
            calculater = cal;
            Unit = unit;
        }

        /// <summary>
        /// Use calculater to calculate Value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public void Calculate()
        {
            // Check data validation first
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            ValueSet = calculater();
            Value = ValueSet.Average();
        }
    }
}
