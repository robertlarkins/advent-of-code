using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day18RamRun;

public class Year2024Day18Part02Solver
{
    private Grid<bool> memorySpace;
    private List<GridPoint> corruptedMemory = [];
    private int rows;
    private int columns;

    public Year2024Day18Part02Solver(
        IEnumerable<string> input,
        int rows,
        int columns)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
        this.rows = rows;
        this.columns = columns;
    }

    public GridPoint Solve()
    {
        var startPoint = 0;
        var endPoint = corruptedMemory.Count - 1;
        var highestTraversable = endPoint;
        var lowestUntraversable = 0;

        while (startPoint <= endPoint)
        {
            var indexToAddTo = (startPoint + endPoint) / 2;

            ResetMemory(indexToAddTo);

            var isTraversable = IsMazeTraversable();

            if (isTraversable)
            {
                startPoint = indexToAddTo + 1;
                highestTraversable = indexToAddTo;
            }
            else
            {
                lowestUntraversable = indexToAddTo;
                endPoint = indexToAddTo - 1;
            }
        }

        return corruptedMemory[lowestUntraversable];
    }

    private void ResetMemory(int indexToAddTo)
    {
        memorySpace = new Grid<bool>(rows, columns);

        for (var i = 0; i <= indexToAddTo; i++)
        {
            memorySpace.SetCell(corruptedMemory[i], true);
        }
    }

    private bool IsMazeTraversable()
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
                return true;
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

        return false;
    }

    private void ParseInput(List<string> input)
    {
        foreach (var line in input)
        {
            var coords = line.Split(',').Select(int.Parse).ToArray();
            var row = coords[1];
            var col = coords[0];

            corruptedMemory.Add(new GridPoint(row, col));
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
