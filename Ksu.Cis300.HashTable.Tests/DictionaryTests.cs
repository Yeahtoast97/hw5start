/* DictionaryTests.cs
 * Author: Rod Howell
 */
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ksu.Cis300.HashTable.Tests
{
    /// <summary>
    /// Unit tests for the Dictionary class.
    /// </summary>
    [TestFixture]
    public class DictionaryTests
    {
        /// <summary>
        /// Tests that the initialization is correct.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAInitialization()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            int[] testArray = { -1, -1, -1, -1, -1 };
            Assert.Multiple(() =>
            {
                Assert.That(d.HashTable, Is.EqualTo(testArray)); // The hash table should contain 5 elements, all -1
                Assert.That(d.Capacity, Is.EqualTo(5)); // The cell pool should contain 5 elements.
                Assert.That(d.RemovedList, Is.EqualTo(-1)); // The removed list should be empty.
                Assert.That(d.Count, Is.EqualTo(0)); // The Count should be 0.
            });
        }

        /// <summary>
        /// Tests that looking up a nonexistent key using TryGetValue gives a value of false and sets the out
        /// parameter to it default value.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestATryGetValueEmpty()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            int v;
            bool b = d.TryGetValue("key", out v);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.False); // TryGetValue returned false
                Assert.That(v, Is.EqualTo(0)); // The out parameter was set to the default value
            });
        }

        /// <summary>
        /// Tests that looking up a nonexistent key using ContainsKey gives a value of false.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAContainsKeyEmpty()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            Assert.That(d.ContainsKey("key"), Is.False);
        }

        /// <summary>
        /// Tests that looking up a key in an empty dictionary using an indexer throws the
        /// correct exception.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAGetEmpty()
        {
            Dictionary<string, decimal> d = new Dictionary<string, decimal>();
            Exception e = null;
            try
            {
                decimal x = d["a"]; // This should throw a KeyNotFoundException
            }
            catch (Exception ex)
            {
                e = ex; // e is set to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(KeyNotFoundException)));
        }

        /// <summary>
        /// Tests that removing a nonexistent key returns false.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestARemoveEmpty()
        {
            Dictionary<int, decimal> d = new Dictionary<int, decimal>();
            Assert.That(d.Remove(3), Is.False);
        }

        /// <summary>
        /// Tests that when a duplicate key is added, the proper exception is thrown.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestAAddDuplicateKey()
        {
            Dictionary<int, string> d = new Dictionary<int, string>();
            d.Add(4, "four");
            Exception e = null;
            try
            {
                d.Add(4, "again"); // This should throw an ArgumentException
            }
            catch (Exception ex)
            {
                e = ex; // e is set to any exception that was thrown
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(ArgumentException)));
        }

        /// <summary>
        /// Tests the Count and Capacity after adding a key and value.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBCountOne()
        {
            Dictionary<int, double> d = new Dictionary<int, double>();
            d.Add(17, 32);
            Assert.Multiple(() =>
            {
                Assert.That(d.Count, Is.EqualTo(1));
                Assert.That(d.Capacity, Is.EqualTo(5));
            });
        }

        /// <summary>
        /// Tests that a key is added to the right location of the hash table and its GetHashCode method is called
        /// only once when Add is used.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddOne()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k = new HashTableTester(27, 30); // Hash code is 27.
            d.Add(k, "value");
            Assert.Multiple(() =>
            {
                Assert.That(d.HashTable[2], Is.EqualTo(0)); // Location 2 should refer to the first cell in the pool.
                Assert.That(k.GetHashCodeCount, Is.EqualTo(1)); // k's GetHashCode method should have been called only once.
            });
        }

        /// <summary>
        /// Tests that a key is added to the right location of the hash table and its GetHashCode method
        /// is called only once when an indexer is used.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBSetOne()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k = new HashTableTester(27, 30); // Hash code is 27.
            d[k] = "value"; // Hash code is 27.
            Assert.Multiple(() =>
            {
                Assert.That(d.HashTable[2], Is.EqualTo(0)); // Location 2 should refer to the first cell in the pool.
                Assert.That(k.GetHashCodeCount, Is.EqualTo(1)); // k's GetHashCode method should have been called only once.
            });
        }

        /// <summary>
        /// Adds a key and a value, then looks up that key using TryGetValue.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddOneTryGetValue()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k = new HashTableTester(100000, 5); // Hash code is 100000, Data is 5
            d.Add(k, "value");
            string v;
            bool b = d.TryGetValue(k, out v);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.True); // TryGetValue returned true
                Assert.That(v, Is.EqualTo("value")); // The out parameter was set to "value"
                Assert.That(k.GetHashCodeCount, Is.EqualTo(2)); // GetHashCode should be called once on Add, once on TryGetValue
            });
        }

        /// <summary>
        /// Adds a key and a value, then looks up that key using ContainsKey.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBAddOneContainsKey()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k = new HashTableTester(10000, 25); // Hash code is 10000, Data is 25
            d.Add(k, "value");
            Assert.Multiple(() =>
            {
                Assert.That(d.ContainsKey(k), Is.True);
                Assert.That(k.GetHashCodeCount, Is.EqualTo(2)); // GetHashCode should be called once on Add, once on ContainsKey
            });
        }

        /// <summary>
        /// Tests that adding a key and value using an indexer, then looking up the key using
        /// the indexer, gives the correct result.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestBSetOneGet()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k = new HashTableTester(100000, 5); // Hash code is 100000, Data is 5
            d[k] = "value";
            string v = d[k];
            Assert.Multiple(() =>
            {
                Assert.That(v, Is.EqualTo("value")); // The result of the lookup
                Assert.That(k.GetHashCodeCount, Is.EqualTo(2)); // GetHashCode should be called once on Set, once on Get
            });
        }

        /// <summary>
        /// Adds two keys that should hash to the same location and checks that this location refers to the second
        /// cell in the pool of cells.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddTwoSameLocation()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            d.Add(new HashTableTester(35, 60), 1);
            d.Add(new HashTableTester(75, 100), 2);
            Assert.That(d.HashTable[0], Is.EqualTo(1)); // Location 0 should refer to the second cell in the pool.
        }

        /// <summary>
        /// Adds two keys that should be stored in the same list, then looks up the first (which
        /// should be second in the list) using TryGetValue.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddTwoTryGetValueFirst()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k1 = new HashTableTester(1000, 23);
            HashTableTester k2 = new HashTableTester(1025, 25);
            d.Add(k1, "first");
            d.Add(k2, "second");
            string v;
            bool b = d.TryGetValue(k1, out v);
            Assert.Multiple(() =>
            {
                Assert.That(b, Is.True); // TryGetValue returned true.
                Assert.That(v, Is.EqualTo("first")); // The out parameter was set to "first"
                Assert.That(k1.GetHashCodeCount, Is.EqualTo(2));
                Assert.That(k1.EqualsCount, Is.EqualTo(1)); // k1 should only be involved in one equality test.
                Assert.That(k2.EqualsCount, Is.EqualTo(0)); // k2 should not be involved in any equality tests.
            });
        }

        /// <summary>
        /// Adds two keys that should be stored in the same list, then looks up the first (which
        /// should be second in the list) using ContainsKey.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddTwoContainsKeyFirst()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k1 = new HashTableTester(1000, 23);
            HashTableTester k2 = new HashTableTester(1025, 25);
            d.Add(k1, "first");
            d.Add(k2, "second");
            Assert.Multiple(() =>
            {
                Assert.That(d.ContainsKey(k1), Is.True);
                Assert.That(k1.GetHashCodeCount, Is.EqualTo(2));
                Assert.That(k1.EqualsCount, Is.EqualTo(1)); // k1 should only be involved in one equality test.
                Assert.That(k2.EqualsCount, Is.EqualTo(0)); // k2 should not be involved in any equality tests.
            });
        }

        /// <summary>
        /// Tests that adding two keys to the same list using indexers, then getting the first using
        /// an indexer, yields the correct result.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCSetTwoGetFirst()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k1 = new HashTableTester(1000, 23);
            HashTableTester k2 = new HashTableTester(1025, 25);
            d[k1] = "first";
            d[k2] = "second";
            Assert.Multiple(() =>
            {
                Assert.That(d[k1], Is.EqualTo("first"));
                Assert.That(k1.GetHashCodeCount, Is.EqualTo(2));
                Assert.That(k1.EqualsCount, Is.EqualTo(1)); // k1 should only be involved in one equality test.
                Assert.That(k2.EqualsCount, Is.EqualTo(0)); // k2 should not be involved in any equality tests.
            });
        }

        /// <summary>
        /// Adds two keys that should be stored in the same list, then looks up the second using ContainsKey.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCAddTwoContainsKeySecond()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k1 = new HashTableTester(9998, 1);
            HashTableTester k2 = new HashTableTester(10023, 10);
            d.Add(k1, "first");
            d.Add(k2, "second");
            Assert.That(d.ContainsKey(k2), Is.True);
        }

        /// <summary>
        /// Tests that adding two keys using indexers, then looking up the second using an indexer,
        /// yields the correct result.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestCSetTwoGetSecond()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k1 = new HashTableTester(9998, 1);
            HashTableTester k2 = new HashTableTester(10023, 10);
            d[k1] = "first";
            d[k2] = "second";
            Assert.That(d[k2], Is.EqualTo("second"));
        }

        /// <summary>
        /// Adds three keys with the same hash code, then looks up all three.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestDAddThreeLookUpAll()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            HashTableTester k1 = new HashTableTester(700, 0);
            HashTableTester k2 = new HashTableTester(700, 1);
            HashTableTester k3 = new HashTableTester(700, 2);
            d[k1] = 1;
            d.Add(k2, 2);
            d[k3] = 3;
            List<int> list = new List<int>();
            int v;

            // Look up each key, and add its associated value to list
            d.TryGetValue(k1, out v);
            list.Add(v);
            list.Add(d[k2]);
            d.TryGetValue(k3, out v);
            list.Add(v);
            Assert.Multiple(() =>
            {
                Assert.That(list, Is.Ordered.And.EquivalentTo(new int[] { 1, 2, 3 })); // Checks the results of the lookups
                Assert.That(k1.EqualsCount, Is.EqualTo(5));
            });
        }

        /// <summary>
        /// Adds 4 keys that should end up in different locations, then checks the values of all 5 locations.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEDifferentLocations()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            for (int i = 500; i < 504; i++)
            {
                d.Add(new HashTableTester(i, i), i);
            }
            int[] expected = { 0, 1, 2, 3, -1 };
            Assert.That(d.HashTable, Is.EqualTo(expected));
        }

        /// <summary>
        /// Adds two keys that should end up in different lists, then removes the first.
        /// Tests that the Remove method returns the right value, that the dictionary does 
        /// not contain the removed key, that the Count has been updated, and that the 
        /// removed cell has been added to the removed list.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAddTwoRemoveFirst()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k1 = new HashTableTester(44, 1);
            HashTableTester k2 = new HashTableTester(33, 2);
            d.Add(k1, "value1");
            d.Add(k2, "value2");
            Assert.Multiple(() =>
            {
                Assert.That(d.Remove(k1), Is.True);
                Assert.That(d.ContainsKey(k1), Is.False);
                Assert.That(d.Count, Is.EqualTo(1));
                Assert.That(d.RemovedList, Is.EqualTo(0));
            });
        }

        /// <summary>
        /// Adds two keys to the same list, removes the second (which should be at the beginning of the list), 
        /// and tests that the first remains in the dictionary.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAddTwoRemoveSecond()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            HashTableTester k1 = new HashTableTester(23, 2);
            HashTableTester k2 = new HashTableTester(33, 3);
            d.Add(k1, 2);
            d.Add(k2, 3);
            d.Remove(k2);
            Assert.That(d.ContainsKey(k1));
        }

        /// <summary>
        /// Adds three keys to the same list, removes the second, and tests that only the first and third remain.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAddThreeRemoveSecond()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            HashTableTester k1 = new HashTableTester(22, 2);
            d.Add(k1, 2);
            HashTableTester k2 = new HashTableTester(32, 3);
            d.Add(k2, 3);
            HashTableTester k3 = new HashTableTester(12, 1);
            d.Add(k3, 1);
            d.Remove(k2);
            Assert.Multiple(() =>
            {
                Assert.That(d[k1], Is.EqualTo(2));
                Assert.That(d.ContainsKey(k2), Is.False);
                Assert.That(d[k3], Is.EqualTo(1));
            });
        }

        /// <summary>
        /// Adds two keys, removes them both, then adds a third.
        /// Tests that the removed list now contains only the second cell from the pool,
        /// and that the hash table is using the first cell from the pool.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestEAddUsesRemovedList()
        {
            Dictionary<int, decimal> d = new Dictionary<int, decimal>();
            d.Add(100, 5);
            d.Add(151, 6);
            d.Remove(151);
            d.Remove(100);
            d.Add(199, 3);
            Assert.Multiple(() =>
            {
                Assert.That(d.RemovedList, Is.EqualTo(1));
                Assert.That(d.HashTable[4], Is.EqualTo(0));
            });
        }

        /// <summary>
        /// Tests that adding 5 keys and values does not cause a rehash to occur.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFAddFive()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            for (int i = 199; i < 204; i++)
            {
                d.Add(new HashTableTester(i, i), i);
            }
            Assert.That(d.Capacity, Is.EqualTo(5));
        }

        /// <summary>
        /// Tests that adding five keys, removing four of them, and adding four others
        /// does not do a rehash and ends up with the right keys and values in the dictionary.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestFAdd5Remove4Add4()
        {
            Dictionary<int, decimal> d = new Dictionary<int, decimal>();
            for (int i = 0; i < 5; i++)
            {
                d.Add(i, i);
            }
            for (int i = 1; i < 5; i++)
            {
                d.Remove(i);
            }
            int count = d.Count;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 5; i++)
            {
                sb.Append(d.ContainsKey(i)).Append(';');
            }
            for (int i = 11; i < 15; i++)
            {
                d.Add(i, i);
            }
            decimal[] retrieved = new decimal[5];
            retrieved[0] = d[0];
            for (int i = 11; i < 15; i++)
            {
                retrieved[i - 10] = d[i];
            }
            decimal[] expected = { 0, 11, 12, 13, 14 };
            Assert.Multiple(() =>
            {
                Assert.That(d.Capacity, Is.EqualTo(5)); // No rehash has been done.
                Assert.That(count, Is.EqualTo(1)); // The count after the removes.
                Assert.That(sb.ToString(), Is.EqualTo("True;False;False;False;False;")); // Only key 0 should be present after removes
                Assert.That(retrieved, Is.EqualTo(expected)); // Final dictionary contents are correct.
            });
        }

        /// <summary>
        /// Tests rehashing by adding 6 keys that should end up in different lists
        /// after the rehashing is done.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestGRehash()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            for (int i = 1; i < 7; i++)
            {
                d.Add(new HashTableTester(i, i), i);
            }
            int[] expected = { -1, 0, 1, 2, 3, 4, 5, -1, -1, -1, -1 };
            Assert.Multiple(() =>
            {
                Assert.That(d.Capacity, Is.EqualTo(11));
                Assert.That(d.HashTable, Is.EqualTo(expected));
            });
        }

        /// <summary>
        /// Tests rehashing when all elements should end up in the same list after
        /// rehashing.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestHRehashToSameList()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            List<HashTableTester> keys = new List<HashTableTester>();
            for (int i = 50; i < 116; i += 11)
            {
                keys.Add(new HashTableTester(i, i));
            }
            foreach (HashTableTester k in keys)
            {
                d.Add(k, k.Data); // Adds 6 keys that should end up in the same list after rehashing
            }
            StringBuilder sb = new StringBuilder();
            foreach (HashTableTester k in keys)
            {
                sb.Append(d.ContainsKey(k)).Append(';');
            }
            int[] expected = { -1, -1, -1, -1, -1, -1, 5, -1, -1, -1, -1 };
            Assert.Multiple(() =>
            {
                Assert.That(d.HashTable, Is.EqualTo(expected)); // All keys are in the same list
                Assert.That(sb.ToString(), Is.EqualTo("True;True;True;True;True;True;")); // All keys were found.
            });
        }

        /// <summary>
        /// Tests rehashing twice such that all elements should end up in the same list after
        /// the second rehash.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestIDoubleRehashToSameList()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            List<HashTableTester> elements = new List<HashTableTester>();
            for (int i = 100; i < 376; i += 23)
            {
                HashTableTester k = new HashTableTester(i, i);
                d.Add(k, i); // Adds 12 keys that should end up in the same list after rehashing twice
                elements.Add(k);
            }
            int sum = 0;
            foreach (HashTableTester k in elements)
            {
                if (d.ContainsKey(k))
                {
                    sum++;
                }
            }
            Assert.That(sum, Is.EqualTo(12)); // All 12 keys were found.
        }

        /// <summary>
        /// Tests that Clear resets the dictionary to its initial state.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestIClear()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            for (int i = 0; i < 7; i++)
            {
                d.Add(i.ToString(), i);
            }
            d.Clear();
            Assert.Multiple(() =>
            {
                Assert.That(d.Count, Is.EqualTo(0));
                Assert.That(d.HashTable, Is.EqualTo(new int[] { -1, -1, -1, -1, -1 })); // Five empty lists.
                Assert.That(d.Capacity, Is.EqualTo(5));
                Assert.That(d.RemovedList, Is.EqualTo(-1)); // An empty list.
            });
        }
        /// <summary>
        /// Tests that the Current property of the enumerator obtained from a dictionary
        /// will throw the correct exception if accessed before calling MoveNext.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestJCurrentKeyValuePairAtStart()
        {
            Dictionary<HashTableTester, double> d = new Dictionary<HashTableTester, double>();
            d.Add(new HashTableTester(0, 1), 1.1);
            IEnumerator<KeyValuePair<HashTableTester, double>> enumerator = d.GetEnumerator();
            Exception e = null;
            try
            {
                KeyValuePair<HashTableTester, double> p = enumerator.Current; // Should throw an InvalidOperationException
            }
            catch (Exception ex)
            {
                e = ex; // Sets e to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that the Current property of the key enumerator obtained from a dictionary
        /// will throw the correct exception if accessed before calling MoveNext.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestJCurrentKeyAtStart()
        {
            Dictionary<HashTableTester, double> d = new Dictionary<HashTableTester, double>();
            d.Add(new HashTableTester(0, 1), 1.1);
            IEnumerator<HashTableTester> enumerator = d.Keys.GetEnumerator();
            Exception e = null;
            try
            {
                HashTableTester p = enumerator.Current; // Should throw an InvalidOperationException
            }
            catch (Exception ex)
            {
                e = ex; // Sets e to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that the Current property of the value enumerator obtained from a dictionary
        /// will throw the correct exception if accessed before calling MoveNext.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestJCurrentValueAtStart()
        {
            Dictionary<HashTableTester, double> d = new Dictionary<HashTableTester, double>();
            d.Add(new HashTableTester(0, 1), 1.1);
            IEnumerator<double> enumerator = d.Values.GetEnumerator();
            Exception e = null;
            try
            {
                double p = enumerator.Current; // Should throw an InvalidOperationException
            }
            catch (Exception ex)
            {
                e = ex; // Sets e to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that for any of the three enumerators for a dictionary with one element, MoveNext advances to
        /// the first element and returns true, and Current returns the first KeyValuePair.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestKMoveToFirst()
        {
            Dictionary<HashTableTester, string> d = new Dictionary<HashTableTester, string>();
            HashTableTester k = new HashTableTester(0, 1);
            d.Add(k, "first");
            IEnumerator<KeyValuePair<HashTableTester, string>> kvpEnumerator = d.GetEnumerator();
            IEnumerator<HashTableTester> keyEnumerator = d.Keys.GetEnumerator();
            IEnumerator<string> valueEnumerator = d.Values.GetEnumerator();
            StringBuilder sb = new StringBuilder();
            sb.Append(kvpEnumerator.MoveNext()).Append(';');
            sb.Append(keyEnumerator.MoveNext()).Append(';');
            sb.Append(valueEnumerator.MoveNext()).Append(';');
            Assert.Multiple(() =>
            {
                Assert.That(sb.ToString(), Is.EqualTo("True;True;True;")); // MoveNext should return true for each enumerator.
                Assert.That(kvpEnumerator.Current, Is.EqualTo(new KeyValuePair<HashTableTester, string>(k, "first")));
                Assert.That(keyEnumerator.Current, Is.EqualTo(k));
                Assert.That(valueEnumerator.Current, Is.EqualTo("first"));
            });
        }

        /// <summary>
        /// Tests that MoveNext moves to the second element.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestLMoveToSecond()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            HashTableTester k1 = new HashTableTester(1, 11);
            d.Add(k1, 1);
            HashTableTester k2 = new HashTableTester(4, 44);
            d.Add(k2, 4);

            // Construct a key-value pair enumerator, a key enumerator, and a value enumerator.
            // Call MoveNext once on the key-value pair enumerator, and twice on the other two.
            IEnumerator<KeyValuePair<HashTableTester, int>> kvpEnumerator = d.GetEnumerator();
            kvpEnumerator.MoveNext();
            IEnumerator<HashTableTester> keyEnumerator = d.Keys.GetEnumerator();
            keyEnumerator.MoveNext();
            keyEnumerator.MoveNext();
            IEnumerator<int> valueEnumerator = d.Values.GetEnumerator();
            valueEnumerator.MoveNext();
            valueEnumerator.MoveNext();

            Assert.Multiple(() =>
            {
                Assert.That(kvpEnumerator.MoveNext(), Is.True);
                Assert.That(kvpEnumerator.Current, Is.EqualTo(new KeyValuePair<HashTableTester, int>(k2, 4)));
                Assert.That(keyEnumerator.Current, Is.EqualTo(k2));
                Assert.That(valueEnumerator.Current, Is.EqualTo(4));
            });
        }

        /// <summary>
        /// Tests that the enumerators move past a removed cell.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestLMovePastRemoved()
        {
            Dictionary<int, int> d = new Dictionary<int, int>();
            d.Add(1, 10);
            d.Add(2, 20);
            d.Remove(1);

            // Get a key-value pair enumerator, a key enumerator, and a value enumerator.
            // Call MoveNext once on the key-value pair enumerator and the value enumerator.
            IEnumerator<KeyValuePair<int, int>> kvpEnumerator = d.GetEnumerator();
            kvpEnumerator.MoveNext();
            IEnumerator<int> keyEnumerator = d.Keys.GetEnumerator();
            IEnumerator<int> valueEnumerator = d.Values.GetEnumerator();
            valueEnumerator.MoveNext();

            Assert.Multiple(() =>
            {
                Assert.That(keyEnumerator.MoveNext(), Is.True);
                Assert.That(kvpEnumerator.Current, Is.EqualTo(new KeyValuePair<int, int>(2, 20)));
                Assert.That(keyEnumerator.Current, Is.EqualTo(2));
                Assert.That(valueEnumerator.Current, Is.EqualTo(20));
            });
        }

        /// <summary>
        /// Tests that MoveNext returns false when moving to the end position.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestMMoveToEnd()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            d.Add("first", 1);
            StringBuilder sb = new StringBuilder();
            IEnumerator<KeyValuePair<string, int>> kvpEnumerator = d.GetEnumerator();
            kvpEnumerator.MoveNext();
            sb.Append(kvpEnumerator.MoveNext()).Append(';');
            IEnumerator<string> keyEnumerator = d.Keys.GetEnumerator();
            keyEnumerator.MoveNext();
            sb.Append(keyEnumerator.MoveNext()).Append(';');
            IEnumerator<int> valueEnumerator = d.Values.GetEnumerator();
            valueEnumerator.MoveNext();
            sb.Append(valueEnumerator.MoveNext()).Append(';');
            Assert.That(sb.ToString(), Is.EqualTo("False;False;False;")); // The result of the second call to MoveNext for each enumerator.
        }

        /// <summary>
        /// Tests that Current throws the correct exception when at the end position.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestNKvpCurrentAtEnd()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            IEnumerator<KeyValuePair<string, int>> enumerator = d.GetEnumerator();
            enumerator.MoveNext();
            Exception e = null;
            try
            {
                KeyValuePair<string, int> p = enumerator.Current; // Should throw an InvalidOperationException
            }
            catch (Exception ex)
            {
                e = ex; // Sets e to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that Current throws the correct exception when at the end position.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestNKeyCurrentAtEnd()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            IEnumerator<string> enumerator = d.Keys.GetEnumerator();
            enumerator.MoveNext();
            Exception e = null;
            try
            {
                string p = enumerator.Current; // should throw an InvalidOperationException
            }
            catch (Exception ex)
            {
                e = ex; // Sets e to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that Current throws the correct exception when at the end position.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestNValueCurrentAtEnd()
        {
            Dictionary<string, int> d = new Dictionary<string, int>();
            IEnumerator<int> enumerator = d.Values.GetEnumerator();
            enumerator.MoveNext();
            Exception e = null;
            try
            {
                int p = enumerator.Current; // should throw an InvalidOperationException
            }
            catch (Exception ex)
            {
                e = ex; // Sets e to any exception that was thrown.
            }
            Assert.That(e, Is.Not.Null.And.TypeOf(typeof(InvalidOperationException)));
        }

        /// <summary>
        /// Tests that a foreach correctly iterates through an empty dictionary.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestOForeachEmpty()
        {
            Dictionary<string, double> d = new Dictionary<string, double>();
            List<object> list = new List<object>();
            foreach (KeyValuePair<string, double> p in d)
            {
                list.Add(p);
            }
            foreach (string x in d.Keys)
            {
                list.Add(x);
            }
            foreach (double x in d.Values)
            {
                list.Add(x);
            }
            Assert.That(list, Is.Empty);
        }

        /// <summary>
        /// Tests a foreach on a dictionary with several keys.
        /// </summary>
        [Test, Timeout(1000)]
        public void TestPForeachNonempty()
        {
            Dictionary<HashTableTester, int> d = new Dictionary<HashTableTester, int>();
            HashTableTester[] keys =
            {
                new HashTableTester(50, 1),
                new HashTableTester(90, 2),
                new HashTableTester(39, 3),
                new HashTableTester(68, 4),
                new HashTableTester(17, 5),
                new HashTableTester(43, 6)
            };
            int[] values = { 10, 11, 12, 13, 14, 15 };
            for (int i = 0; i < keys.Length; i++)
            {
                d.Add(keys[i], values[i]);
            }
            List<HashTableTester> keyList = new List<HashTableTester>();
            foreach (HashTableTester t in d.Keys)
            {
                keyList.Add(t);
            }
            List<int> valueList = new List<int>();
            foreach (int i in d.Values)
            {
                valueList.Add(i);
            }
            Assert.Multiple(() =>
            {
                Assert.That(keyList, Is.EquivalentTo(keys));
                Assert.That(valueList, Is.EquivalentTo(values));
            });
        }

        /// <summary>
        /// A class whose instances can be used as keys to test a hash table. Hash codes are set
        /// via a parameter to the constructor. Two instances are considered to be equal if their
        /// hash codes and their Data properties are equal. Instances record the number of times
        /// their GetHashCode has been called and the number of times they have been tested for
        /// equality to another (non-null) instance.
        /// </summary>
        private class HashTableTester
        {
            /// <summary>
            /// The hash code.
            /// </summary>
            private int _hashCode;

            /// <summary>
            /// Gets the data stored in this object.
            /// </summary>
            public int Data { get; }

            /// <summary>
            /// Gets the number of times this object's GetHashCode method has been called.
            /// </summary>
            public int GetHashCodeCount { get; private set; }

            /// <summary>
            /// Gets the number of times this object has been compared for equality to another
            /// (non-null) instance.
            /// </summary>
            public int EqualsCount { get; private set; }

            /// <summary>
            /// Constructs a new instance having the given hash code and data.
            /// </summary>
            /// <param name="hashCode">The hash code.</param>
            /// <param name="data">The data stored in this object.</param>
            public HashTableTester(int hashCode, int data)
            {
                _hashCode = hashCode;
                Data = data;
            }

            /// <summary>
            /// Gets the hash code.
            /// </summary>
            /// <returns>The hash code.</returns>
            public override int GetHashCode()
            {
                GetHashCodeCount++;
                return _hashCode;
            }

            /// <summary>
            /// Determines whether the given object is equal to this instance.
            /// Two HashTableTester objects are considered to be equal if they
            /// store the same hash code and data.
            /// </summary>
            /// <param name="obj">The object to compare to.</param>
            /// <returns>Whether obj is equal to this instance.</returns>
            public override bool Equals(object obj)
            {
                if (obj is HashTableTester)
                {
                    HashTableTester x = (HashTableTester)obj;
                    return x == this;
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// Determines whether the given instances are equal. Two instances are considered
            /// to be equal if they have the same hash code and data.
            /// </summary>
            /// <param name="x">One instance to compare.</param>
            /// <param name="y">The other instance.</param>
            /// <returns>Whether x and y are equal.</returns>
            public static bool operator ==(HashTableTester x, HashTableTester y)
            {
                if (Equals(x, null))
                {
                    return Equals(y, null);
                }
                else if (Equals(y, null))
                {
                    return false;
                }
                else
                {
                    x.EqualsCount++;
                    if (!ReferenceEquals(x, y))
                    {
                        y.EqualsCount++;
                    }
                    return x._hashCode == y._hashCode && x.Data == y.Data;
                }
            }

            /// <summary>
            /// Determines whether the given instances are different.
            /// </summary>
            /// <param name="x">One instance to compare.</param>
            /// <param name="y">The other instance.</param>
            /// <returns>Whether x and y are different.</returns>
            public static bool operator !=(HashTableTester x, HashTableTester y)
            {
                return !(x == y);
            }
        }

    }
}
