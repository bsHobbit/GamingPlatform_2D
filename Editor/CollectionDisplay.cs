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

        /*events*/
        public delegate void CollectionDisplaySelectionEventHandler(CollectionDisplay Sender, int Index);
        public event CollectionDisplaySelectionEventHandler TextureSelected;

        /*nested types*/
        struct TextureInfo
        {
            public Texture2D OriginalTexture { get; private set; }
            public string Info { get; private set; }
            public RenderableObject2D RenderObject { get; set; }
            public int Index { get; private set; }

            public TextureInfo(Texture2D OriginalTexture, string Info, Rectangle2D RenderRect, int Index)
            {
                this.OriginalTexture = OriginalTexture;
                this.Info = Info;
                RenderObject = RenderRect;
                this.Index = Index;
            }
        }

        /*member*/
        List<TextureInfo> Items;
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
        int TotalRows = 0;
        int TopMostRow = 0;
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
            Items = new List<TextureInfo>();
            RenderTarget.Initialize();
            RenderTarget.EnableMouseInput();
            UpdateCamera();
            

            RenderTarget.SizeChanged += (s, e) => {  UpdateThumbails(); UpdateCamera(); };

            RenderTarget.MouseWheel += RenderTarget_MouseWheel;
        }




        /*Managed Items*/

        /*the Info is used to enable the user to filter items*/
        public void AddItem(Texture2D Texture, string Info)
        {
            if (Items == null)
                throw new System.Exception("Call Initialize first!");
            
            if (Texture.IsValid)
            {
                Texture2D thumbnail = new Texture2D(Texture.GetThumbnail(ItemWidth, ItemHeight));
                Rectangle2D renderRect = new Rectangle2D(ItemWidth, ItemHeight, new Vec2(), 0, System.Drawing.Color.Black, System.Drawing.Color.Gray, thumbnail);
                int currentIndex = Items.Count;
                Items.Add(new TextureInfo(Texture, Info, renderRect, currentIndex));
                RenderTarget.AddRenderObject(renderRect);

                /*highlight mouse-over object*/
                renderRect.MouseEnter += (s, e) => { s.OutlineColor = System.Drawing.Color.White; s.ZLocation = 1; };
                renderRect.MouseLeave += (s, e) => { s.OutlineColor = System.Drawing.Color.Gray; s.ZLocation = 0; };

                /*Auswahl durch den Benutzer*/
                renderRect.DoubleClick += (s, e) => { TextureSelected?.Invoke(this, GetIndex((Rectangle2D)s)); };
                UpdateItems();
                UpdateCamera();
            }
        }

        /*Get the texture by it's renderobject for the event ;)*/
        int GetIndex(Rectangle2D RenderRect)
        {
            foreach (var item in Items)
            {
                if (item.RenderObject == RenderRect)
                    return item.Index;
            }

            return -1;
        }

        public void RemoveItem(Texture2D Texture)
        {

            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].OriginalTexture == Texture)
                {
                    RenderTarget.RemoveRenderObject(Items[i].RenderObject);
                    //Items[i].RenderRect.Texture.Dispose();
                    Items.RemoveAt(i);
                    return;
                }
            }
        }

        public void ClearItems()
        {
            if (Items != null)
            {
                while (Items.Count > 0)
                    RemoveItem(Items[0].OriginalTexture);
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
                    if (item.OriginalTexture.IsValid)
                    {
                        Texture2D thumbnail = new Texture2D(item.OriginalTexture.GetThumbnail(ItemWidth, ItemHeight));
                        item.RenderObject.Texture = thumbnail;
                        if (item.RenderObject is Rectangle2D renderRect)
                            renderRect.Update(item.RenderObject.Location, itemWidth, itemHeight);
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
            /*keep track of the current row and collumn might be a good idea
             * Rows needs to be saved for user navigation
             */
            int currentCol = 0;
            TotalRows = 0;

            /*loop through every object that needs to be rendered*/
            foreach (var item in Items)
            {
                if (IsInFilter(item.Info)) /*ignore fake textures & filtered items*/
                {
                    RenderableObject2D renderObject = item.RenderObject;
                    renderObject.Location = new Vec2(currentCol * ItemWidth, TotalRows * itemHeight);

                    /*make sure the next item is positioned properly*/
                    currentCol++;
                    if (currentCol == ItemsPerRow)
                    {
                        currentCol = 0;
                        TotalRows++;
                    }
                }
                
            }

        }

        /*Update the camera for it to view the correct row*/
        void UpdateCamera()
        {
            /*check if anything makes sense anyway*/
            if (TopMostRow > TotalRows)
                TopMostRow = TotalRows;

            float cameraX = RenderTarget.Width / 2 - 2; /*@FixMe: Warum sieht es nur mit -2 zentiert aus?*/
            float cameraY = (RenderTarget.Height / 2) + (TopMostRow * ItemHeight);

            RenderTarget.Camera.LookAt = new Vec2(cameraX, cameraY);
        }

        /*Allow the user to scroll the collection*/
        private void RenderTarget_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
                TopMostRow++;
            else if (e.Delta >0)
                TopMostRow--;

            if (TopMostRow < 0) TopMostRow = 0;
            else if (TopMostRow > TotalRows) TopMostRow = TotalRows;

            UpdateCamera();
        }
    }
}
