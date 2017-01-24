using ChallengeCupV1.DataSource;
using ChallengeCupV1.DataSource.GearStatus;
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
            // Init text box
            statusReportDir.Text = SettingData.StatusReportDir;
            E_PARM.Text = StatusConstantParam.E.ToString();
            u_PARM.Text = StatusConstantParam.u.ToString();
            delta_PARM.Text = StatusConstantParam.delta.ToString();
            alpha_PARM.Text = StatusConstantParam.alpha.ToString();
        }

      
        private void browseFile_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            string dir = dialog.SelectedPath.Trim();
            statusReportDir.Text = dir;
        }

        private void apply_Click(object sender, RoutedEventArgs e)
        {
            SettingData.StatusReportDir = statusReportDir.Text;
            StatusConstantParam.E = double.Parse(E_PARM.Text);
            StatusConstantParam.u = double.Parse(u_PARM.Text);
            StatusConstantParam.delta = double.Parse(delta_PARM.Text);
            StatusConstantParam.alpha = double.Parse(alpha_PARM.Text);
        }
    }
}
