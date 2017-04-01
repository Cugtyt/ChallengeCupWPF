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
            return axisAngleRotation;
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
            camera.Position = new Point3D(-6.404708862305, 0.303300857543901, 664.503122417004);
            axisAngleRotation.Angle = 0; 
        }
    }
}
