using GameCore;
using System.Windows.Forms;

namespace Editor
{

    public partial class ContentBrowser : Form
    {
        const int TEXTURES_PER_ROW = 10;

        /*member*/
        ContentManager GameContent;

        /*ctor*/
        public ContentBrowser()
        {
            InitializeComponent();
        }

        /*init*/
        public void Initialize(ContentManager GameContent)
        {
            this.GameContent = GameContent;

            /*Init collection displays*/
            collectionDisplayTextures.Initialize(TEXTURES_PER_ROW);

            UpdateTextures();
        }


        /*Make sure every available texture is displayed in the collecttion Display*/
        void UpdateTextures()
        {
            collectionDisplayTextures.ClearItems();
            for (int i = 0; i < GameContent.Textures.Count; i++)
            {
                var Texture = GameContent.Textures[i];
                collectionDisplayTextures.AddItem(Texture, Texture.Name);
            }
        }
    }
}
