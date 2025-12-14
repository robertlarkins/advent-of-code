namespace Larkins.AdventOfCode.AdventOfCode2025.Day06TrashCompactor;

public class Year2025Day06Part02Solver
{
    private List<List<long>> numberGroups = [];
    private char[] operators;

    public Year2025Day06Part02Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        long sum = 0;

        for (var i = 0; i < operators.Length; i++)
        {
            var op = operators[i];
            var numberGroup = numberGroups[i];

            if (op == '*')
            {
                var product = numberGroup.Aggregate(
                    1L,
                    (productSoFar, nextNumber) => productSoFar * nextNumber);

                sum += product;
            }
            else
            {
                sum += numberGroup.Sum();
            }
        }

        return sum;
    }

    private void ParseInput(string input)
    {
        var rows = input.Split(Environment.NewLine);
        operators = rows[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(char.Parse)
                            .ToArray();

        var rowCount = rows.Length - 1;

        var maxRowLength = rows.SkipLast(1).Max(r => r.Length);

        List<long> numberGroup = [];

        for (var x = 0; x < maxRowLength; x++)
        {
            var number = "";

            for (var y = 0; y < rowCount; y++)
            {
                if (x >= rows[y].Length ||
                    char.IsWhiteSpace(rows[y][x]))
                {
                    continue;
                }

                number += rows[y][x];
            }

            if (string.IsNullOrEmpty(number))
            {
                numberGroups.Add(numberGroup);
                numberGroup = [];
            }
            else
            {
                numberGroup.Add(int.Parse(number));
            }
        }

        numberGroups.Add(numberGroup);
    }
}
