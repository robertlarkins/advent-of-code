namespace Larkins.AdventOfCode.AdventOfCode2024.Day19LinenLayout;

public class Year2024Day19Part02Solver
{
    private List<string> desiredDesigns = [];
    private List<string> availableTowels = [];

    public Year2024Day19Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public long Solve()
    {
        return desiredDesigns.Sum(CountWaysToFormPattern);
    }

    private long CountWaysToFormPattern(string pattern)
    {
        var dp = new long[pattern.Length + 1]; // This array tells us how many successful combinations got to each index
        dp[0] = 1;

        for (var i = 1; i <= pattern.Length; i++)
        {
            foreach (var towel in availableTowels)
            {
                var priorIndex = i - towel.Length;
                if (i >= towel.Length && pattern[priorIndex..i] == towel)
                {
                    dp[i] += dp[priorIndex];
                }
            }
        }

        return dp[pattern.Length];
    }

    private void ParseInput(List<string> input)
    {
        var towelPatterns = input[0].Split(",", StringSplitOptions.TrimEntries);
        availableTowels = towelPatterns.ToList();
        desiredDesigns = input.Skip(2).ToList();
    }
}
