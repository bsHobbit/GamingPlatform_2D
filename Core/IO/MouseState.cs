using Box2DX.Common;

namespace Core.IO
{
    public struct MouseState
    {
        public Vec2 Location { get; internal set; }
        public ButtonState[] ButtonStates { get; private set; }
        public int Delta { get; internal set; }

        public MouseState(Vec2 Location, bool LeftButtonPressed, bool MiddleButtonPressed, bool RightButtonPressed, int Delta)
        {
            this.Location = Location;
            this.Delta = Delta;
            ButtonStates = new ButtonState[System.Enum.GetNames(typeof(MouseButtons)).Length];
            ButtonStates[(int)MouseButtons.Left  ]   = LeftButtonPressed   ? ButtonState.Pressed : ButtonState.Released;
            ButtonStates[(int)MouseButtons.Middle]   = MiddleButtonPressed ? ButtonState.Pressed : ButtonState.Released;
            ButtonStates[(int)MouseButtons.Right ]   = RightButtonPressed  ? ButtonState.Pressed : ButtonState.Released;
        }

        public ButtonState this[MouseButtons button]
        {
            get => ButtonStates[(int)button];
            set { ButtonStates[(int)button] = value; }
        }

        public ButtonState this[int button]
        {
            get => ButtonStates[button];
            set { ButtonStates[button] = value; }
        }

    }
}
