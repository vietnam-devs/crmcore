using System;
using System.Collections.Generic;
using System.Linq;

namespace CRMCore.Framework.Entities.Extensions
{
    public static class CollectionExtensions
    {
        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> creator)
        {
            if (!dictionary.TryGetValue(key, out var result))
            {
                result = creator(key);

                dictionary.Add(key, result);
            }

            return result;
        }

        public static int DictionaryHashCode<TKey, TValue>(this IDictionary<TKey, TValue> dictionary)
        {
            return DictionaryHashCode(dictionary, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
        }

        public static int DictionaryHashCode<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            var hashCode = 17;

            foreach (var kvp in dictionary.OrderBy(x => x.Key))
            {
                hashCode = (hashCode * 23) + keyComparer.GetHashCode(kvp.Key);

                if (!Equals(kvp.Value, null))
                {
                    hashCode = (hashCode * 23) + valueComparer.GetHashCode(kvp.Value);
                }
            }

            return hashCode;
        }

        public static bool EqualsDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other)
        {
            return EqualsDictionary(dictionary, other, EqualityComparer<TKey>.Default, EqualityComparer<TValue>.Default);
        }

        public static bool EqualsDictionary<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, IReadOnlyDictionary<TKey, TValue> other, IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
        {
            var comparer = new KeyValuePairComparer<TKey, TValue>(keyComparer, valueComparer);

            return other != null && dictionary.Count == other.Count && !dictionary.Except(other, comparer).Any();
        }

        public sealed class KeyValuePairComparer<TKey, TValue> : IEqualityComparer<KeyValuePair<TKey, TValue>>
        {
            private readonly IEqualityComparer<TKey> keyComparer;
            private readonly IEqualityComparer<TValue> valueComparer;

            public KeyValuePairComparer(IEqualityComparer<TKey> keyComparer, IEqualityComparer<TValue> valueComparer)
            {
                this.keyComparer = keyComparer;
                this.valueComparer = valueComparer;
            }

            public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
            {
                return keyComparer.Equals(x.Key, y.Key) && valueComparer.Equals(x.Value, y.Value);
            }

            public int GetHashCode(KeyValuePair<TKey, TValue> obj)
            {
                return keyComparer.GetHashCode(obj.Key) ^ valueComparer.GetHashCode(obj.Value);
            }
        }
    }
}
