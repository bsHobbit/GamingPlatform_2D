using System.Drawing;
using Core.ComponentModel;
using System.Collections.Generic;

namespace Graphics
{
    public class Texture2D : Component 
    {
        Bitmap Bitmap;
        Dictionary<int, Bitmap> Thumbnails;
        public int Width;
        public int Height;
        public bool IsValid { get; private set; }

        public Texture2D(Bitmap Bitmap)
        {
            IsValid = Bitmap != null;
            Thumbnails = new Dictionary<int, Bitmap>();
            if (IsValid)
            {
                this.Bitmap = ToDispose(Bitmap);
                Width = Bitmap.Width;
                Height = Bitmap.Height;
            }
        }

        public void Draw(System.Drawing.Graphics g, float X, float Y, float TargetWidth, float TargetHeight, RectangleF Segment)
        {
            /*Render*/
            if (Segment.IsEmpty)

                g.DrawImage(Bitmap,
                            new RectangleF(X, Y, TargetWidth, TargetHeight),
                            new RectangleF(0, 0, Bitmap.Width, Bitmap.Height),
                            GraphicsUnit.Pixel);
            else
            {
                g.DrawImage(Bitmap,
                            new RectangleF(X, Y, TargetWidth, TargetHeight),
                            Segment,
                            GraphicsUnit.Pixel);
            }
        }



        /*thumbnail creation*/
        public Bitmap GetThumbnail(int ThumbnailWidth, int ThumbnailHeight, RectangleF Segment)
        {
            Size s = new Size(ThumbnailWidth, ThumbnailHeight);

            int hash = 17;
            hash = hash * 23 + s.GetHashCode();
            hash = hash * 23 + Segment.GetHashCode();

            if (Thumbnails.ContainsKey(hash))
                return Thumbnails[hash];
            else
                Thumbnails.Add(hash, ToDispose(CreateThumbnail(ThumbnailHeight, ThumbnailHeight, Segment)));

            /*check if the current thumbnail is valid, if not create a valid one*/
            return Thumbnails[hash];
        }

        Bitmap CreateThumbnail(int Width, int Height, RectangleF Segment)
        {
            Bitmap result = new Bitmap(Width, Height);
            

            if (Bitmap != null)
            {
                /*create the graphics to object to render the bitmap in*/
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(result);

                /*size for the image*/
                int targetX = 0, targetY = 0;
                int targetWidth = 0, targetHeight = 0;

                /*check wich aspec ratio fits the thumbnail and create the thumbnail*/
                float aspect = this.Height / (float)this.Width;
                if (!Segment.IsEmpty) aspect = Segment.Height / Segment.Width;
                if (Width * aspect < Height)
                {
                    targetWidth = Width;
                    targetHeight = (int)(Height * aspect);
                    targetY = Width / 2 - targetHeight / 2;
                }
                else
                {
                    aspect = this.Width / (float)this.Height; /*ofc... it's the other that fits better*/
                    if (!Segment.IsEmpty) aspect = Segment.Width / Segment.Height;
                    targetHeight = Height;
                    targetWidth = (int)(Width * aspect);
                    targetX = Height / 2 - targetWidth / 2;
                }

                /*scale the original image down to the best size to fit in the thumbnail*/
                Draw(g, targetX, targetY, targetWidth, targetHeight, Segment);

                /*free the graphics object*/
                g.Dispose();

            }
            return result;
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
                        return new Texture2D(bitmap);
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
