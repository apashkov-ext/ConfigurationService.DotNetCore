using System.Collections.Generic;

namespace ConfigurationService.Persistence.ConfigImporting.Abstractions
{
    /// <summary>
    /// Node of a tree-like data structure.
    /// </summary>
    /// <typeparam name="T">Type of node value.</typeparam>
    public abstract class Node<T>
    {
        public bool Visited { get; protected set; }

        public void Replace(T value)
        {
            if (Visited)
            {
                return;
            }

            ReplaceAction(value);
            Visited = true;
        }

        public abstract T Value { get; }
        public abstract IEnumerable<Node<T>> Children { get; }
        public abstract Node<T> AddChild(T value);
        public abstract void RemoveChild(Node<T> child);
        public abstract Node<T> FindChild(T value);
        protected abstract void ReplaceAction(T value);
    }
}