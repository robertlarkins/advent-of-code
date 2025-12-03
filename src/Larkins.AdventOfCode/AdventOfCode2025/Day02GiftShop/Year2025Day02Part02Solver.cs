using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;

public class Year2025Day02Part02Solver
{
    private readonly List<NumberRange> ranges = [];

    public Year2025Day02Part02Solver(string input)
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
                if (HasRepeatedPattern(i.ToString()))
                {
                    sum += i;
                }
            }
        }

        return sum;
    }

    public static bool HasRepeatedPattern(ReadOnlySpan<char> number)
    {
        var numberOfDigits = number.Length;
        var halfOfDigits = numberOfDigits / 2;

        // go through from 1 to half of number of digits
        for (var i = 1; i <= halfOfDigits; i++)
        {
            var times = numberOfDigits / (double)i;
            if (times.HasFractionalPart(1e-08))
            {
                continue;
            }

            // break number into these portions and see if they are the same
            // get the identifier number
            var pattern = number[..i];
            var restOfNumber = number[i..];

            if (IsPatternRepeatedInString(pattern, restOfNumber))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsPatternRepeatedInString(ReadOnlySpan<char> pattern, ReadOnlySpan<char> target)
    {
        // can pattern go into target equally
        if (target.Length % pattern.Length != 0)
        {
            return false;
        }

        // how many times does pattern fit into target
        var times = Math.Round(target.Length / (double)pattern.Length);

        for (var i = 0; i < times; i++)
        {
            var start = i * pattern.Length;
            var end = start + pattern.Length;

            if (!target[start..end].SequenceEqual(pattern))
            {
                return false;
            }
        }

        return true;
    }

    private void ParseInput(string input)
    {
        var inputSpan = input.AsSpan();
        const string separator = ",";
        var splitLines = inputSpan.Split(separator);

        foreach (var lineRange in splitLines)
        {
            var line = inputSpan[lineRange];

            var parts = line.Split("-");

            List<long> items = [];

            foreach (var itemRange in parts)
            {
                items.Add(long.Parse(line[itemRange]));
            }

            ranges.Add(new NumberRange(items[0], items[1]));
        }
    }

    private record NumberRange(long Start, long End);
}
