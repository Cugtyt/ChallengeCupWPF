using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using System.Windows.Threading;

namespace ChallengeCupV1.GearLib
{
    public static class GearAction
    {
        private static bool isMouseDown;
        private static Point mouseLastPos;
        private static RotateStyle rotateStyle;
        public enum RotateStyle
        {
            Horizontal, vertical
        }

        #region Zoom in and zoom out
        public static void Zoom(this IGear gear, double z)
        {
            PerspectiveCamera camera = gear.GetCamera();
            camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - z / 10d);
        }

        #endregion

        #region Rotate
        public static void SetRotateStyle(this IGear gear, RotateStyle rs)
        {
            gear.Reset();
            rotateStyle = rs;
            gear.GetAxisAngleRotation().Axis = rs == RotateStyle.vertical ?
                new Vector3D(1, 0, 0) : new Vector3D(0, 1, 0);
        }

        public static void MouseUp(this IGear gear)
        {
            isMouseDown = false;
        }

        public static void MouseDown(this IGear gear, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            var viewPort = gear.GetViewPort();
            //Point pos = Mouse.GetPosition(viewPort);
            //mouseLastPos = new Point(pos.X - viewPort.ActualWidth / 2, viewPort.ActualHeight / 2 - pos.Y);
            mouseLastPos = Mouse.GetPosition(viewPort);
        }

        public static void MouseMove(this IGear gear)
        {
            if (isMouseDown == false)
            {
                return;
            }
            var viewPort = gear.GetViewPort();
            Point curPos = Mouse.GetPosition(viewPort);
            //double diffX = curPos.X - mouseLastPos.X;
            //// Speed up factor is 16
            //gear.GetAxisAngleRotation().Angle += diffX / viewPort.ActualWidth * 64 * Math.PI;
            double diff = rotateStyle == RotateStyle.Horizontal ?
                curPos.X - mouseLastPos.X : mouseLastPos.Y - curPos.Y;
#if DEBUG
            Console.WriteLine($"current position: {curPos.X}, {curPos.Y}");
            Console.WriteLine($"diff: {diff}");
#endif
            // Speed up factor is 16
            gear.GetAxisAngleRotation().Angle += diff / 
                (rotateStyle == RotateStyle.Horizontal ? viewPort.ActualWidth : viewPort.ActualHeight )
                 * 64 * Math.PI;
            mouseLastPos = curPos;
        }
        #endregion

    }
}
