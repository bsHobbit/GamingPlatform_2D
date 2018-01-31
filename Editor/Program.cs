using GameCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Game Game = new Game();
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest2.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest2.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest2.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest2.png"));
            

            Application.Run(new AnimationEditor(new Graphics.Animation.TilesetAnimation(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"), 10, 0), Game.ContentManager));
        }
    }
}
