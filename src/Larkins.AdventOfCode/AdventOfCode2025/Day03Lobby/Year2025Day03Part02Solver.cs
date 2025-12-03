using System.Text;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day03Lobby;

public class Year2025Day03Part02Solver(IEnumerable<string> input)
{
    private readonly List<string> inputLines = input.ToList();

    public long Solve()
    {
        var sum = 0L;

        foreach (var line in inputLines)
        {
            var builder = new StringBuilder();
            var digitsToGet = 12;
            var startIndex = 0;

            for (var digit = 0; digit < digitsToGet; digit++)
            {
                var (digitChar, locationIndex) = FindLargestNumber(
                    line,
                    startIndex,
                    line.Length + digit - digitsToGet);
                builder.Append(digitChar);
                startIndex = locationIndex + 1;
            }

            sum += long.Parse(builder.ToString());
        }

        return sum;
    }

    private (char number, int index) FindLargestNumber(
        string batteryBank,
        int startIndex,
        int endIndex)
    {
        var largestNumber = '0';
        var largestIndex = startIndex;

        for (var i = startIndex; i <= endIndex; i++)
        {
            if (batteryBank[i] > largestNumber)
            {
                largestNumber = batteryBank[i];
                largestIndex = i;
            }
        }

        return (largestNumber, largestIndex);
    }
}
