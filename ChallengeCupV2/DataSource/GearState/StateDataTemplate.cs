using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChallengeCupV2.DataSource.GearState
{
    /// <summary>
    /// StateDataTemplate hold infos of state data
    /// </summary>
    public class StateDataTemplate : DependencyObject
    {

        public int GratingID
        {
            get { return (int)GetValue(GratingIDProperty); }
            set { SetValue(GratingIDProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GratingID.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GratingIDProperty =
            DependencyProperty.Register("GratingID", typeof(int), typeof(StateDataTemplate));


        /// <summary>
        /// State data name
        /// </summary>
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Name.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NameProperty =
            DependencyProperty.Register("Name", typeof(string), typeof(StateDataTemplate));



        /// <summary>
        /// State data unit
        /// </summary>
        public string Unit
        {
            get { return (string)GetValue(UnitProperty); }
            set { SetValue(UnitProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Unit.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UnitProperty =
            DependencyProperty.Register("Unit", typeof(string), typeof(StateDataTemplate));



        /// <summary>
        /// Average value of  state value set
        /// </summary>
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(double), typeof(StateDataTemplate), new PropertyMetadata(0.0));


        /// <summary>
        /// State value set
        /// </summary>
        public IEnumerable<double> ValueSet
        {
            get { return (IEnumerable<double>)GetValue(ValueSetProperty); }
            set { SetValue(ValueSetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueSet.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueSetProperty =
            DependencyProperty.Register("ValueSet", typeof(IEnumerable<double>), typeof(StateDataTemplate));





        /// <summary>
        /// This calculater is used to calculate the Value
        /// </summary>
        //private Func<IEnumerable<double>> calculater;
        private Calculator calculater;

        public StateDataTemplate(int ID, string name, Calculator c, string unit)
        {
            if (name == null || unit == null)
            {
#if DEBUG
                Console.WriteLine("StateDataTemplate: StateDataTemplate() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("StateDataTemplate: StateDataTemplate()");
            }
            GratingID = ID;
            Name = name;
            calculater = c;
            Unit = unit;
        }

        /// <summary>
        /// Use calculater to calculate Value
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //public void Calculate()
        //{
        //    // Check data validation first
        //    if (!GratingDataContainer.IsDataReady)
        //    {
        //        return;
        //    }
        //    ValueSet = calculater();
        //    Value = ValueSet.Average();
        //}

        public void Get()
        {
#if DEBUG
            Console.WriteLine("StateDataTemplate: Get()");
#endif
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            Value = StateCalculator.Get(GratingID, calculater);
#if DEBUG
            Console.WriteLine(Name + " " + Value);
#endif
        }
    }
}
