using System.Collections.Generic;
using Box2DX.Common;
using Core.IO;

namespace Graphics.Interaction
{
    public class MouseInputManager
    {
        /*constants*/
        const float DOUBLECLICK_TIME_MS = 100;
        const float CAMERA_TIME_MS = 500;
        const float CAMERA_MOVEMENT_VALID_DISTANCE = 10;
        const MouseButtons CAMERA_MOVEMENT_BUTTON = MouseButtons.Right;

        /*Members*/
        List<RenderableObject2D> Items;
        RenderableObject2D ItemAtMouse;
        Mouse Input;
        bool checkForDoubleClick = false;
        bool callMouseUp = false;
        float doubleClickTimer = 0;

        CameraMovementStyle cameraMovementStyle;
        float cameraMovementTimer = 0;
        bool MovingCamera;
        bool CheckCameraMovement = false;
        Vec2 mouseDownCameraMovementStart; /*i need this to check movement distance for camera movement check*/
        

        /*ctor*/
        public MouseInputManager(Mouse Input, List<RenderableObject2D> Items)
        {
            this.Items = Items;
            this.Input = Input;
            ItemAtMouse = null;
        }


        /*camera settings*/
        public void SetCameraMovement(CameraMovementStyle Style)
        {
            cameraMovementStyle = Style;
        }


        /*logic*/

        bool IsMouseOverItem(RenderableObject2D Item)
        {
            if (Item.Vertices.Count == 2)
            {
                var transformedVertices = Item.TransformedVertices();
                return RenderableObject2D.DistanceFromPointToLine(Input.CurrentState.Location, transformedVertices[0], transformedVertices[1]) <= 5;
            }
            else if (Item.Vertices.Count >= 3)
                return Item.IsPointInsideObject(Input.CurrentState.Location);

            return false;
        }

        RenderableObject2D GetItemAtMouse()
        {
            if (Items != null && Items.Count > 0)
                for (int i = Items.Count - 1; i >= 0; i--)
                    if (Items[i].Visible && Items[i].Enabled && IsMouseOverItem(Items[i]))
                        return Items[i];
            return null;
        }

        /*Input handling - it's just a big state machine dont bother looking into it as long as it's working ;)
         * this most likely wont work when the user is pressing multiple mousebuttons at once ... who cares what the user wants anyway*/
        public void Update(float Elapsed, ref Camera2D Camera)
        {

            if (checkForDoubleClick)
                doubleClickTimer += Elapsed;

            bool cameraMovementPossible = MovingCamera || ((cameraMovementStyle != CameraMovementStyle.None) && 
                                                           (cameraMovementStyle == CameraMovementStyle.Always || (cameraMovementStyle == CameraMovementStyle.NoObject && ItemAtMouse == null)));

            if (cameraMovementPossible)
            {
                /*check for scaling*/
                if (Input.CurrentState.Delta > 0) Camera.ZoomAt(Input.CurrentState.Location, true);
                else if (Input.CurrentState.Delta < 0) Camera.ZoomAt(Input.CurrentState.Location, false);

                /*if no mousebutton is pressed right now, no movement is possible*/
                if (Input.CurrentState[CAMERA_MOVEMENT_BUTTON] == ButtonState.Released)
                {
                    cameraMovementTimer = 0;
                    MovingCamera = false;
                    CheckCameraMovement = false;
                }

                /*if left mousebutton is pressed we might need to check if the user just wants to move the camera*/
                if (!CheckCameraMovement && !MovingCamera && Input.CurrentState[CAMERA_MOVEMENT_BUTTON] == ButtonState.Pressed && cameraMovementTimer == 0f)
                {
                    CheckCameraMovement = true;
                    mouseDownCameraMovementStart = Input.CurrentState.Location;
                }

                /*check if the user most likely wants to move the camera*/
                if (CheckCameraMovement)
                {

                    cameraMovementTimer += Elapsed;

                    /*timeout user didn't want to move the camera?*/
                    if (cameraMovementTimer >= CAMERA_TIME_MS)
                    {
                        MovingCamera = false;
                        CheckCameraMovement = false;
                    }
                    /*Camera-Movement detected*/
                    else if ((mouseDownCameraMovementStart - Input.CurrentState.Location).Length() > (CAMERA_MOVEMENT_VALID_DISTANCE / Camera.Scale))
                    {
                        cameraMovementTimer = 0;
                        MovingCamera = true;
                        CheckCameraMovement = false;
                    }
                }
            }
            else
                MovingCamera = false;

            if ((!MovingCamera && !CheckCameraMovement) || !cameraMovementPossible)
            {
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

                        /*Might be the same cause it moved?*/
                        newItemAtMouse = GetItemAtMouse();
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
                        if (!checkForDoubleClick && !callMouseUp)
                        {
                            ItemAtMouse.OnMouseDown(Input);
                            callMouseUp = true;
                        }
                    }
                    else if (Input.AnyKeyWithState(ButtonState.Released))
                    {
                        if (callMouseUp)
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

            /*Update Camera movement by user-input*/
            else if (MovingCamera)
                    UpdateCamera(ref Camera);
        }

        /*Update Camera*/
        void UpdateCamera(ref Camera2D Camera)
        {
            Camera.LookAt += Input.Diff;
            Input.OffsetLocation(Input.Diff);
        }
    }
}
