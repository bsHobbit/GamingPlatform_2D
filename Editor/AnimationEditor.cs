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
        public Animation Animation { get; private set; }
        AnimationState selectedState;
        AnimationState SelectedState
        {
            get => selectedState;
            set { selectedState = value; UpdateStateGUI(); }
        }
        AnimationTransition selectedTransition;
        AnimationTransition SelectedTransition
        {
            get => selectedTransition;
            set { selectedTransition = value; UpdateTransitionGUI(); }
        }
        Dictionary<AnimationState, Rectangle2D> RenderObjects_States;
        Dictionary<AnimationTransition, Line2D> RenderObjects_Transitions;
        GameCore.ContentManager GameContent;

        public AnimationEditor()
        {
            InitializeComponent();
            comboBoxTransitionCondition.Items.AddRange(System.Enum.GetNames(typeof(AnimationTransition.Condition.eConditionType)));
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

            RenderTarget.AddRenderObject(Animation);

            /*Update render-objects*/
            UpdateVisuals();
            UpdateAttributeList();

            FormClosing += (s, e) => { RenderTarget.StopRendering(); };

            textBoxAnimationName.Text = Animation.Name;
            /*allow the user to change the animation-name*/
            textBoxAnimationName.TextChanged += (s, e) =>
            {
                string newName = textBoxAnimationName.Text;
                if (GameContent.GetRenderableObject<Animation>(Name) == null)
                    Animation.Name = newName;
            };
            textBoxAnimationName.LostFocus += (s, e) => { textBoxAnimationName.Text = Animation.Name; };

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
                    MinStateTime = .1f,
                    IsFinalState = false
                };
                SelectedState = Animation.Entry;
            }
            /*create a new animationy state with a condition for the selected animationstate*/
            else if (SelectedState != null)
            {
                TilesetAnimation tsa = ContentBrowser.SelectTilesetAnimation(GameContent);
                /*let the user select a condition for this transition*/
                AnimationState newState = new AnimationState()
                {
                    TilesetAnimation = tsa,
                    MinStateTime = .1f,
                    IsFinalState = false 
                };
                SelectedState.AddTransitition(new AnimationTransition(new AnimationTransition.Condition(AnimationTransition.Condition.eConditionType.Equal, "", 0f), newState));
                SelectedState = newState;
            }
            else
                MessageBox.Show("Select a stat you want to start from.");

            UpdateVisuals();
        }

        /*remove an existing animation-state*/
        private void buttonRemoveState_Click(object sender, System.EventArgs e)
        {
            if (SelectedState != null)
            {
                /*remove the animationstate*/
                var removedTransitions = Animation.RemoveStateAndReferences(SelectedState);
                RenderTarget.RemoveRenderObject(RenderObjects_States[SelectedState]);
                RenderObjects_States.Remove(SelectedState);

                /*remove all references to this state*/
                for (int i = 0; i < removedTransitions.Count; i++)
                {
                    RenderTarget.RemoveRenderObject(RenderObjects_Transitions[removedTransitions[i]]);
                    RenderObjects_Transitions.Remove(removedTransitions[i]);
                }


                UpdateAnimationState_RenderObjects(Animation.Entry);
                SelectedState = null;
                SelectedTransition = null;

            }
        }

        /*Add an attribute to the animation*/
        private void buttonAddAttribute_Click(object sender, System.EventArgs e)
        {
            string newName = Animation.GetFreeAttributeName();
            Animation.AddAttribute(newName, 0f);
            RegisterAttributeControl(new AnimationAttributeEditor(Animation, newName, 0f));
        }

        /*Update state gui*/
        void UpdateStateGUI()
        {
            groupBoxState.Enabled = selectedState != null;
            if (selectedState != null)
            {
                /*the tmp stuff is to prevent gui events from changing values*/
                var tmpState = selectedState;
                selectedState = null;
                numericUpDownStateMinTime.Value = (decimal)tmpState.MinStateTime;
                checkBoxIsFinalState.Checked = tmpState.IsFinalState;
                selectedState = tmpState;
            }
        }

        /*Update Transition gui*/
        void UpdateTransitionGUI()
        {
            groupBoxTransitionSettings.Enabled = selectedTransition != null;
            if (selectedTransition != null)
            {
                /*the tmp stuff is to prevent gui events from changing values*/
                var tmpTransition = selectedTransition;
                selectedTransition = null;
                comboBoxTransitionAttribute.Text = tmpTransition.TransitionCondition.Attribute;
                comboBoxTransitionCondition.SelectedIndex = (int)tmpTransition.TransitionCondition.ConditionType;
                numericUpDownTransitionValue.Value = (decimal)((float)tmpTransition.TransitionCondition.Value);
                selectedTransition = tmpTransition;
            }
            else
            {
                comboBoxTransitionAttribute.Text = "";
            }
        }

        void UpdateAttributeCombobox()
        {
            comboBoxTransitionAttribute.Items.Clear();
            var attributeNames = Animation.GetAttributeNames();
            foreach (var item in attributeNames)
                comboBoxTransitionAttribute.Items.Add(item);
        }

        /*Update Attributelist*/
        void UpdateAttributeList()
        {
            var attributeNames = Animation.GetAttributeNames();
            foreach (var item in attributeNames)
                RegisterAttributeControl(new AnimationAttributeEditor(Animation, item, (float)Animation.GetAttribute(item)));
        }

        void RegisterAttributeControl (AnimationAttributeEditor control)
        {
            panelAttributes.Controls.Add(control);
            control.Dock = DockStyle.Top;
            control.RemoveAttribute += (s) => { Animation.RemoveAttribute(s.CurrentAttributeName); panelAttributes.Controls.Remove(s); UpdateAttributeCombobox(); };
            control.AttributeNameChanged += (s) => { UpdateAttributeCombobox(); };
            UpdateAttributeCombobox();
        }

        /*make sure everything is displayed properly*/
        void UpdateAnimationState_RenderObjects(AnimationState State)
        {
            if (State != null)
            {
                System.Drawing.Color outlineColor = State == Animation.Entry ? System.Drawing.Color.LimeGreen : System.Drawing.Color.White;
                string text = State.TilesetAnimation == null ? "ERROR" : State.TilesetAnimation.Name;
                RenderableText description = new RenderableText(text, new System.Drawing.Font("Arial", 12), System.Drawing.Color.White, new Vec2(), true);

                if (!RenderObjects_States.ContainsKey(State))
                {
                    /*create new render-object*/
                    
                    Rectangle2D stateRenderRect = new Rectangle2D((int)description.GetSize().X, (int)description.GetSize().Y, State.WindowLocation, 0, System.Drawing.Color.Gray, outlineColor);
                    stateRenderRect.AddText(description);
                    stateRenderRect.Tag = State;
                    stateRenderRect.EnableUserTranslation();

                    RenderObjects_States.Add(State, stateRenderRect);

                    /*make sure the user can select it*/
                    stateRenderRect.MouseDown += (s, e) => { SelectedState = s.Tag as AnimationState; SelectedTransition = null; };
                    stateRenderRect.LocationChanged += (s, e, x) => { ((AnimationState)s.Tag).WindowLocation = e; UpdateAnimationTransition_RenderObjects(Animation.Entry); };

                    /*make sure it's displayed*/
                    RenderTarget.AddRenderObject(stateRenderRect);
                }
                else
                {
                    RenderObjects_States[State].OutlineColor = outlineColor;
                    RenderObjects_States[State].ClearText();
                    RenderObjects_States[State].AddText(description);
                    RenderObjects_States[State].Update(RenderObjects_States[State].Location, (int)description.GetSize().X, (int)description.GetSize().Y);
                }

                for (int i = 0; i < State.PossibleTransitions.Count; i++)
                    UpdateAnimationState_RenderObjects(State.PossibleTransitions[i].TranslateInto);
            }
        }

        void UpdateAnimationTransition_RenderObjects(AnimationState State)
        {
            if (State != null)
            {
                for (int i = 0; i < State.PossibleTransitions.Count; i++)
                {
                    /*make sure the arrow starts and finishes at the correct locations*/
                    if (RenderObjects_States.ContainsKey(State.PossibleTransitions[i].TranslateInto) && RenderObjects_States.ContainsKey(State))
                    {
                        float xDiff = Math.Abs(RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location.X - RenderObjects_States[State].Location.X);
                        float yDiff = Math.Abs(RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location.Y - RenderObjects_States[State].Location.Y);
                        Vec2 startLocation;
                        Vec2 targetLocation;
                        if (yDiff > xDiff)
                        {
                            startLocation = RenderObjects_States[State].Location + new Vec2(RenderObjects_States[State].Width / 2, RenderObjects_States[State].Height);
                            targetLocation = RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location + new Vec2(RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Width / 2, 0);
                            if (RenderObjects_States[State].Location.Y > RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location.Y)
                            {
                                startLocation = RenderObjects_States[State].Location + new Vec2(RenderObjects_States[State].Width / 2, 0);
                                targetLocation = RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location + new Vec2(RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Width / 2, RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Height);
                            }
                        }
                        else
                        {
                            startLocation = RenderObjects_States[State].Location + new Vec2(RenderObjects_States[State].Width, RenderObjects_States[State].Height / 2);
                            targetLocation = RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location + new Vec2(0, RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Height / 2);
                            if (RenderObjects_States[State].Location.X > RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location.X)
                            {
                                startLocation = RenderObjects_States[State].Location + new Vec2(0, RenderObjects_States[State].Height / 2);
                                targetLocation = RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Location + new Vec2(RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Width, RenderObjects_States[State.PossibleTransitions[i].TranslateInto].Height / 2);
                            }
                        }

                        /*create the transition arrow visually for the user*/
                        if (!RenderObjects_Transitions.ContainsKey(State.PossibleTransitions[i]))
                        {
                            Line2D transitionLine = new Line2D(startLocation, targetLocation, new Vec2(), 1, System.Drawing.Color.Black, 2, true);
                            transitionLine.Tag = State.PossibleTransitions[i];
                            RenderObjects_Transitions.Add(State.PossibleTransitions[i], transitionLine);
                            RenderTarget.AddRenderObject(transitionLine);
                            transitionLine.MouseEnter += (s, e) => { s.Color = System.Drawing.Color.Blue; };
                            transitionLine.MouseLeave += (s, e) => { s.Color = System.Drawing.Color.Black; };

                            transitionLine.MouseDown += (s, e) => { SelectedTransition = (AnimationTransition)s.Tag; SelectedState = null; };

                        }
                        else
                            RenderObjects_Transitions[State.PossibleTransitions[i]].Update(startLocation, targetLocation);


                        UpdateAnimationTransition_RenderObjects(State.PossibleTransitions[i].TranslateInto);
                    }
                }
            }
        }

        void UpdateVisuals()
        {
            /*add all states first*/
            UpdateAnimationState_RenderObjects(Animation.Entry);

            /*now that every animationstate has it's own renderobject we can add the transitions*/
            UpdateAnimationTransition_RenderObjects(Animation.Entry);
        }

        
        /*Transition-Conidtion user input*/
        void UpdateTransitionCondition()
        {
            if (selectedTransition != null)
            {
                selectedTransition.TransitionCondition.Attribute = comboBoxTransitionAttribute.Text;
                selectedTransition.TransitionCondition.ConditionType = (AnimationTransition.Condition.eConditionType)comboBoxTransitionCondition.SelectedIndex;
                selectedTransition.TransitionCondition.Value = (float)numericUpDownTransitionValue.Value;
            }
        }

        private void comboBoxTransitionAttribute_SelectedIndexChanged(object sender, System.EventArgs e) { UpdateTransitionCondition(); }
        private void comboBoxTransitionCondition_SelectedIndexChanged(object sender, System.EventArgs e) { UpdateTransitionCondition(); }
        private void numericUpDownTransitionValue_ValueChanged(object sender, System.EventArgs e) { UpdateTransitionCondition(); }


        /*state user input*/
        private void numericUpDownStateMinTime_ValueChanged(object sender, System.EventArgs e)
        {
            if (selectedState != null)
            {
                selectedState.MinStateTime = (float)numericUpDownStateMinTime.Value;
            }
        }

        private void checkBoxIsFinalState_CheckedChanged(object sender, System.EventArgs e)
        {
            if (selectedState != null)
                selectedState.IsFinalState = checkBoxIsFinalState.Checked;
        }

        private void buttonUpdateTilesetAnimation_Click(object sender, System.EventArgs e)
        {
            if (selectedState != null)
            {
                TilesetAnimation newTSA = ContentBrowser.SelectTilesetAnimation(GameContent);
                if (newTSA != null)
                    selectedState.TilesetAnimation = newTSA;
                UpdateVisuals();
            }
        }
    }
}
