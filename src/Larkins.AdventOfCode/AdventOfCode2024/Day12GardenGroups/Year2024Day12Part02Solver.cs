using Larkins.AdventOfCode.Extensions;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day12GardenGroups;

public class Year2024Day12Part02Solver
{
    private Grid grid;
    private bool[,] visited;
    
    public Year2024Day12Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        grid = new Grid(inputData);
        visited = new bool[grid.Height, grid.Width];
    }

    public int Solve()
    {
        var regions = new List<(int Area, int Sides)>();
        
        for (var row = 0; row < grid.Height; row++)
        {
            for (var col = 0; col < grid.Width; col++)
            {
                var gridPoint = new GridPoint(row, col);

                if (visited[row, col])
                {
                    continue;
                }

                var regionInfo = ProcessRegion(gridPoint);
                regions.Add(regionInfo);
            }
        }
        
        var totalPrice = regions.Sum(region => region.Area * region.Sides);
        
        return totalPrice;
    }

    private (int Area, int Sides) ProcessRegion(GridPoint point)
    {
        var valueToFind = grid.GetCell(point);
        var cellsToCheck = new Queue<GridPoint>([point]);
        var area = 0;
        var perimeter = 0;
        var upDirection = new Dictionary<int, SortedSet<int>>();
        var downDirection = new Dictionary<int, SortedSet<int>>();
        var leftDirection = new Dictionary<int, SortedSet<int>>();
        var rightDirection = new Dictionary<int, SortedSet<int>>();

        while (cellsToCheck.Count > 0)
        {
            var cell = cellsToCheck.Dequeue();

            if (visited[cell.Row, cell.Col])
            {
                continue;
            }
            
            area++;
            
            visited[cell.Row, cell.Col] = true;
            
            var neighbours = new[]
            {
                (cell with { Row = cell.Row - 1 }, Direction.Up),
                (cell with { Row = cell.Row + 1 }, Direction.Down),
                (cell with { Col = cell.Col - 1 }, Direction.Left),
                (cell with { Col = cell.Col + 1 }, Direction.Right)
            };

            foreach (var (neighbour, direction) in neighbours)
            {
                if (neighbour.IsWithinGrid(grid.Height, grid.Width) &&
                    visited[neighbour.Row, neighbour.Col])
                {
                    if (grid.GetCell(neighbour) != valueToFind)
                    {
                        perimeter++;
                        TryAddPerimeterCell(cell, direction);
                    }
                    
                    continue;
                }
                
                if (grid.IsPointInsideGrid(neighbour) &&
                    grid.GetCell(neighbour) == valueToFind)
                {
                    cellsToCheck.Enqueue(neighbour);
                }
                else
                {
                    perimeter++;
                    TryAddPerimeterCell(cell, direction);
                }
            }
        }

        var totalSideCount =
            CountSides(upDirection) +
            CountSides(downDirection) +
            CountSides(leftDirection) +
            CountSides(rightDirection);
        
        return (area, totalSideCount);

        void TryAddPerimeterCell(
            GridPoint point,
            Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    upDirection.AddOrInsert(point.Row, point.Col);
                    break;
                case Direction.Down:
                    downDirection.AddOrInsert(point.Row, point.Col);
                    break;
                case Direction.Left:
                    leftDirection.AddOrInsert(point.Col, point.Row);
                    break;
                case Direction.Right:
                    rightDirection.AddOrInsert(point.Col, point.Row);
                    break;
            }
        }
    }

    private int CountSides(Dictionary<int, SortedSet<int>> perimeterCells)
    {
        var edgeCount = 0;
        
        foreach (var group in perimeterCells.Values)
        {
            edgeCount += group.Skip(1)
                .Zip(group, (curr, prev) => curr - prev > 1)
                .Count(x => x) + 1; // Add 1 to include single edge, or the last edge
        }
        
        return edgeCount;
    }
}
