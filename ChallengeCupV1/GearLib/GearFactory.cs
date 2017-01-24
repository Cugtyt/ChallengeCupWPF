using ChallengeCupV1.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.GearLib
{
    public static class GearFactory
    {
        public static IGear GetGear(int id, int gratingNumber = 0)
        {
            //string gearClassName = "ChallengeCupV1.GearLib.Gear" + id +
            //    (gratingNumber == 0 ? "Grating" + gratingNumber : "");
            //return Assembly.GetExecutingAssembly()
            //    .CreateInstance("ChallengeCupV1.GearLib.Gear" + id +
            //    (gratingNumber != 0 ? "Grating" + gratingNumber : ""))
            //    as IGear;
            return ReflectionUtils.GetClassByName(
                "ChallengeCupV1.GearLib.Gear" + id + (gratingNumber != 0 ? "Grating" + gratingNumber : ""))
                as IGear;
        }
    }
}
