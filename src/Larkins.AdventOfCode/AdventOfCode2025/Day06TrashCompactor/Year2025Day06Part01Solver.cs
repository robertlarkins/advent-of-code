namespace Larkins.AdventOfCode.AdventOfCode2025.Day06TrashCompactor;

public class Year2025Day06Part01Solver
{
    private int[,] values;
    private char[] operators;

    public Year2025Day06Part01Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        long sum = 0;

        for (var i = 0; i < values.GetLength(1); i++)
        {
            var op = operators[i];
            long combine = values[0, i];

            for (var j = 1; j < values.GetLength(0); j++)
            {
                if (op == '*')
                {
                    combine *= values[j, i];
                }
                else
                {
                    combine += values[j, i];
                }
            }

            sum += combine;
        }

        return sum;
    }

    private void ParseInput(string input)
    {
        var rows = input.Split(Environment.NewLine);
        operators = rows[^1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
                            .Select(char.Parse)
                            .ToArray();

        values  = new int[rows.Length - 1, operators.Length];

        for (var i = 0; i < rows.Length - 1; i++)
        {
            var rowValues = rows[i].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

            for (var j = 0; j < rowValues.Length; j++)
            {
                values[i, j] = rowValues[j];
            }
        }
    }
}
