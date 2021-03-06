﻿using Core.ComponentModel;
using Box2DX.Common;
using System.Drawing.Drawing2D;
using System.Collections.Generic;
using System.Drawing;
using Core.IO;

namespace Graphics
{
    public abstract class RenderableObject2D : Component
    {

        /*Event Stuff*/
        public delegate void RenderableObject2DLocationChangedEventHandler(RenderableObject2D Sender, Vec2 NewLocation, Vec2 PreviousLocation);
        public event RenderableObject2DLocationChangedEventHandler LocationChanged;
        public void OnLocationChanged(Vec2 NewLocation, Vec2 PreviousLocation) { LocationChanged?.Invoke(this, NewLocation, PreviousLocation); }
        public delegate void InputManagerMouseEventArgs(RenderableObject2D Sender, Mouse MouseEventArgs);
        public event InputManagerMouseEventArgs MouseEnter;
        public void OnMouseEnter(Mouse Mouse) { MouseEnter?.Invoke(this, Mouse); }
        public event InputManagerMouseEventArgs MouseLeave;
        public void OnMouseLeave(Mouse Mouse) { MouseLeave?.Invoke(this, Mouse); }
        public event InputManagerMouseEventArgs MouseMove;
        public void OnMouseMove(Mouse Mouse) { MouseMove?.Invoke(this, Mouse); }
        public event InputManagerMouseEventArgs MouseDown;
        public void OnMouseDown(Mouse Mouse) { MouseDown?.Invoke(this, Mouse); }
        public event InputManagerMouseEventArgs MouseUp;
        public void OnMouseUp(Mouse Mouse) { MouseUp?.Invoke(this, Mouse); }
        public event InputManagerMouseEventArgs Click;
        public void OnClick(Mouse Mouse) { Click?.Invoke(this, Mouse); }
        public event InputManagerMouseEventArgs DoubleClick;
        public void OnDoubleClick(Mouse Mouse) { DoubleClick?.Invoke(this, Mouse); }


        public object Tag { get; set; }

        float rotation;
        public float  Rotation
        {
            get => rotation;
            set { rotation = value; UpdateWorldMatrix(); }
        }

        float scale;
        public float  Scale
        {
            get => scale;
            set { scale = value; UpdateWorldMatrix(); }
        }

        Vec2 location;
        public Vec2   Location
        {
            get => location;
            set
            {
                Vec2 prevLocation = location;
                location = value;
                UpdateWorldMatrix();
                OnLocationChanged(location, prevLocation);
            }
        }

        int Z;
        public int ZLocation
        {
            get => Z;
            set { Z = value; }
        }

        Vec2 rotationOffset;
        public Vec2   RotationOffset
        {
            get => rotationOffset;
            set { rotationOffset = value; UpdateWorldMatrix(); }
        }

        public Color Color { get; set; }
        public Color OutlineColor { get; set; }
        public float OutlineWidth { get; set; }

        public bool Visible { get; set; }
        public bool Enabled { get; set; }


        List<Vec2> vertices;
        public List<Vec2> Vertices
        {
            get => vertices;
            internal set
            {
                vertices = value;
                UpdateBoundingBox();
            }
        }
        public VerticeInterpretation VerticeInterpretation;

        RectangleF boundingBox;
        public RectangleF BoundingBox
        {
            get => boundingBox;
        }

        public Texture2D Texture { get; set; }
        public RectangleF TextureSegment { get; set; }

        public Matrix WorldMatrix { get; set; }
        Matrix InverseWorldMatrix;

        Dictionary<int, RenderableText> TextToRender;


        protected void Initialize(Vec2 Location,
                                  int Z,
                                  List<Vec2> Vertices,
                                  Color Color,
                                  Color OutlineColor,
                                  RectangleF? TextureSegment = null,
                                  Texture2D Texture = null,
                                  float OutlineWidth = 1, 
                                  float Scale = 1, 
                                  float Rotation = 0,
                                  VerticeInterpretation Interpretation = VerticeInterpretation.Solid)
        {
            scale = Scale >= 1 && Scale <= 100 ? Scale : 1;
            VerticeInterpretation = Interpretation;
            this.Color = Color;
            this.OutlineColor = OutlineColor;
            this.Vertices = Vertices;
            this.Texture = Texture;
            this.OutlineWidth = OutlineWidth >= 1 && OutlineWidth <= 100 ? OutlineWidth : 1;
            rotation = Rotation;
            location = Location;
            this.Z = Z;
            if (TextureSegment != null)
                this.TextureSegment = (RectangleF)TextureSegment;
            else
                this.TextureSegment = RectangleF.Empty;

            TextToRender = new Dictionary<int, RenderableText>();

            Visible = true;
            Enabled = true;
            
            UpdateWorldMatrix();
            UpdateBoundingBox();
        }

        void UpdateWorldMatrix()
        {
            WorldMatrix = new Matrix();

            /*Rotate around point of rotation*/
            WorldMatrix.Translate(-rotationOffset.X, -rotationOffset.Y, MatrixOrder.Append);
            WorldMatrix.Rotate(Rotation, MatrixOrder.Append);
            WorldMatrix.Translate(rotationOffset.X, rotationOffset.Y, MatrixOrder.Append);


            /*Scale Object*/
            WorldMatrix.Scale(Scale, Scale, MatrixOrder.Append);

            /*Translate to location*/
            WorldMatrix.Translate(Location.X, location.Y, MatrixOrder.Append);

            InverseWorldMatrix = WorldMatrix.Clone();
            InverseWorldMatrix.Invert();
        }

        public static RectangleF GetBoundingBox(List<Vec2> vertices)
        {
            if (vertices != null)
            {
                float minX = float.MaxValue, minY = float.MaxValue;
                float maxX = float.MinValue, maxY = float.MinValue;
                for (int i = 0; i < vertices.Count; i++)
                {
                    if (vertices[i].X < minX) minX = vertices[i].X;
                    if (vertices[i].Y < minY) minY = vertices[i].Y;
                    if (vertices[i].X > maxX) maxX = vertices[i].X;
                    if (vertices[i].Y > maxY) maxY = vertices[i].Y;
                }
                return new RectangleF(minX, minY, maxX - minX, maxY - minY);
            }
            else
                return RectangleF.Empty;
        }

        void UpdateBoundingBox()
        {
            boundingBox = GetBoundingBox(vertices);
        }

        public virtual void Update(float Elapsed) { }

        public virtual void Draw<Renderer>(Renderer g, Camera2D Camera)
        {
            if (g is System.Drawing.Graphics)
                Draw(g as System.Drawing.Graphics, Camera);
        }


        /*transformation*/
        public Vec2 TransformIntoLocalSpace(Vec2 WorldSpaceCoordinate)
        {
            PointF[] tmp = new PointF[] { new PointF(WorldSpaceCoordinate.X, WorldSpaceCoordinate.Y) };
            InverseWorldMatrix.TransformPoints(tmp);
            return new Vec2(tmp[0].X, tmp[0].Y);
        }

        /*Check stuff*/

        public List<Vec2> TransformedVertices()
        {
            List<Vec2> result = new List<Vec2>();

            if (vertices != null)
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    PointF[] tmpPoints = new PointF[] { new PointF(Vertices[i].X, Vertices[i].Y) };
                    WorldMatrix.TransformPoints(tmpPoints);
                    result.Add(new Vec2(tmpPoints[0].X, tmpPoints[0].Y));
                }
            }
            return result;
        }
        bool IsInPolygon(Vec2 testPoint, List<Vec2> Vertices)
        {
            if (Vertices == null || Vertices.Count < 3) return false;
            bool isInPolygon = false;
            var lastVertex = Vertices[Vertices.Count - 1];
            foreach (var vertex in Vertices)
            {
                if (IsBetween(testPoint.Y, lastVertex.Y, vertex.Y))
                {
                    double t = (testPoint.Y - lastVertex.Y) / (vertex.Y - lastVertex.Y);
                    double x = t * (vertex.X - lastVertex.X) + lastVertex.X;
                    if (x >= testPoint.X) isInPolygon = !isInPolygon;
                }
                else
                {
                    if (testPoint.Y == lastVertex.Y && testPoint.X < lastVertex.X && vertex.Y > testPoint.Y) isInPolygon = !isInPolygon;
                    if (testPoint.Y == vertex.Y && testPoint.X < vertex.X && lastVertex.Y > testPoint.Y) isInPolygon = !isInPolygon;
                }

                lastVertex = vertex;
            }

            return isInPolygon;
        }
        public bool IsPointInsideObject(Vec2 Point) => IsInPolygon(Point, TransformedVertices());


        public static double DistanceFromPointToLine(Vec2 point, Vec2 l1, Vec2 l2)
        {
            return Math.Abs((l2.X - l1.X) * (l1.Y - point.Y) - (l1.X - point.X) * (l2.Y - l1.Y)) /
                   Math.Sqrt((float)System.Math.Pow(l2.X - l1.X, 2) + (float)System.Math.Pow(l2.Y - l1.Y, 2));
        }


        public static bool IsBetween(float x, float a, float b)
        {
            return (x - a) * (x - b) < 0;
        }

        /*helping stuff*/
        public static List<Vec2> CreateRectangle2D(int Width, int Height) => new List<Vec2>() { new Vec2(0, 0), new Vec2(Width, 0), new Vec2(Width, Height), new Vec2(0, Height) };


        /*manage text*/
        public void AddText(RenderableText Text)
        {
            int hash = Text.GetHashCode();
            if (!TextToRender.ContainsKey(hash))
                TextToRender.Add(hash, Text);
        }

        public void RemoveText(RenderableText Text)
        {
            int hash = Text.GetHashCode();
            if (TextToRender.ContainsKey(hash))
                TextToRender.Remove(hash);
        }

        public void ClearText()
        {
            TextToRender.Clear();
        }

        /*make clones available, deepcopy only for the gamestate since u want each animation to be unique u dont need that extra data in the editor*/
        public virtual RenderableObject2D Clone(bool DeepCopy = false) { return null; }
        internal void CopyBaseInfo<T>(ref T Result) where T : RenderableObject2D
        {

            if (vertices != null)
                for (int i = 0; i < vertices.Count; i++)
                    Result.vertices.Add(vertices[i]);
            if (TextToRender != null)
                for (int i = 0; i < TextToRender.Count; i++)
                    Result.AddText(TextToRender[i].Clone());

            Result.Color = Color;
            Result.Enabled = Enabled;
            Result.Location = Location;
            Result.Name = Name;
            Result.OutlineColor = OutlineColor;
            Result.OutlineWidth = OutlineWidth;
            Result.Rotation = Rotation;
            Result.RotationOffset = RotationOffset;
            Result.Scale = Scale;
            Result.Texture = Texture;
            Result.TextureSegment = TextureSegment;
            Result.Visible = Visible;
            Result.ZLocation = ZLocation;

        }

        /*GDI Only-Stuff*/

        public static PointF[] ToPointFArray(List<Vec2> Vertices)
        {
            PointF[] result = new PointF[Vertices.Count];
            for (int i = 0; i < Vertices.Count; i++)
                result[i] = new PointF(Vertices[i].X, Vertices[i].Y);

            return result;
        }
        void Draw(System.Drawing.Graphics g, Camera2D camera)
        {
            /*Transform*/
            var world_view_projection = WorldMatrix.Clone();
            
            world_view_projection.Multiply(camera.ViewMatrix, MatrixOrder.Append);
            g.Transform = world_view_projection;

            if (VerticeInterpretation == VerticeInterpretation.Solid || VerticeInterpretation == VerticeInterpretation.Wireframe)
            {
                /*only draw if it's realy visible*/
                if (vertices != null && vertices.Count >= 3)
                {
                    /*Create Path*/
                    GraphicsPath path = new GraphicsPath();
                    path.StartFigure();
                    path.AddPolygon(ToPointFArray(Vertices));
                    path.CloseFigure();

                    /*Draw Object*/
                    if (VerticeInterpretation == VerticeInterpretation.Solid)
                        g.FillPath(new SolidBrush(Color), path);

                    if (!OutlineColor.IsEmpty)
                        g.DrawPath(new Pen(new SolidBrush(OutlineColor), OutlineWidth), path);


                    /*Draw Texture*/
                    if (Texture != null && Texture.IsValid && VerticeInterpretation == VerticeInterpretation.Solid)
                    {
                        if (!TextureSegment.IsEmpty)
                        {
                            TextureBrush tbrush = new TextureBrush(Texture.Bitmap, WrapMode.Tile, TextureSegment);
                            g.FillPath(tbrush, path);
                        }
                        else
                        {
                            TextureBrush tbrush = new TextureBrush(Texture.Bitmap,  WrapMode.Tile);
                            g.FillPath(tbrush, path);
                        }
                        //g.SetClip(path);
                        //Texture.Draw(g, boundingBox.X, boundingBox.Y, boundingBox.Width, boundingBox.Height, TextureSegment);
                        //g.ResetClip();
                    }

                    /*Draw Text*/
                    if (TextToRender != null)
                    {
                        foreach (var text in TextToRender)
                        {
                            if (text.Value.ClipWithObject) g.SetClip(path);
                            text.Value.Draw(g);
                            if (text.Value.ClipWithObject) g.ResetClip();
                        }
                    }
                }
            }
            else if (VerticeInterpretation == VerticeInterpretation.Lines)
            {
                for (int i = 0; i < Vertices.Count; i += 2)
                {
                    Vec2 v1 = Vertices[i];
                    Vec2 v2 = Vertices[i + 1];
                    g.DrawLine(new Pen(new SolidBrush(Color), OutlineWidth), v1.X, v1.Y, v2.X, v2.Y);
                }
            }
            else if (VerticeInterpretation == VerticeInterpretation.Arrows)
            {
                for (int i = 0; i < Vertices.Count; i += 2)
                {
                    Vec2 v1 = Vertices[i];
                    Vec2 v2 = Vertices[i + 1];
                    g.DrawLine(new Pen(new SolidBrush(Color), OutlineWidth), v1.X, v1.Y, v2.X, v2.Y);

                    float totalLength = (v2 - v1).Length();
                    float fixedOffset = 10 / totalLength;
                    Vec2 v = v2 + ((v1 - v2) * fixedOffset);
                    Vec2 p = Core.Vec2Extension.LeftNormal(v1 - v2) * 5;
                    Vec2 vr = v + p;
                    g.DrawLine(new Pen(new SolidBrush(OutlineColor), OutlineWidth), v2.X, v2.Y, vr.X, vr.Y);

                    p = Core.Vec2Extension.RightNormal(v1 - v2) * 5;
                    vr = v + p;
                    g.DrawLine(new Pen(new SolidBrush(OutlineColor), OutlineWidth), v2.X, v2.Y, vr.X, vr.Y);
                }
            }
            else if (VerticeInterpretation == VerticeInterpretation.Points)
            {
                for (int i = 0; i < Vertices.Count; i++)
                {
                    Vec2 v = Vertices[i];
                    g.FillEllipse(new SolidBrush(Color), v.X - (OutlineWidth / 2), v.Y - (OutlineWidth / 2), OutlineWidth, OutlineWidth);
                    g.DrawEllipse(new Pen(new SolidBrush(OutlineColor), 1), v.X - (OutlineWidth / 2), v.Y - (OutlineWidth / 2), OutlineWidth, OutlineWidth);
                }
            }
        }
    }
}
