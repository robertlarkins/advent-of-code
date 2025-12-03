namespace Larkins.AdventOfCode.AdventOfCode2025.Day03Lobby;

public class Year2025Day03Part01Solver(IEnumerable<string> input)
{
    private readonly List<string> inputLines = input.ToList();

    public int Solve()
    {
        var sum = 0;

        foreach (var line in inputLines)
        {
            var (digit1, index1) = FindLargestNumber(line, 0, line.Length - 2);
            var (digit2, _) = FindLargestNumber(line, index1 + 1, line.Length - 1);

            sum += int.Parse($"{digit1}{digit2}");
        }

        // go from left to second to last index. record largest number seen and index
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
