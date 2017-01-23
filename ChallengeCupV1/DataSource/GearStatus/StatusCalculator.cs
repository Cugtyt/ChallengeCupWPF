using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearStatus
{
    public static class StatusCalculator
    {

        /// <summary>
        /// 应力
        ///     σ_x = - E / (μ * (δ ** b)) * Δλ_B / α_ε
        /// Param:
        ///     E -- 被测物材料的弹性模量
        ///     μ -- 材料的泊松比
        ///     δ -- 与齿轮材料有关的常量
        ///     b -- 齿宽，mm
        ///     Δλ_B -- 反射光波偏移量
        ///     α_ɛ -- 对于中心波长处于1550nm附近的光栅，其灵敏度系数大约为α_ɛ=1.2pm/μɛ
        /// Input:
        ///     Δλ_B -- 反射光波偏移量
        ///     b -- 齿宽，mm
        /// Output:
        ///     σ_x -- 应力
        /// </summary>
        public static Func<List<double>, double> StressCalculator = (input) => 
        {
            if (input.Count != 2)
            {
                throw new Exception("Illegal input");
            }
            //return -1 * StatusConstantParam.E * input[0]
            /// (StatusConstantParam.u
            //* (Math.Pow(StatusConstantParam.delta, input[1])) 
            //* StatusConstantParam.alpha);
            return 0.0;
        };

        /// <summary>
        /// 应变
        ///     ε_y = Δλ_B / α_ε
        /// Param:
        ///     Δλ_B -- 反射光波偏移量
        ///     α_ɛ -- 对于中心波长处于1550nm附近的光栅，其灵敏度系数大约为α_ɛ=1.2pm/μɛ
        /// Input:
        ///     Δλ_B -- 反射光波偏移量
        /// Output:
        ///     ε_y -- 应变
        /// </summary>
        public static Func<List<double>, double> StrainCalculator = (input) =>
        {
            if (input.Count != 1)
            {
                throw new Exception("Illegal input");
            }
            //return input[0] / StatusConstantParam.alpha;
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
