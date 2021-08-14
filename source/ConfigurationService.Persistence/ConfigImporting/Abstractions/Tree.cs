using System.Linq;

namespace ConfigurationService.Persistence.ConfigImporting.Abstractions
{
    /// <summary>
    /// Tree-like data structure.
    /// </summary>
    /// <typeparam name="T">Tree node value type.</typeparam>
    public abstract class Tree<T>
    {
        protected abstract Node<T> Root { get; }

        /// <summary>
        /// Perfoms a replace operation. The current tree nodes will be replaced by nodes from the source tree.
        /// </summary>
        /// <param name="source">Source tree.</param>
        public void ReplaceNodes(Tree<T> source)
        {
            Replace(Root, source.Root);
        }

        private static void Replace(Node<T> dest, Node<T> source)
        {
            dest.Replace(source.Value);

            foreach (var sourceChild in source.Children)
            {
                var existed = dest.FindChild(sourceChild.Value);
                if (existed != null)
                {
                    Replace(existed, sourceChild);
                }
                else
                {
                    dest.AddChild(sourceChild.Value);
                }
            }

            foreach (var destChild in dest.Children.Where(x => !x.Visited).ToArray())
            {
                dest.RemoveChild(destChild);
            }
        }
    }
}