using System.Collections.Concurrent;

namespace Opa.WebAssembly.Extensions;

public static class EnumerableExtensions
{
    public static ConcurrentDictionary<TKey,TValue> ToConcurrentDictionary<TKey,TValue>(this IEnumerable<TValue> enumerable, Func<TValue, TKey> keySelector)
        where TKey : notnull
    {
        var dict = new ConcurrentDictionary<TKey, TValue>();

        enumerable.ForEach(item => dict.TryAdd(keySelector(item), item));

        return dict;
    }

    public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumerable, Action<T> callback)
    {
        foreach (T item in enumerable)
        {
            callback(item);
        }

        return enumerable;
    }

    public static async Task ForEach<T>(this IEnumerable<T> enumerable, Func<T, Task> callback)
    {
        foreach(T item in enumerable)
        {
            await callback(item);
        }
    }
}
