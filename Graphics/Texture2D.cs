using System.Drawing;
using Core.ComponentModel;

namespace Graphics
{
    public class Texture2D : Component 
    {
        Bitmap Bitmap;
        public RectangleF Segment;
        public int Width;
        public int Height;

        Texture2D(Bitmap Bitmap, RectangleF Segment)
        {
            this.Bitmap = ToDispose(Bitmap);
            this.Segment = Segment;
            Width = Bitmap.Width;
            Height = Bitmap.Height;
        }

        public void Draw(System.Drawing.Graphics g, float X, float Y, float TargetWidth, float TargetHeight)
        {
            /*Render*/
            g.DrawImage(Bitmap,
                        new RectangleF(X, Y, TargetWidth, TargetHeight),
                        Segment,
                        GraphicsUnit.Pixel);
        }


        public static Texture2D FromFile(string Path)
        {
            if (System.IO.File.Exists (Path))
            {
                try
                {
                    Bitmap bitmap = (Bitmap)Image.FromFile(Path);
                    if (bitmap == null)
                        throw new System.Exception("Unsupported image format :(");
                    else
                    {
                        return new Texture2D(bitmap, new RectangleF(0, 0, bitmap.Width, bitmap.Height));
                    }
                }
                catch (System.Exception e)
                {
                    throw new System.Exception("Unable to load Texture!\n" + e.ToString());
                }
                
            }
            throw new System.Exception("File not found!\n" + Path);
        }
    }
}
