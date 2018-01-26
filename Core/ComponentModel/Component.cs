using System;

namespace Core.ComponentModel
{
    public class Component : DisposeCollector
    {
        public string Name { get; set; }
        bool Disposed = false;
        public bool IsDispose { get { return Disposed; } }

        public Component(string Name = "")
        {
            this.Name = Name;
        }
    }
}
