using System;
using System.Collections;
using System.Collections.Generic;

namespace Kovu.EventSystems
{
    /// <summary>
    /// Analogue of Python's defaultdict.
    /// </summary>
    /// <see cref="http://stackoverflow.com/a/15622935"/>
    /// <typeparam name="TKey">Key type</typeparam>
    /// <typeparam name="TValue">Value type</typeparam>
    public class DefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        private readonly Func<TValue> _defaultSelector;
        private readonly Dictionary<TKey, TValue> _localDictionary = new Dictionary<TKey, TValue>();

        public DefaultDictionary(IDictionary<TKey, TValue> dictionary, Func<TValue> defaultSelector)
        {
            _localDictionary = new Dictionary<TKey, TValue>(dictionary);
            _defaultSelector = defaultSelector;
        }

        public DefaultDictionary(Func<TValue> defaultSelector)
        {
            _defaultSelector = defaultSelector;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _localDictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            ((IDictionary<TKey, TValue>)_localDictionary).Add(item);
        }

        public void Clear()
        {
            _localDictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)_localDictionary).Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            ((IDictionary<TKey, TValue>)_localDictionary).CopyTo(array, arrayIndex);
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return ((IDictionary<TKey, TValue>)_localDictionary).Remove(item);
        }

        public int Count { get { return _localDictionary.Count; } }
        public bool IsReadOnly { get { return ((IDictionary<TKey, TValue>)_localDictionary).IsReadOnly; } }

        public bool ContainsKey(TKey key)
        {
            return _localDictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            _localDictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _localDictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return _localDictionary.TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get
            {
                if (!_localDictionary.ContainsKey(key))
                {
                    _localDictionary.Add(key, _defaultSelector());
                }
                return _localDictionary[key];
            }
            set
            {
                if (!_localDictionary.ContainsKey(key))
                {
                    _localDictionary.Add(key, _defaultSelector());
                }
                _localDictionary[key] = value;
            }
        }

        public ICollection<TKey> Keys { get { return _localDictionary.Keys; } }
        public ICollection<TValue> Values { get { return _localDictionary.Values; } }

        public Dictionary<TKey, TValue> ToDictionary()
        {
            return new Dictionary<TKey, TValue>(_localDictionary);
        }
    }
}