using System;
using System.Collections.Generic;

namespace Core
{
    public class DisposeCollector : IDisposable
    {
        internal List<object> Items;
        bool Disposed;

        public DisposeCollector()
        {
            Items = new List<object>();
            Disposed = false;
        }

        public T ToDispose<T>(T Item) 
        {
            if (!Disposed)
            {
                if (!Items.Contains(Item))
                    Items.Add(Item);
            }
            return Item;
        }

        public bool RemoveAndDispose<T>(T Item)
        {
            if (!Disposed)
            {
                if (Remove(Item) && Item is IDisposable disposeMe)
                {
                    disposeMe.Dispose();
                    return true;
                }
            }
            return false;
        }

        public bool Remove<T>(T Item)
        {
            if (!Disposed)
            {
                if (Items.Contains(Item))
                {
                    Items.Remove(Item);
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
                for (int i = Items.Count - 1; i >= 0; i--)
                    RemoveAndDispose(Items[i]);
            }
        }
    }
}
