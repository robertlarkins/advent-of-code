namespace Larkins.AdventOfCode.AdventOfCode2024.Day11PlutonianPebbles;

public class Year2024Day11Part01Solver
{
    private List<long> stones;
    private int blinks;
    
    public Year2024Day11Part01Solver(string input, int blinks)
    {
        ParseInput(input);
        this.blinks = blinks;
    }

    public int Solve()
    {
        for (var blink = 0; blink < blinks; blink++)
        {
            ProcessStones();
        }
        
        return stones.Count;
    }

    private void ProcessStones()
    {
        for (var i = 0; i < stones.Count; i++)
        {
            long digits;
            
            if (stones[i] == 0)
            {
                stones[i] = 1;
            }
            else if ((digits = (long)Math.Floor(Math.Log10(stones[i]) + 1)) % 2 == 0) // stones[i].ToString().Length % 2 == 0
            {
                var stone1 = (long)(stones[i] / Math.Pow(10, digits / 2));
                var stone2 = (long)(stones[i] % Math.Pow(10, digits / 2));

                stones[i] = stone1;
                stones.Insert(i + 1, stone2);
                
                i++;
            }
            else
            {
                stones[i] *= 2024;
            }
        }        
    }
    
    private void ParseInput(string input)
    {
        stones = input.Split(" ").Select(long.Parse).ToList();
    }
}
