using System;
using System.Collections.Generic;

namespace AdvancedCSharp01
{
    #region Q1: Generic Class Concept
    // Q1: What is a generic class? Why use generics?
    // ANSWER:
    // A Generic class allows you to define type-safe classes with placeholders (type parameters <T>)
    // without specifying the exact data type until the class is instantiated.
    // Benefits:
    // 1. Type Safety: Caught at compile time rather than runtime.
    // 2. Performance: Eliminates boxing/unboxing overhead for value types.
    // 3. Code Reusability: Write logic once, use with any type.
    #endregion

    #region Q2: Container<T>
    public class Container<T>
    {
        private List<T> items = new List<T>();
        public void Add(T item) => items.Add(item);
        public T Get(int index) => items[index];
    }
    #endregion

    #region Q3: Pair<TKey, TValue>
    public class Pair<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }
        public Pair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
    #endregion

    #region Q4: Generic Method Swap<T>
    public static class Utility
    {
        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;
        }

        #region Q5: FindMax<T>
        public static T FindMax<T>(T a, T b) where T : IComparable<T>
        {
            return a.CompareTo(b) > 0 ? a : b;
        }
        #endregion
    }
    #endregion

    #region Q6: Generic Interface IRepository<T>
    public interface IRepository<T>
    {
        void Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        void Delete(int id);
    }
    #endregion

    #region Q7: struct Constraint
    public class StructContainer<T> where T : struct
    {
        public T Value { get; set; }
    }
    #endregion

    #region Q8: class Constraint
    public class ReferenceContainer<T> where T : class
    {
        public T Value { get; set; }
    }
    #endregion

    #region Q9: new() Constraint
    public class Factory<T> where T : new()
    {
        public T CreateInstance() => new T();
    }
    #endregion

    #region Q10: Interface Constraint
    public class Sorter<T> where T : IComparable<T>
    {
        public int Compare(T a, T b) => a.CompareTo(b);
    }
    #endregion

    #region Q11: Base Class Constraint
    public class Entity { public int Id { get; set; } }
    public class EntityService<T> where T : Entity
    {
        public void PrintId(T item) => Console.WriteLine($"Entity ID: {item.Id}");
    }
    #endregion

    #region Q12: Multiple Constraints
    public class AdvancedProcessor<T> where T : class, IComparable<T>, new()
    {
        public T Process() => new T();
    }
    #endregion

    #region Q13 & Q14: Default Keyword & SafeList<T>
    public class SafeList<T>
    {
        private List<T> list = new List<T>();
        public void Add(T item) => list.Add(item);

        public T GetSafe(int index)
        {
            if (index >= 0 && index < list.Count)
                return list[index];
            return default(T); // Returns null for reference types, 0 for numeric, false for bool
        }
    }
    #endregion

    #region Q15: Covariance (out)
    // Covariance enables you to use a more derived type than originally specified.
    public interface IProducer<out T>
    {
        T Produce();
    }
    #endregion

    #region Q16: Contravariance (in)
    // Contravariance enables you to use a more generic (less derived) type than originally specified.
    public interface IConsumer<in T>
    {
        void Consume(T item);
    }
    #endregion

    #region Q17: Difference between Covariance and Contravariance
    // ANSWER:
    // - Covariance (out): Preserves assignment compatibility for output positions (return types). E.g. IEnumerable<out T>.
    // - Contravariance (in): Reverses assignment compatibility for input positions (method parameters). E.g. Action<in T>, IComparer<in T>.
    #endregion

    #region Q18: Static Members in Generics
    public class GenericCounter<T>
    {
        public static int Count = 0;
    }
    // Each closed generic type (GenericCounter<int> vs GenericCounter<string>) gets its OWN independent static field!
    #endregion

    #region Q19: Generic Inheritance
    // Open generic inheritance:
    public class BaseGeneric<T> { }
    public class DerivedGeneric<T> : BaseGeneric<T> { }
    // Closed generic inheritance:
    public class StringDerived : BaseGeneric<string> { }
    #endregion

    #region Q20: Complete Exercise - Cache<TKey, TValue> with Expiration
    public class CacheItem<TValue>
    {
        public TValue Value { get; }
        public DateTime ExpirationTime { get; }

        public CacheItem(TValue value, TimeSpan duration)
        {
            Value = value;
            ExpirationTime = DateTime.UtcNow.Add(duration);
        }

        public bool IsExpired => DateTime.UtcNow > ExpirationTime;
    }

    public class Cache<TKey, TValue> where TKey : notnull
    {
        private readonly Dictionary<TKey, CacheItem<TValue>> store = new Dictionary<TKey, CacheItem<TValue>>();

        public void Add(TKey key, TValue value, TimeSpan duration)
        {
            store[key] = new CacheItem<TValue>(value, duration);
        }

        public bool TryGet(TKey key, out TValue value)
        {
            if (store.TryGetValue(key, out var item))
            {
                if (!item.IsExpired)
                {
                    value = item.Value;
                    return true;
                }
                store.Remove(key); // Remove expired item
            }
            value = default(TValue);
            return false;
        }

        public bool Remove(TKey key) => store.Remove(key);
        public bool Contains(TKey key) => store.ContainsKey(key) && !store[key].IsExpired;
    }
    #endregion

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("==================================================");
            Console.WriteLine("=== Advanced C# Assignment 01 - Generics Demo ===");
            Console.WriteLine("==================================================");

            // Test Swap
            int x = 10, y = 20;
            Console.WriteLine($"Before Swap: x={x}, y={y}");
            Utility.Swap(ref x, ref y);
            Console.WriteLine($"After Swap: x={x}, y={y}");

            // Test FindMax
            Console.WriteLine($"FindMax(45, 99): {Utility.FindMax(45, 99)}");
            Console.WriteLine($"FindMax(\"Apple\", \"Zebra\"): {Utility.FindMax("Apple", "Zebra")}");

            // Test SafeList with default
            SafeList<string> safeList = new SafeList<string>();
            safeList.Add("Route Academy");
            Console.WriteLine($"SafeList Valid Index 0: '{safeList.GetSafe(0)}'");
            Console.WriteLine($"SafeList Invalid Index 5 (default): '{safeList.GetSafe(5)}' (null)");

            // Test Static in Generics
            GenericCounter<int>.Count = 5;
            GenericCounter<string>.Count = 10;
            Console.WriteLine($"GenericCounter<int>: {GenericCounter<int>.Count}");
            Console.WriteLine($"GenericCounter<string>: {GenericCounter<string>.Count}");

            // Test Generic Cache with Expiration
            Console.WriteLine("\n--- Testing Generic Cache with Expiration ---");
            Cache<string, string> userCache = new Cache<string, string>();
            userCache.Add("user:1", "Ahmed Mohamed", TimeSpan.FromMinutes(5));
            if (userCache.TryGet("user:1", out string cachedUser))
            {
                Console.WriteLine($"Cached value retrieved: {cachedUser}");
            }

            Console.WriteLine("\nAdvanced C# Assignment 01 completed successfully!");
        }
    }
}
