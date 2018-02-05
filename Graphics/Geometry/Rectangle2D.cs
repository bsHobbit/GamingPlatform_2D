using Box2DX.Common;
using System.Drawing;

namespace Graphics.Geometry
{
    public class Rectangle2D : RenderableObject2D
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Rectangle2D(int Width, int Height, Vec2 Location, int Z, Color Color, Color OutlineColor, Texture2D Texture = null, float OutlineWidth = 1, float Scale = 1, float Rotation = 0, VerticeInterpretation Interpretation = VerticeInterpretation.Solid)
        {
            this.Width = this.Width;
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
    }
}
