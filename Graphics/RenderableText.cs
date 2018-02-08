using Box2DX.Common;
using System.Drawing;

namespace Graphics
{
    public class RenderableText
    {
        public Font Font { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public bool ClipWithObject { get; set; }
        public Vec2 Offset { get; set; }

        public RenderableText(string Text, Font Font, Color Color, Vec2 Offset, bool ClipWithObject)
        {
            this.Text = Text;
            this.Font = Font;
            this.Color = Color;
            this.Offset = Offset;
            this.ClipWithObject = ClipWithObject;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Font.GetHashCode();
            hash = hash * 23 + Text.GetHashCode();
            hash = hash * 23 + Color.GetHashCode();
            hash = hash * 23 + ClipWithObject.GetHashCode();
            hash = hash * 23 + Offset.X.GetHashCode();
            hash = hash * 23 + Offset.Y.GetHashCode();
            return hash;
        }


        /*gdi+ stuff*/
        public void Draw(System.Drawing.Graphics g)
        {
            if (!string.IsNullOrEmpty(Text) && Font != null)
                g.DrawString(Text, Font, new SolidBrush(Color), Offset.X, Offset.Y);
        }

        public Vec2 GetSize()
        {
            Bitmap tmpImage = new Bitmap(10, 10);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(tmpImage);
            var tmpSize = g.MeasureString(Text, Font);
            g.Dispose();
            tmpImage.Dispose();
            return new Vec2(tmpSize.Width, tmpSize.Height);
        }

        public RenderableText Clone()
        {
            return new RenderableText(Text, Font, Color, Offset, ClipWithObject);
        }
    }
}
