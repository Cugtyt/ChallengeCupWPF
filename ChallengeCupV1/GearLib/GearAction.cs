using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace ChallengeCupV1.GearLib
{
    public static class GearAction
    {
        private static bool isMouseDown;
        private static Point mouseLastPos;

        #region Zoom in and zoom out
        public static void Zoom(this IGear gear, double z)
        {
            PerspectiveCamera camera = gear.GetCamera();
            camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - z / 10d);
        }

        #endregion

        #region Rotate

        public static void MouseUp(this IGear gear)
        {
            isMouseDown = false;
        }

        public static void MouseDown(this IGear gear, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            var viewPort = gear.GetViewPort();
            Point pos = Mouse.GetPosition(viewPort);
            mouseLastPos = new Point(pos.X - viewPort.ActualWidth / 2, viewPort.ActualHeight / 2 - pos.Y);
        }

        public static void MouseMove(this IGear gear)
        {
            if (!isMouseDown)
            {
                return;
            }
            var viewPort = gear.GetViewPort();
            Point curPos = Mouse.GetPosition(viewPort);
            double diff = curPos.X - mouseLastPos.X;
            // Speed up factor is 16
            gear.GetAxisAngleRotation().Angle += diff / viewPort.ActualWidth * 64 * Math.PI;
            mouseLastPos = curPos;
        }
        #endregion

    }
}
