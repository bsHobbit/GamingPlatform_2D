
using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class Animation : RenderableObject2D
    {
        Dictionary<string, object> Attributes;

        AnimationState entry;
        AnimationState Entry
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
                {
                    CurrentAnimationState = newState;
                    UpdateRenderParameter();
                }
            }
        }


        /*make sure the current-animationstate is rendered correct*/
        void UpdateRenderParameter()
        {
            if (CurrentAnimationState != null)
                CurrentAnimationState.TilesetAnimation.CopyRenderParameter(this);
        }


        /*Attribute management*/
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
    }
}
