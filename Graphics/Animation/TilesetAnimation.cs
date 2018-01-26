using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class TilesetAnimation 
    {
        public Texture2D Tileset { get; private set; }
        List<Frame> Frames = new List<Frame>();
        int CurrentFrame;

        public float Speed;
        float FrameTimer;

        public bool Loop { get; set; }
        public bool IsReverseLoop { get; set; }
        bool Reverse;
        public bool AnimationInProgress { get; private set; }

        public TilesetAnimation(Texture2D Tileset, int FPS)
        {
            this.Tileset = Tileset;
            Speed = SpeedFromFPS(FPS);
        }
        
        public void Draw<Renderer>(Renderer g, Camera2D Camera)
        {
            if (g is System.Drawing.Graphics)
                Draw(g as System.Drawing.Graphics);
        }

        void Draw(System.Drawing.Graphics g)
        {
            if (Frames != null && CurrentFrame >= 0 && CurrentFrame < Frames.Count)
            {
                Frame f = Frames[CurrentFrame];
                Tileset.Segment = new System.Drawing.RectangleF(f.StartX, f.StartY, f.Width, f.Height);
                //Tileset.Draw(g);
            }
        }

        
        public void Update(float Elapsed)
        {
            if (AnimationInProgress)
            {
                FrameTimer += Elapsed;

                if (FrameTimer >= Speed)
                {
                    UpdateCurrentFrame();
                    FrameTimer = 0;
                }
            }
        }
        void UpdateCurrentFrame()
        {
            CurrentFrame += Reverse ? -1 : 1;
            if (CurrentFrame == Frames.Count || CurrentFrame == -1)
            {
                if (Loop && IsReverseLoop)
                {
                    Reverse = !Reverse;
                    CurrentFrame += Reverse ? -1 : 1;
                }
                else if (Loop && !IsReverseLoop)
                    CurrentFrame = 0;
                else if (!Loop)
                {
                    CurrentFrame = Frames.Count - 1;
                    AnimationInProgress = false;
                }
            }
        }

        public void Reset()
        {
            CurrentFrame = 0;
            AnimationInProgress = true;
            FrameTimer = 0;
        }

        public Frame AddFrame(int X, int Y, int Width, int Height)
        {
            Frame frame = new Frame(X, Y, Width, Height);
            Frames.Add(frame);
            return frame;
        }

        public void RemoveFrame(Frame frame)
        {
            if (Frames.Contains(frame))
                Frames.Remove(frame);
        }

        public void AutoCut(int TileWidht, int TileHeight)
        {
            Frames.Clear();

            int cx = (int)(Tileset.Width / TileWidht);
            int cy = (int)(Tileset.Width / TileWidht);
            for (int x = 0; x < cx; x++)
                for (int y = 0; y < cy; y++)
                    AddFrame(x * TileWidht, y * TileHeight, TileWidht, TileHeight);
            Reset();
        }

        public static float SpeedFromFPS(float FPS) => 1000f / FPS; 

    }
}
