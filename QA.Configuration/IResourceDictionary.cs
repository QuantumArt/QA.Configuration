using System;
using System.Windows.Markup;
namespace QA.Configuration
{
    public interface IResourceDictionary<TKey, TValue>
    {
        void Add(TKey key, TValue value);
        bool TryGetValue(TKey key, out TValue value);
    }
}
