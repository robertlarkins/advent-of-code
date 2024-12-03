namespace Larkins.AdventOfCode.AdventOfCode2024.Day02;

public class Year2024Day02Part02Solver
{
    public int Solve(IEnumerable<string> input)
    {
        var safeReports = 0;
        
        foreach (var line in input)
        {
            var values = line.Split(" ");
            var levels = values.Select(int.Parse).ToList();

            var isSafe = IsReportSafe(levels); // This line isn't needed as StepThroughRemovingLevels could be used directly
            if (!isSafe)
            {
                isSafe = StepThroughRemovingLevels(levels);
            }
            
            if (isSafe)
            {
                safeReports++;
            }
        }
        
        return safeReports;
    }
    
    private bool StepThroughRemovingLevels(List<int> levels)
    {
        for (var i = 0; i < levels.Count; i++)
        {
            var removedLevel = levels[i];
            levels.RemoveAt(i);
            var isSafe = IsReportSafe(levels);
            levels.Insert(i, removedLevel);

            if (isSafe)
            {
                return true;
            }
        }

        return false;
    }

    private bool IsReportSafe(List<int> levels)
    {
        // either the whole report matches this trend
        // or the report does not have a consistent trend.
        var diffTrend = levels[0] - levels[1];
        
        for (var i = 0; i < levels.Count - 1; i++)
        {
            var diff = levels[i] - levels[i + 1];

            if (diff == 0 || // has no value change
                Math.Sign(diffTrend) != Math.Sign(diff) || // has change in trend
                Math.Abs(diff) > 3) // level change is too big
            {
                return false;
            }
        }

        return true;
    }
    
    // Failed attempt at detecting which levels are bad, rather than trying to remove each level.
    //
    // private bool IsReportSafe(List<int> levels)
    // {
    //     var previousDiff = levels[0] - levels[1];
    //     var badChanges = CountBadLevelChanges(levels);
    //     var badDifferences = CountBadDifferences(levels);
    //     var badLevels = trends;
    //     
    //     
    //
    //     return true;
    // }
    //
    // private int CountBadDifferences(List<int> levels)
    // {
    //     for (var i = 0; i < levels.Count - 1; i++)
    //     {
    //         var diff = levels[i] - levels[i + 1];
    //
    //         if (Math.Sign(previousDiff) != Math.Sign(diff))
    //         {
    //             return false;
    //         }
    //
    //         diff = Math.Abs(diff);
    //         
    //         if (!(diff >= 1 && diff <= 3))
    //         {
    //             return false;
    //         }
    //     }
    // }
    //
    // private bool IsBadReport(List<int> levels)
    // {
    //     var diffs = new List<int>();
    //     var signs = new List<int>();
    //
    //     var positiveDiff = 0;
    //     var negativeDiff = 0;
    //     var zeroDiff = 0;
    //     var unsafeChange = 0;
    //     
    //     for (var i = 0; i < levels.Count - 1; i++)
    //     {
    //         var diff = Math.Abs(levels[i] - levels[i + 1]);
    //         diffs.Add(diff);
    //         signs.Add(Math.Sign(diff));
    //
    //         if (diff is < 1 or > 3)
    //         {
    //             unsafeChange++;
    //         }
    //
    //         if (Math.Sign(diff) == 1)
    //         {
    //             positiveDiff++;
    //         }
    //         
    //         if (Math.Sign(diff) == -1)
    //         {
    //             negativeDiff++;
    //         }
    //         
    //         if (Math.Sign(diff) == 0)
    //         {
    //             zeroDiff++;
    //         }
    //     }
    //
    //     if (zeroDiff > 0)
    //     {
    //         return true;
    //     }
    //
    //     if (unsafeChange > 1)
    //     {
    //         return true;
    //     }
    //
    //     if (positiveDiff == 0 || negativeDiff == 0)
    //     {
    //         return 0;
    //     }
    //     
    //     if (positiveDiff == 1 || negativeDiff == 1)
    //     {
    //         return 1;
    //     }
    //
    //     return 10;
    // }
}