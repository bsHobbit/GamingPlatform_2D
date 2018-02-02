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

            Game Game = new Game(@"D:\Programmierung\Temp\game\");
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"));
            Game.ContentManager.AddTexture(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest2.png"));
            Game.LoadContent();


            //Application.Run(new TilesetAnimationEditor(new Graphics.Animation.TilesetAnimation(Graphics.Texture2D.FromFile(@"D:\Temp\kk\Fumiko.png"), 10, 0), Game.ContentManager));
            ContentBrowser browser = new ContentBrowser();
            browser.Initialize(Game.ContentManager, false, ContentBrowser.eBrowsers.Texture | ContentBrowser.eBrowsers.TilesetAnimations);

            Application.Run(browser);
            Game.SaveContent();
        }
    }
}
