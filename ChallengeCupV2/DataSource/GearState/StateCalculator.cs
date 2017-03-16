using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV2.DataSource.GearState
{
    /// <summary>
    /// StateCalculator is a calculate methods set, stores all algorithm
    /// of param need to be calculated.
    /// 
    /// These methods should be used as a part of StateDataTemplate
    /// when a state data object is produced, the way to pass the methods
    /// is the enum Calculator behind StateCalculator, pass Calculator value
    /// to initial StateDataTemplate.
    /// </summary>
    public static class StateCalculator
    {
        public static int SamplingStep = 10;
        /// <summary>
        /// Buffer of delta result, gotten by CalculateDELTA
        /// </summary>
        //private static List<double> DELTABuffer = new List<double>();
        private static List<double>[] DELTABuffer = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] stress = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] strain = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] temperature = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] frequency = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };

        public static List<Complex[]>[] FFTResults = new List<Complex[]>[4] 
        { new List<Complex[]>(), new List<Complex[]>(), new List<Complex[]>(), new List<Complex[]>() };

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
        private static void calculateStress()
        {
            //            if (input == null)
            //            {
            //#if DEBUG
            //                Console.WriteLine("StateCalculator: StressCalculator() -> Illegal input, argument can not be null.");
            //#endif
            //                throw new ArgumentNullException("StateCalculator: StressCalculator()");
            //            }
            //            if (input.Length != 3)
            //            {
            //#if DEBUG
            //                Console.WriteLine("StateCalculator: StressCalculator() -> Illegal input, expect count of input is 3, but accpet "
            //                    + input.Length + ".");
            //#endif
            //                throw new ArgumentException("StateCalculator: StressCalculator() -> Illegal input, expect count of input is 3, but accpet "
            //                    + input.Length + ".");
            //            }
            //return -1 * StateConstantParam.E * input[0]
            /// (StateConstantParam.u
            //* (Math.Pow(StateConstantParam.delta, input[1])) 
            //* StateConstantParam.alpha);

            // Check data validation first
            if (DELTABuffer[0].Count == 0)
            {
                return;
            }
            //stress.Clear();
            //stress.AddRange(from delta in DELTABuffer
            //                select -1 * StateConstantParam.E * delta
            //                / (StateConstantParam.u * (Math.Pow(StateConstantParam.DELTA, StateConstantParam.GEAR_WIDTH))
            //                * StateConstantParam.ALPHA));
            for (int i = 0; i < stress.Length; i++)
            {
                stress[i].Clear();
                stress[i].AddRange(from delta in DELTABuffer[i]
                                   select -1 * StateConstantParam.E * delta
                                   / (StateConstantParam.u * (Math.Pow(StateConstantParam.DELTA, StateConstantParam.GEAR_WIDTH))
                                   * StateConstantParam.ALPHA));
            }
        }

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
        private static void calculateStrain()
        {
            //            if (input == null)
            //            {
            //#if DEBUG
            //                Console.WriteLine("StateCalculator: StrainCalculator() -> Illegal input, argument can not be null.");
            //#endif
            //                throw new ArgumentNullException("StateCalculator: StrainCalculator()");
            //            }
            //            if (input.Length != 2)
            //            {
            //#if DEBUG
            //            Console.WriteLine("StateCalculator: StrainCalculator() -> Illegal input, expect count of input is 2, but accpet "
            //                + input.Length + ".");
            //#endif
            //                throw new ArgumentException("StateCalculator: StrainCalculator() -> Illegal input, expect count of input is 2, but accpet "
            //                + input.Length + ".");
            //            }
            //return input[0] / StateConstantParam.alpha;

            // Check data validation first
            if (DELTABuffer[0].Count == 0)
            {
                return;
            }
            //strain.Clear();
            //strain.AddRange(from delta in DELTABuffer
            //                select delta / StateConstantParam.ALPHA);
            for (int i = 0; i < strain.Length; i++)
            {
                strain[i].Clear();
                strain[i].AddRange(from delta in DELTABuffer[i]
                                   select delta / StateConstantParam.ALPHA);
            }
        }

        /// <summary>
        /// Calculate temperature
        /// 
        /// Temperature change 1 ℃ when delta of wavelength change 10pm.
        /// </summary>
        private static void calculateTemperature()
        {
            // Check data validation first
            if (DELTABuffer[0].Count == 0)
            {
                return;
            }
            //temperature.Clear();
            //temperature.AddRange(from delta in DELTABuffer
            //                select delta / 10 + SettingContainer.InitTemperature);
            for (int i = 0; i < temperature.Length; i++)
            {
                temperature[i].Clear();
                temperature[i].AddRange(from delta in DELTABuffer[i]
                                        select delta / 10 + SettingContainer.InitTemperature);
            }
        }

        /// <summary>
        /// Calculate frequency of gear 
        /// </summary>
        private static void calculateFrequency()
        {
            // Check data validation first
            if (DELTABuffer[0].Count == 0)
            {
                return;
            }
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            lock (FFTResults)
            {
                //FFTResults.Clear();
                //frequency.Clear();
                //for (int i = 0; i < GratingDataContainer.Data.Length; i++)
                //{
                //    var temp = (from y in GratingDataContainer.Data[i]
                //                select new Complex(y, 0)).ToArray();
                //    FFT.DataFFT.Forward(temp);
                //    FFTResults.Add(temp);
                //    double max = FFTResults[i][0].Real;
                //    int maxIndex = 0;
                //    for (int j = 1; j < FFTResults[i].Length / 2; j++)
                //    {
                //        if (FFTResults[i][j].Real > max)
                //        {
                //            max = FFTResults[i][j].Real;
                //            maxIndex = j;
                //        }
                //    }
                //    frequency.Add(maxIndex * StateConstantParam.DemodulationFrequency / temp.Length);

                for (int i = 0; i < frequency.Length; i++)
                {
                    frequency[i].Clear();
                    FFTResults[i].Clear();
                    for (int j = 0; j < GratingDataContainer.Data[i].Length; j++)
                    {
                        var temp = (from y in GratingDataContainer.Data[i][j]
                                    select new Complex(y, 0)).ToArray();
                        FFT.DataFFT.Forward(temp);
                        FFTResults[i].Add(temp);
                        double max = FFTResults[i][j][0].Real;
                        int maxIndex = 0;
                        for (int k = 1; k < FFTResults[i][j].Length / 2; k++)
                        {
                            if (FFTResults[i][j][k].Real > max)
                            {
                                max = FFTResults[i][j][k].Real;
                                maxIndex = k;
                            }
                        }
                        frequency[i].Add(maxIndex * StateConstantParam.DemodulationFrequency / temp.Length);
                    }
                }
            }
        }
            //frequency.Clear();
            //frequency.AddRange(new List<double>() { 0, 0, 0, 0});
            //stress.AddRange(from delta in DELTABuffer
            //                select delta / 10);
        //}

        /// <summary>
        /// Calculate delta and store result in DELTABuffer
        /// </summary>
        private static void calculateDELTA()
        {
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            // Clear delta buffer
            for (int i = 0; i < DELTABuffer.Length; i++)
            {
                DELTABuffer[i].Clear();
            }
            //DELTABuffer.Clear();
            double temp;
            //for (int i = 0; i < GratingDataContainer.Data.Length; i++)
            //{
            //    temp = 0;
            //    // Sampling according to SamplingStep
            //    for (int j = 0; j < GratingDataContainer.Data[0].Count; 
            //        j += (int)(GratingDataContainer.Data[0].Count / SamplingStep))
            //    {
            //        //temp += (GratingDataContainer.Data[i][j] - GratingDataContainer.Data[0][j]);
            //        temp += GratingDataContainer.Data[i][j] - StateConstantParam.WaveLengthReference;
            //    }
            //    DELTABuffer.Add(temp * SamplingStep / GratingDataContainer.Data[0].Count);
            //}
            for (int i = 0; i < GratingDataContainer.Data.Length; i++)
            {
                for (int j = 0; j < GratingDataContainer.Data[i].Length; j++)
                {
                    temp = 0;
                    for (int k = 0; k < GratingDataContainer.Data[i][j].Count; 
                        k += (int)(GratingDataContainer.Data[i][j].Count / SamplingStep))
                    {
                        temp += GratingDataContainer.Data[i][j][k] - StateConstantParam.WaveLengthReference;
                    }
                    DELTABuffer[i].Add(temp * SamplingStep / GratingDataContainer.Data[i][j].Count);
                }
            }
        }

        /// <summary>
        /// Update all state data above when a timer all this method,
        /// this is the only way to update state data.
        /// </summary>
        public static void Calculate()
        {
            calculateDELTA();
            calculateStress();
            calculateStrain();
            calculateTemperature();
            calculateFrequency();
        }

        /// <summary>
        /// Get value by given gratingID and calculator
        /// </summary>
        /// <param name="gratingID"></param>
        /// <param name="cal"></param>
        /// <returns></returns>
        public static double Get(int ch, int gratingID, Calculator cal)
        {
            int chIndex = ch - 1;
            int gratingIndex = gratingID - 1;
            if (stress.Length < chIndex || stress[chIndex].Count <= gratingIndex)
            {
                return 0;
            }
            switch (cal)
            {
                case Calculator.Stress:
                    return stress[chIndex][gratingIndex];
                case Calculator.Strain:
                    return strain[chIndex][gratingIndex];
                case Calculator.Temperature:
                    return temperature[chIndex][gratingIndex];
                case Calculator.Frequency:
                    return frequency[chIndex][gratingIndex];
                default:
                    return 0;
            }
        }
    }

    public enum Calculator
    {
        Stress,
        Strain,
        Temperature,
        Frequency
    }
}
