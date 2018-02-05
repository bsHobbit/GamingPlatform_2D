using Box2DX.Common;
using System.Drawing;

namespace Graphics.Geometry
{
    public class Rectangle2D : RenderableObject2D
    {
        static Rectangle2D TranslatingRect;

        public int Width { get; private set; }
        public int Height { get; private set; }

        bool IsTranslationEnabled;

        public Rectangle2D(int Width, int Height, Vec2 Location, int Z, Color Color, Color OutlineColor, Texture2D Texture = null, float OutlineWidth = 1, float Scale = 1, float Rotation = 0, VerticeInterpretation Interpretation = VerticeInterpretation.Solid)
        {
            this.Width = Width;
            this.Height = Height;
            Initialize(Location, Z, CreateRectangle2D(Width, Height), Color, OutlineColor, null, Texture, OutlineWidth, Scale, Rotation, Interpretation);
            RotationOffset = new Vec2(Width / 2, Height / 2);
        }

        public void Update(Vec2 Location, int Width, int Height)
        {
            this.Width = Width;
            this.Height = Height;
            Vertices = CreateRectangle2D(Width, Height);
            this.Location = Location;
        }


        /*User-Translation*/

        public void EnableUserTranslation()
        {
            if (!IsTranslationEnabled)
            {
                MouseLeave += Rectangle2D_MouseLeave;
                MouseDown += Rectangle2D_MouseDown;
                MouseUp += Rectangle2D_MouseUp;
                MouseMove += Rectangle2D_MouseMove;

                IsTranslationEnabled = true;
            }
        }

        public void DisableUserTranslation()
        {
            if (IsTranslationEnabled)
            {
                if (TranslatingRect == this)
                    TranslatingRect = null;

                MouseLeave -= Rectangle2D_MouseLeave;
                MouseDown -= Rectangle2D_MouseDown;
                MouseUp -= Rectangle2D_MouseUp;
                MouseMove -= Rectangle2D_MouseMove;

                IsTranslationEnabled = false;
            }
        }

        private void Rectangle2D_MouseLeave(RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            if (TranslatingRect == this)
                Location -= MouseEventArgs.Diff;
        }

        private void Rectangle2D_MouseDown(RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            if (TranslatingRect == null || TranslatingRect == this)
            {
                if (MouseEventArgs.CurrentState[Core.IO.MouseButtons.Left] == Core.IO.ButtonState.Pressed)
                    TranslatingRect = this;
            }
        }

        private void Rectangle2D_MouseUp(RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            if (TranslatingRect == this && MouseEventArgs.CurrentState[Core.IO.MouseButtons.Left] == Core.IO.ButtonState.Released)
                TranslatingRect = null;
        }

        private void Rectangle2D_MouseMove(RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            if (IsTranslationEnabled && TranslatingRect == this)
                Location -= MouseEventArgs.Diff;
        }

    }
}
