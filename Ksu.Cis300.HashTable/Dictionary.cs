/* Dictionary.cs
 * Author: Rod Howell
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.HashTable
{
    /// <summary>
    /// A dictionary implemented using a hash table.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public class Dictionary<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// Gets the number of keys in the dictionary.
        /// </summary>
        public int Count => throw new NotImplementedException();

        /// <summary>
        /// Gets an enumerable for the keys.
        /// </summary>
        public IEnumerable<TKey> Keys => throw new NotImplementedException();

        /// <summary>
        /// Gets an enumerable for the values.
        /// </summary>
        public IEnumerable<TValue> Values => throw new NotImplementedException();

#if DEBUG
        /* This section contains conditionally-compiled code that exposes part of the implementation
         * to the unit test code. When the build configuration is "Debug", this code will be included
         * in the class definition, but when it is "Release", the compiler will ignore this code.
         */

        /// <summary>
        /// Gets the hash table.
        /// </summary>
        public int[] HashTable => throw new NotImplementedException();

        /// <summary>
        /// Gets the size of the current pool of cells.
        /// </summary>
        public int Capacity => throw new NotImplementedException();

        /// <summary>
        /// Gets the index of the first cell of the removed cells list, or -1 if this
        /// list is empty.
        /// </summary>
        public int RemovedList => throw new NotImplementedException();

#endif

        /// <summary>
        /// Tries to get the value associated with the given key.
        /// If the given key is null, throws an ArgumentNullException.
        /// </summary>
        /// <param name="k">The key to look up.</param>
        /// <param name="v">The value associated with k, or the default value for
        /// this type if k is not in the dictionary.</param>
        /// <returns>Whether k was found.</returns>
        public bool TryGetValue(TKey k, out TValue v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds the given key with the given associated value to the dictionary.
        /// If the given key is null, throws an ArgumentNullException.
        /// If the key already exists in the dictionary, throws an ArgumentException.
        /// </summary>
        /// <param name="k">The key to add.</param>
        /// <param name="v">The value to be associated with k.</param>
        public void Add(TKey k, TValue v)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets or sets the value associated with the given key.
        /// If the given key is null, throws an ArgumentNullException.
        /// If an attempt is made to get a nonexistent key, throws a KeyNotFoundException.
        /// If an attempt is made to set a key that already exists in the dictionary, the 
        /// value associated with the key is replaced by the value provided.
        /// </summary>
        /// <param name="k">The key.</param>
        /// <returns>The value associated with k.</returns>
        public TValue this[TKey k]
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

        /// <summary>
        /// Removes the given key and its associated value from the dicitonary.
        /// If the given key is null, throws an ArgumentNullException.
        /// </summary>
        /// <param name="k">The key to remove.</param>
        /// <returns>Whether k was found.</returns>
        public bool Remove(TKey k)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Determines whether the dictionary contains the given key.
        /// </summary>
        /// <param name="k">The key to look up.</param>
        /// <returns>Whether the dictionary contains the key k.</returns>
        public bool ContainsKey(TKey k)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Removes all keys and values from the dictionary.
        /// </summary>
        public void Clear()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an enumerator for the key-value pairs in the dictionary.
        /// </summary>
        /// <returns>An enumerator for the key-value pairs in the dictionary.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets an enumerator for the key-value pairs in the dictionary.
        /// </summary>
        /// <returns>An enumerator for the key-value pairs in the dictionary.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}
