// Owners: Karlov Nikolay

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Markup;

namespace QA.Configuration
{
    /// <summary>
    /// Ресурсный словарь
    /// </summary>
    [Ambient]
    [UsableDuringInitializationAttribute(true)]
    public class ResourceDictionary<TKey, TValue> : IDictionary<TKey, TValue>, IResourceDictionary<TKey,TValue>, IDictionary
    {
        IDictionary<TKey, TValue> _innerDictionary = new Dictionary<TKey, TValue>();
        protected Object Parent;

        internal ResourceDictionary(Object parentNode)
        {
            Parent = parentNode;
        }

        public void Add(TKey key, TValue value)
        {
            if (value is ConfigurableItem && Parent is ConfigurableItem)
            {
                (value as ConfigurableItem).SetParent((ConfigurableItem)Parent);
            }

            _innerDictionary.Add(key, value);
        }

        public bool ContainsKey(TKey key)
        {
            return _innerDictionary.ContainsKey(key);
        }

        public ICollection<TKey> Keys
        {
            get { return _innerDictionary.Keys; }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _innerDictionary.TryGetValue(key, out value);
        }

        public ICollection<TValue> Values
        {
            get { return _innerDictionary.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return _innerDictionary[key];
            }
            set
            {
                _innerDictionary[key] = value;
            }
        }

        public int Count
        {
            get { return _innerDictionary.Count; }
        }


        public bool Remove(TKey key)
        {
            throw new NotSupportedException("This feature is not supported.");
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _innerDictionary.Add(item);
        }

        public void Clear()
        {
            throw new NotSupportedException("This feature is not supported.");
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _innerDictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            throw new NotSupportedException("This feature is not supported.");
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new NotSupportedException("This feature is not supported.");
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _innerDictionary.GetEnumerator();
        }

		void IDictionary.Add(object key, object value)
		{
			((IDictionary)_innerDictionary).Add(key, value);
		}

		void IDictionary.Clear()
		{
			throw new NotImplementedException();
		}

		bool IDictionary.Contains(object key)
		{
			throw new NotImplementedException();
		}

		IDictionaryEnumerator IDictionary.GetEnumerator()
		{
			throw new NotImplementedException();
		}

		bool IDictionary.IsFixedSize
		{
			get { throw new NotImplementedException(); }
		}

		bool IDictionary.IsReadOnly
		{
			get { throw new NotImplementedException(); }
		}

		ICollection IDictionary.Keys
		{
			get { throw new NotImplementedException(); }
		}

		void IDictionary.Remove(object key)
		{
			throw new NotImplementedException();
		}

		ICollection IDictionary.Values
		{
			get { throw new NotImplementedException(); }
		}

		object IDictionary.this[object key]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		void ICollection.CopyTo(Array array, int index)
		{
			throw new NotImplementedException();
		}

		int ICollection.Count
		{
			get { throw new NotImplementedException(); }
		}

		bool ICollection.IsSynchronized
		{
			get { throw new NotImplementedException(); }
		}

		object ICollection.SyncRoot
		{
			get { throw new NotImplementedException(); }
		}
	}
}
