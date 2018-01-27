﻿using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class TilesetAnimation : RenderableObject2D
    {
        public Texture2D Tileset { get; private set; }
        List<Frame> frames = new List<Frame>();
        public List<Frame> Frames { get => frames; }
        int CurrentFrame;

        public float Speed;
        float FrameTimer;

        public bool Loop { get; set; }
        bool isReverseLoop;
        public bool IsReverseLoop
        {
            get => isReverseLoop;
            set { isReverseLoop = value; Reverse = false; }
        }
        bool Reverse;
        public bool AnimationInProgress { get; private set; }

        public TilesetAnimation(Texture2D Tileset, int FPS, int Z)
        {
            this.Tileset = Tileset;
            Speed = SpeedFromFPS(FPS);
            IsReverseLoop = true;
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
            if (CurrentFrame == frames.Count || CurrentFrame == -1)
            {
                if (Loop && IsReverseLoop)
                {
                    Reverse = !Reverse;
                    CurrentFrame += Reverse ? -2 : 2;
                }
                else if (Loop && !IsReverseLoop)
                    CurrentFrame = 0;
                else if (!Loop)
                {
                    CurrentFrame = frames.Count - 1;
                    AnimationInProgress = false;
                }
            }
        }

        /*update render paremter to display the animation*/
        void UpdateRenderParameter()
        {
            /*make sure the frame is valid*/
            if (CurrentFrame >= 0 && CurrentFrame < frames.Count)
            {
                Vertices = CreateRectangle2D(frames[CurrentFrame].Width, frames[CurrentFrame].Height);
                TextureSegment = new System.Drawing.RectangleF(frames[CurrentFrame].StartX, frames[CurrentFrame].StartY, frames[CurrentFrame].Width, frames[CurrentFrame].Height);
            }
            else
            {
                /*error in current frame selection make sure nothing is rendered*/
                Vertices = null;

                /*display in console for debug, only needed if a animation is not displayed corret and i wanted to know if it's cause of the update*/
                System.Console.WriteLine(string.Format("Error in animation frame count you should go check it.\nFrame: {0}\nTotal frames: {1}", CurrentFrame, frames.Count));
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
            frames.Add(frame);
            return frame;
        }

        public void RemoveFrame(Frame frame)
        {
            if (frames.Contains(frame))
                frames.Remove(frame);
        }

        public void AutoCut(int StartX, int StartY, int Widht, int Height, int TileWidht, int TileHeight)
        {
            frames.Clear();

            /*Create all the frames the user didnt want to create by himself*/
            int cx = (Widht / TileWidht);
            int cy = (Height / TileHeight);
            for (int y = 0; y < cy; y++)
                for (int x = 0; x < cx; x++)
                    AddFrame(StartX + x * TileWidht, StartY + y * TileHeight, TileWidht, TileHeight);
            Reset();
        }

        public bool SwapFrames(int a, int b)
        {
            if (a >= 0 && a < frames.Count && b >= 0 && b < frames.Count)
            {
                Frame fa = frames[a];
                Frame fb = frames[b];
                frames[a] = fb;
                frames[b] = fa;
                return true;
            }
            return false;
        }
        /*helper*/
        public static float SpeedFromFPS(float FPS) => 1000f / FPS; 

    }
}
