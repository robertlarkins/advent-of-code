using Larkins.AdventOfCode.Extensions;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day20RaceCondition;

public class Year2024Day20Part02Solver
{
    private readonly int minimumSavings;
    private readonly int cheatLength;
    private readonly int trackLength;
    private List<GridPoint> raceTrack;
    private Dictionary<int, int> raceResult = [];
    private CharGrid charGrid;

    public Year2024Day20Part02Solver(IEnumerable<string> input, int minimumSavings, int cheatLength)
    {
        this.minimumSavings = minimumSavings;
        this.cheatLength = cheatLength;
        var inputData = input.ToList();
        raceTrack = ParseInput(inputData);
    }

    public int Solve()
    {
        for (var position = 0; position < raceTrack.Count - minimumSavings; position++)
        {
            for (var newPosition = position + minimumSavings;
                 newPosition < raceTrack.Count;
                 newPosition++)
            {
                var saving = CalculateSaving(position, newPosition);

                if (saving >= minimumSavings)
                {
                    raceResult.AddOrIncrement(saving);
                }
            }
        }

        return raceResult.Sum(saving => saving.Value);
    }

    private int CalculateSaving(int currentPosition, int newPosition)
    {
        var currentGridPoint = raceTrack[currentPosition];
        var newGridPoint = raceTrack[newPosition];

        var stepsBetween = currentGridPoint.TaxiCabDistanceTo(newGridPoint);

        if (stepsBetween > cheatLength)
        {
            return 0;
        }

        return newPosition - currentPosition - stepsBetween;
    }

    private List<GridPoint> ParseInput(List<string> input)
    {
        charGrid = new CharGrid(input);

        return TracePath(charGrid);
    }

    private List<GridPoint> TracePath(CharGrid grid)
    {
        var startPoint = new GridPoint(0, 0);

        for (var row = 0; row < grid.Height; row++)
        {
            for (var col = 0; col < grid.Width; col++)
            {
                var place = new GridPoint(row, col);

                if (grid.GetCell(place) == 'S')
                {
                    startPoint = place;
                }
            }
        }

        var trackSteps = new List<GridPoint>
        {
            startPoint,
        };

        while (true)
        {
            var neighbours = grid.GetHorizontalAndVerticalNeighbours(trackSteps[^1]);

            var newPosition = neighbours.Single(neighbour =>
                grid.GetCell(neighbour) != '#' &&
                grid.GetCell(neighbour) != 'S' &&
                (trackSteps.Count == 1 || neighbour != trackSteps[^2]));

            trackSteps.Add(newPosition);

            if (grid.GetCell(newPosition) == 'E')
            {
                break;
            }
        }

        return trackSteps;
    }
}
