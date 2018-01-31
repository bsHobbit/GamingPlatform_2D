using GameCore;
using Graphics;
using System.Windows.Forms;

namespace Editor
{

    public partial class ContentBrowser : Form
    {
        const int TEXTURES_PER_ROW = 10;

        /*nested types*/
        [System.Flags]
        public enum eBrowsers
        {
            Texture = 1,
            Animations = 2
        }

        /*member*/
        ContentManager GameContent;
        bool CloseOnSelection;

        Texture2D selectedTexture;
        public Texture2D SelectedTexture
        {
            get => selectedTexture;
            set { selectedTexture = value; }
        }


        /*ctor*/
        public ContentBrowser()
        {
            InitializeComponent();
        }

        /*init*/
        public void Initialize(ContentManager GameContent, bool CloseOnSelection , eBrowsers VisibleBrowsers)
        {
            this.GameContent = GameContent;
            this.CloseOnSelection = CloseOnSelection;

            UpdateBrowserVisibility(VisibleBrowsers);

            /*Init collection displays*/
            if (VisibleBrowsers.HasFlag(eBrowsers.Texture))
                collectionDisplayTextures.Initialize(TEXTURES_PER_ROW);


            /*Register events*/
            collectionDisplayTextures.TextureSelected += (s, e) => { selectedTexture = e;  SomethingGotSelected(); };
            UpdateTextures();
        }


        /*Close on selection?*/
        void SomethingGotSelected()
        {
            if (CloseOnSelection )
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        void UpdateBrowserVisibility(eBrowsers VisibleBrowsers)
        {
            if (!VisibleBrowsers.HasFlag(eBrowsers.Texture))
                tabControlContent.TabPages.Remove(tabPageTextures);
            if (!VisibleBrowsers.HasFlag(eBrowsers.Animations))
                tabControlContent.TabPages.Remove(tabPageAnimations);
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
