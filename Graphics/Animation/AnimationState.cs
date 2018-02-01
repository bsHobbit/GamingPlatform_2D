using System.Collections.Generic;

namespace Graphics.Animation
{
    public class AnimationState
    {

        /*member*/
        public TilesetAnimation TilesetAnimation { get; set; }
        float MinStateTime; /*minimun Tilesetanitiom time that has to be reached to be allowed to translate*/
        List<AnimationTransition> PossibleTranslations;


        /*ctor*/
        public AnimationState()
        {
            PossibleTranslations = new List<AnimationTransition>();
        }


        /*update the animationstate*/
        public AnimationState Update(float Elapsed, Animation Animation)
        {

            TilesetAnimation.Update(Elapsed); /*Update the underlying tileset animation*/

            /*check if a transition is possible*/
            if (TilesetAnimation.AnimationTime >= MinStateTime)
            {
                /*check if the animstion has to be changed*/
                for (int i = 0; i < PossibleTranslations.Count; i++)
                {
                    /*hey, there is a new state i can translate into let's do this!*/
                    if (PossibleTranslations[i].CanTranslateInto(Animation))
                        return PossibleTranslations[i].TranslateInto;
                }
                
            }

            /*Animationstate did not change*/
            return this;
        }
    }
}
