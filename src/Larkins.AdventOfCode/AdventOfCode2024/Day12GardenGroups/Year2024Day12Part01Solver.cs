using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day12GardenGroups;

public class Year2024Day12Part01Solver
{
    private CharGrid charGrid;
    private bool[,] visited;
    
    public Year2024Day12Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        charGrid = new CharGrid(inputData);
        visited = new bool[charGrid.Height, charGrid.Width];
    }

    public int Solve()
    {
        var regions = new List<(int Area, int Perimeter)>();
        
        for (var row = 0; row < charGrid.Height; row++)
        {
            for (var col = 0; col < charGrid.Width; col++)
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
        
        var totalPrice = regions.Sum(region => region.Area * region.Perimeter);
        
        return totalPrice;
    }

    private (int Area, int Perimeter) ProcessRegion(GridPoint point)
    {
        var valueToFind = charGrid.GetCell(point);
        var cellsToCheck = new Queue<GridPoint>([point]);
        var area = 0;
        var perimeter = 0;

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
                cell with { Row = cell.Row - 1 },
                cell with { Row = cell.Row + 1 },
                cell with { Col = cell.Col - 1 },
                cell with { Col = cell.Col + 1 },
            };

            foreach (var neighbour in neighbours)
            {
                if (neighbour.IsWithinGrid(charGrid.Height, charGrid.Width) &&
                    visited[neighbour.Row, neighbour.Col])
                {
                    if (charGrid.GetCell(neighbour) != valueToFind)
                    {
                        perimeter++;
                    }
                    
                    continue;
                }
                
                if (charGrid.IsPointInsideGrid(neighbour) &&
                    charGrid.GetCell(neighbour) == valueToFind)
                {
                    cellsToCheck.Enqueue(neighbour);
                }
                else
                {
                    perimeter++;
                }
            }

            
            // get four neighbours
            // check if the neighbours are valid (within the grid or have the same value)
        }
        
        return (area, perimeter);
    }
}
