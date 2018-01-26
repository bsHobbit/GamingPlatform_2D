using Box2DX.Common;
using System.Collections.Generic;
using System.Drawing;

namespace Graphics.Geometry
{
    public class Rectangle2D : RenderableObject2D
    {
        public Rectangle2D(int Widht, int Height, Vec2 Location, int Z, Color Color, Color OutlineColor, Texture2D Texture = null, float OutlineWidth = 1, float Scale = 1, float Rotation = 0, VerticeInterpretation Interpretation = VerticeInterpretation.Solid)
        {
            List<Vec2> vertices = new List<Vec2>() { new Vec2(0, 0), new Vec2(Widht, 0), new Vec2(Widht, Height), new Vec2(0, Height) };
            Initialize(Location, Z, vertices, Color, OutlineColor, Texture, OutlineWidth, Scale, Rotation, Interpretation);
            RotationOffset = new Vec2(Widht / 2, Height / 2);
        }

        public override void Update(float Elapsed)
        {
        }
    }
}
