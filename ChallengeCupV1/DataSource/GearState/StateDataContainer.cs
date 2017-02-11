using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearState
{
    /// <summary>
    /// StatusDataContainer contains a list of StatusDataTemplate
    /// in which all status data are stored.
    /// </summary>
    public class StateDataContainer
    {
        /// <summary>
        /// All status data are stored in StatusData. when new param 
        /// is needed, add a new StatusDataTemplate object to it.
        /// </summary>
        public ObservableCollection<StateDataTemplate> StateData = new ObservableCollection<StateDataTemplate>()
        {
            new StateDataTemplate(1, "Stress", Calculator.Stress, "N"),
            new StateDataTemplate(1, "Strain", Calculator.Strain, "N"),
            new StateDataTemplate(1, "Temperature", Calculator.Temperature, "℃"),
            new StateDataTemplate(1, "Frequency", Calculator.Frequency, "Hz")
        };

        /// <summary>
        /// Calculate and update the value of each status value in StatusData
        /// </summary>
        //        public void Calculate()
        //        {
        //#if DEBUG
        //            Console.WriteLine("StatusDataContainer: Calculate() -> calculating");
        //#endif
        //            //StatusData[0].Calculate(new List<double>() { });
        //            //StatusData[1].Calculate(new List<double>() { });
        //            //StatusData[2].Calculate(new List<double>() { });
        //            //StatusData[3].Calculate(new List<double>() { });
        //            //StatusData[0].Value = 1;
        //            //StatusData[1].Value = 2;
        //            //StatusData[2].Value = 3;
        //            //StatusData[3].Value = 4;
        //            //StatusCalculator.CalculateDELTA();
        //            //foreach (var s in StatusData)
        //            //{
        //            //    s.Calculate();
        //            //}
        //        }

        /// <summary>
        /// Update StatusData
        /// 
        /// If StateData is not suit for Data in GratingDataContainer, update StateData,
        /// and call Calculate method in StateCalculator to update data set,
        /// then get the result value for each element in StateData
        /// </summary>
        public void Update()
        {
#if DEBUG
            Console.WriteLine("StateDataContainer: Update()");
#endif
            if (GratingDataContainer.Data.Length - 1 != StateData.Count / 4)
            {
                StateData.Clear();
                for (int i = 0; i < GratingDataContainer.Data.Length - 1; i++)
                {
                    StateData.Add(new StateDataTemplate(i + 1, "Stress", Calculator.Stress, "N"));
                    StateData.Add(new StateDataTemplate(i + 1, "Strain", Calculator.Strain, "N"));
                    StateData.Add(new StateDataTemplate(i + 1, "Temperature", Calculator.Temperature, "℃"));
                    StateData.Add(new StateDataTemplate(i + 1, "Frequency", Calculator.Frequency, "Hz"));
                }
            }
            StateCalculator.Calculate();
            foreach (var st in StateData)
            {
                st.Get();
            }
        }
    }
}
