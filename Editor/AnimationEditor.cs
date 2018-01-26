using Graphics;
using System.Windows.Forms;
using Animation = Graphics.Animation.TilesetAnimation;


namespace Editor
{
    public partial class AnimationEditor : Form
    {
        Animation Animation;

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
            

            var mainRect = new Graphics.Geometry.Rectangle2D(Animation.Tileset.Width, Animation.Tileset.Height, new Box2DX.Common.Vec2(0, 0), -1, System.Drawing.Color.Transparent, System.Drawing.Color.Empty, Animation.Tileset) { Scale = 1f };
            rendertargetTileset.AddRenderObject(mainRect);
            rendertargetTileset.AddRenderObject(new Graphics.Geometry.Grid2D(new Box2DX.Common.Vec2(), Animation.Tileset.Width, Animation.Tileset.Height, 32, 32, 0, System.Drawing.Color.Red));

            renderTargetAnimation.AddRenderObject(Animation);

            UpdateTilesetCamera();
        }

        void InitializeRenderTargets()
        {
            rendertargetTileset.Initialize();
            rendertargetTileset.EnableMouseInput();
            rendertargetTileset.EnableCameraControl(true);

            renderTargetAnimation.Initialize();
            renderTargetAnimation.EnableCameraControl(true);
            //renderTargetCurrentFrame.Initialize();
        }

        void UpdateTilesetCamera()
        {
            rendertargetTileset.Camera.LookAt = new Box2DX.Common.Vec2(Animation.Tileset.Width / 2f, Animation.Tileset.Height / 2f);
        }

        void UpdateGUI()
        {

        }


        /*create the animation for the lazy ppl*/
        private void buttonAutoFrame_Click(object sender, System.EventArgs e)
        {
            Animation.AutoCut((int)numericUpDownAutoFrameWidth.Value, (int)numericUpDownAutoFrameHeight.Value);
        }
    }
}
