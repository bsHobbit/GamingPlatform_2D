using Box2DX.Common;
using Graphics.Geometry;
using System.Collections.Generic;
using System.Windows.Forms;
using Animation = Graphics.Animation.TilesetAnimation;


namespace Editor
{
    public partial class AnimationEditor : Form
    {
        Animation Animation;
        Rectangle2D TilesetRect;
        Rectangle2D SelectionRect;
        Rectangle2D FrameRect;
        Grid2D TilesetGrid;

        int SelectedFrame { get => trackBarFrameSelection.Value; }

        bool UserIsSelectingInTileset;
        Vec2 selectionMouseDownLocation;

        public AnimationEditor()
        {
            InitializeComponent();
        }

        public AnimationEditor(Animation Animation)
            : this()
        {
            this.Animation = Animation;
            Text += " - " + Animation.Name;

            InitializeRenderTargets();

            /*add at least one frame to the animation to work with*/
            if (Animation.Frames.Count == 0)
                Animation.AddFrame(0, 0, 1, 1);

            /*user isnt doing any input*/
            UserIsSelectingInTileset = false;

            /*render the tileset into this rectangle*/
            TilesetRect = new Rectangle2D(Animation.Tileset.Width, Animation.Tileset.Height, new Vec2(0, 0), -1, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, Animation.Tileset) { Scale = 1f };

            /*allow the user to select stuff in the tileset*/
            TilesetRect.MouseDown += (s, e) => { UserIsSelectingInTileset = true; selectionMouseDownLocation = ApplyGrid(s.TransformIntoLocalSpace(e.CurrentState.Location), true); };
            TilesetRect.MouseUp += (s, e) => { UserIsSelectingInTileset = false; UpdateFrame(); };
            TilesetRect.MouseMove += TilesetRect_MouseMove;

            /*visualize the user selection*/
            SelectionRect = new Rectangle2D(10, 10, new Vec2(), 1, System.Drawing.Color.FromArgb(80, System.Drawing.Color.Green), System.Drawing.Color.Black);
            SelectionRect.Visible = false;
            SelectionRect.Enabled = false;

            /*Visualize the selected tilesetpart*/
            FrameRect = new Rectangle2D(10, 10, new Vec2(), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, Animation.Tileset);
            FrameRect.Visible = false;
            renderTargetCurrentFrame.AddRenderObject(FrameRect);
            
            /*add the renderobject to the renderpipeline*/
            rendertargetTileset.AddRenderObject(TilesetRect);
            rendertargetTileset.AddRenderObject(SelectionRect);

            /*Update the grid that can be used while selection is done in the tileset*/
            UpdateGrid();

            /*render the animation itself*/
            renderTargetAnimation.AddRenderObject(Animation);

            /*Update the camera for the tileset so it's centered*/
            UpdateTilesetCamera();

            /*Update the grid if the user so desires*/
            checkBoxGrid.CheckedChanged += (s, e) => { UpdateGrid(); };
            numericUpDownGridWidth.ValueChanged +=(s,e) => { UpdateGrid(); };
            numericUpDownGridHeight.ValueChanged += (s, e) => { UpdateGrid(); };

            /*Update Animation properties if the user so desires*/
            numericUpDownAnimationFPS.ValueChanged += (s, e) => { Animation.Speed = Animation.SpeedFromFPS((float)numericUpDownAnimationFPS.Value); Animation.Reset(); };
            checkBoxLoopAnimation.CheckedChanged += (s, e) => { Animation.Loop = checkBoxLoopAnimation.Checked; Animation.Reset(); };
            checkBoxReverse.CheckedChanged += (s, e) => { Animation.IsReverseLoop = checkBoxReverse.Checked; Animation.Reset(); };
            numericUpDownScale.ValueChanged += (s, e) => { Animation.Scale = (float)numericUpDownScale.Value; };
            numericUpDownRotation.ValueChanged += (s, e) => { Animation.Rotation = (float)numericUpDownRotation.Value; };
            numericUpDownRotationX.ValueChanged += (s, e) => { Animation.RotationOffset = new Vec2((float)numericUpDownRotationX.Value, (float)numericUpDownRotationY.Value); };
            numericUpDownRotationY.ValueChanged += (s, e) => { Animation.RotationOffset = new Vec2((float)numericUpDownRotationX.Value, (float)numericUpDownRotationY.Value); };

            /*update the windows controls*/
            UpdateGUI();
        }


        void InitializeRenderTargets()
        {
            rendertargetTileset.Initialize();
            rendertargetTileset.EnableMouseInput();
            rendertargetTileset.EnableCameraControl(true);

            renderTargetAnimation.Initialize();
            renderTargetAnimation.EnableCameraControl(true);
            renderTargetCurrentFrame.Initialize();
            renderTargetCurrentFrame.EnableCameraControl(true);
        }

        void UpdateTilesetCamera()
        {
            rendertargetTileset.Camera.LookAt = new Box2DX.Common.Vec2(Animation.Tileset.Width / 2f, Animation.Tileset.Height / 2f);
        }

        /*Update the grid*/
        void UpdateGrid()
        {
            rendertargetTileset.RemoveRenderObject(TilesetGrid);
            if (checkBoxGrid.Checked)
            {
                TilesetGrid = new Grid2D(new Box2DX.Common.Vec2(), Animation.Tileset.Width, Animation.Tileset.Height, (int)numericUpDownGridWidth.Value, (int)numericUpDownGridHeight.Value, 0, System.Drawing.Color.Red);
                TilesetGrid.Enabled = false;
                rendertargetTileset.AddRenderObject(TilesetGrid);
            }
        }

        /*update user selection*/
        Vec2 ApplyGrid(Vec2 input, bool RoundUp)
        {
            if (RoundUp)
                return checkBoxGrid.Checked ? new Vec2((int)System.Math.Round(input.X / (float)numericUpDownGridWidth.Value) * (float)numericUpDownGridWidth.Value,
                                                       (int)System.Math.Round(input.Y / (float)numericUpDownGridHeight.Value) * (float)numericUpDownGridHeight.Value) : input;
            else
                return checkBoxGrid.Checked ? new Vec2((int)(input.X / (float)numericUpDownGridWidth.Value) * (float)numericUpDownGridWidth.Value,
                                                       (int)(input.Y / (float)numericUpDownGridHeight.Value) * (float)numericUpDownGridHeight.Value) : input;
        }


        /*Update the selection-rect*/
        private void TilesetRect_MouseMove(Graphics.RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            if (UserIsSelectingInTileset)
            {
                
                Vec2 location = ApplyGrid(Sender.TransformIntoLocalSpace(MouseEventArgs.CurrentState.Location), true);
                Vec2 topLeft = new Vec2(selectionMouseDownLocation.X < location.X ? selectionMouseDownLocation.X : location.X, selectionMouseDownLocation.Y < location.Y ? selectionMouseDownLocation.Y : location.Y);
                Vec2 bottomRight = new Vec2(selectionMouseDownLocation.X > location.X ? selectionMouseDownLocation.X : location.X, selectionMouseDownLocation.Y > location.Y ? selectionMouseDownLocation.Y : location.Y);
                SelectionRect.Update(topLeft, (int)(bottomRight.X - topLeft.X), (int)(bottomRight.Y - topLeft.Y));
                SelectionRect.Visible = true;
                UpdateFrame();
            }
        }


        /*update the user interface... all these controls*/
        void UpdateGUI()
        {
            /*Update the Trackbar so the user can scroll through the single frames*/
            int currentSelectedFrame = SelectedFrame;
            trackBarFrameSelection.Value = 0;
            trackBarFrameSelection.Maximum = Animation.Frames.Count - 1;
            if (trackBarFrameSelection.Maximum >= currentSelectedFrame)
                trackBarFrameSelection.Value = currentSelectedFrame;
        }


        /*create the animation for the lazy ppl like me*/
        private void buttonAutoFrame_Click(object sender, System.EventArgs e)
        {
            if (checkBoxGrid.Checked)
            {
                Animation.AutoCut((int)SelectionRect.Location.X, (int)SelectionRect.Location.Y, SelectionRect.Width, SelectionRect.Height, (int)numericUpDownGridWidth.Value, (int)numericUpDownGridHeight.Value);
                UpdateGUI();
                UpdateFrameDisplay();
            }
        }

        /*Update the frame by user description*/
        void UpdateFrame()
        {
            if (SelectedFrame >= 0 && SelectedFrame < Animation.Frames.Count)
            {
                Animation.Frames[SelectedFrame].StartX = (int)SelectionRect.Location.X;
                Animation.Frames[SelectedFrame].StartY = (int)SelectionRect.Location.Y;
                Animation.Frames[SelectedFrame].Width = SelectionRect.Width;
                Animation.Frames[SelectedFrame].Height = SelectionRect.Height;
                UpdateFrameDisplay();
            }

        }

        /*update the user-selected frame so it's displayed correctly*/
        void UpdateFrameDisplay()
        {
            if (SelectedFrame >= 0 && SelectedFrame < Animation.Frames.Count)
            {
                FrameRect.Update(new Vec2(), Animation.Frames[SelectedFrame].Width, Animation.Frames[SelectedFrame].Height);
                FrameRect.TextureSegment = new System.Drawing.RectangleF(Animation.Frames[SelectedFrame].StartX, Animation.Frames[SelectedFrame].StartY, Animation.Frames[SelectedFrame].Width, Animation.Frames[SelectedFrame].Height);
                FrameRect.Visible = true;
            }
        }

        /*select a new frame*/
        private void trackBarFrameSelection_ValueChanged(object sender, System.EventArgs e)
        {
            UpdateFrameDisplay();
        }

        /*remove current frame*/
        private void buttonRemoveFrame_Click(object sender, System.EventArgs e)
        {
            /*there has to be at least 1 frame for the animation to exist*/
            if (Animation.Frames.Count > 1)
            {
                Animation.RemoveFrame(Animation.Frames[SelectedFrame]);
                Animation.Reset();
                UpdateGUI();
                UpdateFrameDisplay();
            }
        }

        /*moving frames in order*/
        private void buttonMoveFrameTop_Click(object sender, System.EventArgs e)
        {
            if (Animation.SwapFrames(SelectedFrame, SelectedFrame - 1))
                trackBarFrameSelection.Value--;
        }

        private void buttonMoveFrameBottom_Click(object sender, System.EventArgs e)
        {
            if (Animation.SwapFrames(SelectedFrame, SelectedFrame + 1))
                trackBarFrameSelection.Value++;
        }


        /*add empty frame*/
        private void buttonAddFrame_Click(object sender, System.EventArgs e)
        {
            Animation.AddFrame(0, 0, 0, 0);
            UpdateGUI();
            trackBarFrameSelection.Value++;
            Animation.Reset();
        }
    }
}
