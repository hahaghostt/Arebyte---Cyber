using System.Collections.Generic;

namespace DS.Utilities
{
    public static class CollectionUtility
    {
        public static void AddItem<K, V>(this SerializableDictionary<K, List<V>> serializableDictionary, K key, V value)
        {
            if (serializableDictionary.ContainsKey(key))
            {
                serializableDictionary[key].Add(value);

                return;
            }
            // If there isn't a list at [key] in the dictionary, create a new one with the given value and add it to the dictionary
            serializableDictionary.Add(key, new List<V>() { value });
        }
    }
}
