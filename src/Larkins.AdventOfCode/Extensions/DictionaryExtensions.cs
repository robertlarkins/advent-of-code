namespace Larkins.AdventOfCode.Extensions;

public static class DictionaryExtensions
{
    public static void AddOrIncrement<T>(
        this Dictionary<T, int> dictionary,
        T item)
        where T : notnull
    {
        var isAddSuccessful = dictionary.TryAdd(item, 1);
            
        if (!isAddSuccessful)
        {
            dictionary[item]++;
        }
    }
    
    public static void AddOrIncrement<T>(
        this Dictionary<T, long> dictionary,
        T item)
        where T : notnull
    {
        var isAddSuccessful = dictionary.TryAdd(item, 1);
            
        if (!isAddSuccessful)
        {
            dictionary[item]++;
        }
    }

    public static void AddOrInsert<TKey, TItem>(
        this Dictionary<TKey, SortedSet<TItem>> dictionary,
        TKey key,
        TItem item)
        where TKey : notnull
        where TItem : notnull
    {
        var isAddSuccessful = dictionary.TryAdd(key, [item]);
        
        if (!isAddSuccessful)
        {
            dictionary[key].Add(item);
        }
    }
}