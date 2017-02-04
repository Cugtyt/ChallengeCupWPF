using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearStatus
{
    /// <summary>
    /// StatusCalculator is a calculator set, stores all algorithm
    /// of param need to be calculated.
    /// 
    /// These calculators should be used as a part of StatusDataTemplate
    /// when a status data object is produced.
    /// </summary>
    public static class StatusCalculator
    {
        public static int SamplingStep = 10;
        /// <summary>
        /// Buffer of delta result, gotten by CalculateDELTA
        /// </summary>
        private static List<double> DELTABuffer = new List<double>();
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
        ///         ---- 需要基准值和实时值
        ///     b -- 齿宽，mm
        /// Output:
        ///     σ_x -- 应力
        /// </summary>
        public static Func<IEnumerable<double>> StressCalculator = () =>
        {
            //            if (input == null)
            //            {
            //#if DEBUG
            //                Console.WriteLine("StatusCalculator: StressCalculator() -> Illegal input, argument can not be null.");
            //#endif
            //                throw new ArgumentNullException("StatusCalculator: StressCalculator()");
            //            }
            //            if (input.Length != 3)
            //            {
            //#if DEBUG
            //                Console.WriteLine("StatusCalculator: StressCalculator() -> Illegal input, expect count of input is 3, but accpet "
            //                    + input.Length + ".");
            //#endif
            //                throw new ArgumentException("StatusCalculator: StressCalculator() -> Illegal input, expect count of input is 3, but accpet "
            //                    + input.Length + ".");
            //            }
            //return -1 * StatusConstantParam.E * input[0]
            /// (StatusConstantParam.u
            //* (Math.Pow(StatusConstantParam.delta, input[1])) 
            //* StatusConstantParam.alpha);
            return from delta in DELTABuffer
                   select -1 * StatusConstantParam.E * delta
                   / (StatusConstantParam.u * (Math.Pow(StatusConstantParam.DELTA, StatusConstantParam.GEAR_WIDTH))
                   * StatusConstantParam.ALPHA); 
        };

        /// <summary>
        /// 应变
        ///     ε_y = Δλ_B / α_ε
        /// Param:
        ///     Δλ_B -- 反射光波偏移量
        ///     α_ɛ -- 对于中心波长处于1550nm附近的光栅，其灵敏度系数大约为α_ɛ=1.2pm/μɛ
        /// Input:
        ///     Δλ_B -- 反射光波偏移量
        ///         ---- 需要基准值和实时值
        /// Output:
        ///     ε_y -- 应变
        /// </summary>
        public static Func<IEnumerable<double>> StrainCalculator = () =>
        {
            //            if (input == null)
            //            {
            //#if DEBUG
            //                Console.WriteLine("StatusCalculator: StrainCalculator() -> Illegal input, argument can not be null.");
            //#endif
            //                throw new ArgumentNullException("StatusCalculator: StrainCalculator()");
            //            }
            //            if (input.Length != 2)
            //            {
            //#if DEBUG
            //            Console.WriteLine("StatusCalculator: StrainCalculator() -> Illegal input, expect count of input is 2, but accpet "
            //                + input.Length + ".");
            //#endif
            //                throw new ArgumentException("StatusCalculator: StrainCalculator() -> Illegal input, expect count of input is 2, but accpet "
            //                + input.Length + ".");
            //            }
            //return input[0] / StatusConstantParam.alpha;
            return from delta in DELTABuffer select delta / StatusConstantParam.ALPHA;
        };

        public static Func<IEnumerable<double>> TemperatureCalculator = () =>
        {
            return new List<double>() { 0 };
        };

        public static Func<IEnumerable<double>> FrequencyCalculator = () =>
        {
            return new List<double>() { 0 };
        };

        /// <summary>
        /// Calculate delta and store result in DELTABuffer
        /// </summary>
        public static void CalculateDELTA()
        {
            DELTABuffer.Clear();
            double temp;
            // Basic value is array index 0, so starts from 1
            for (int i = 1; i < GratingDataContainer.Data.Length; i++)
            {
                temp = 0;
                // Sampling according to SamplingStep
                for (int j = 0; j < GratingDataContainer.Data[0].Count; 
                    j += (int)(GratingDataContainer.Data[0].Count / SamplingStep))
                {
                    temp += (GratingDataContainer.Data[i][j] - GratingDataContainer.Data[0][j]);
                }
                DELTABuffer.Add(temp * SamplingStep / GratingDataContainer.Data[0].Count);
            }
        }
    }
}
