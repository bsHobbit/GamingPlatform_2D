using Box2DX.Common;
using Graphics.Geometry;
using System.Collections.Generic;
using GameCore;
using System.Windows.Forms;
using Graphics.Animation;



namespace Editor
{
    public partial class TilesetAnimationEditor : Form
    {
        TilesetAnimation TilesetAnimation;
        Rectangle2D TilesetRect;
        Rectangle2D SelectionRect;
        Rectangle2D FrameRect;
        Grid2D TilesetGrid;
        ContentManager GameContent;

        int SelectedFrame { get => trackBarFrameSelection.Value; }

        bool UserIsSelectingInTileset;
        Vec2 selectionMouseDownLocation;

        public TilesetAnimationEditor()
        {
            InitializeComponent();
        }

        public TilesetAnimationEditor(TilesetAnimation TilesetAnimation, ContentManager GameContent)
            : this()
        {
            /*keep references to every info that is needed*/
            this.GameContent = GameContent;
            this.TilesetAnimation = TilesetAnimation;
            Text += " - " + TilesetAnimation.Name;

            InitializeRenderTargets();

            /*add at least one frame to the animation to work with*/
            if (TilesetAnimation.Frames.Count == 0)
                TilesetAnimation.AddFrame(0, 0, 1, 1);

            /*user isnt doing any input*/
            UserIsSelectingInTileset = false;

            /*render the tileset into this rectangle*/
            TilesetRect = new Rectangle2D(TilesetAnimation.Texture.Width, TilesetAnimation.Texture.Height, new Vec2(0, 0), -1, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, TilesetAnimation.Texture);

            /*visualize the user selection*/
            SelectionRect = new Rectangle2D(10, 10, new Vec2(), 1, System.Drawing.Color.FromArgb(80, System.Drawing.Color.Green), System.Drawing.Color.Black);
            SelectionRect.Visible = false;
            SelectionRect.Enabled = false;

            /*Visualize the selected tilesetpart*/
            FrameRect = new Rectangle2D(10, 10, new Vec2(), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, TilesetAnimation.Texture);
            FrameRect.Visible = false;

            /*add the renderobject to the renderpipeline*/
            rendertargetTileset.AddRenderObject(TilesetRect);
            renderTargetCurrentFrame.AddRenderObject(FrameRect);
            rendertargetTileset.AddRenderObject(SelectionRect);

            /*Update the grid that can be used while selection is done in the tileset*/
            UpdateGrid();

            /*render the animation itself*/
            renderTargetAnimation.AddRenderObject(TilesetAnimation);

            /*Update the camera for the tileset so it's centered*/
            UpdateTilesetCamera();

            /*Update the grid if the user so desires*/
            checkBoxGrid.CheckedChanged += (s, e) => { UpdateGrid(); };
            numericUpDownGridWidth.ValueChanged +=(s,e) => { UpdateGrid(); };
            numericUpDownGridHeight.ValueChanged += (s, e) => { UpdateGrid(); };

            /*Update Animation properties if the user so desires*/
            numericUpDownAnimationFPS.ValueChanged += (s, e) => { TilesetAnimation.FPS = (int)numericUpDownAnimationFPS.Value; TilesetAnimation.Reset(); };
            checkBoxLoopAnimation.CheckedChanged += (s, e) => { TilesetAnimation.Loop = checkBoxLoopAnimation.Checked; TilesetAnimation.Reset(); };
            checkBoxReverse.CheckedChanged += (s, e) => { TilesetAnimation.IsReverseLoop = checkBoxReverse.Checked; TilesetAnimation.Reset(); };
            numericUpDownScale.ValueChanged += (s, e) => { TilesetAnimation.Scale = (float)numericUpDownScale.Value; };
            numericUpDownRotation.ValueChanged += (s, e) => { TilesetAnimation.Rotation = (float)numericUpDownRotation.Value; };
            numericUpDownRotationX.ValueChanged += (s, e) => { TilesetAnimation.RotationOffset = new Vec2((float)numericUpDownRotationX.Value, (float)numericUpDownRotationY.Value); };
            numericUpDownRotationY.ValueChanged += (s, e) => { TilesetAnimation.RotationOffset = new Vec2((float)numericUpDownRotationX.Value, (float)numericUpDownRotationY.Value); };

            /*allow the user to select stuff in the tileset*/
            TilesetRect.MouseDown += (s, e) => { UserIsSelectingInTileset = true; selectionMouseDownLocation = ApplyGrid(s.TransformIntoLocalSpace(e.CurrentState.Location), true); };
            TilesetRect.MouseUp += (s, e) => { UserIsSelectingInTileset = false; UpdateFrame(); };
            TilesetRect.MouseMove += TilesetRect_MouseMove;
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
            rendertargetTileset.Camera.LookAt = new Vec2(TilesetAnimation.Texture.Width / 2f, TilesetAnimation.Texture.Height / 2f);
        }


        /*let the user select a texture for the animation*/
        private void buttonSelectTexture_Click(object sender, System.EventArgs e)
        {
            ContentBrowser browser = new ContentBrowser();
            browser.Initialize(GameContent, true, ContentBrowser.eBrowsers.Texture | ContentBrowser.eBrowsers.TilesetAnimations);

            /*Update the texture if the user really wants to...*/
            if (browser.ShowDialog() == DialogResult.OK )
            {
                if (browser.SelectedTexture != null)
                {
                    TilesetAnimation.Texture = browser.SelectedTexture;

                    /*Update the visuals for this editor too*/
                    TilesetRect.Update(new Vec2(), browser.SelectedTexture.Width, browser.SelectedTexture.Height);
                    TilesetRect.Texture = browser.SelectedTexture;
                    FrameRect.Texture = browser.SelectedTexture;
                    UpdateGrid();
                    UpdateTilesetCamera();
                }
            }
        }

        /*Update the grid*/
        void UpdateGrid()
        {
            rendertargetTileset.RemoveRenderObject(TilesetGrid);
            if (checkBoxGrid.Checked)
            {
                TilesetGrid = new Grid2D(new Box2DX.Common.Vec2(), TilesetAnimation.Texture.Width, TilesetAnimation.Texture.Height, (int)numericUpDownGridWidth.Value, (int)numericUpDownGridHeight.Value, 0, System.Drawing.Color.Red);
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
            trackBarFrameSelection.Maximum = TilesetAnimation.Frames.Count - 1;
            if (trackBarFrameSelection.Maximum >= currentSelectedFrame)
                trackBarFrameSelection.Value = currentSelectedFrame;

            checkBoxLoopAnimation.Checked = TilesetAnimation.Loop;
            checkBoxReverse.Checked = TilesetAnimation.IsReverseLoop;
            numericUpDownAnimationFPS.Value = TilesetAnimation.FPS;
            numericUpDownRotation.Value = (decimal)TilesetAnimation.Rotation;
            numericUpDownRotationX.Value = (decimal)TilesetAnimation.RotationOffset.X;
            numericUpDownRotationY.Value = (decimal)TilesetAnimation.RotationOffset.Y;
        }


        /*create the animation for the lazy ppl like me*/
        private void buttonAutoFrame_Click(object sender, System.EventArgs e)
        {
            if (checkBoxGrid.Checked)
            {
                TilesetAnimation.AutoCut((int)SelectionRect.Location.X, (int)SelectionRect.Location.Y, SelectionRect.Width, SelectionRect.Height, (int)numericUpDownGridWidth.Value, (int)numericUpDownGridHeight.Value);
                UpdateGUI();
                UpdateFrameDisplay();
            }
        }

        /*Update the frame by user description*/
        void UpdateFrame()
        {
            if (SelectedFrame >= 0 && SelectedFrame < TilesetAnimation.Frames.Count)
            {
                TilesetAnimation.Frames[SelectedFrame].StartX = (int)SelectionRect.Location.X;
                TilesetAnimation.Frames[SelectedFrame].StartY = (int)SelectionRect.Location.Y;
                TilesetAnimation.Frames[SelectedFrame].Width = SelectionRect.Width;
                TilesetAnimation.Frames[SelectedFrame].Height = SelectionRect.Height;
                UpdateFrameDisplay();
            }

        }

        /*update the user-selected frame so it's displayed correctly*/
        void UpdateFrameDisplay()
        {
            if (SelectedFrame >= 0 && SelectedFrame < TilesetAnimation.Frames.Count)
            {
                FrameRect.Update(new Vec2(), TilesetAnimation.Frames[SelectedFrame].Width, TilesetAnimation.Frames[SelectedFrame].Height);
                FrameRect.TextureSegment = new System.Drawing.RectangleF(TilesetAnimation.Frames[SelectedFrame].StartX, TilesetAnimation.Frames[SelectedFrame].StartY, TilesetAnimation.Frames[SelectedFrame].Width, TilesetAnimation.Frames[SelectedFrame].Height);
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
            if (TilesetAnimation.Frames.Count > 1)
            {
                TilesetAnimation.RemoveFrame(TilesetAnimation.Frames[SelectedFrame]);
                TilesetAnimation.Reset();
                UpdateGUI();
                UpdateFrameDisplay();
            }
        }

        /*moving frames in order*/
        private void buttonMoveFrameTop_Click(object sender, System.EventArgs e)
        {
            if (TilesetAnimation.SwapFrames(SelectedFrame, SelectedFrame - 1))
                trackBarFrameSelection.Value--;
        }

        private void buttonMoveFrameBottom_Click(object sender, System.EventArgs e)
        {
            if (TilesetAnimation.SwapFrames(SelectedFrame, SelectedFrame + 1))
                trackBarFrameSelection.Value++;
        }


        /*add empty frame*/
        private void buttonAddFrame_Click(object sender, System.EventArgs e)
        {
            TilesetAnimation.AddFrame(0, 0, 0, 0);
            UpdateGUI();
            trackBarFrameSelection.Value++;
            TilesetAnimation.Reset();
        }

        /*stop the rendering as soon as the form is closing*/
        private void TilesetAnimationEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            renderTargetAnimation.StopRendering();
            renderTargetCurrentFrame.StopRendering();
            rendertargetTileset.StopRendering();
        }
    }
}
