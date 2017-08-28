using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace ChallengeCupV2.Models
{
    /// <summary>
    /// ModelAction define all actions model can do,
    /// zoom in and zoom out, rotate in vertial and horizontal
    /// </summary>
    public static class ModelAction
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
        /// Rotate style choosed of model, there are two style: vertical and horizontal
        /// </summary>
        private static RotateStyle rotateStyle;
        public enum RotateStyle
        {
            Horizontal, vertical
        }

        //private static int AutoRotationAngle = 0;
        public static void AutoRotation(this IModel model)
        {
            model.AutoRotation();
            //model.ResetView();
            //model.GetAxisAngleRotation().Axis = new Vector3D(0, 0, 1);
            //model.GetAxisAngleRotation().Angle += (AutoRotationAngle += AngleStep);
        }

        #region Zoom in and zoom out
        /// <summary>
        /// Zoom in and out of model
        /// 
        /// Set z of camera's positon by the input argument
        /// </summary>
        /// <param name="model"></param>
        /// <param name="z"></param>
        public static void Zoom(this IModel model, double z)
        {
            PerspectiveCamera camera = model.GetCamera();
            camera.Position = new Point3D(camera.Position.X, camera.Position.Y, camera.Position.Z - z / 10d);
        }

        #endregion

        #region Rotate
        /// <summary>
        /// Set model's rotate style
        /// 
        /// First reset model's view, without this model will have the
        /// angle got before, so for a comfortable view, reset is better.
        /// Then set rotateStyle, and set axis
        /// </summary>
        /// <param name="model"></param>
        /// <param name="rs"></param>
        public static void SetRotateStyle(this IModel model, RotateStyle rs)
        {
            model.ResetView();
            rotateStyle = rs;
            //model.GetAxisAngleRotation().Axis = rs == RotateStyle.vertical ?
            //    new Vector3D(1, 0, 0) : new Vector3D(0, 1, 0);
        }

        /// <summary>
        /// Set isMouseDown to be false
        /// </summary>
        /// <param name="model"></param>
        public static void MouseUp(this IModel model)
        {
            isMouseDown = false;
        }

        /// <summary>
        /// Set isMouseDown to be true
        /// 
        /// When mouse down, store the positon of current mouse.
        /// </summary>
        /// <param name="model"></param>
        /// <param name="e"></param>
        public static void MouseDown(this IModel model, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            var viewPort = model.GetViewPort();
            //Point pos = Mouse.GetPosition(viewPort);
            //mouseLastPos = new Point(pos.X - viewPort.ActualWidth / 2, viewPort.ActualHeight / 2 - pos.Y);
            mouseLastPos = Mouse.GetPosition(viewPort);
        }

        /// <summary>
        /// Rotate model
        /// 
        /// When mouse down and move, this method will be called,
        /// get the positon of current mouse, then calculate the difference
        /// between it and last mouse positon which was stored in mouseLastPos,
        /// then set the model's angle of AxisAngleRotation.
        /// 
        /// There is a speed up factor to enlarge the calculated angle, 
        /// cause calculated angle isn't big enough to set a comfortable view.
        /// </summary>
        /// <param name="model"></param>
        public static void Rotate(this IModel model)
        {
            if (isMouseDown == false)
            {
                return;
            }
            var viewPort = model.GetViewPort();
            Point curPos = Mouse.GetPosition(viewPort);
            //double diffX = curPos.X - mouseLastPos.X;
            //// Speed up factor is 16
            //model.GetAxisAngleRotation().Angle += diffX / viewPort.ActualWidth * 64 * Math.PI;
            double diff = rotateStyle == RotateStyle.Horizontal ?
                curPos.X - mouseLastPos.X : mouseLastPos.Y - curPos.Y;
#if DEBUG
            Console.WriteLine($"current position: {curPos.X}, {curPos.Y}");
            Console.WriteLine($"diff: {diff}");
#endif
            model.GetAxisAngleRotation().Axis = rotateStyle == RotateStyle.vertical ?
                new Vector3D(1, 0, 0) : new Vector3D(0, 1, 0);
            // Speed up factor is 16
            model.GetAxisAngleRotation().Angle += diff /
                (rotateStyle == RotateStyle.Horizontal ? viewPort.ActualWidth : -viewPort.ActualHeight)
                 * 64 * Math.PI;
            mouseLastPos = curPos;
        }
        #endregion

    }
}
