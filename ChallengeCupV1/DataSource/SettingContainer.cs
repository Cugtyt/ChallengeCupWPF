using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource
{
    /// <summary>
    /// SettingContainer stores a set of setting data,
    /// that can be set in setting tab
    /// </summary>
    public static class SettingContainer
    {
        /// <summary>
        /// Wave data dir
        /// </summary>
        public static string WaveDataDir = File.FileUtils.GetRootPath() + @"\DataSource\data\";
        /// <summary>
        /// File where to generate report
        /// </summary>
        public static string StatusReportDir = File.FileUtils.GetRootPath() + @"\StatusReport\";
        /// <summary>
        /// Max and min of y axis when time domain is selected
        /// </summary>
        public static double WavePlotTimeDomainMaxY = 1556.5;
        public static double WavePlotTimeDomainMinY = 1556.0;
        
    }

}
