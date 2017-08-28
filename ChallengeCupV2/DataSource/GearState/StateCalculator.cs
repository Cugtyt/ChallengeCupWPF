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
        private static List<double>[] average = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] DELTABuffer = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] stress = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        private static List<double>[] strain = new List<double>[4] 
        { new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        //private static List<double>[] temperature = new List<double>[4] 
        //{ new List<double>(), new List<double>(), new List<double>(), new List<double>() };
        //private static List<double>[] frequency = new List<double>[4] 
        //{ new List<double>(), new List<double>(), new List<double>(), new List<double>() };

        //public static List<Complex[]>[] FFTResults = new List<Complex[]>[4] 
        //{ new List<Complex[]>(), new List<Complex[]>(), new List<Complex[]>(), new List<Complex[]>() };
        //private static List<double>[][] dataClone;
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
            // Check data validation first
            if (DELTABuffer[0].Count == 0 && DELTABuffer[1].Count == 0 
                && DELTABuffer[2].Count == 0 && DELTABuffer[3].Count == 0)
            {
                return;
            }
            for (int i = 0; i < stress.Length; i++)
            {
                stress[i].Clear();
                stress[i].AddRange(from delta in DELTABuffer[i]
                                   select StateConstantParam.StressMultiplier * delta);
                                   //select -1 * StateConstantParam.E * delta
                                   /// (StateConstantParam.u * (Math.Pow(StateConstantParam.DELTA, StateConstantParam.GEAR_WIDTH))
                                   //* StateConstantParam.ALPHA));
            }
        }

        /// <summary>
        /// 应变
        ///     ε_y = Δλ_B / α_ε
        /// Param:
        ///     Δλ_B -- 反射光波偏移量
        ///     α_ɛ -- 对于中心波长处于1550nm附近的光栅，其灵敏度系数大约为α_ɛ=1.2pm/μɛ
        ///     Note:
        ///         To reduce the deviation, add some params, let delta lambda wavelength * 1000 - 180,
        ///         then * 1.6 according to experiment.
        /// Input:
        ///     Δλ_B -- 反射光波偏移量
        ///         ---- 需要基准值和实时值
        /// Output:
        ///     ε_y -- 应变
        /// </summary>
        private static void calculateStrain()
        {
            // Check data validation first
            if (DELTABuffer[0].Count == 0 && DELTABuffer[1].Count == 0
                && DELTABuffer[2].Count == 0 && DELTABuffer[3].Count == 0)
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
                                   select (delta * 1000 / StateConstantParam.ALPHA - 180) * 1.6);
            }
        }

        /// <summary>
        /// Calculate temperature
        /// 
        /// Temperature change 1 ℃ when delta of wavelength change 10pm.
        /// </summary>
        //private static void calculateTemperature()
        //{
        //    // Check data validation first
        //    if (DELTABuffer[0].Count == 0 && DELTABuffer[1].Count == 0
        //        && DELTABuffer[2].Count == 0 && DELTABuffer[3].Count == 0)
        //    {
        //        return;
        //    }
        //    //temperature.Clear();
        //    //temperature.AddRange(from delta in DELTABuffer
        //    //                select delta / 10 + SettingContainer.InitTemperature);
        //    for (int i = 0; i < temperature.Length; i++)
        //    {
        //        temperature[i].Clear();
        //        temperature[i].AddRange(from delta in DELTABuffer[i]
        //                                select delta / 10 + SettingContainer.InitTemperature);
        //    }
        //}

        /// <summary>
        /// Calculate frequency of gear 
        /// </summary>
        //private static void calculateFrequency()
        //{
        //    // Check data validation first
        //    if (DELTABuffer[0].Count == 0 && DELTABuffer[1].Count == 0
        //        && DELTABuffer[2].Count == 0 && DELTABuffer[3].Count == 0)
        //    {
        //        return;
        //    }
        //    lock (FFTResults)
        //    {
        //        //FFTResults.Clear();
        //        //frequency.Clear();
        //        //for (int i = 0; i < GratingDataContainer.Data.Length; i++)
        //        //{
        //        //    var temp = (from y in GratingDataContainer.Data[i]
        //        //                select new Complex(y, 0)).ToArray();
        //        //    FFT.DataFFT.Forward(temp);
        //        //    FFTResults.Add(temp);
        //        //    double max = FFTResults[i][0].Real;
        //        //    int maxIndex = 0;
        //        //    for (int j = 1; j < FFTResults[i].Length / 2; j++)
        //        //    {
        //        //        if (FFTResults[i][j].Real > max)
        //        //        {
        //        //            max = FFTResults[i][j].Real;
        //        //            maxIndex = j;
        //        //        }
        //        //    }
        //        //    frequency.Add(maxIndex * StateConstantParam.DemodulationFrequency / temp.Length);
        //        for (int i = 0; i < frequency.Length; i++)
        //        {
        //            frequency[i].Clear();
        //            FFTResults[i].Clear();
        //            for (int j = 0; j < GratingDataContainer.Data[i].Length; j++)
        //            {
        //                if (GratingDataContainer.Data[i][j].Count == 0)
        //                {
        //                    FFTResults[i].Add(new Complex[1]);
        //                    continue;
        //                }

        //                //var temp = (from y in dataClone[i][j]
        //                //            select new Complex(y, 0)).ToArray();
        //                List<Complex> cl = new List<Complex>();
        //                //foreach (var y in dataClone[i][j])
        //                //{
        //                //    cl.Add(new Complex(y, 0));
        //                //}
        //                for (int m = 0; m < GratingDataContainer.Data[i][j].Count / 2; m++)
        //                {
        //                    cl.Add(new Complex(GratingDataContainer.Data[i][j][m], 0));
        //                }
        //                var temp = cl.ToArray();
        //                FFT.DataFFT.Forward(temp);
        //                FFTResults[i].Add(temp);
        //                //double max = FFTResults[i][j][1].Real;
        //                double max = 0;
        //                int maxIndex = 0;
        //                for (int k = 2; k < FFTResults[i][j].Length / 2; k++)
        //                {
        //                    if (FFTResults[i][j][k].Real > max)
        //                    {
        //                        max = FFTResults[i][j][k].Real;
        //                        maxIndex = k;
        //                    }
        //                }
        //                frequency[i].Add(maxIndex * StateConstantParam.DemodulationFrequency / temp.Length);
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// Calculate delta and store result in DELTABuffer
        /// </summary>
        private static void calculateDELTAAndAve()
        {
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            // Clear delta buffer
            for (int i = 0; i < DELTABuffer.Length; i++)
            {
                DELTABuffer[i].Clear();
                average[i].Clear();
            }
            //DELTABuffer.Clear();
            double temp;
            double max;
            double min;
            for (int i = 0; i < GratingDataContainer.Data.Length; i++)
            {
                for (int j = 0; j < GratingDataContainer.Data[i].Length; j++)
                {
                    if (GratingDataContainer.Data[i][j].Count <= 0)
                    {
                        continue;
                    }
                    temp = min = max = GratingDataContainer.Data[i][j][0];
                    for (int k = 0; k < GratingDataContainer.Data[i][j].Count; k++)
                    {
                        temp += GratingDataContainer.Data[i][j][k];
                        max = max > GratingDataContainer.Data[i][j][k] ? max : GratingDataContainer.Data[i][j][k];
                        min = min < GratingDataContainer.Data[i][j][k] ? min : GratingDataContainer.Data[i][j][k];
                    }
                    average[i].Add(temp / GratingDataContainer.Data[i][j].Count);
                    //DELTABuffer[i].Add(GratingDataContainer.Data[i][j].Max() - GratingDataContainer.Data[i][j].Min());
                    DELTABuffer[i].Add(max - min);
                }
            }
        }

        /// <summary>
        /// Update all state data above when a timer all this method,
        /// this is the only way to update state data.
        /// </summary>
        public static void Calculate()
        {
            lock (typeof(StateCalculator))
            {
                calculateDELTAAndAve();
                calculateStress();
                calculateStrain();
                //calculateTemperature();
                //calculateFrequency();
            }
        }

        /// <summary>
        /// Get value by given gratingID and calculator
        /// </summary>
        /// <param name="grating"></param>
        /// <param name="cal"></param>
        /// <returns></returns>
        public static double GetParam(int ch, int grating, Calculator cal)
        {
            int chIndex = ch - 1;
            int gratingIndex = grating - 1;
            switch (cal)
            {
                case Calculator.Stress:
                    if (chIndex > stress.Length || grating > stress[chIndex].Count)
                    {
                        return 0;
                    }
                    return stress[chIndex][gratingIndex];
                case Calculator.Strain:
                    if (chIndex > strain.Length || grating > strain[chIndex].Count)
                    {
                        return 0;
                    }
                    return strain[chIndex][gratingIndex];
                //case Calculator.Temperature:
                //    if (chIndex > temperature.Length || grating > temperature[chIndex].Count)
                //    {
                //        return 0;
                //    }
                //    return temperature[chIndex][gratingIndex];
                //case Calculator.Frequency:
                //    if (chIndex > frequency.Length || grating > frequency[chIndex].Count)
                //    {
                //        return 0;
                //    }
                //    return frequency[chIndex][gratingIndex];
                default:
                    return 0;
            }
        }

        /// <summary>
        /// Get average wavelength for para display
        /// </summary>
        /// <param name="ch"></param>
        /// <param name="grating"></param>
        /// <returns></returns>
        public static double GetAve(int ch, int grating)
        {
            int chIndex = ch - 1;
            int gratingIndex = grating - 1;
            if (average.Length <= chIndex || average[chIndex].Count <= gratingIndex)
            {
                return 0;
            }
            return average[chIndex][gratingIndex];
        }

        public static double GetDELTA(int ch, int grating)
        {
            int chIndex = ch - 1;
            int gratingIndex = grating - 1;
            if (DELTABuffer.Length <= chIndex || DELTABuffer[chIndex].Count <= gratingIndex)
            {
                return 0;
            }
            return DELTABuffer[chIndex][gratingIndex];
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
