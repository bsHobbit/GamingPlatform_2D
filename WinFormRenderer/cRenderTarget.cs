using System.Windows.Forms;
using System.Collections.Generic;
using Core;
using Graphics;
using System.Diagnostics;
using Core.IO;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace WinFormRenderer
{
    public partial class cRenderTarget : UserControl
    {

        public event System.Action<float> OnUpdate;

        public Mouse Mouse { get => device_Mouse; }
        Mouse device_Mouse;
        PointF device_Mouse_Location;
        Core.IO.ButtonState[] device_Mouse_ButtonStates;
        int device_Mouse_Delta = 0;

        Camera2D camera;
        public Camera2D Camera
        {
            get => camera;
            set { camera = value; }
        }
        public float W => renderTarget.Width;
        public float H => renderTarget.Height;
        

        List<RenderableObject2D> ObjectsToRender = new List<RenderableObject2D>();
        DisposeCollector disposeCollector = new DisposeCollector();

        Graphics.Interaction.MouseInputManager mouseInputManager;

        

        Stopwatch Timer = Stopwatch.StartNew();

        public cRenderTarget()
        {
            InitializeComponent();
            DoubleBuffered = true;
            device_Mouse_ButtonStates = new Core.IO.ButtonState[System.Enum.GetNames(typeof(Core.IO.MouseButtons)).Length];
            device_Mouse = new Mouse();
            device_Mouse_Location = new PointF();
        }

        ~cRenderTarget()
        {
            disposeCollector.Dispose();
        }

        /*Handle everything rendering related*/
        public void AddRenderObject(RenderableObject2D Item)
        {
            ObjectsToRender.Add(disposeCollector.ToDispose(Item));
        }

        public void RemoveRenderObject(RenderableObject2D Item)
        {
            if (ObjectsToRender.Contains(Item))
            {
                ObjectsToRender.Remove(Item);
                disposeCollector.RemoveAndDispose(Item);
            }
        }

        private void RenderTarget_Paint(object sender, PaintEventArgs e)
        {
            /*Reset everthing*/
            e.Graphics.Clear(System.Drawing.Color.CornflowerBlue);
            e.Graphics.ResetTransform();

            e.Graphics.Transform = Camera.ViewMatrix;

            /*Draw stuff*/
            for (int i = 0; i < ObjectsToRender.Count; i++)
                if (ObjectsToRender[i].Visible)
                    ObjectsToRender[i].Draw(e.Graphics, Camera);
        }


        /*Initialize everything that needs initialization*/
        public void Initialize()
        {
            Camera = disposeCollector.ToDispose(new Camera2D());
            Camera.LookAt = new Box2DX.Common.Vec2(0, 0);
            

            /*Einfacher Timer zum Rendern*/
            Timer timer = new Timer()
            {
                Enabled = true,
                Interval = (int)Graphics.Animation.TilesetAnimation.SpeedFromFPS(60)
            };
            timer.Tick += (s, e) => 
            {
                float elapsed = (float)Timer.Elapsed.TotalMilliseconds;

                /*Update User-Input*/
                device_Mouse.Update(GetMouseState(device_Mouse_Location), elapsed);
                ResetDevice_Mouse();
                mouseInputManager?.Update(elapsed, ref camera);

                /*call the update event for other stuff to update*/
                OnUpdate?.Invoke(elapsed);

                /*Order by Z Value TODO: Nicht immer sortieren, nur wenn nötig*/
                ObjectsToRender.Sort((x, y) => x.ZLocation.CompareTo(y.ZLocation));


                /*Update all objects*/
                for (int i = 0; i < ObjectsToRender.Count; i++)
                    ObjectsToRender[i].Update(elapsed);

                /*make sure a new image is rendered*/
                renderTarget.Invalidate();

                Timer = Stopwatch.StartNew();
            };

            /*Rendering related events*/
            renderTarget.Paint += RenderTarget_Paint;
            Camera.ScreenSize = new Box2DX.Common.Vec2(renderTarget.ClientSize.Width, renderTarget.ClientSize.Height);
            renderTarget.SizeChanged += (s, e) => { Camera.ScreenSize = new Box2DX.Common.Vec2(renderTarget.ClientSize.Width, renderTarget.ClientSize.Height); };

            /*Input related events*/
            renderTarget.MouseMove += RenderTarget_MouseMove;
            renderTarget.MouseDown += RenderTarget_MouseDown;
            renderTarget.MouseUp += RenderTarget_MouseUp;
            renderTarget.MouseWheel += RenderTarget_MouseWheel;
            renderTarget.MouseEnter += (s, e) => { renderTarget.Focus(); };
        }

        /*Manage User Input */
        public void EnableMouseInput()
        {
            mouseInputManager = new Graphics.Interaction.MouseInputManager(Mouse, ObjectsToRender);
        }

        public void DisableMouseInput()
        {
            mouseInputManager = null;
        }

        public void EnableCameraControl(bool Always)
        {
            if (mouseInputManager == null)
                EnableMouseInput();
            mouseInputManager.SetCameraMovement(Always ? Graphics.Interaction.CameraMovementStyle.Always : Graphics.Interaction.CameraMovementStyle.NoObject);
        }

        public void DisableCameraControl ()
        {
            mouseInputManager?.SetCameraMovement(Graphics.Interaction.CameraMovementStyle.None);
        }


        MouseState GetMouseState(PointF currentMouseLocation)
        {
            /*Transform to world*/
            PointF[] mouseLocation = new PointF[] { new PointF(currentMouseLocation.X, currentMouseLocation.Y) };
            Camera.InverseViewMatrix.TransformPoints(mouseLocation);

            /*Create MouseState structure*/
            return new MouseState(new Box2DX.Common.Vec2(mouseLocation[0].X, mouseLocation[0].Y), 
                                  device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Left] == Core.IO.ButtonState.Pressed,
                                  device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Middle] == Core.IO.ButtonState.Pressed,
                                  device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Right] == Core.IO.ButtonState.Pressed,
                                  device_Mouse_Delta);
        }

        void ResetDevice_Mouse()
        {
            device_Mouse_Delta = 0;
        }
        private void RenderTarget_MouseMove(object sender, MouseEventArgs e)
        {
            device_Mouse_Location = new PointF(e.X, e.Y);
        }
        private void RenderTarget_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Left] = Core.IO.ButtonState.Pressed;
            if (e.Button == System.Windows.Forms.MouseButtons.Middle) device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Middle] = Core.IO.ButtonState.Pressed;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Right] = Core.IO.ButtonState.Pressed;
        }
        private void RenderTarget_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left) device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Left] = Core.IO.ButtonState.Released;
            if (e.Button == System.Windows.Forms.MouseButtons.Middle) device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Middle] = Core.IO.ButtonState.Released;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) device_Mouse_ButtonStates[(int)Core.IO.MouseButtons.Right] = Core.IO.ButtonState.Released;
        }
        private void RenderTarget_MouseWheel(object sender, MouseEventArgs e)
        {
            device_Mouse_Delta = e.Delta;
        }

    }
}
