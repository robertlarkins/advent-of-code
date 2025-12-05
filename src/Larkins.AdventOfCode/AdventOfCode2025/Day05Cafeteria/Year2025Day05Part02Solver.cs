namespace Larkins.AdventOfCode.AdventOfCode2025.Day05Cafeteria;

public class Year2025Day05Part02Solver(string input)
{
    private readonly SortedList<long, long> ranges = ParseInput(input);

    public long Solve()
    {
        return ranges.Sum(range => range.Value - range.Key + 1);
    }

    public static SortedList<long, long> ParseInput(string input)
    {
        var lines = input.Split(Environment.NewLine);
        SortedList<long, long> ranges = [];

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                break;
            }

            var values = line.Split('-');

            var rangeStart = long.Parse(values[0]);
            var rangeEnd = long.Parse(values[1]);

            if (rangeStart > rangeEnd)
            {
                throw new ArgumentException("Bad Data Input");
            }

            if (ranges.TryAdd(rangeStart, rangeEnd))
            {
                continue;
            }

            if (ranges[rangeStart] < rangeEnd)
            {
                ranges[rangeStart] = rangeEnd;
            }
        }

        for (var i = ranges.Count - 1; i > 0; i--)
        {
            var currentStart = ranges.GetKeyAtIndex(i);
            var currentEnd = ranges.GetValueAtIndex(i);
            var previousStart = ranges.GetKeyAtIndex(i-1);
            var previousEnd = ranges.GetValueAtIndex(i-1);

            // prior.Start is always before current.Start
            if (currentStart == previousEnd + 1 ||
                currentStart == previousEnd ||
                currentStart < previousEnd && previousEnd < currentEnd)
            {
                ranges[previousStart] = currentEnd;
                ranges.RemoveAt(i);
                i++;

                continue;
            }

            if (currentEnd <= previousEnd)
            {
                ranges.RemoveAt(i);
                i++;
            }
        }

        // validate range
        for (var i = 1; i < ranges.Count; i++)
        {
            if (ranges.GetKeyAtIndex(i) <= ranges.GetValueAtIndex(i - 1))
            {
                throw new Exception("Bad Ordering");
            }

            if (ranges.GetValueAtIndex(i - 1) < ranges.GetKeyAtIndex(i - 1))
            {
                throw new Exception("Bad Ordering single");
            }
        }

        return ranges;
    }
}
