using Box2DX.Common;
using System.Collections.Generic;
using System.Drawing;


namespace Graphics.Geometry
{
    public class Line2D : RenderableObject2D
    {
        public Vec2 V1 { get; private set; }
        public Vec2 V2 { get; private set; }

        public Line2D(Vec2 V1, Vec2 V2, Vec2 Location, int Z, Color Color, float LineWidth = 1, float Scale = 1, float Rotation = 0)
        {
            this.V1 = V1;
            this.V2 = V2;
            List<Vec2> vertices = new List<Vec2>() { V1, V2 };
            Initialize(Location, 0, vertices, Color, Color, RectangleF.Empty, null, LineWidth, Scale, Rotation, VerticeInterpretation.Lines);
            RotationOffset = (V1 - V2) * .5f;
        }
    }
}
