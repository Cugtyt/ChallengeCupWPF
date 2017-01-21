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
        /// <summary>
        /// Stress 应力
        /// </summary>
        public double Stress
        {
            get { return (double)GetValue(StressProperty); }
            set { SetValue(StressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Stress.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StressProperty =
            DependencyProperty.Register("Stress", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));

        /// <summary>
        /// Strain 应变
        /// </summary>
        public double Strain
        {
            get { return (double)GetValue(StrainProperty); }
            set { SetValue(StrainProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Strain.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StrainProperty =
            DependencyProperty.Register("Strain", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));


        /// <summary>
        /// Temperature 温度
        /// </summary>
        public double Temperature
        {
            get { return (double)GetValue(TemperatureProperty); }
            set { SetValue(TemperatureProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Temperature.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TemperatureProperty =
            DependencyProperty.Register("Temperature", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));


        /// <summary>
        /// Frequency 频率
        /// </summary>
        public double Frequency
        {
            get { return (double)GetValue(FrequencyProperty); }
            set { SetValue(FrequencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Frequency.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FrequencyProperty =
            DependencyProperty.Register("Frequency", typeof(double), typeof(GearStatusData), new PropertyMetadata(0.0));


        /// <summary>
        /// Calculate stress
        /// </summary>
        /// <returns></returns>
        public double CalculateStress()
        {
            // TODO: implement this
            return 0;
        }

        /// <summary>
        /// Calculate strain
        /// </summary>
        /// <returns></returns>
        public double CalculateStrain()
        {
            // TODO: implement this
            return 0;
        }

        /// <summary>
        /// Calculate temperature
        /// </summary>
        /// <returns></returns>
        public double CalculateTemperature()
        {
            // TODO: implement this
            return 0;
        }

        /// <summary>
        /// Calculate trequency
        /// </summary>
        /// <returns></returns>
        public double CalculateFrequency()
        {
            // TODO: implement this
            return 0;
        }
    }
}
