using Graphics.Animation;
using System;

namespace GameCore
{
    public partial class ContentManager
    {
        void LoadTexture(string File)
        {
            try
            {
                Graphics.Texture2D texture = Graphics.Texture2D.FromFile(File);
                texture.Name = System.IO.Path.GetFileName(File);
                AddTexture(texture);
            }
            catch { }
        }
    }
}