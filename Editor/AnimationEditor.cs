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

            rendertargetTileset.Initialize();
            rendertargetTileset.EnableMouseInput();
            rendertargetTileset.OnUpdate += RendertargetTileset_OnUpdate;


            var mainRect = new Graphics.Geometry.Rectangle2D(500, 500, new Box2DX.Common.Vec2(0, 0), -1, System.Drawing.Color.Red, System.Drawing.Color.Black, Animation.Tileset) { Scale = 1f };
            mainRect.MouseEnter += MainRect_MouseEnter;
            mainRect.MouseLeave += MainRect_MouseLeave;
            mainRect.MouseDown += (s, e) => { Text = "Down"; };
            mainRect.MouseUp += (s, e) => { Text = "Up"; };
            mainRect.Click += (s, e) => { Text = "Click"; };
            mainRect.DoubleClick += (s, e) => { Text = "DoubleClick"; };
            rendertargetTileset.AddRenderObject(mainRect);
            rendertargetTileset.AddRenderObject(new Graphics.Geometry.Rectangle2D(100, 100, new Box2DX.Common.Vec2(), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Black));
            rendertargetTileset.AddRenderObject(new Graphics.Geometry.Rectangle2D(100, 100, new Box2DX.Common.Vec2(500, 0), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Black, Animation.Tileset) { Scale = 1f });
            rendertargetTileset.AddRenderObject(new Graphics.Geometry.Rectangle2D(100, 100, new Box2DX.Common.Vec2(500, 500), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Black));
            rendertargetTileset.AddRenderObject(new Graphics.Geometry.Rectangle2D(100, 100, new Box2DX.Common.Vec2(0, 500), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Black));
            rendertargetTileset.AddRenderObject(new Graphics.Geometry.Rectangle2D(10, 10, new Box2DX.Common.Vec2(50, 50),0, System.Drawing.Color.Purple, System.Drawing.Color.Black));
        }

        private void MainRect_MouseLeave(RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            Text = "Leave";
        }

        private void MainRect_MouseEnter(RenderableObject2D Sender, Core.IO.Mouse MouseEventArgs)
        {
            Text = "Enter";
        }

        private void RendertargetTileset_OnUpdate(float obj)
        {
        }

        void UpdateGUI()
        {

        }
    }
}
