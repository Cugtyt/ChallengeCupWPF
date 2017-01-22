using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearStatus
{
    public static class StatusCalculator
    {
        public static Func<List<double>, double> StressCalculator = (input) => 
        {
            return 0.0;
        };

        public static Func<List<double>, double> StrainCalculator = (input) =>
        {
            return 0.0;
        };

        public static Func<List<double>, double> TemperatureCalculator = (input) =>
        {
            return 0.0;
        };

        public static Func<List<double>, double> FrequencyCalculator = (input) =>
        {
            return 0.0;
        };
    }
}
