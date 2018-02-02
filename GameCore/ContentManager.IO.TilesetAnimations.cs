using Graphics.Animation;
using System;

namespace GameCore
{
    public partial class ContentManager
    {
        void SaveTilesetAnimations(string Path)
        {
            var tilesetAnimations = TilesetAnimations;
            foreach (var tsa in tilesetAnimations)
            {
                string tsaFile = Path + tsa.Name + ".tsa";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(tsaFile, false);
                sw.WriteLine(tsa.Texture.Name);
                sw.WriteLine(tsa.Loop);
                sw.WriteLine(tsa.IsReverseLoop);
                sw.WriteLine(tsa.FPS);
                sw.WriteLine(tsa.Scale);
                sw.WriteLine(tsa.Rotation);
                sw.WriteLine(tsa.RotationOffset.X);
                sw.WriteLine(tsa.RotationOffset.Y);
                sw.WriteLine(tsa.Frames.Count);
                for (int i = 0; i < tsa.Frames.Count; i++)
                {
                    sw.WriteLine(tsa.Frames[i].StartX);
                    sw.WriteLine(tsa.Frames[i].StartY);
                    sw.WriteLine(tsa.Frames[i].Width);
                    sw.WriteLine(tsa.Frames[i].Height);

                }
                sw.Close();
            }
        }

        void LoadTilesetAnimations(string File)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(File);
            string tsaName = System.IO.Path.GetFileNameWithoutExtension(File);
            string TextureName = sr.ReadLine();
            bool Loop = Convert.ToBoolean(sr.ReadLine());
            bool ReverseLoop = Convert.ToBoolean(sr.ReadLine());
            int FPS = Convert.ToInt32(sr.ReadLine());
            float Scale = (float)Convert.ToDouble(sr.ReadLine());
            float Rotation = (float)Convert.ToDouble(sr.ReadLine());
            float RotationX = (float)Convert.ToDouble(sr.ReadLine());
            float RotationY = (float)Convert.ToDouble(sr.ReadLine());
            int Frames = Convert.ToInt32(sr.ReadLine());

            var baseTexture = GetTexture(TextureName);
            if (baseTexture != null)
            {
                TilesetAnimation result = new TilesetAnimation(baseTexture, FPS, 0)
                {
                    Name = tsaName,
                    Loop = Loop,
                    FPS = FPS,
                    IsReverseLoop = ReverseLoop,
                    Scale = Scale,
                    Rotation = Rotation,
                    RotationOffset = new Box2DX.Common.Vec2(RotationX, RotationY),
                };

                for (int i = 0; i < Frames; i++)
                    result.AddFrame(Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()), Convert.ToInt32(sr.ReadLine()));

                AddTilesetAnimation(result);
            }
            sr.Close();
        }
    }
}