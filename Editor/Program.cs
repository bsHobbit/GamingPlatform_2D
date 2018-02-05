using GameCore;
using System;
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

            //Game Game = new Game(@"D:\Programmierung\Temp\game\");
            Game Game = new Game(@"D:\Temp\kk\game\");
            
            Game.LoadContent();


            //Application.Run(new TilesetAnimationEditor(new Graphics.Animation.TilesetAnimation(Graphics.Texture2D.FromFile(@"D:\Temp\kk\Fumiko.png"), 10, 0), Game.ContentManager));
            ContentBrowser browser = new ContentBrowser();
            browser.Initialize(Game.ContentManager, false, ContentBrowser.eBrowsers.Texture | 
                                                           ContentBrowser.eBrowsers.TilesetAnimations | 
                                                           ContentBrowser.eBrowsers.Animations);

            Application.Run(browser);
            Game.SaveContent();
        }
    }
}
