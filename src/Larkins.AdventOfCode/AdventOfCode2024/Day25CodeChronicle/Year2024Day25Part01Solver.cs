namespace Larkins.AdventOfCode.AdventOfCode2024.Day25CodeChronicle;

public class Year2024Day25Part01Solver
{
    private readonly HashSet<(int a, int b, int c, int d, int e)> keys = [];
    private readonly HashSet<(int a, int b, int c, int d, int e)> locks = [];

    public Year2024Day25Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public long Solve()
    {
        return BruteForce();
    }

    private int BruteForce()
    {
        // go through each key, and see if it fits into a lock
        var pairs = 0;

        foreach (var @lock in locks)
        {
            foreach (var key in keys)
            {
                var isColumnAComplete = @lock.a + key.a <= 5;
                var isColumnBComplete = @lock.b + key.b <= 5;
                var isColumnCComplete = @lock.c + key.c <= 5;
                var isColumnDComplete = @lock.d + key.d <= 5;
                var isColumnEComplete = @lock.e + key.e <= 5;

                if (isColumnAComplete &&
                    isColumnBComplete &&
                    isColumnCComplete &&
                    isColumnDComplete &&
                    isColumnEComplete)
                {
                    pairs++;
                }
            }
        }

        return pairs;
    }

    private void ParseInput(List<string> input)
    {
        var isStartOfSchematic = true;
        var isLock = true;
        var columns = new int[5];

        for (var lineNumber = 0; lineNumber < input.Count; lineNumber++)
        {
            var line = input[lineNumber];

            // store the found schematic and reset. The lineNumber check is for the last schematic
            // which may not have a following empty line.
            if (string.IsNullOrWhiteSpace(line) || lineNumber == input.Count - 1)
            {
                var schematicTuple = (columns[0], columns[1], columns[2], columns[3], columns[4]);

                if (isLock)
                {
                    locks.Add(schematicTuple);
                }
                else
                {
                    keys.Add(schematicTuple);
                }

                isStartOfSchematic = true;
                columns = new int[5];
                continue;
            }

            // ignore the last line in each schematic
            if (string.IsNullOrWhiteSpace(input[lineNumber + 1]))
            {
                continue;
            }

            if (isStartOfSchematic)
            {
                isLock = line == "#####";
                isStartOfSchematic = false;
                continue;
            }

            // count #s in each column
            for (var c = 0; c < columns.Length; c++)
            {
                if (line[c] == '#')
                {
                    columns[c]++;
                }
            }
        }
    }
}
