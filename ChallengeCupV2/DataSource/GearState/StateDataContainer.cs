using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChallengeCupV2.DataSource.GearState
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
            new StateDataTemplate(1, 1, "Stress", Calculator.Stress, "MPa", OutlierJudge.StressJudge),
            new StateDataTemplate(1, 1, "Strain", Calculator.Strain, "nε", OutlierJudge.StrainJudge),
            //new StateDataTemplate(1, 1, "Temperature", Calculator.Temperature, "℃", OutlierJudge.TemperatureJudge),
            //new StateDataTemplate(1, 1, "Frequency", Calculator.Frequency, "Hz", OutlierJudge.FrequencyJudge)
        };


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
            //if (StateData.Count == 4)
            if (StateData.Count != (from ch in GratingDataContainer.Data select ch.Length).Sum())
            {
                StateData.Clear();
                for (int i = 0; i < GratingDataContainer.Data.Length; i++)
                {
                    for (int j = 0; j < GratingDataContainer.Data[i].Length; j++)
                    {
                        if (GratingDataContainer.Data[i][j].Count <= 0)
                        {
                            continue;
                        }
                        StateData.Add(new StateDataTemplate(i + 1, j + 1, "Stress", Calculator.Stress, "MPa", OutlierJudge.StressJudge));
                        StateData.Add(new StateDataTemplate(i + 1, j + 1, "Strain", Calculator.Strain, "nε",  OutlierJudge.StrainJudge));
                        //StateData.Add(new StateDataTemplate(i + 1, j + 1, "Temperature", Calculator.Temperature, "℃", OutlierJudge.TemperatureJudge));
                        //StateData.Add(new StateDataTemplate(i + 1, j + 1, "Frequency", Calculator.Frequency, "Hz", OutlierJudge.FrequencyJudge));
                    }
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
