﻿using Box2DX.Common;
using System.Collections.Generic;

namespace Graphics.Animation
{
    public class AnimationState
    {

        /*member*/
        public TilesetAnimation TilesetAnimation { get; set; }
        public float MinStateTime { get; set; } /*minimun Tilesetanitiom time that has to be reached to be allowed to translate*/
        public bool IsFinalState { get; set; } /*if the state is not final and has no possible transitions it will enter the entry state*/
        List<AnimationTransition> possibleTransitions;
        public List<AnimationTransition> PossibleTransitions
        {
            get => possibleTransitions;
        }

        /*onyl for the editor*/
        public Vec2 WindowLocation { get; set; }


        /*ctor*/
        public AnimationState()
        {
            possibleTransitions = new List<AnimationTransition>();
            MinStateTime = 0;
        }

        public void AddTransitition(AnimationTransition Transition)
        {
            if (!possibleTransitions.Contains(Transition))
                possibleTransitions.Add(Transition);
        }

        public void RemoveTransition(AnimationTransition Transition)
        {
            if (possibleTransitions.Contains(Transition))
                possibleTransitions.Remove(Transition);
        }




        /*update the animationstate*/
        public AnimationState Update(float Elapsed, Animation Animation)
        {

            TilesetAnimation.Update(Elapsed); /*Update the underlying tileset animation*/

            /*check if a transition is possible*/
            if (TilesetAnimation.AnimationTime >= MinStateTime)
            {
                if (!IsFinalState && possibleTransitions.Count == 0)
                    return Animation.Entry.Update(Elapsed, Animation); 

                /*check if the animstion has to be changed*/
                for (int i = 0; i < possibleTransitions.Count; i++)
                {
                    /*hey, there is a new state i can translate into let's do this!*/
                    if (possibleTransitions[i].CanTranslateInto(Animation))
                        return possibleTransitions[i].TranslateInto;
                }
            }

            /*Animationstate did not change*/
            return this;
        }

    }
}
