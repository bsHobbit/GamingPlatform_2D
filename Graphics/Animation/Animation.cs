
using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class Animation : RenderableObject2D
    {
        Dictionary<string, object> Attributes;

        AnimationState entry;
        public AnimationState Entry
        {
            get => entry;
            set
            {
                entry = value;
                if (CurrentAnimationState == null)
                    CurrentAnimationState = entry;
                UpdateRenderParameter();
            }
        }

        AnimationState CurrentAnimationState;


        /*ctor*/
        public Animation(Vec2 Location, int Z)
        {
            Attributes = new Dictionary<string, object>();
            Initialize(Location, Z, new List<Vec2>(), System.Drawing.Color.Transparent, System.Drawing.Color.Empty);
        }

        /*make sure the animation state is updated*/
        public override void Update(float Elapsed)
        {
            if (CurrentAnimationState != null)
            {
                AnimationState newState = CurrentAnimationState.Update(Elapsed, this);
                if (newState != CurrentAnimationState)
                    CurrentAnimationState = newState;
                UpdateRenderParameter();
            }
        }


        /*make sure the current-animationstate is rendered correct*/
        void UpdateRenderParameter()
        {
            if (CurrentAnimationState != null && CurrentAnimationState.TilesetAnimation != null)
                CurrentAnimationState.TilesetAnimation.CopyRenderParameter(this);
        }

        /*state management*/
        public List<AnimationTransition> RemoveStateAndReferences(AnimationState State)
        {
            List<AnimationTransition> removedTransitions = new List<AnimationTransition>();
            RemoveStateAndReferences(Entry, State, removedTransitions);
            for (int i = 0; i < State.PossibleTransitions.Count; i++)
                removedTransitions.Add(State.PossibleTransitions[i]);

            if (State == entry)
            {
                if (entry.PossibleTransitions.Count > 0)
                    entry = entry.PossibleTransitions[0].TranslateInto;
                else
                    entry = null;
            }

            if (State == CurrentAnimationState)
                CurrentAnimationState = Entry;

            return removedTransitions;
        }

        void RemoveStateAndReferences(AnimationState CurrentState, AnimationState StateToRemove, List<AnimationTransition> removedTransitions)
        {
            for (int i = 0; i < CurrentState.PossibleTransitions.Count; i++)
            {
                RemoveStateAndReferences(CurrentState.PossibleTransitions[i].TranslateInto, StateToRemove, removedTransitions);

                List<AnimationTransition> tmpRemovedList = new List<AnimationTransition>();
                if (CurrentState.PossibleTransitions[i].TranslateInto == StateToRemove)
                {
                    removedTransitions.Add(CurrentState.PossibleTransitions[i]);
                    tmpRemovedList.Add(CurrentState.PossibleTransitions[i]);
                }

                for (int j = 0; j < tmpRemovedList.Count; j++)
                    CurrentState.RemoveTransition(tmpRemovedList[j]);
            }
        }

        /*remove tileset-animation reference*/
        public void RemoveTilesetReference(AnimationState CurrentState, TilesetAnimation TilesetAnimation)
        {
            if (CurrentState != null)
            {
                if (CurrentState.TilesetAnimation == TilesetAnimation)
                    CurrentState.TilesetAnimation = null;
                for (int i = 0; i < CurrentState.PossibleTransitions.Count; i++)
                    RemoveTilesetReference(CurrentState.PossibleTransitions[i].TranslateInto, TilesetAnimation);
            }
        }
        public void RemoveTilesetReference(TilesetAnimation TilesetAnimation)
        {
            RemoveTilesetReference(Entry, TilesetAnimation);
        }

        /*Attribute management*/
        public List<string> GetAttributeNames()
        {
            List<string> result = new List<string>();
            foreach (var item in Attributes)
                result.Add(item.Key);
            return result;
        }

        public object GetAttribute(string Name)
        {
            if (Attributes.ContainsKey(Name))
                return Attributes[Name];
            return null;
        }

        public void SetAttribute(string Name, object Value)
        {
            if (Attributes.ContainsKey(Name))
                Attributes[Name] = Value;
        }

        public string GetFreeAttributeName()
        {
            string name = "attr_01";
            int cnt = 2;
            while (Attributes.ContainsKey(name))
            {
                name = "attr_" + cnt.ToString("00");
                cnt++;
            }
            return name;
        }

        public void AddAttribute(string Name, object Value)
        {
            if (IsFreeAttributeName(Name))
                Attributes.Add(Name, Value);
        }

        public void RemoveAttribute(string Name)
        {
            if (Attributes.ContainsKey(Name))
                Attributes.Remove(Name);
        }

        public bool IsFreeAttributeName(string Name) => !Attributes.ContainsKey(Name);

        public void UpdateAttributeName(string CurrentName, string NewName)
        {
            if (Attributes.ContainsKey(CurrentName) && !Attributes.ContainsKey(NewName))
            {
                object currentValue = Attributes[CurrentName];
                Attributes.Remove(CurrentName);
                Attributes.Add(NewName, currentValue);
            }
        }

        /*make a copy of the animation*/
        public override RenderableObject2D Clone(bool DeepCopy = false)
        {
            Animation result = new Animation(Location, ZLocation);
            CopyBaseInfo(ref result);
            result.Entry = Entry.Clone(DeepCopy);
            return result;
        }
    }
}
