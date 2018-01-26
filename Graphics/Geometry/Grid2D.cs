using Box2DX.Common;
using System.Collections.Generic;
using System.Drawing;


namespace Graphics.Geometry
{
    public class Grid2D : RenderableObject2D
    {
        public Grid2D(Vec2 Location, int Width, int Height, int TileWidth, int TileHeight,  int Z, Color Color, float LineWidth = 1, float Scale = 1, float Rotation = 0)
        {
            List<Vec2> vertices = new List<Vec2>();

            int cntX = Width / TileWidth + 1;
            int cntY = Height / TileHeight + 1;

            for (int x = 0; x < cntX; x++)
            {
                vertices.Add(new Vec2(x * TileWidth, 0));
                vertices.Add(new Vec2(x * TileWidth, Height));
            }
            for (int y = 0; y < cntY; y++)
            {
                vertices.Add(new Vec2(0, y * TileHeight));
                vertices.Add(new Vec2(Width, y * TileHeight));
            }

            Initialize(Location, Z, vertices, Color, Color, null, null, LineWidth, Scale, Rotation, VerticeInterpretation.Lines);
            RotationOffset = new Vec2(Width / 2, Height / 2);
        }
    }
}
