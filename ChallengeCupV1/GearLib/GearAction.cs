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
    /// <summary>
    /// GearAction define all actions gear model can do,
    /// zoom in and zoom out, rotate in vertial and horizontal
    /// </summary>
    public static class GearAction
    {
        /// <summary>
        /// If mouse is down now
        /// </summary>
        private static bool isMouseDown;
        /// <summary>
        /// Store last position of mouse
        /// </summary>
        private static Point mouseLastPos;
        /// <summary>
        /// Rotate style choosed of gear, there are two style: vertical and horizontal
        /// </summary>
        private static RotateStyle rotateStyle;
        public enum RotateStyle
        {
            Horizontal, vertical
        }

        private static int AutoRotationAngle = 0;
        public static void AutoRotation(this IGear gear, int AngleStep = 1)
        {
            gear.ResetView();
            gear.GetAxisAngleRotation().Axis = new Vector3D(0, 0, 1);
            gear.GetAxisAngleRotation().Angle += (AutoRotationAngle += AngleStep);
        }

        #region Zoom in and zoom out
        /// <summary>
        /// Zoom in and out of gear
        /// 
        /// Set z of camera's positon by the input argument
        /// </summary>
        /// <param name="gear"></param>
        /// <param name="z"></param>
        public static void Zoom(this IGear gear, double z)
        {
            PerspectiveCamera camera = gear.GetCamera();
            camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - z / 10d);
        }

        #endregion

        #region Rotate
        /// <summary>
        /// Set gear's rotate style
        /// 
        /// First reset gear's view, without this gear will have the
        /// angle got before, so for a comfortable view, reset is better.
        /// Then set rotateStyle, and set axis
        /// </summary>
        /// <param name="gear"></param>
        /// <param name="rs"></param>
        public static void SetRotateStyle(this IGear gear, RotateStyle rs)
        {
            gear.ResetView();
            rotateStyle = rs;
            //gear.GetAxisAngleRotation().Axis = rs == RotateStyle.vertical ?
            //    new Vector3D(1, 0, 0) : new Vector3D(0, 1, 0);
        }

        /// <summary>
        /// Set isMouseDown to be false
        /// </summary>
        /// <param name="gear"></param>
        public static void MouseUp(this IGear gear)
        {
            isMouseDown = false;
        }

        /// <summary>
        /// Set isMouseDown to be true
        /// 
        /// When mouse down, store the positon of current mouse.
        /// </summary>
        /// <param name="gear"></param>
        /// <param name="e"></param>
        public static void MouseDown(this IGear gear, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            var viewPort = gear.GetViewPort();
            //Point pos = Mouse.GetPosition(viewPort);
            //mouseLastPos = new Point(pos.X - viewPort.ActualWidth / 2, viewPort.ActualHeight / 2 - pos.Y);
            mouseLastPos = Mouse.GetPosition(viewPort);
        }

        /// <summary>
        /// Rotate gear
        /// 
        /// When mouse down and move, this method will be called,
        /// get the positon of current mouse, then calculate the difference
        /// between it and last mouse positon which was stored in mouseLastPos,
        /// then set the gear's angle of AxisAngleRotation.
        /// 
        /// There is a speed up factor to enlarge the calculated angle, 
        /// cause calculated angle isn't big enough to set a comfortable view.
        /// </summary>
        /// <param name="gear"></param>
        public static void Rotate(this IGear gear)
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
            gear.GetAxisAngleRotation().Axis = rotateStyle == RotateStyle.vertical ?
                new Vector3D(1, 0, 0) : new Vector3D(0, 1, 0);
            // Speed up factor is 16
            gear.GetAxisAngleRotation().Angle += diff / 
                (rotateStyle == RotateStyle.Horizontal ? viewPort.ActualWidth : viewPort.ActualHeight )
                 * 64 * Math.PI;
            mouseLastPos = curPos;
        }
        #endregion

    }
}
