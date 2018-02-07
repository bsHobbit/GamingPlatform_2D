using Graphics.Animation;
using System;

namespace GameCore
{
    public partial class ContentManager
    {
        void SaveAnimations(string Path)
        {
            var animations = Animations;
            foreach (var animation in animations)
            {
                string animationFile = Path + animation.Name + ".ani";
                System.IO.StreamWriter sw = new System.IO.StreamWriter(animationFile, false);
                sw.WriteLine(animation.Name);
                sw.WriteLine(animation.Location.X);
                sw.WriteLine(animation.Location.Y);
                sw.WriteLine(animation.Rotation);
                sw.WriteLine(animation.RotationOffset.X);
                sw.WriteLine(animation.RotationOffset.Y);
                sw.WriteLine(animation.Scale);

                var attributes = animation.GetAttributeNames();
                sw.WriteLine(attributes.Count);
                for (int i = 0; i < attributes.Count; i++)
                {
                    sw.WriteLine(attributes[i]);
                    sw.WriteLine((float)animation.GetAttribute(attributes[i]));
                }

                if (animation.Entry == null)
                    sw.WriteLine("null");
                else
                {
                    sw.WriteLine("ENTRYSTATE");
                    WriteAnimationState(animation.Entry, sw);
                }

                sw.Close();
            }
        }

        void WriteAnimationState(AnimationState State, System.IO.StreamWriter sw)
        {
            if (State.TilesetAnimation == null)
                sw.WriteLine("");
            else
                sw.WriteLine(State.TilesetAnimation.Name);
            sw.WriteLine(State.IsFinalState);
            sw.WriteLine(State.MinStateTime);
            sw.WriteLine(State.WindowLocation.X);
            sw.WriteLine(State.WindowLocation.Y);
            sw.WriteLine(State.PossibleTransitions.Count);

            for (int i = 0; i < State.PossibleTransitions.Count; i++)
            {
                var transition = State.PossibleTransitions[i];
                sw.WriteLine(transition.TransitionCondition.Attribute);
                sw.WriteLine((int)transition.TransitionCondition.ConditionType);
                sw.WriteLine((float)transition.TransitionCondition.Value);
                if (transition.TranslateInto == null)
                    sw.WriteLine("null");
                else
                    WriteAnimationState(transition.TranslateInto, sw);
            }

            
        }

        void LoadAnimation(string File)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(File);
            
            string animationName = sr.ReadLine();
            float locationX = (float)Convert.ToDouble(sr.ReadLine());
            float locationY = (float)Convert.ToDouble(sr.ReadLine());
            float rotation = (float)Convert.ToDouble(sr.ReadLine());
            float rotationX = (float)Convert.ToDouble(sr.ReadLine());
            float rotationY = (float)Convert.ToDouble(sr.ReadLine());
            float scale = (float)Convert.ToDouble(sr.ReadLine());



            Animation animation = new Animation(new Box2DX.Common.Vec2(locationX, locationY), 0)
            {
                Rotation = rotation,
                RotationOffset = new Box2DX.Common.Vec2(rotationX, rotationY),
                Scale = scale,
                Name = animationName
            };

            int totalAttributes = Convert.ToInt32(sr.ReadLine());
            for (int i = 0; i < totalAttributes; i++)
                animation.AddAttribute(sr.ReadLine(), (float)Convert.ToDouble(sr.ReadLine()));

            bool hasEntryState = sr.ReadLine() != "null";

            if (hasEntryState)
                animation.Entry = ReadAnimationState(sr);

            sr.Close();

            AddRenderableObject(animation);
        }

        AnimationState ReadAnimationState(System.IO.StreamReader sr)
        {
            AnimationState result = new AnimationState();
            string tilesetAnimationName = sr.ReadLine();
            result.IsFinalState = Convert.ToBoolean(sr.ReadLine());
            result.MinStateTime = (float)Convert.ToDouble(sr.ReadLine());
            result.WindowLocation = new Box2DX.Common.Vec2((float)Convert.ToDouble(sr.ReadLine()), (float)Convert.ToDouble(sr.ReadLine()));
            int possibleTransitionCount = Convert.ToInt32(sr.ReadLine());
            result.TilesetAnimation = GetRenderableObject<TilesetAnimation>(tilesetAnimationName);

            for (int i = 0; i < possibleTransitionCount; i++)
            {
                string conditionAttribute = sr.ReadLine();
                AnimationTransition.Condition.eConditionType conditionType = (AnimationTransition.Condition.eConditionType)(Convert.ToInt32(sr.ReadLine()));
                float conditionValue = (float)Convert.ToDouble(sr.ReadLine());
                result.AddTransitition(new AnimationTransition(new AnimationTransition.Condition(conditionType, conditionAttribute, conditionValue), ReadAnimationState(sr)));
            }
            

            return result;
        }
    }
}