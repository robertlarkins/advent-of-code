using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day11PlutonianPebbles;

public class Year2024Day11Part02Solver
{
    private Dictionary<long, long> stoneCache = [];
    private int blinks;
    
    public Year2024Day11Part02Solver(string input, int blinks)
    {
        this.blinks = blinks;
        ParseInput(input);
    }

    public long Solve()
    {
        for (var blink = 0; blink < blinks; blink++)
        {
            ProcessStones();
        }
        
        return stoneCache.Sum(x => x.Value);
    }

    private void ProcessStones()
    {
        var tempCache = new Dictionary<long, long>();
        
        foreach (var kvp in stoneCache)
        {
            if (kvp.Key == 0)
            {
                AddItemOrAddValue(tempCache, 1, kvp.Value);

                continue;
            }

            var digits = (long) Math.Floor(Math.Log10(kvp.Key) + 1);
            
            if (digits % 2 == 0) // stones[i].ToString().Length % 2 == 0
            {
                var pow = Math.Pow(10, digits / 2);
                var stone1 = (long)(kvp.Key / pow);
                var stone2 = (long)(kvp.Key % pow);

                AddItemOrAddValue(tempCache, stone1, kvp.Value);
                AddItemOrAddValue(tempCache, stone2, kvp.Value);
            }
            else
            {
                AddItemOrAddValue(tempCache, kvp.Key * 2024, kvp.Value);
            }
        }
        
        stoneCache = tempCache;
    }

    private void AddItemOrAddValue(Dictionary<long, long> dict, long key, long value)
    {
        if (!dict.TryAdd(key, value))
        {
            dict[key] += value;
        }
    }
    
    private void ParseInput(string input)
    {
        var stones = input.Split(" ").Select(long.Parse).ToList();
        foreach (var stone in stones)
        {
            stoneCache.AddOrIncrement(stone);
        }
    }
}
