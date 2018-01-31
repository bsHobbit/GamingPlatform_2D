using GameCore;
using Graphics;
using System.Windows.Forms;

namespace Editor
{

    public partial class ContentBrowser : Form
    {
        const int TEXTURES_PER_ROW = 10;

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
        public void Initialize(ContentManager GameContent, bool CloseOnSelection)
        {
            this.GameContent = GameContent;
            this.CloseOnSelection = CloseOnSelection;

            /*Init collection displays*/
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
