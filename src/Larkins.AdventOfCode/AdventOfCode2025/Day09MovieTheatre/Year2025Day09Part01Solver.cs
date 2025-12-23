using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day09MovieTheatre;

public class Year2025Day09Part01Solver
{
    private readonly List<GridPoint> values = [];

    public Year2025Day09Part01Solver(string input)
    {
        ParseInput(input);
    }

    public long Solve()
    {
        var largestArea = 0L;

        for (var gp1 = 0; gp1 < values.Count - 1; gp1++)
        {
            for (var gp2 = 1; gp2 < values.Count; gp2++)
            {
                largestArea = Math.Max(largestArea, CalculateRectangularArea(values[gp1], values[gp2]));
            }
        }

        return largestArea;

        static long CalculateRectangularArea(GridPoint point1, GridPoint point2)
        {
            // the 1L makes these longs, otherwise height and width are int, which multiplied
            // together give an int. This product int is then placed into a long.
            var width = Math.Abs(point1.Col - point2.Col) + 1L;
            var height = Math.Abs(point1.Row - point2.Row) + 1L;

            return height * width;
        }
    }

    private void ParseInput(string input)
    {
        var inputSpan = input.AsSpan();
        var lines = inputSpan.Split(Environment.NewLine);

        foreach (var lineRange in lines)
        {
            var line = inputSpan[lineRange];

            var parts = line.Split(",");

            List<int> coords = [];

            foreach (var partRange in parts)
            {
                coords.Add(int.Parse(line[partRange]));
            }

            values.Add(new GridPoint(coords[1], coords[0]));
        }
    }
}
