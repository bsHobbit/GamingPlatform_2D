using System.Collections.Generic;
using System.Windows.Forms;
using Box2DX.Common;
using Graphics;
using Graphics.Geometry;

namespace Editor
{
    public partial class CollectionDisplay : UserControl
    {
        /*member*/
        Dictionary<Texture2D, Rectangle2D> Items;
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
            Items = new Dictionary<Texture2D, Rectangle2D>();
            RenderTarget.Initialize();
            RenderTarget.EnableMouseInput();

            RenderTarget.SizeChanged += (s, e) => { UpdateThumbails(); };
        }


        /*Managed Items*/
        public void AddItem(Texture2D Texture)
        {
            if (Items == null)
                throw new System.Exception("Call Initialize first!");

            if (Texture.IsValid && !Items.ContainsKey(Texture))
            {
                Texture2D thumbnail = new Texture2D(Texture.GetThumbnail(ItemWidth, ItemHeight));
                Items.Add(Texture, new Rectangle2D(ItemWidth, ItemHeight, new Vec2(), 0, System.Drawing.Color.Black, System.Drawing.Color.Gray, thumbnail));
                /*@ToDo: Events wegen des auswählesn hinzufügen*/
                UpdateItems();
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
                        item.Value.Texture = thumbnail;
                    }
                }

                /*Make sure they are dislayed correct*/
                UpdateItems();
            }
        }

        /*Rearrange items in the renderer to make them appear correct*/
        void UpdateItems()
        {
            /*keep track of the current row and collumn might be a good idea*/
            int currentCol = 0, currentRow = 0;

            /*loop through every object that needs to be rendered*/
            foreach (var item in Items)
            {
                if (item.Key.IsValid) /*ignore fake textures*/
                {
                    Rectangle2D rect = item.Value;
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
