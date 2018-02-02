using Box2DX.Common;

namespace Core.IO
{
    public class Mouse
    {

        const float CLICK_TIME_MS = 200;

        MouseState currentState;
        public MouseState CurrentState { get => currentState; }
        public Vec2 Diff { get; private set; }
        float[] clickTimer;

        int ButtonCount { get => System.Enum.GetNames(typeof(MouseButtons)).Length; }

        public Mouse()
        {
            currentState = new MouseState(new Vec2(), false, false, false, 0);
            clickTimer = new float[ButtonCount];
        }


        public void Update(MouseState Mouse, float Elapsed)
        {

            /*Check if any mousebutton got triggered*/
            for (int i = 0; i < ButtonCount; i++)
            {
                if (Mouse[i] == ButtonState.Pressed)
                    clickTimer[i] += Elapsed;

                //if (Mouse[i] == ButtonState.Released && clickTimer[i] > 0)
                //{
                //    if (clickTimer[i] < CLICK_TIME_MS && currentState[i] == ButtonState.Pressed)
                //        currentState[i] = ButtonState.Triggered;
                //}

                if (clickTimer[i] < CLICK_TIME_MS && currentState[i] == ButtonState.Pressed && Mouse[i] == ButtonState.Released)
                    currentState[i] = ButtonState.Triggered;
                else
                    currentState[i] = Mouse[i];


                if (Mouse[i] == ButtonState.Released)
                    clickTimer[i] = 0;
            }

            /*Update location stuff*/
            Diff = currentState.Location - Mouse.Location;
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

        /*need this for camera movement otherwise the diff will change with the lookat change of the camera because of the input transformation*/
        public void OffsetLocation(Vec2 Offset)
        {
            currentState.Location += Offset;
        }

    }
}
