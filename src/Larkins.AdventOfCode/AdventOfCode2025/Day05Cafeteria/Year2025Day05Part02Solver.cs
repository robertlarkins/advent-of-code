namespace Larkins.AdventOfCode.AdventOfCode2025.Day05Cafeteria;

public class Year2025Day05Part02Solver
{
    private readonly List<(long start, long end)> ranges = [];

    public Year2025Day05Part02Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        return ids.Count(IsInFreshRange);
    }

    private bool IsInFreshRange(long id)
    {
        foreach (var (rangeStart, rangeEnd) in ranges)
        {
            if (id >= rangeStart && id <= rangeEnd)
            {
                return true;
            }
        }
        // want to find the indexes of ranges where id is >= start

        return false;
    }

    private void ParseInput(string input)
    {
        var lines = input.Split(Environment.NewLine);

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            var values = line.Split('-');

            ranges.Add((long.Parse(values[0]), long.Parse(values[1])));
        }

        ranges.Sort();
    }
}
