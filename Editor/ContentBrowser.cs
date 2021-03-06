﻿using GameCore;
using Graphics;
using Graphics.Animation;
using System.Windows.Forms;

namespace Editor
{

    public partial class ContentBrowser : Form
    {
        const int TEXTURES_PER_ROW = 10;
        const int TILESETANIMATIONS_PER_ROW = 10;
        const int ANIMATIONS_PER_ROW = 10;

        /*nested types*/
        [System.Flags]
        public enum eBrowsers
        {
            Texture = 1,
            TilesetAnimations = 2,
            Animations = 4,
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


        TilesetAnimation selectedTilesetAnimation;
        public TilesetAnimation SelectedTilesetAnimation
        {
            get => selectedTilesetAnimation;
            set { selectedTilesetAnimation = value; }
        }

        Animation selectedAnimation;
        public Animation SelectedAnimation
        {
            get => selectedAnimation;
            set { selectedAnimation = value; }
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
            if (VisibleBrowsers.HasFlag(eBrowsers.Animations))
                collectionDisplayAnimations.Initialize(ANIMATIONS_PER_ROW);


            /*Register events*/
            collectionDisplayTextures.TextureSelected += (s, Index, doubleClicked) => { selectedTexture = GameContent.Textures[Index];  if (doubleClicked) NeedsClosingAfterSelection(); };
            collectionDisplayTilesetAnimations.TextureSelected += TilesetAnimationSelected;
            collectionDisplayAnimations.TextureSelected += AnimationSelected;

            /*filter-events*/
            textBoxFilterTilesetAnimations.TextChanged += (s, e) => { collectionDisplayTilesetAnimations.Filter = textBoxFilterTilesetAnimations.Text; };
            textBoxFilterAnimations.TextChanged += (s, e) => { collectionDisplayAnimations.Filter = textBoxFilterAnimations.Text; };
            textBoxFilterTexture.TextChanged += (s, e) => { collectionDisplayTextures.Filter = textBoxFilterTexture.Text; };

            /*making stuff inivisible that should not be visible if in selectionmode*/
            buttonAddTilesetAnimation.Visible = !CloseOnSelection;
            buttonAddAnimation.Visible = !CloseOnSelection;
            buttonRemoveTexture.Visible = !CloseOnSelection;
            buttonRemoveTilesetAnimation.Visible = !CloseOnSelection;
            buttonCopyTilesetAnimation.Visible = !CloseOnSelection;
            buttonRemoveAnimation.Visible = !CloseOnSelection;
            buttonCopyAnimation.Visible = !CloseOnSelection;

            if (!CloseOnSelection)
            {
                buttonAddTilesetAnimation.Click += (s, e) => 
                {
                    Hide();
                    Texture2D animationTexture = SelectTexture(GameContent);
                    if (animationTexture != null)
                        GameContent.AddRenderableObject(new TilesetAnimation(animationTexture, 10, 0)); 
                    UpdateTilesetAnimations();
                    Show();
                };
                buttonAddAnimation.Click += (s, e) =>
                {
                    GameContent.AddRenderableObject(new Animation(new Box2DX.Common.Vec2(), 0));
                    UpdateAnimations();
                };
            }

            UpdateTextures();
            UpdateTilesetAnimations();
            UpdateAnimations();
        }

        /*Handle-Animation-Selection*/
        private void AnimationSelected(CollectionDisplay Sender, int Index, bool doubleClicked)
        {
            selectedAnimation = GameContent.Animations[Index];
            if (doubleClicked && !NeedsClosingAfterSelection())
            {
                var animation = GameContent.Animations[Index];
                AnimationEditor Editor = new AnimationEditor(animation, GameContent);
                Editor.Show();

                /*Update the thumbnail in the contentbrowser to distinct it from the other animations visually*/
                Editor.FormClosing += (s, e) =>
                {
                    if (GameContent.ContainsRenderableObject(Editor.Animation)) /*make sure it's not already deleted*/
                    {
                        collectionDisplayAnimations.UpdateThumbnailSegment(Index, Editor.Animation.TextureSegment);
                        collectionDisplayAnimations.UpdateThumbnailTexture(Index, Editor.Animation.Texture);
                        collectionDisplayAnimations.UpdateThumbnailText(Index, Editor.Animation.Name);
                    }
                    Editor.Dispose();
                };
            }
        }

        /*Handle tilesetanimation selection*/
        private void TilesetAnimationSelected(CollectionDisplay Sender, int Index, bool doubleClicked)
        {
            SelectedTilesetAnimation = GameContent.TilesetAnimations[Index];
            if (doubleClicked && !NeedsClosingAfterSelection())
            {
                /*Allow the user to edit the animation*/

                var animation = GameContent.TilesetAnimations[Index];
                /*check if the animation has a valid texture, of not make sure the user is able to select one*/
                if (animation.Texture == null)
                    animation.Texture = SelectTexture(GameContent);

                TilesetAnimationEditor Editor = new TilesetAnimationEditor(animation, GameContent);
                Editor.Show();

                /*Update the thumbnail in the contentbrowser to distinct it from the other tilesetanimations visually*/
                Editor.FormClosing += (s, e) =>
                {
                    /*make sure the object has not been deleted*/
                    if (GameContent.ContainsRenderableObject(Editor.TilesetAnimation))
                    {
                        collectionDisplayTilesetAnimations.UpdateThumbnailSegment(Index, Editor.TilesetAnimation.GetSegment(0));
                        collectionDisplayTilesetAnimations.UpdateThumbnailTexture(Index, Editor.TilesetAnimation.Texture);
                        collectionDisplayTilesetAnimations.UpdateThumbnailText(Index, Editor.TilesetAnimation.Name);
                    }
                    Editor.Dispose();
                };
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
            if (!VisibleBrowsers.HasFlag(eBrowsers.Animations))
                tabControlContent.TabPages.Remove(tabPageAnimations);
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


        void UpdateAnimations()
        {
            if (VisibleBrowsers.HasFlag(eBrowsers.Animations))
            {
                collectionDisplayAnimations.ClearItems();
                var animations = GameContent.Animations;
                for (int i = 0; i < animations.Count; i++)
                {
                    var Texture = animations[i].Texture;
                    collectionDisplayAnimations.AddItem(Texture, animations[i].Name, animations[i].TextureSegment);
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

        /*Remove tilesetanimation*/
        private void buttonRemoveTilesetAnimation_Click(object sender, System.EventArgs e)
        {
            if (selectedTilesetAnimation != null)
            {
                GameContent.RemoveRenderableObject(selectedTilesetAnimation);
                GameContent.RemoveReferences(selectedTilesetAnimation);
                selectedTilesetAnimation = null;
                UpdateTilesetAnimations();
                UpdateAnimations();
            }
        }

        /*copy tilesetanimation*/
        private void buttonCopyTilesetAnimation_Click(object sender, System.EventArgs e)
        {
            if (selectedTilesetAnimation != null)
            {
                TilesetAnimation newTilesetAnimation = selectedTilesetAnimation.Clone() as TilesetAnimation;
                newTilesetAnimation.Name = GameContent.GetFreeName<TilesetAnimation>();
                GameContent.AddRenderableObject(newTilesetAnimation);
                UpdateTilesetAnimations();
            }
        }

        /*remove animation*/
        private void buttonRemoveAnimation_Click(object sender, System.EventArgs e)
        {
            if (selectedAnimation != null)
            {
                GameContent.RemoveRenderableObject(selectedAnimation);
                UpdateAnimations();
                selectedAnimation = null;
            }
            
        }

        /*make a copy of the selected animation*/
        private void buttonCopyAnimation_Click(object sender, System.EventArgs e)
        {
            if (selectedAnimation != null)
            {
                var newAnimation = selectedAnimation.Clone();
                newAnimation.Name = GameContent.GetFreeName<Animation>();
                GameContent.AddRenderableObject(newAnimation);
                UpdateAnimations();
            }
        }

        /*Static helpers*/
        public static Texture2D SelectTexture(ContentManager ContentManager)
        {
            ContentBrowser browser = new ContentBrowser();
            browser.Initialize(ContentManager, true, eBrowsers.Texture);
            if (browser.ShowDialog() == DialogResult.OK)
                return browser.SelectedTexture;
            return null;
        }

        public static TilesetAnimation SelectTilesetAnimation(ContentManager ContentManager)
        {
            ContentBrowser browser = new ContentBrowser();
            browser.Initialize(ContentManager, true, eBrowsers.TilesetAnimations);
            if (browser.ShowDialog() == DialogResult.OK)
                return browser.SelectedTilesetAnimation;
            return null;
        }

    }
}
