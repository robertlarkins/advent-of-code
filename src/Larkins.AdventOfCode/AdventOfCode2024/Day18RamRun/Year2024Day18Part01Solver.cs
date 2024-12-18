using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day18RamRun;

public class Year2024Day18Part01Solver
{
    private Grid<bool> memorySpace;
    private int rows;
    private int columns;

    public Year2024Day18Part01Solver(
        IEnumerable<string> input,
        int rows,
        int columns,
        int byteCount)
    {
        memorySpace = new Grid<bool>(rows, columns);
        var inputData = input.ToList();
        ParseInput(inputData, byteCount);
        this.rows = rows;
        this.columns = columns;
    }

    public int Solve()
    {
        return TraverseMaze();
    }

    private int TraverseMaze()
    {
        var visited = new HashSet<GridPoint>();
        var queue = new PriorityQueue<(GridPoint gridPoint, int steps), int>();
        queue.Enqueue((new GridPoint(0, 0), 0), 0);

        while (queue.Count > 0)
        {
            var currentPlace = queue.Dequeue();

            if (visited.Contains(currentPlace.gridPoint))
            {
                continue;
            }

            if (currentPlace.gridPoint.Row == rows - 1 && currentPlace.gridPoint.Col == columns - 1)
            {
                return currentPlace.steps;
            }

            visited.Add(currentPlace.gridPoint);

            var neighbours = memorySpace.GetHorizontalAndVerticalNeighbours(currentPlace.gridPoint);

            foreach (var neighbour in neighbours)
            {
                if (visited.Contains(neighbour))
                {
                    continue;
                }

                if (memorySpace.GetCell(neighbour))
                {
                    continue;
                }

                queue.Enqueue((neighbour, currentPlace.steps + 1), currentPlace.steps + 1);
            }
        }

        return -1;
    }

    private int TurnsBetweenDirections(Direction startDirection, Direction desiredDirection)
    {
        var diff = Math.Abs(desiredDirection - startDirection);

        return diff switch
        {
            0 => 0,
            2 => 2,
            _ => 1
        };
    }

    private void ParseInput(List<string> input, int byteCount)
    {
        for (var i = 0; i < byteCount; i++)
        {
            var coords = input[i].Split(',').Select(int.Parse).ToArray();
            var row = coords[1];
            var col = coords[0];

            memorySpace.SetCell(new GridPoint(row, col), true);
        }
    }

    public void PrintGrid()
    {
        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < columns; col++)
            {
                var gridPoint = new GridPoint(row, col);
                Console.Write(memorySpace.GetCell(gridPoint) ? 'X' : '.' );
            }
            Console.WriteLine();
        }
    }
}
