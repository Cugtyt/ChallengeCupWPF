using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearStatus
{
    /// <summary>
    /// StatusConstantParam contains some constant params
    /// used by calculator associate with material and etc.
    /// </summary>
    public static class StatusConstantParam
    {
        /// <summary>
        /// E -- 被测物材料的弹性模量
        /// </summary>
        public static double E = 1;

        /// <summary>
        /// μ -- 材料的泊松比
        /// </summary>
        public static double u = 1;

        /// <summary>
        /// δ -- 与齿轮材料有关的常量
        /// </summary>
        public static double delta = 1;

        /// <summary>
        /// α_ɛ -- 对于中心波长处于1550nm附近的光栅，其灵敏度系数大约为α_ɛ=1.2pm/μɛ
        /// </summary>
        public static double alpha = 1.2;
    }
}
