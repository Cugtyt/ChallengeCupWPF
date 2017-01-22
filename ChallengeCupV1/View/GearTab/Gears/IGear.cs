﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Media3D;

namespace ChallengeCupV1.View.GearTab.Gears
{
    /// <summary>
    /// All gear in Gears(which is lib of gear) must implement iterface IGear
    /// </summary>
    public interface IGear
    {
        PerspectiveCamera GetCamera();
        GeometryModel3D GetModel();
        Viewport3D GetViewPort();
        AxisAngleRotation3D GetAxisAngleRotation();
    }
}
