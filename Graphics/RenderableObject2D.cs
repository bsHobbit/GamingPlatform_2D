using Core.ComponentModel;
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
            set { location = value; UpdateWorldMatrix(); }
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

        public List<Vec2> Vertices { get; protected set; }
        public VerticeInterpretation VerticeInterpretation;

        RectangleF BoundingBox;

        public Texture2D Texture { get; set; }

        public Matrix WorldMatrix { get; set; }


        protected void Initialize(Vec2 Location, int Z, List<Vec2> Vertices, Color Color, Color OutlineColor, Texture2D Texture = null, float OutlineWidth = 1, float Scale = 1, float Rotation = 0, VerticeInterpretation Interpretation = VerticeInterpretation.Solid)
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
        }

        void UpdateBoundingBox()
        {
            float minX = float.MaxValue, minY = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue;
            for (int i = 0; i < Vertices.Count; i++)
            {
                if (Vertices[i].X < minX) minX = Vertices[i].X;
                if (Vertices[i].Y < minY) minY = Vertices[i].Y;
                if (Vertices[i].X > maxX) maxX = Vertices[i].X;
                if (Vertices[i].Y > maxY) maxY = Vertices[i].Y;
            }
            BoundingBox = new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        public virtual void Update(float Elapsed) { }

        public virtual void Draw<Renderer>(Renderer g, Camera2D Camera)
        {
            if (g is System.Drawing.Graphics)
                Draw(g as System.Drawing.Graphics, Camera);
        }


        /*Check stuff*/

        public List<Vec2> TransformedVertices()
        {
            List<Vec2> result = new List<Vec2>();
            for (int i = 0; i < Vertices.Count; i++)
            {
                PointF[] tmpPoints = new PointF[] { new PointF(Vertices[i].X, Vertices[i].Y) };
                WorldMatrix.TransformPoints(tmpPoints);
                result.Add(new Vec2(tmpPoints[0].X, tmpPoints[0].Y));
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

        public static bool IsBetween(float x, float a, float b)
        {
            return (x - a) * (x - b) < 0;
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

            /*Create Path*/
            if (VerticeInterpretation == VerticeInterpretation.Solid || VerticeInterpretation == VerticeInterpretation.Wireframe)
            {
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
                if (Texture != null && VerticeInterpretation == VerticeInterpretation.Solid)
                {
                    g.SetClip(path);
                    Texture.Draw(g, BoundingBox.X, BoundingBox.Y, BoundingBox.Width, BoundingBox.Height);
                    g.ResetClip();
                }
            }
            else if (VerticeInterpretation == VerticeInterpretation.Lines)
            {
                int count = Vertices.Count / 2;

                for (int i = 0; i < count; i += 2)
                {
                    Vec2 v1 = Vertices[i];
                    Vec2 v2 = Vertices[i + 1];
                    g.DrawLine(new Pen(new SolidBrush(Color), OutlineWidth), v1.X, v1.Y, v2.X, v2.Y);
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
