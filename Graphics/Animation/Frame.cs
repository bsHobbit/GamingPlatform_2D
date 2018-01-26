namespace Graphics.Animation
{
    public sealed class Frame
    {
        public int StartX   { get; set; }
        public int StartY   { get; set; }
        public int Width    { get; set; }
        public int Height   { get; set; }

        public Frame(int StartX, int StartY, int Width, int Height)
        {
            this.StartX = StartX;
            this.StartY = StartY;
            this.Width = Width;
            this.Height = Height;
        }
    }
}
