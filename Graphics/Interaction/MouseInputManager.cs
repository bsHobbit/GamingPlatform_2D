using System.Collections.Generic;
using Box2DX.Common;
using Core.IO;

namespace Graphics.Interaction
{
    public class MouseInputManager
    {
        /*constants*/
        const float DOUBLECLICK_TIME_MS = 150;


        /*Members*/
        List<RenderableObject2D> Items;
        RenderableObject2D ItemAtMouse;
        Mouse Input;
        bool checkForDoubleClick = false;
        bool callMouseUp = false;
        float doubleClickTimer = 0;

        
        /*ctor*/
        public MouseInputManager(Mouse Input, List<RenderableObject2D> Items)
        {
            this.Items = Items;
            this.Input = Input;
            ItemAtMouse = null;
        }


        /*logic*/

        bool IsMouseOverItem(RenderableObject2D Item) => Item.IsPointInsideObject(Input.CurrentState.Location);

        RenderableObject2D GetItemAtMouse()
        {
            if (Items != null && Items.Count > 0)
                for (int i = Items.Count - 1; i >= 0; i--)
                    if (IsMouseOverItem(Items[i]))
                        return Items[i];
            return null;
        }

        /*Input handling - it's just a big state machine dont bother looking into it as long as it's working ;)
         * this most likely wont work when the user is pressing multiple mousebuttons at once ... who cares what the user wants anyway*/
        public void Update(float Elapsed)
        {

            if (checkForDoubleClick)
                doubleClickTimer += Elapsed;

            /*Get the current object the mouse is hovering over*/
            RenderableObject2D newItemAtMouse = GetItemAtMouse();

            /*checking for alle the enter,leave and move action*/
            if (ItemAtMouse == null) 
            {
                if (newItemAtMouse != null)
                {
                    /*hello new item*/
                    newItemAtMouse.OnMouseEnter(Input);
                    checkForDoubleClick = false;
                }
            }
            /*mouse already is over an item*/
            else
            {
                /*The mouse changed items*/
                if (newItemAtMouse != ItemAtMouse)
                {
                    /*goodbye old item*/
                    ItemAtMouse.OnMouseLeave(Input);
                    /*hello new item*/
                    if (newItemAtMouse != null)
                        newItemAtMouse.OnMouseEnter(Input);

                    /*double click is impossible now!*/
                    checkForDoubleClick = false;
                }
                /*the mouse stayed on the same item*/
                else 
                {
                    ItemAtMouse.OnMouseMove(Input);
                }
            }

            /*Make sure the next time we check we are on the right path*/
            ItemAtMouse = newItemAtMouse;

            /*check all the clicking stuff that could have happened*/
            if (ItemAtMouse != null)
            {
                if (Input.AnyKeyWithState(ButtonState.Triggered))
                {
                    /*user did doubleclick*/
                    if (checkForDoubleClick) 
                    {
                        ItemAtMouse.OnDoubleClick(Input);
                        checkForDoubleClick = false;
                    }
                    /*user might double click*/
                    else
                    {
                        doubleClickTimer = 0;
                        checkForDoubleClick = true;
                    }
                }
                else if (Input.AnyKeyWithState(ButtonState.Pressed))
                {
                    /*Mousedown-event, if user is up to a click or double click we dont want to call mousedown every single time*/
                    if (!checkForDoubleClick)
                    {
                        ItemAtMouse.OnMouseDown(Input);
                        callMouseUp = true;
                    }
                }
                else if (Input.AnyKeyWithState(ButtonState.Released))
                {
                    if (callMouseUp )
                    {
                        ItemAtMouse.OnMouseUp(Input);
                        callMouseUp = false;
                    }
                    /*user did not doubleclick but single-click*/
                    if (checkForDoubleClick && doubleClickTimer >= DOUBLECLICK_TIME_MS)
                    {
                        ItemAtMouse.OnClick(Input);
                        checkForDoubleClick = false;
                    }
                    /*mousemove-event*/
                    else if (!checkForDoubleClick)
                        ItemAtMouse.OnMouseMove(Input);

                }
            }
        }
    }
}
