using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day10HoofIt;

public class Year2024Day10Part02Solver
{
    private readonly List<string> input;
    private int mapHeight;
    private int mapWidth;
    private char[,] map;

    public Year2024Day10Part02Solver(IEnumerable<string> input)
    {
        this.input = input.ToList();
        mapHeight = this.input.Count;
        mapWidth = this.input[0].Length;

        ParseInput();
    }

    public int Solve()
    {
        var trailheads = new Dictionary<GridPoint, int>();
        
        for (var row = 0; row < mapHeight; row++)
        {
            for (var col = 0; col < mapWidth; col++)
            {
                if (map[row, col] != '0')
                {
                    continue;
                }

                Recurse(new GridPoint(row, col), new GridPoint(row, col), '0');
            }
        }

        return trailheads.Sum(kvp => kvp.Value);
        
        void Recurse(GridPoint start, GridPoint point, char expectedValue)
        {
            var pointValue = map[point.Row, point.Col];
            
            if (pointValue == expectedValue && pointValue == '9')
            {
                if (!trailheads.TryAdd(start, 1))
                {
                    trailheads[start]++;
                }

                return;
            }

            if (pointValue != expectedValue)
            {
                return;
            }
            
            var neighbours = new[]
            {
                point with { Row = point.Row - 1 },
                point with { Row = point.Row + 1 },
                point with { Col = point.Col - 1 },
                point with { Col = point.Col + 1 }
            };

            foreach (var neighbour in neighbours)
            {
                if (neighbour.IsWithinGrid(mapHeight, mapWidth))
                {
                    Recurse(start, neighbour, (char)(expectedValue + 1));
                }
            }
        }
    }
    
    private void ParseInput()
    {
        var rows = input.ToList();
        map = new char[mapHeight, mapWidth];

        for (var row = 0; row < mapHeight; row++)
        {
            var rowValues = rows[row];
            
            for (var col = 0; col < mapWidth; col++)
            {
                map[row, col] = rowValues[col];
            }
        }
    }
}