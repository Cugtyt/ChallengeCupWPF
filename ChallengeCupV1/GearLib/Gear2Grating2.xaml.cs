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

namespace ChallengeCupV1.GearLib
{
    /// <summary>
    /// Gear2Grating2.xaml 的交互逻辑
    /// </summary>
    public partial class Gear2Grating2 : UserControl, IGear, IGrating
    {
        public Gear2Grating2()
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

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}
