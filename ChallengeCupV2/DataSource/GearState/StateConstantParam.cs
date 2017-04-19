using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV2.DataSource.GearState
{
    /// <summary>
    /// StateConstantParam contains some constant params
    /// used by calculator associate with material and etc.
    /// </summary>
    public static class StateConstantParam
    {
        /// <summary>
        /// E -- 被测物材料的弹性模量
        /// </summary>
        public static double E = 70;

        /// <summary>
        /// μ -- 材料的泊松比
        /// </summary>
        public static double u = 0.3;

        /// <summary>
        /// δ -- 与齿轮材料有关的常量
        /// </summary>
        public static double DELTA = 0.6;

        /// <summary>
        /// α_ɛ -- 对于中心波长处于1550nm附近的光栅，其灵敏度系数大约为α_ɛ=1.2pm/μɛ
        /// </summary>
        public static double ALPHA = 1.2;

        /// <summary>
        /// Gear width
        /// </summary>
        public static double GEAR_WIDTH = 30;

        /// <summary>
        /// StressMultiplier =  - E / (μ * (δ ** b)) / α_ε
        /// for stress calculator
        /// </summary>
        public static double StressMultiplier = 880;

        public static double DemodulationFrequency = 4e4;

        public static void UpdateStressMultiplier()
        {
            StressMultiplier = -1 * StateConstantParam.E / 
                (StateConstantParam.u * (Math.Pow(StateConstantParam.DELTA, StateConstantParam.GEAR_WIDTH) 
                * StateConstantParam.ALPHA));
        }
    }
}
