using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;

public class Year2025Day02Part01Solver
{
    private List<Range> ranges = [];

    public Year2025Day02Part01Solver(IEnumerable<string> input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        var sum = 0L;

        foreach (var range in ranges)
        {
            for (var i = range.Start; i <= range.End; i++)
            {
                var numberOfDigits = i.NumberOfDigits();

                if (numberOfDigits % 2 == 1)
                {
                    continue;
                }

                var digitShift = (int)Math.Pow(10, numberOfDigits / 2);
                var firstHalf = i / digitShift;
                var secondHalf = i - firstHalf * digitShift;

                if (firstHalf == secondHalf)
                {
                    sum += i;
                }
            }
        }

        return sum;
    }

    private void ParseInput(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var parts = line.Split("-");
            ranges.Add(new Range(long.Parse(parts[0]), long.Parse(parts[1])));
        }
    }

    private record Range(long Start, long End);
}
