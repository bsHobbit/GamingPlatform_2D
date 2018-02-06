using System.Collections.Generic;
using Graphics;
using Graphics.Animation;
using System.Linq;

namespace GameCore
{
    public partial class ContentManager : Core.ComponentModel.Component
    {
        /*event stuff*/
        public delegate void ContentManagerTextureEventHandler(ContentManager Sender, Texture2D Content);
        public event ContentManagerTextureEventHandler OnTextureAdded;
        public event ContentManagerTextureEventHandler OnTextureRemoved;

        /*members*/
        public List<Texture2D> Textures { get; private set; }
        internal List<RenderableObject2D> RenderableObjects;

        /*seprating the renderableobjects 2D into the differenct categories*/
        public List<TilesetAnimation> TilesetAnimations
        {
            get
            {
                List<TilesetAnimation> result = new List<TilesetAnimation>();
                for (int i = 0; i < RenderableObjects.Count; i++)
                {
                    if (RenderableObjects[i] is TilesetAnimation tsa)
                        result.Add(tsa);
                }
                return result;
            }
        }

        public List<Animation> Animations
        {
            get
            {
                List<Animation> result = new List<Animation>();
                for (int i = 0; i < RenderableObjects.Count; i++)
                {
                    if (RenderableObjects[i] is Animation animation)
                        result.Add(animation);
                }
                return result;
            }
        }

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
            if (Textures.Contains(Texture))
            {
                Textures.Remove(Texture);
                RemoveAndDispose(Texture);
                OnTextureRemoved?.Invoke(this, Texture);
            }
        }

        public Texture2D GetTexture(string Name)
        {
            for (int i = 0; i < Textures.Count; i++)
            {
                if (Textures[i].Name == Name)
                    return Textures[i];
            }
            return null;
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

        /*Manage Renderable Objects*/
        public void AddRenderableObject<T>(T Object) where T: RenderableObject2D
        {
            if (!RenderableObjects.Contains(Object))
            {
                /*make sure the item gets a default name*/
                if (string.IsNullOrEmpty(Object.Name))
                    Object.Name = GetFreeName<T>();

                RenderableObjects.Add(ToDispose(Object));
            }
        }

        public void RemoveRenderableObject<T>(T Object) where T: RenderableObject2D
        {
            if (RenderableObjects.Contains(Object))
            {
                RenderableObjects.Remove(Object);
                RemoveAndDispose(Object);
            }
        }

        /*manage names*/
        public T GetRenderableObject<T>(string Name) where T: RenderableObject2D
        {
            for (int i = 0; i < RenderableObjects.Count; i++)
                if (RenderableObjects[i].GetType() == typeof(T) && RenderableObjects[i].Name == Name)
                    return (T)RenderableObjects[i];
            return null;
        }

        public string GetFreeName<T>(int i = 0) where T: RenderableObject2D
        {
            string currentName = "obj_" + i.ToString();
            if (GetRenderableObject<T>(currentName) == null)
                return currentName;
            return GetFreeName<T>(i + 1);
        }


        /*save and load*/

        void CreatePath(string Path)
        {
            if (!System.IO.Directory.Exists(Path))
                System.IO.Directory.CreateDirectory(Path);
        }


        void RemovePath(string Path)
        {
            if (System.IO.Directory.Exists(Path))
                System.IO.Directory.Delete(Path, true);
        }

        internal void Save(string Path)
        {
            
            CreatePath(Path);
            string contentPath = Path + "Content\\";

            /*create new stuff*/
            CreatePath(contentPath);

            /*Save all tilesetanimations*/
            string tilesetAnimationsPath = contentPath + "tileset_animations\\";
            RemovePath(tilesetAnimationsPath);
            CreatePath(tilesetAnimationsPath);
            SaveTilesetAnimations(tilesetAnimationsPath);


            /*Save all animations*/
            string animationPath = contentPath + "animations\\";
            RemovePath(animationPath);
            CreatePath(animationPath);
            SaveAnimations(animationPath);
        }

        internal void Load(string Path)
        {
            string contentPath = Path + "Content\\";

            /*textures*/
            string texturePath = contentPath + "textures\\";
            if (System.IO.Directory.Exists(texturePath))
            {
                string[] Files = System.IO.Directory.GetFiles(texturePath);
                foreach (var imageFile in Files)
                    LoadTexture(imageFile);
            }

            /*tileset-animations*/
            string tilesetAnimationsPath = contentPath + "tileset_animations\\";
            if (System.IO.Directory.Exists(tilesetAnimationsPath))
            {
                string[] tsaFiles = System.IO.Directory.GetFiles(tilesetAnimationsPath, "*.tsa");
                foreach (var tsaFile in tsaFiles)
                    LoadTilesetAnimations(tsaFile);
            }

            /*animations*/
            string animationsPath = contentPath + "animations\\";
            if (System.IO.Directory.Exists(animationsPath))
            {
                string[] animationFiles = System.IO.Directory.GetFiles(animationsPath, "*.ani");
                foreach (var animationFile in animationFiles)
                    LoadAnimation(animationFile);
            }
        }


    }
}
