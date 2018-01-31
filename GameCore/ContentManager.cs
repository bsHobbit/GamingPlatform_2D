﻿using System.Collections.Generic;
using Graphics;


namespace GameCore
{
    public class ContentManager : Core.ComponentModel.Component
    {
        /*event stuff*/
        public delegate void ContentManagerTextureEventHandler(ContentManager Sender, Texture2D Content);
        public event ContentManagerTextureEventHandler OnTextureAdded;
        public event ContentManagerTextureEventHandler OnTextureRemoved;

        /*members*/
        public List<Texture2D> Textures { get; private set; }
        internal List<RenderableObject2D> RenderableObjects;

        /*init everything*/
        public ContentManager()
        {
            Textures = new List<Texture2D>();
            RenderableObjects = new List<RenderableObject2D>();
        }

        /*Manage textures*/
        public void AddTexture(Texture2D Texture)
        {
            if (!Textures.Contains(Texture))
            {
                Textures.Add(ToDispose(Texture));
                OnTextureAdded?.Invoke(this, Texture);
            }
        }

        public void RemoveTexture(Texture2D Texture)
        {
            if (Textures.Contains (Texture))
            {
                Textures.Remove(Texture);
                RemoveAndDispose(Texture);
                OnTextureRemoved?.Invoke(this, Texture);
            }
        }

        /*Remove all objects that use a specific texture*/
        public void RemoveReferences(Texture2D Texture)
        {
            foreach (var item in RenderableObjects)
            {
                if (item.Texture == Texture)
                    item.Texture = null;
            }
        }

        /*Manage Animation*/
        public void AddAnimation(Graphics.Animation.TilesetAnimation Animation)
        {
            if (!RenderableObjects.Contains(Animation))
            {
                RenderableObjects.Add(ToDispose(Animation));
            }
        }

        public void RemoveAnimation(Graphics.Animation.TilesetAnimation Animation)
        {
            if (RenderableObjects.Contains(Animation))
            {
                RenderableObjects.Remove(Animation);
                RemoveAndDispose(Animation);
            }
        }

    }
}