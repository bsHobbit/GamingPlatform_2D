using GameCore;
using Graphics;
using System.Windows.Forms;

namespace Editor
{

    public partial class ContentBrowser : Form
    {
        const int TEXTURES_PER_ROW = 10;
        const int TILESETANIMATIONS_PER_ROW = 15;

        /*nested types*/
        [System.Flags]
        public enum eBrowsers
        {
            Texture = 1,
            TilesetAnimations = 2
        }

        /*member*/
        eBrowsers VisibleBrowsers;
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
            this.VisibleBrowsers = VisibleBrowsers;
            this.GameContent = GameContent;
            this.CloseOnSelection = CloseOnSelection;

            UpdateBrowserVisibility(VisibleBrowsers);

            /*Init collection displays*/
            if (VisibleBrowsers.HasFlag(eBrowsers.Texture))
                collectionDisplayTextures.Initialize(TEXTURES_PER_ROW);
            if (VisibleBrowsers.HasFlag(eBrowsers.TilesetAnimations))
                collectionDisplayTilesetAnimations.Initialize(TILESETANIMATIONS_PER_ROW);


            /*Register events*/
            collectionDisplayTextures.TextureSelected += (s, Index) => { selectedTexture = GameContent.Textures[Index];  NeedsClosingAfterSelection(); };
            collectionDisplayTilesetAnimations.TextureSelected += TilesetAnimationSelected;


            buttonAddTilesetAnimation.Visible = !CloseOnSelection;
            if (!CloseOnSelection)
            {
                buttonAddTilesetAnimation.Click += (s, e) => 
                {
                    Hide();
                    Texture2D animationTexture = SelectTexture(GameContent);
                    if (animationTexture != null)
                        GameContent.AddTilesetAnimation(new Graphics.Animation.TilesetAnimation(animationTexture, 10, 0)); 
                    UpdateTilesetAnimations();
                    Show();
                };
            }

            UpdateTextures();
            UpdateTilesetAnimations();
        }

        /*Handle tilesetanimation selection*/
        private void TilesetAnimationSelected(CollectionDisplay Sender, int Index)
        {
            if (!NeedsClosingAfterSelection())
            {
                /*Allow the user to edit the animation*/

                var animation = GameContent.TilesetAnimations[Index];
                /*check if the animation has a valid texture, of not make sure the user is able to select one*/
                if (animation.Texture == null)
                    animation.Texture = SelectTexture(GameContent);

                if (animation.Texture != null)
                {
                    TilesetAnimationEditor Editor = new TilesetAnimationEditor(animation, GameContent);
                    Editor.Show();

                    /*Update the thumbnail in the contentbrowser to distinct it from the other tilesetanimations visually*/
                    Editor.FormClosing += (s, e) =>
                    {
                        collectionDisplayTilesetAnimations.UpdateThumbnailSegment(Index, GameContent.TilesetAnimations[Index].GetSegment(0));
                        collectionDisplayTilesetAnimations.UpdateThumbnailTexture(Index, GameContent.TilesetAnimations[Index].Texture);
                        Editor.Dispose();
                    };
                }
            }
        }


        /*Close on selection?*/
        bool NeedsClosingAfterSelection()
        {
            if (CloseOnSelection )
            {
                DialogResult = DialogResult.OK;
                Close();
                return true;
            }
            return false;
        }

        void UpdateBrowserVisibility(eBrowsers VisibleBrowsers)
        {
            if (!VisibleBrowsers.HasFlag(eBrowsers.Texture))
                tabControlContent.TabPages.Remove(tabPageTextures);
            if (!VisibleBrowsers.HasFlag(eBrowsers.TilesetAnimations))
                tabControlContent.TabPages.Remove(tabPageTilesetAnimations);
        }


        /*Make sure every available texture is displayed in the collecttion Display*/
        void UpdateTextures()
        {
            if (VisibleBrowsers.HasFlag(eBrowsers.Texture))
            {
                collectionDisplayTextures.ClearItems();
                for (int i = 0; i < GameContent.Textures.Count; i++)
                {
                    var Texture = GameContent.Textures[i];
                    collectionDisplayTextures.AddItem(Texture, Texture.Name, System.Drawing.RectangleF.Empty);
                }
            }
        }

        void UpdateTilesetAnimations()
        {

            if (VisibleBrowsers.HasFlag(eBrowsers.TilesetAnimations))
            {
                collectionDisplayTilesetAnimations.ClearItems();
                var tilesetAnimations = GameContent.TilesetAnimations;
                for (int i = 0; i < tilesetAnimations.Count; i++)
                {
                    var Texture = tilesetAnimations[i].Texture;
                    collectionDisplayTilesetAnimations.AddItem(Texture, tilesetAnimations[i].Name, tilesetAnimations[i].GetSegment(0));
                }
            }
        }


        /*remove textures*/
        private void buttonRemoveTexture_Click(object sender, System.EventArgs e)
        {
            if (selectedTexture != null)
            {
                GameContent.RemoveTexture(selectedTexture);
                GameContent.RemoveReferences(selectedTexture);
                selectedTexture = null;
                UpdateTextures();
                UpdateTilesetAnimations();
            }

        }



        /*Static helpers*/
        public Texture2D SelectTexture(ContentManager ContentManager)
        {
            ContentBrowser browser = new ContentBrowser();
            browser.Initialize(ContentManager, true, eBrowsers.Texture);
            if (browser.ShowDialog() == DialogResult.OK)
                return browser.SelectedTexture;
            return null;
        }

    }
}
