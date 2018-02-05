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
        Dictionary<AnimationState, Rectangle2D> RenderObjects;

        public AnimationEditor()
        {
            InitializeComponent();
        }

        public AnimationEditor(Animation Animation)
            :this()
        {
            this.Animation = Animation;

            /*Init renderobjects*/
            RenderObjects = new Dictionary<AnimationState, Rectangle2D>();


            /*initialize rendering*/
            RenderTarget.Initialize();
            RenderTarget.EnableCameraControl(true);

            /*Update render-objects*/
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
                    RenderObjects.Add(State, stateRenderRect);

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
