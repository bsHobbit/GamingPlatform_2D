using Box2DX.Common;
using System.Collections.Generic;
using System.Drawing;

namespace Graphics.Geometry
{
    public class Rectangle2D : RenderableObject2D
    {
        public Rectangle2D(int Widht, int Height, Vec2 Location, int Z, Color Color, Color OutlineColor, Texture2D Texture = null, float OutlineWidth = 1, float Scale = 1, float Rotation = 0, VerticeInterpretation Interpretation = VerticeInterpretation.Solid)
        {
            
            Initialize(Location, Z, CreateRectangle2D(Widht, Height), Color, OutlineColor, null, Texture, OutlineWidth, Scale, Rotation, Interpretation);
            RotationOffset = new Vec2(Widht / 2, Height / 2);
        }
    }
}
