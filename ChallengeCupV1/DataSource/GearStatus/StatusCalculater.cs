using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearStatus
{
    public static class StatusCalculater
    {
        public static Func<List<double>, double> StressCalculater = (input) => 
        {
            return 0.0;
        };

        public static Func<List<double>, double> StrainCalculater = (input) =>
        {
            return 0.0;
        };

        public static Func<List<double>, double> TemperatureCalculater = (input) =>
        {
            return 0.0;
        };

        public static Func<List<double>, double> FrequencyCalculater = (input) =>
        {
            return 0.0;
        };
    }
}
