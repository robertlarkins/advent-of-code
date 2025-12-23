using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day07Laboratories;

public class Year2025Day07Part01Solver
{
    private readonly Queue<GridPoint> beamStartPoint = [];
    private readonly SortedList<int, SortedSet<int>> splitters = [];
    private readonly HashSet<GridPoint> visitedSplitters = [];
    private int maxRow;

    public Year2025Day07Part01Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        var count = 0;

        while (beamStartPoint.Count > 0)
        {
            var beam = beamStartPoint.Dequeue();

            if (!splitters.TryGetValue(beam.Col, out var splittersInColumn))
            {
                continue;
            }

            var nextSplitters = splittersInColumn.GetViewBetween(beam.Row, maxRow);

            if (nextSplitters.Count == 0)
            {
                continue;
            }

            var splitterRow = nextSplitters.First();

            var splitter = beam with { Row = splitterRow };

            if (!visitedSplitters.Add(splitter))
            {
                continue;
            }

            beamStartPoint.Enqueue(new GridPoint(splitterRow, beam.Col - 1));
            beamStartPoint.Enqueue(new GridPoint(splitterRow, beam.Col + 1));
            count++;
        }

        return count;
    }

    private void ParseInput(string input)
    {
        var rows = input.Split(Environment.NewLine);
        maxRow = rows.Length;

        for (var row = 0; row < rows.Length; row++)
        {
            var inputRow = rows[row];

            for (var col = 0; col < inputRow.Length; col++)
            {
                switch (inputRow[col])
                {
                    case 'S':
                        beamStartPoint.Enqueue(new GridPoint(row, col));
                        break;
                    case '^':
                        if (!splitters.TryAdd(col, [row]))
                        {
                            splitters[col].Add(row);
                        }
                        break;
                }
            }
        }
    }
}
