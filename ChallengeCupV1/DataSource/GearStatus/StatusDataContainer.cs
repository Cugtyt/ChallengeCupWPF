using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearStatus
{
    public class StatusDataContainer
    {
        public List<StatusDataTemplate> StatusData = new List<StatusDataTemplate>()
        {
            new StatusDataTemplate("Stress", StatusCalculator.StressCalculator, "N"),
            new StatusDataTemplate("Strain", StatusCalculator.StressCalculator, "N"),
            new StatusDataTemplate("Temperature", StatusCalculator.TemperatureCalculator, "N"),
            new StatusDataTemplate("Frequency", StatusCalculator.FrequencyCalculator, "N")
        };

        public void Calculate()
        {
#if DEBUG
            Console.WriteLine("StatusDataContainer: Calculate() -> calculating");
#endif
            //StatusData[0].Calculate(new List<double>() { });
            //StatusData[1].Calculate(new List<double>() { });
            //StatusData[2].Calculate(new List<double>() { });
            //StatusData[3].Calculate(new List<double>() { });

            StatusData[0].Value = 1;
            StatusData[1].Value = 2;
            StatusData[2].Value = 3;
            StatusData[3].Value = 4;
        }
    }
}
