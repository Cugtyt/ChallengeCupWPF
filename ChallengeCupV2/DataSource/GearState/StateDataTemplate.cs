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
        public int CH
        {
            get { return (int)GetValue(CHProperty); }
            set { SetValue(CHProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CH.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CHProperty =
            DependencyProperty.Register("CH", typeof(int), typeof(StateDataTemplate));



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
        /// This calculater is used to calculate the Value
        /// </summary>
        //private Func<IEnumerable<double>> calculater;
        private Calculator calculater;


        private double threshold;
        public bool IsOutlier = false;
        private Func<double, double, bool> judge;

        public StateDataTemplate(int ch, int ID, string name, Calculator c, string unit, Func<double, double, bool> jg)
        {
            if (name == null || unit == null)
            {
#if DEBUG
                Console.WriteLine("StateDataTemplate: StateDataTemplate() -> Illegal input, argument can not be null.");
#endif
                throw new ArgumentNullException("StateDataTemplate: StateDataTemplate()");
            }
            CH = ch;
            GratingID = ID;
            Name = name;
            calculater = c;
            Unit = unit;
            //threshold = thd;
            judge = jg;
        }

        public void Get()
        {
#if DEBUG
            Console.WriteLine("StateDataTemplate: Get()");
#endif
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            Value = StateCalculator.GetParam(CH, GratingID, calculater);
#if DEBUG
            Console.WriteLine(Name + " " + Value);
#endif
            IsOutlier = judge(StateCalculator.GetDELTA(CH, GratingID), 0);
        }
    }

    public static class OutlierJudge
    {
        public static Func<double, double, bool> StressJudge = (value, threshold) =>
        {
            return Math.Abs(value - threshold) > 0.18;
        };
        public static Func<double, double, bool> StrainJudge = (value, threshold) =>
        {
            return Math.Abs(value - threshold) > 0.18;
        };
        public static Func<double, double, bool> TemperatureJudge = (value, threshold) =>
        {
            return value - threshold > 10;
        };
        public static Func<double, double, bool> FrequencyJudge = (value, threshold) =>
        {
            return value - threshold > 10;
        };
    }

}
