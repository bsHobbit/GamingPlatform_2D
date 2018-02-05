﻿using System.Collections.Generic;
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
        Dictionary<AnimationState, Rectangle2D> RenderObjects_States;
        Dictionary<AnimationTransition, Line2D> RenderObjects_Transitions;
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
            RenderObjects_States = new Dictionary<AnimationState, Rectangle2D>();
            RenderObjects_Transitions = new Dictionary<AnimationTransition, Line2D>();


            /*initialize rendering*/
            RenderTarget.Initialize();
            RenderTarget.EnableCameraControl(true);

            /*Update render-objects*/
            UpdateVisuals();
        }

        /*Manage states*/
        private void buttonAddState_Click(object sender, System.EventArgs e)
        {

            /*create the entry point*/
            if (SelectedState == null && Animation.Entry == null)
            {
                TilesetAnimation tsa = ContentBrowser.SelectTilesetAnimation(GameContent);
                Animation.Entry = new AnimationState()
                {
                    TilesetAnimation = tsa,
                    MinStateTime = 1f
                };
            }
            /*create a new animationy state with a condition for the selected animationstate*/
            else if (SelectedState != null)
            {
                TilesetAnimation tsa = ContentBrowser.SelectTilesetAnimation(GameContent);
                /*let the user select a condition for this transition*/
                AnimationState newState = new AnimationState()
                {
                    TilesetAnimation = tsa,
                    MinStateTime = 1f
                };
                SelectedState.AddTransitition(new AnimationTransition(new AnimationTransition.Condition(AnimationTransition.Condition.eConditionType.Equal, "", 0f), newState));
            }

            UpdateVisuals();
        }

        /*make sure everything is displayed properly*/
        void AddAnimationState(AnimationState State)
        {
            if (State != null)
            {
                if (!RenderObjects_States.ContainsKey(State))
                {
                    /*create new render-object*/
                    RenderableText description = new RenderableText(State.TilesetAnimation.Name, new System.Drawing.Font("Arial", 12), System.Drawing.Color.White, new Vec2(), true);
                    Rectangle2D stateRenderRect = new Rectangle2D((int)description.GetSize().X, (int)description.GetSize().Y, State.WindowLocation, 0, System.Drawing.Color.Gray, System.Drawing.Color.White);
                    stateRenderRect.AddText(description);
                    stateRenderRect.Tag = State;
                    stateRenderRect.EnableUserTranslation();
                    RenderObjects_States.Add(State, stateRenderRect);

                    /*make sure the user can select it*/
                    stateRenderRect.MouseDown += (s, e) => { SelectedState = s.Tag as AnimationState; };
                    stateRenderRect.LocationChanged += (s, e, x) => { AddAnimationTransition(Animation.Entry);  };

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
                {
                    Vec2 startLocation = RenderObjects_States[State].Location + (new Vec2(RenderObjects_States[State].Width, RenderObjects_States[State].Height) * .5f);
                    Vec2 targetLocation = RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location + (new Vec2(RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Width, RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Height) * .5f);

                    if (!RenderObjects_Transitions.ContainsKey(State.PossibleTransitions[i]))
                    {
                        Line2D transitionLine = new Line2D(startLocation, targetLocation, new Vec2(), -1, System.Drawing.Color.Black, 2);
                        RenderObjects_Transitions.Add(State.PossibleTransitions[i], transitionLine);
                        RenderTarget.AddRenderObject(transitionLine);

                    }
                    else
                        RenderObjects_Transitions[State.PossibleTransitions[i]].Update(startLocation, targetLocation);
                        

                    AddAnimationTransition(State.PossibleTransitions[i].TranslateInto);
                }
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
