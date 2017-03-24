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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChallengeCupV2.Models
{
    /// <summary>
    /// Shaft.xaml 的交互逻辑
    /// </summary>
    public partial class Shaft : UserControl, IModel
    {
        public Shaft()
        {
            InitializeComponent();
        }

        public AxisAngleRotation3D GetAxisAngleRotation()
        {
            throw new NotImplementedException();
        }

        public PerspectiveCamera GetCamera()
        {
            throw new NotImplementedException();
        }

        public GeometryModel3D GetModel()
        {
            throw new NotImplementedException();
        }

        public Viewport3D GetViewPort()
        {
            throw new NotImplementedException();
        }

        public void ResetView()
        {
            throw new NotImplementedException();
        }
    }
}
