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
            Application.Run(new AnimationEditor(new Graphics.Animation.TilesetAnimation(Graphics.Texture2D.FromFile(@"D:\Programmierung\Temp\charsettest.png"), 10, 0)));
        }
    }
}
