
using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class Animation 
    {
        Dictionary<string, object> Attributes;

        AnimationState Entry;
        AnimationState CurrentAnimationState;

        public Animation()
        {
            Attributes = new Dictionary<string, object>();
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
    }
}
