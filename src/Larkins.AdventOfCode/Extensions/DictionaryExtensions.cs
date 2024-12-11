namespace Larkins.AdventOfCode.Extensions;

public static class DictionaryExtensions
{
    public static void AddOrIncrement<T>(
        this Dictionary<T, int> dictionary,
        T item)
        where T : notnull
    {
        var isItemInDictionary = dictionary.TryAdd(item, 1);
            
        if (!isItemInDictionary)
        {
            dictionary[item]++;
        }
    }
    
    public static void AddOrIncrement<T>(
        this Dictionary<T, long> dictionary,
        T item)
        where T : notnull
    {
        var isItemInDictionary = dictionary.TryAdd(item, 1);
            
        if (!isItemInDictionary)
        {
            dictionary[item]++;
        }
    }
}