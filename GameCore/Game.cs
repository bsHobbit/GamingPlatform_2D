namespace GameCore
{
    public class Game
    {
        /*delegates und events*/
        public delegate void GameUpdateEventHandler(float Elapsed);
        public event GameUpdateEventHandler Update;

        /*nested types*/
        enum ELoopState
        {
            Running,
            Paused,
        }


        /*member*/
        ContentManager contentManager;
        public ContentManager ContentManager { get => contentManager; }
        ELoopState LoopState;
        System.Timers.Timer timerGameLoop;
        System.Diagnostics.Stopwatch elapsedTimeTimer;


        /*ctor*/
        public Game()
        {
            contentManager = new ContentManager();
            LoopState = ELoopState.Paused;
            elapsedTimeTimer = System.Diagnostics.Stopwatch.StartNew();
            timerGameLoop = new System.Timers.Timer();
            timerGameLoop.Interval = 1;
            timerGameLoop.AutoReset = true;
            timerGameLoop.Elapsed += TimerGameLoop_Elapsed;
        }


        /*run / stop / pause / reset*/

        public void Run()
        {
            LoopState = ELoopState.Running;
        }

        public void Pause()
        {
            LoopState = ELoopState.Paused;
        }


        public void Reset()
        {
            LoopState = ELoopState.Paused;
        }


        /*Game-Loop*/

        private void TimerGameLoop_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            GameLoop();
        }


        void GameLoop()
        {
            if (LoopState == ELoopState.Running)
            {
                float elapsed = (float)elapsedTimeTimer.Elapsed.TotalMilliseconds;

                /*Update Game-Objects*/

                Update?.Invoke(elapsed);
            }
            elapsedTimeTimer = System.Diagnostics.Stopwatch.StartNew();
        }



        /*save and load content*/
        public void SaveContent(string Path)
        {
            Pause();


            Run();
        }

        public void LoadContent(string Path)
        {
            Pause();

            Run();
        }
    }
}
