using Box2DX.Common;

namespace Core.IO
{
    public class Mouse
    {

        const float CLICK_TIME_MS = 100;

        MouseState currentState;
        public MouseState CurrentState { get => currentState; }
        public Vec2 Diff { get; private set; }
        public int Delta { get; private set; }
        float[] clickTimer;

        int ButtonCount { get => System.Enum.GetNames(typeof(MouseButtons)).Length; }

        public Mouse()
        {
            currentState = new MouseState(new Vec2(), false, false, false, 0);
            clickTimer = new float[ButtonCount];
        }


        public void Update(MouseState Mouse, float Elapsed)
        {
            for (int i = 0; i < ButtonCount; i++)
                clickTimer[i] += Elapsed;

            /*reset timer on mousedown*/
            for (int i = 0; i < ButtonCount; i++)
            {
                if (Mouse[i] == ButtonState.Pressed && currentState[i] == ButtonState.Released)
                    clickTimer[i] = 0f;
            }

            /*Check if any mousebutton got triggered*/
            for (int i = 0; i < ButtonCount; i++)
            {
                if (clickTimer[i] < CLICK_TIME_MS && currentState[i] == ButtonState.Pressed && Mouse[i] == ButtonState.Released)
                    currentState[i] = ButtonState.Triggered;
                else
                    currentState[i] = Mouse[i];
            }

            /*Update location stuff*/
            Diff = Mouse.Location - currentState.Location;
            currentState.Location = Mouse.Location;
            currentState.Delta = Mouse.Delta;
        }

        /*check for input*/
        public bool AnyKeyWithState(ButtonState State)
        {
            for (int i = 0; i < ButtonCount; i++)
                if (currentState[i] == State)
                    return true;
            return false;
        }

        public void Reset()
        {
            //Delta = 0;
            //for (int i = 0; i < ButtonCount; i++)
            //    currentState[i] = ButtonState.Released;
        }

    }
}
