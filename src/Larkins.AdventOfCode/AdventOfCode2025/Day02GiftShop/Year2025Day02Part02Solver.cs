using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;

public class Year2025Day02Part02Solver
{
    private List<Range> ranges = [];

    public Year2025Day02Part02Solver(IEnumerable<string> input)
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
                if (HasRepeatedPattern(i))
                {
                    sum += i;
                }
            }
        }

        return sum;
    }

    public static bool HasRepeatedPattern(long number)
    {
        var numberOfDigits = number.NumberOfDigits();
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
            var digitShift = (int)Math.Pow(10, i);
            var restOfNumber = number / digitShift;
            var pattern = number - restOfNumber * digitShift;
            var different = false;

            for (var j = 0; j < times - 1; j++)
            {
                var nextRest = restOfNumber / digitShift;
                var nextChunk = restOfNumber - nextRest * digitShift;
                restOfNumber = nextRest;

                if (nextChunk == 0)
                {
                    nextChunk = nextRest;
                }

                if (nextChunk != pattern)
                {
                    different = true;
                    break;
                }
            }

            if (!different)
            {
                return true;
            }
        }

        return false;
    }

    // private static List<(int factor, int times)> CalculateFactors(int number)
    // {
    //     List<(int factor, int times)> factors = [];
    //
    //     for (var factor = 1; factor <= Math.Floor(Math.Sqrt(number)); factor++)
    //     {
    //         var times = number / (double)factor;
    //
    //         if (times.HasFractionalPart(1e-8))
    //         {
    //             factors.Add((factor, (int)Math.Round(times)));
    //         }
    //     }
    //
    //     return factors;
    // }

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
