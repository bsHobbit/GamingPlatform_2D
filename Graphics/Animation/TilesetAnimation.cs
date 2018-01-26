using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class TilesetAnimation : RenderableObject2D
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

        public TilesetAnimation(Texture2D Tileset, int FPS, int Z)
        {
            this.Tileset = Tileset;
            Speed = SpeedFromFPS(FPS);
            IsReverseLoop = false;
            Loop = true;
            Initialize(new Vec2(), Z, null, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, null, Tileset);
        }


        /*update the animation*/
        public override void Update(float Elapsed)
        {
            if (AnimationInProgress)
            {
                FrameTimer += Elapsed;

                if (FrameTimer >= Speed)
                {
                    /*Calcluate new frame*/
                    UpdateCurrentFrame();

                    /*set render paremters for current frame to make sure the animation is rendered correct*/
                    UpdateRenderParameter();

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

        /*update render paremter to display the animation*/
        void UpdateRenderParameter()
        {
            /*make sure the frame is valid*/
            if (CurrentFrame >= 0 && CurrentFrame < Frames.Count)
            {
                Vertices = CreateRectangle2D(Frames[CurrentFrame].Width, Frames[CurrentFrame].Height);
                TextureSegment = new System.Drawing.RectangleF(Frames[CurrentFrame].StartX, Frames[CurrentFrame].StartY, Frames[CurrentFrame].Width, Frames[CurrentFrame].Height);
            }
            else
            {
                /*error in current frame selection make sure nothing is rendered*/
                Vertices = null;

                /*display in console for debug, only needed if a animation is not displayed corret and i wanted to know if it's cause of the update*/
                System.Console.WriteLine(string.Format("Error in animation frame count you should go check it.\nFrame: {0}\nTotal frames: {1}", CurrentFrame, Frames.Count));
            }
        }
        


        /*reset the animation*/
        public void Reset()
        {
            CurrentFrame = 0;
            AnimationInProgress = true;
            FrameTimer = 0;
            UpdateRenderParameter();
        }


        /*Manage frames*/
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
            int cy = (int)(Tileset.Height / TileHeight);
            for (int y = 0; y < cy; y++)
                for (int x = 0; x < cx; x++)
                    AddFrame(x * TileWidht, y * TileHeight, TileWidht, TileHeight);
            Reset();
        }

        /*helper*/
        public static float SpeedFromFPS(float FPS) => 1000f / FPS; 

    }
}
