using ChallengeCupV1.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.GearLib
{
    /// <summary>
    /// GearFactory produce gear
    /// </summary>
    public static class GearFactory
    {
        /// <summary>
        /// Get gear by id and grating number
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gratingNum"></param>
        /// <returns></returns>
        public static IGear GetGear(int id, int gratingNum = 0)
        {
            //string gearClassName = "ChallengeCupV1.GearLib.Gear" + id +
            //    (gratingNumber == 0 ? "Grating" + gratingNumber : "");
            //return Assembly.GetExecutingAssembly()
            //    .CreateInstance("ChallengeCupV1.GearLib.Gear" + id +
            //    (gratingNumber != 0 ? "Grating" + gratingNumber : ""))
            //    as IGear;
            return ReflectionUtils.GetClassByName(
                "ChallengeCupV1.GearLib.Gear" + id + (gratingNum != 0 ? "Grating" + gratingNum : ""))
                as IGear;
        }
    }
}
