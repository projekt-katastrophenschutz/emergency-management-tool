using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektKatastrophenschutz.Extensions
{
    public static class Collections
    {
        public static async Task ReplaceAllItemsAsync<T>(this ICollection<T> collection,
            ICollection<T> collectionWithNewItems)
        {
            await Task.Run(() =>
            {
                collection.Clear();
                foreach (var element in collectionWithNewItems)
                    collection.Add(element);
            });
        }

        public static IEnumerable<T> Clone<T>(this IEnumerable<T> collection) where T : ICloneable
        {
            return collection.Select(item => (T)item.Clone());
        }
    }
}
