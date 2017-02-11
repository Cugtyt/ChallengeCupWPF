using ChallengeCupV1.DataSource;
using ChallengeCupV1.DataSource.GearState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChallengeCupV1.View.SettingTab
{
    /// <summary>
    /// SettingTabContent.xaml 的交互逻辑
    /// </summary>
    public partial class SettingTabContent : UserControl
    {
        public SettingTabContent()
        {
            InitializeComponent();
            UserControlManager.Register(this, this.GetType().Name);
            // Init text box
            stateReportDir.Text = SettingContainer.StateReportDir;
            maxYTimeDomain.Text = SettingContainer.MaxYWavePlotTimeDomain.ToString();
            minYTimeDomain.Text = SettingContainer.MinYWavePlotTimeDomain.ToString();
            initTemperature.Text = SettingContainer.InitTemperature.ToString();
            //referYTimeDomain.Text = SettingContainer.ReferYWavePlotTimeDomain.ToString();
            referYTimeDomain.Text = StateConstantParam.WaveLengthReference.ToString();
            E_PARM.Text = StateConstantParam.E.ToString();
            u_PARM.Text = StateConstantParam.u.ToString();
            delta_PARM.Text = StateConstantParam.DELTA.ToString();
            alpha_PARM.Text = StateConstantParam.ALPHA.ToString();
            gearWidth.Text = StateConstantParam.GEAR_WIDTH.ToString();
        }

        /// <summary>
        /// Brower and set generate report dir path of setting data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseDir_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string dir = dialog.SelectedPath.Trim();
            stateReportDir.Text = dir;
        }

        /// <summary>
        /// Apply all settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void apply_Click(object sender, RoutedEventArgs e)
        {
            SettingContainer.StateReportDir = stateReportDir.Text;
            SettingContainer.MaxYWavePlotTimeDomain = double.Parse(maxYTimeDomain.Text);
            SettingContainer.MinYWavePlotTimeDomain = double.Parse(minYTimeDomain.Text);
            SettingContainer.InitTemperature = double.Parse(initTemperature.Text);
            //SettingContainer.ReferYWavePlotTimeDomain = double.Parse(referYTimeDomain.Text);
            StateConstantParam.WaveLengthReference = double.Parse(referYTimeDomain.Text);
            StateConstantParam.E = double.Parse(E_PARM.Text);
            StateConstantParam.u = double.Parse(u_PARM.Text);
            StateConstantParam.DELTA = double.Parse(delta_PARM.Text);
            StateConstantParam.ALPHA = double.Parse(alpha_PARM.Text);
            StateConstantParam.GEAR_WIDTH = double.Parse(gearWidth.Text);
            (UserControlManager.Get("WavePlot") as WaveTab.WavePlot).UpdateYRange();
            (UserControlManager.Get("FunctionBar") as FunctionBar).UpdateDir();
        }
    }
}
