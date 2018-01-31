using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Box2DX.Common;
using Graphics;
using Graphics.Geometry;

namespace Editor
{
    public partial class CollectionDisplay : UserControl
    {
        /*nested types*/
        struct TextureInfo
        {
            public string Info { get; private set; }
            public Rectangle2D RenderRect { get; set; }

            public TextureInfo(string Info, Rectangle2D RenderRect)
            {
                this.Info = Info;
                this.RenderRect = RenderRect;
            }
        }

        /*member*/
        Dictionary<Texture2D, TextureInfo> Items;
        int itemWidth;
        int ItemWidth
        {
            get => itemWidth;
            set { itemWidth = value; UpdateThumbails(); }
        }
        int itemHeight;
        int ItemHeight
        {
            get => itemHeight;
            set { itemHeight = value; UpdateThumbails(); }
        }

        int ItemsPerRow;
        public string Filter { get; set; }


        /*ctor*/
        public CollectionDisplay()
        {
            InitializeComponent();
            ItemsPerRow = 10;
            ItemWidth = 10;
            ItemHeight = 10;
        }

        /*init stuff*/
        public void Initialize(int ItemsPerRow)
        {
            /*set the counts*/
            this.ItemsPerRow = ItemsPerRow;

            /*update the size*/
            UpdateSize();


            /*Initialize rendering*/
            Items = new Dictionary<Texture2D, TextureInfo>();
            RenderTarget.Initialize();
            //RenderTarget.EnableMouseInput();
            RenderTarget.EnableCameraControl(true);

            RenderTarget.SizeChanged += (s, e) => { UpdateThumbails(); };
        }



        /*Managed Items*/

        /*the Info is used to enable the user to filter items*/
        public void AddItem(Texture2D Texture, string Info)
        {
            if (Items == null)
                throw new System.Exception("Call Initialize first!");

            if (Texture.IsValid && !Items.ContainsKey(Texture))
            {
                Texture2D thumbnail = new Texture2D(Texture.GetThumbnail(ItemWidth, ItemHeight));
                Rectangle2D renderRect = new Rectangle2D(ItemWidth, ItemHeight, new Vec2(), 0, System.Drawing.Color.Black, System.Drawing.Color.Gray, thumbnail);
                Items.Add(Texture, new TextureInfo(Info, renderRect));
                RenderTarget.AddRenderObject(renderRect);
                renderRect.MouseEnter += (s, e) => { s.OutlineColor = System.Drawing.Color.White; s.ZLocation = 1; };
                renderRect.MouseLeave += (s, e) => { s.OutlineColor = System.Drawing.Color.Gray; s.ZLocation = 0; };
                /*@ToDo: Events wegen des auswählesn hinzufügen*/
                UpdateItems();
            }
        }

        public void RemoveItem(Texture2D Texture)
        {
            if (Items != null && Items.ContainsKey(Texture))
            {
                RenderTarget.RemoveRenderObject(Items[Texture].RenderRect);
                Items[Texture].RenderRect.Texture.Dispose();
                Items.Remove(Texture);
                UpdateItems();
            }
        }

        public void ClearItems()
        {
            if (Items != null)
            {
                while (Items.Count > 0)
                    RemoveItem(Items.Keys.ElementAt(0));
            }
        }
        
        /*Size changed? calculate new itemwidth and height*/
        void UpdateSize()
        {
            itemWidth = RenderTarget.Width / ItemsPerRow;
            itemHeight = ItemWidth; /*make them squares, who cares anyway*/
        }

        /*Update thumbnails if the size is changed*/
        void UpdateThumbails()
        {
            /*make sure it's already initialized*/
            if (Items != null)
            {
                /*just make sure size is calculated correctly*/
                UpdateSize();

                /*update each thumbnail*/
                foreach (var item in Items)
                {
                    if (item.Key.IsValid)
                    {
                        Texture2D thumbnail = new Texture2D(item.Key.GetThumbnail(ItemWidth, ItemHeight));
                        item.Value.RenderRect.Texture = thumbnail;
                    }
                }

                /*Make sure they are dislayed correct*/
                UpdateItems();
            }
        }

        /*check filter*/
        bool IsInFilter(string Text) => string.IsNullOrEmpty(Filter) || Filter.ToLower().Contains(Text.ToLower());

        /*Rearrange items in the renderer to make them appear correct*/
        void UpdateItems()
        {
            /*keep track of the current row and collumn might be a good idea*/
            int currentCol = 0, currentRow = 0;

            /*loop through every object that needs to be rendered*/
            foreach (var item in Items)
            {
                if (item.Key.IsValid && IsInFilter(item.Value.Info)) /*ignore fake textures & filtered items*/
                {
                    Rectangle2D rect = item.Value.RenderRect;
                    rect.Location = new Vec2(currentCol * ItemWidth, currentRow * itemHeight);

                    /*make sure the next item is positioned properly*/
                    currentCol++;
                    if (currentCol == ItemsPerRow)
                    {
                        currentCol = 0;
                        currentRow++;
                    }
                }
                
            }

        }
    }
}
