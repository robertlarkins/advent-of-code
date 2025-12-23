using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day07Laboratories;

public class Year2025Day07Part02Solver
{
    // row, columns splitters are in
    private GridPoint beamStartPoint;
    private readonly SortedList<int, SortedSet<int>> splittersByRow = [];
    private readonly SortedList<int, SortedSet<int>> splittersByCol = [];
    private readonly Dictionary<GridPoint, long> timelinesFromSplitter = [];
    private int maxRow;

    public Year2025Day07Part02Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        // how many timelines are formed from a particular splitter
        // work from the bottom up and find splitters
        // the bottom splitters have two.

        // Go throw each row from bottom to top
        // for each splitter check its two beam. If a beam has no splitters below it. Then add 1
        // if the splitter has splitters below it, add the timelines of the splitter below

        var rows = splittersByRow.Keys.Reverse();

        foreach (var row in rows)
        {
            var splitterColumns = splittersByRow[row];

            foreach (var splitterColumn in splitterColumns)
            {
                var splitterLocation = new GridPoint(row, splitterColumn);
                var leftBeam = splitterLocation with { Col = splitterColumn - 1 };
                var rightBeam = splitterLocation with { Col = splitterColumn + 1 };
                // splittersByCol
                // from this splitter find if there is a splitter below
                var timelines = TimelinesFromSplitterBeam(leftBeam) + TimelinesFromSplitterBeam(rightBeam);

                timelinesFromSplitter.Add(splitterLocation, timelines);
            }
        }

        var firstSplitterRow = splittersByCol[beamStartPoint.Col].First();

        // Get the first splitter
        return timelinesFromSplitter[beamStartPoint with { Row = firstSplitterRow }];

        long TimelinesFromSplitterBeam(GridPoint beam)
        {
            if (!splittersByCol.TryGetValue(beam.Col, out var splittersInColumn))
            {
                return 1;
            }

            var nextSplitters = splittersInColumn.GetViewBetween(beam.Row, maxRow);

            if (nextSplitters.Count == 0)
            {
                return 1;
            }

            var splitterRow = nextSplitters.First();

            var nextSplitter = beam with { Row = splitterRow };

            return timelinesFromSplitter[nextSplitter];
        }
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
                        beamStartPoint = new GridPoint(row, col);
                        break;
                    case '^':
                        if (!splittersByCol.TryAdd(col, [row]))
                        {
                            splittersByCol[col].Add(row);
                        }

                        if (!splittersByRow.TryAdd(row, [col]))
                        {
                            splittersByRow[row].Add(col);
                        }
                        break;
                }
            }
        }
    }
}
