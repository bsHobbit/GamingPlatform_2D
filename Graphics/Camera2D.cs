using Box2DX.Common;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Graphics
{
    public class Camera2D
    {
        Vec2 lookAt;
        public Vec2 LookAt
        {
            get => lookAt;
            set { lookAt = value; UpdateViewMatrix(); }
        }

        float rotation;
        public float Rotation
        {
            get => rotation;
            set { rotation = value; UpdateViewMatrix(); }
        }

        Vec2 screenSize;
        public Vec2 ScreenSize
        {
            get => screenSize;
            set { screenSize = value; UpdateViewMatrix(); }
        }

        float scale;
        public float Scale
        {
            get => scale;
            set { scale = value; UpdateViewMatrix(); }
        }
        

        public Matrix ViewMatrix { get; private set; }
        public Matrix InverseViewMatrix { get; private set; }

        public Camera2D()
        {
            scale = 1;
            UpdateViewMatrix();
        }

        void UpdateViewMatrix()
        {
            ViewMatrix = new Matrix();
            /*Make the LookAt the new center*/
            ViewMatrix.Translate(-lookAt.X, -lookAt.Y, MatrixOrder.Append);
            /*Rotate around new center*/
            ViewMatrix.Rotate(rotation, MatrixOrder.Append);
            /*Scale everything thats visible in the camera*/
            ViewMatrix.Scale(scale, scale, MatrixOrder.Append);
            /*Move the lookat center screen*/
            ViewMatrix.Translate((screenSize.X / 2), (screenSize.Y / 2), MatrixOrder.Append);

            InverseViewMatrix = ViewMatrix.Clone();
            InverseViewMatrix.Invert();
        }

        public void ZoomAt(Vec2 Location, bool ZoomIn)
        {
            float newScale = ZoomIn ? scale + .1f * scale : scale - .1f * scale;
            if (newScale >= .1f && newScale < 100f)
            {
                float Factor = newScale;
                PointF[] tmpPoints = new PointF[] { new PointF(Location.X, Location.Y) };
                ViewMatrix.TransformPoints(tmpPoints);
                Vec2 prevScreenLocation = new Vec2(tmpPoints[0].X, tmpPoints[0].Y);

                Scale = newScale;

                tmpPoints = new PointF[] { new PointF(Location.X, Location.Y) };
                ViewMatrix.TransformPoints(tmpPoints);
                Vec2 newScreenLocation = new Vec2(tmpPoints[0].X, tmpPoints[0].Y);

                Vec2 diff = (prevScreenLocation - newScreenLocation);
                diff = new Vec2(diff.X / Factor, diff.Y / Factor);
                LookAt -= diff;
            }
        }
    }
}
