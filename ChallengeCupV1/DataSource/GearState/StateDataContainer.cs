using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV1.DataSource.GearState
{
    /// <summary>
    /// StateDataContainer contains a list of StateDataTemplate
    /// in which all state data are stored.
    /// </summary>
    public class StateDataContainer
    {
        /// <summary>
        /// All state data are stored in StateData. when new param 
        /// is needed, add a new StateDataTemplate object to it.
        /// </summary>
        public ObservableCollection<StateDataTemplate> StateData = new ObservableCollection<StateDataTemplate>()
        {
            new StateDataTemplate(1, "Stress", Calculator.Stress, "N"),
            new StateDataTemplate(1, "Strain", Calculator.Strain, "%"),
            new StateDataTemplate(1, "Temperature", Calculator.Temperature, "℃"),
            new StateDataTemplate(1, "Frequency", Calculator.Frequency, "Hz")
        };

        /// <summary>
        /// Calculate and update the value of each state value in StateData
        /// </summary>
        //        public void Calculate()
        //        {
        //#if DEBUG
        //            Console.WriteLine("StateDataContainer: Calculate() -> calculating");
        //#endif
        //            //StateData[0].Calculate(new List<double>() { });
        //            //StateData[1].Calculate(new List<double>() { });
        //            //StateData[2].Calculate(new List<double>() { });
        //            //StateData[3].Calculate(new List<double>() { });
        //            //StateData[0].Value = 1;
        //            //StateData[1].Value = 2;
        //            //StateData[2].Value = 3;
        //            //StateData[3].Value = 4;
        //            //StateCalculator.CalculateDELTA();
        //            //foreach (var s in StateData)
        //            //{
        //            //    s.Calculate();
        //            //}
        //        }

        /// <summary>
        /// Update StateData
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
            if (!GratingDataContainer.IsDataReady)
            {
                return;
            }
            if (GratingDataContainer.Data.Length != StateData.Count / 4)
            {
                StateData.Clear();
                for (int i = 0; i < GratingDataContainer.Data.Length; i++)
                {
                    StateData.Add(new StateDataTemplate(i + 1, "Stress", Calculator.Stress, "N"));
                    StateData.Add(new StateDataTemplate(i + 1, "Strain", Calculator.Strain, "%"));
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
