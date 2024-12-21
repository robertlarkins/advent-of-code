using Larkins.AdventOfCode.Extensions;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day20RaceCondition;

public class Year2024Day20Part01Solver
{
    private readonly int minimumSavings;
    private readonly int trackLength;
    private Dictionary<GridPoint, int> raceTrack;
    private Dictionary<int, int> raceResult = [];
    private CharGrid charGrid;

    public Year2024Day20Part01Solver(IEnumerable<string> input, int minimumSavings)
    {
        this.minimumSavings = minimumSavings;
        var inputData = input.ToList();
        raceTrack = ParseInput(inputData);
    }

    public int Solve()
    {
        foreach (var (position, steps) in raceTrack)
        {
            var neighbours = Neighbours(position);
            foreach (var neighbour in neighbours)
            {
                var saving = CalculateSaving((position, steps), neighbour);

                raceResult.AddOrIncrement(saving);
            }
        }

        return raceResult.Where(saving => saving.Key >= minimumSavings)
                         .Sum(saving => saving.Value);
    }

    public int CalculateSaving(
        (GridPoint point, int steps) currentPosition,
        (GridPoint point, int steps) neighbourPosition)
    {
        return neighbourPosition.steps - currentPosition.steps - 2;
    }

    private List<(GridPoint point, int steps)> Neighbours(GridPoint point)
    {
        var neighbours = new List<GridPoint>
        {
            point with { Row = point.Row - 2 }, // up
            point with { Row = point.Row + 2 }, // down
            point with { Col = point.Col - 2 }, // left
            point with { Col = point.Col + 2 }, // right
        }.Where(IsNeighbourTrack).ToList();

        return neighbours.Select(np => (np, raceTrack[np])).ToList();

        bool IsNeighbourTrack(GridPoint neighbourPoint)
        {
            return charGrid.IsPointInsideGrid(neighbourPoint) &&
                   raceTrack.ContainsKey(neighbourPoint);
        }
    }

    private Dictionary<GridPoint, int> ParseInput(List<string> input)
    {
        charGrid = new CharGrid(input);

        return TracePath(charGrid);
    }

    private Dictionary<GridPoint, int> TracePath(CharGrid grid)
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

        var dictionary = new Dictionary<GridPoint, int>
        {
            { startPoint, 0 },
        };

        var currentPosition = startPoint;
        var currentStep = 0;

        while (true)
        {
            if (grid.GetCell(currentPosition) == 'E')
            {
                break;
            }

            var neighbours = grid.GetHorizontalAndVerticalNeighbours(currentPosition);
            foreach (var neighbour in neighbours)
            {
                if (grid.GetCell(neighbour) == '#' ||
                    grid.GetCell(neighbour) == 'S' ||
                    dictionary.ContainsKey(neighbour))
                {
                    continue;
                }

                currentPosition = neighbour;
                currentStep++;
                dictionary.Add(currentPosition, currentStep);
            }
        }

        return dictionary;
    }
}
