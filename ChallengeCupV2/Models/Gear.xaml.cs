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
    /// Gear.xaml 的交互逻辑
    /// </summary>
    public partial class Gear : UserControl, IModel
    {
        public Gear()
        {
            InitializeComponent();
        }

        public AxisAngleRotation3D GetAxisAngleRotation()
        {
            return axisAngleRotation;
        }

        static int AutoRotationAngle = 0;
        public void AutoRotation()
        {
            ResetView();
            axisAngleRotation.Axis = new Vector3D(0, 0, 1);
            axisAngleRotation.Angle += (AutoRotationAngle += 1);
        }

        public PerspectiveCamera GetCamera()
        {
            return camera;
        }

        public GeometryModel3D GetModel()
        {
            return model;
        }

        public Viewport3D GetViewPort()
        {
            return viewPort;
        }

        public void ResetView()
        {
            camera.Position = new Point3D(3.4332275191673E-05, -0.000160217284999931, 373.953315783106);
            axisAngleRotation.Angle = 0;
        }
    }
}
