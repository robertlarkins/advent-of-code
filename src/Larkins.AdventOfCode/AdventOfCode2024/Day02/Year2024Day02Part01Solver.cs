namespace Larkins.AdventOfCode.AdventOfCode2024.Day02;

public class Year2024Day02Part01Solver
{
    public int Solve(IEnumerable<string> input)
    {
        var safeReports = 0;
        
        foreach (var line in input)
        {
            var values = line.Split(" ");
            var levels = values.Select(int.Parse).ToList();

            var isSafe = IsReportSafe(levels);
            if (isSafe)
            {
                safeReports++;
            }
        }
        
        return safeReports;
    }

    private bool IsReportSafe(List<int> levels)
    {
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
}