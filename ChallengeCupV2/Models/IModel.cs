using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace ChallengeCupV2.Models
{
    /// <summary>
    /// IModel is a identification of models to be shown in ModelTabContent
    /// </summary>
    public interface IModel
    {
        PerspectiveCamera GetCamera();
        GeometryModel3D GetModel();
        Viewport3D GetViewPort();
        AxisAngleRotation3D GetAxisAngleRotation();
        void ResetView();
    }
}
