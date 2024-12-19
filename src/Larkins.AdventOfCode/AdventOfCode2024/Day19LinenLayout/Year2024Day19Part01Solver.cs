namespace Larkins.AdventOfCode.AdventOfCode2024.Day19LinenLayout;

public class Year2024Day19Part01Solver
{
    private List<string> desiredDesigns = [];
    private List<string> availableTowels = [];

    public Year2024Day19Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public int Solve()
    {
        return desiredDesigns.Count(CanFormPattern);
    }

    /// <summary>
    /// Co-Pilot generated solution
    ///
    /// How It Works
    /// Initialization: The dp array is initialized with dp[0] set to true because an empty pattern can be formed by an empty combination of portions.
    /// Iterating through the pattern: For each position i in the pattern, the algorithm checks all portions.
    /// Matching portions: If a portion matches the substring of the pattern ending at position i, the algorithm updates the dp array.
    /// Final result: The value of dp[n] (where n is the length of the pattern) indicates whether the pattern can be formed by any combination of the portions.
    /// This algorithm efficiently checks all possible combinations of portions to form the pattern using dynamic programming. If you have any more questions or need further assistance, feel free to ask!
    /// </summary>
    private bool CanFormPattern(string pattern)
    {
        var dp = new bool[pattern.Length + 1]; // This array tells us if there were any successful combinations that got to each index
        dp[0] = true;

        for (var i = 1; i <= pattern.Length; i++)
        {
            foreach (var towel in availableTowels)
            {
                var towelLength = towel.Length;
                if (i >= towelLength && pattern[(i-towelLength)..i] == towel)
                {
                    dp[i] = dp[i] || dp[i - towelLength]; // was there successful patterns up to just before the length of this towel?
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
