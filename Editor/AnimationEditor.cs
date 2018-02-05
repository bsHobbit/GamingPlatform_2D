using System.Collections.Generic;
using System.Windows.Forms;

using Graphics;
using Graphics.Animation;
using Graphics.Geometry;

using Box2DX.Common;


namespace Editor
{
    public partial class AnimationEditor : Form
    {
        Animation Animation;
        AnimationState SelectedState;
        Dictionary<AnimationState, Rectangle2D> RenderObjects;
        GameCore.ContentManager GameContent;

        public AnimationEditor()
        {
            InitializeComponent();
        }

        public AnimationEditor(Animation Animation, GameCore.ContentManager GameContent)
            :this()
        {
            this.Animation = Animation;
            this.GameContent = GameContent;

            /*Init renderobjects*/
            RenderObjects = new Dictionary<AnimationState, Rectangle2D>();


            /*initialize rendering*/
            RenderTarget.Initialize();
            RenderTarget.EnableCameraControl(true);

            /*Update render-objects*/
            UpdateVisuals();
        }

        /*Manage states*/
        private void buttonAddState_Click(object sender, System.EventArgs e)
        {

            TilesetAnimation tsa = ContentBrowser.SelectTilesetAnimation(GameContent);
            if (tsa != null)
            {
                /*create the entry point*/
                if (SelectedState == null && Animation.Entry == null)
                {
                    Animation.Entry = new AnimationState()
                    {
                        TilesetAnimation = tsa,
                        MinStateTime = 1f
                    };
                }
                /*create a new animationy state with a condition for the selected animationstate*/
                else if (SelectedState != null)
                {
                    /*let the user select a condition for this transition*/
                }
            }

            UpdateVisuals();
        }

        /*make sure everything is displayed properly*/
        void AddAnimationState(AnimationState State)
        {
            if (State != null)
            {
                if (!RenderObjects.ContainsKey(State))
                {
                    /*create new render-object*/
                    RenderableText description = new RenderableText(State.TilesetAnimation.Name, new System.Drawing.Font("Arial", 12), System.Drawing.Color.White, new Vec2(), true);
                    Rectangle2D stateRenderRect = new Rectangle2D((int)description.GetSize().X, (int)description.GetSize().Y, State.WindowLocation, 0, System.Drawing.Color.Gray, System.Drawing.Color.White);
                    stateRenderRect.AddText(description);
                    stateRenderRect.Tag = State;
                    stateRenderRect.EnableUserTranslation();
                    RenderObjects.Add(State, stateRenderRect);

                    /*make sure the user can select it*/
                    stateRenderRect.Click += (s, e) => { SelectedState = s.Tag as AnimationState; };

                    /*make sure it's displayed*/
                    RenderTarget.AddRenderObject(stateRenderRect);
                }

                for (int i = 0; i < State.PossibleTransitions.Count; i++)
                    AddAnimationState(State.PossibleTransitions[i].TranslateInto);
            }
        }

        void AddAnimationTransition(AnimationState State)
        {
            if (State != null)
            {
                for (int i = 0; i < State.PossibleTransitions.Count; i++)
                    AddAnimationTransition(State.PossibleTransitions[i].TranslateInto);
            }
        }

        void UpdateVisuals()
        {
            /*add all states first*/
            AddAnimationState(Animation.Entry);


            /*now that every animationstate has it's own renderobject we can add the transitions*/
            AddAnimationTransition(Animation.Entry);
            
        }

    }
}
