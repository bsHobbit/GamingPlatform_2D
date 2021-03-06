﻿using System.Collections.Generic;
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
        public delegate void CollectionDisplaySelectionEventHandler(CollectionDisplay Sender, int Index, bool DoubleClick);
        public event CollectionDisplaySelectionEventHandler TextureSelected;

        /*nested types*/
        class TextureInfo
        {
            public Texture2D OriginalTexture { get; internal set; }
            public string Info { get; private set; }
            public RenderableObject2D RenderObject { get; set; }
            public int Index { get; private set; }
            public System.Drawing.RectangleF Segment { get; set; }
            public bool IsHighlighted { get; set; }

            public TextureInfo(Texture2D OriginalTexture, string Info, Rectangle2D RenderRect, System.Drawing.RectangleF Segment, int Index)
            {
                this.OriginalTexture = OriginalTexture;
                this.Segment = Segment;
                this.Info = Info;
                RenderObject = RenderRect;
                IsHighlighted = false;
                this.Index = Index;
            }

            internal void UpdateSegment(System.Drawing.RectangleF Segment)
            {
                this.Segment = Segment;
            }
        }

        /*member*/
        List<TextureInfo> Items;
        int itemWidth;
        int ItemWidth
        {
            get => itemWidth;
            set { itemWidth = value; UpdateThumbnails(); }
        }
        int itemHeight;
        int ItemHeight
        {
            get => itemHeight;
            set { itemHeight = value; UpdateThumbnails(); }
        }

        int ItemsPerRow;
        int TotalRows = 0;
        int TopMostRow = 0;
        string filter;
        public string Filter
        {
            get => filter;
            set { filter = value; UpdateItems(); }
        }


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
            

            RenderTarget.SizeChanged += (s, e) => {  UpdateThumbnails(); UpdateCamera(); };

            RenderTarget.MouseWheel += RenderTarget_MouseWheel;
        }




        /*Managed Items*/

        /*the Info is used to enable the user to filter items*/
        public void AddItem(Texture2D Texture, string Info, System.Drawing.RectangleF Segment)
        {
            if (Items == null)
                throw new System.Exception("Call Initialize first!");


            if (Texture == null)
                Texture = new Texture2D(new System.Drawing.Bitmap(ItemWidth, ItemHeight), Info);

            if (Texture.IsValid)
            {
                Texture2D thumbnail = new Texture2D(Texture.GetThumbnail(ItemWidth, ItemHeight, Segment));
                Rectangle2D renderRect = new Rectangle2D(ItemWidth, ItemHeight, new Vec2(), 0, System.Drawing.Color.Transparent, System.Drawing.Color.Gray, thumbnail);
                renderRect.AddText(new RenderableText(Info, new System.Drawing.Font("Arial", 12), System.Drawing.Color.Black, new Vec2(), true));
                int currentIndex = Items.Count;
                Items.Add(new TextureInfo(Texture, Info, renderRect, Segment, currentIndex));
                RenderTarget.AddRenderObject(renderRect);

                /*highlight mouse-over object*/
                renderRect.MouseEnter += (s, e) => { s.OutlineColor = System.Drawing.Color.White; s.ZLocation = 1; };
                renderRect.MouseLeave += (s, e) => { UpdateHighLight(null); s.ZLocation = 0; };

                /*Auswahl durch den Benutzer*/
                renderRect.Click += (s, e) => { UpdateHighLight(s); TextureSelected?.Invoke(this, GetIndex((Rectangle2D)s), false); };
                renderRect.DoubleClick += (s, e) => { UpdateHighLight(s); TextureSelected?.Invoke(this, GetIndex((Rectangle2D)s), true); };
                UpdateItems();
                UpdateCamera();
            }
            
        }

        /*update the highlighted object*/
        void UpdateHighLight(RenderableObject2D Highlight)
        {
            for (int i = 0; i < Items.Count; i++)
            {
                if (Highlight != null) /*if it's null it means mouseleave event is fired!*/
                    Items[i].IsHighlighted = Items[i].RenderObject == Highlight;
                if (Items[i].IsHighlighted)
                    Items[i].RenderObject.OutlineColor = System.Drawing.Color.Green;
                else
                    Items[i].RenderObject.OutlineColor = System.Drawing.Color.Gray;
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
        void UpdateThumbnails()
        {
            /*make sure it's already initialized*/
            if (Items != null)
            {
                /*just make sure size is calculated correctly*/
                UpdateSize();

                /*update each thumbnail*/
                foreach (var item in Items)
                    UpdateThumbnail(item);

                /*Make sure they are dislayed correct*/
                UpdateItems();
            }
        }

        void UpdateThumbnail(TextureInfo Item)
        {
            if (Item.OriginalTexture != null && Item.OriginalTexture.IsValid)
            {
                Texture2D thumbnail = new Texture2D(Item.OriginalTexture.GetThumbnail(ItemWidth, ItemHeight, Item.Segment));
                Item.RenderObject.Texture = thumbnail;
                if (Item.RenderObject is Rectangle2D renderRect)
                    renderRect.Update(Item.RenderObject.Location, itemWidth, itemHeight);
            }

        }

        /*check filter*/
        bool IsInFilter(string Text)
        { 
            bool simpleFilter = string.IsNullOrEmpty(Filter) || Text.ToLower().Contains(Filter.ToLower());
            if (simpleFilter)
                return true;

            string[] possibleKeywords = Filter.ToLower().Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            string lowerText = Text.ToLower();
            foreach (var word in possibleKeywords)
                if (lowerText.Contains(word))
                    return true;

            return false;
        }

        /*Rearrange items in the renderer to make them appear correct*/
        void UpdateItems()
        {
            if (Items != null)
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
                        renderObject.Visible = true;
                        renderObject.Location = new Vec2(currentCol * ItemWidth, TotalRows * itemHeight);

                        /*make sure the next item is positioned properly*/
                        currentCol++;
                        if (currentCol == ItemsPerRow)
                        {
                            currentCol = 0;
                            TotalRows++;
                        }
                    }
                    else
                    {
                        if (item.RenderObject != null)
                            item.RenderObject.Visible = false;
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

        public void UpdateThumbnailSegment(int ItemIndex, System.Drawing.RectangleF Segment)
        {
            Items[ItemIndex].UpdateSegment(Segment);
            UpdateThumbnail(Items[ItemIndex]);
        }

        public void UpdateThumbnailTexture(int ItemIndex, Texture2D Texture)
        {
            Items[ItemIndex].OriginalTexture = Texture;
            UpdateThumbnail(Items[ItemIndex]);
        }

        public void UpdateThumbnailText(int ItemIndex, string Text)
        {
            Items[ItemIndex].RenderObject.ClearText();
            Items[ItemIndex].RenderObject.AddText(new RenderableText(Text, new System.Drawing.Font("Arial", 12), System.Drawing.Color.Black, new Vec2(), true));
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
