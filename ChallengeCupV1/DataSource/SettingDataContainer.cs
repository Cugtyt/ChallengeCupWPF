using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource
{
    public static class SettingDataContainer
    {
        public static string WaveDataDir = File.FileUtils.GetRootPath() + @"\DataSource\data\";
        public static string StatusReportDir = File.FileUtils.GetRootPath() + @"\StatusReport\";
    }
}
