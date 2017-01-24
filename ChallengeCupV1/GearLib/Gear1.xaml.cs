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
    /// Gear1.xaml 的交互逻辑
    /// </summary>
    public partial class Gear1 : UserControl, IGear
    {
        //public DataSource.GearStatusData Status = new DataSource.GearStatusData();

        public Gear1()
        {
            InitializeComponent();
        }

        public PerspectiveCamera GetCamera()
        {
            return camera;
        }

        public GeometryModel3D GetModel()
        {
            return gearModel;
        }

        
        public Viewport3D GetViewPort()
        {
            return viewPort;
        }

        public AxisAngleRotation3D GetAxisAngleRotation()
        {
            return axisAngleRotation;
        }

        public void Reset()
        {
            camera.Position = new Point3D(-9.53674351933387E-07, 0.000154495239200969, 220.326262603187);
            axisAngleRotation.Angle = 90;
        }
    }
}
