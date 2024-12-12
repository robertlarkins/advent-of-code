namespace Larkins.AdventOfCode.Models;

public class Grid
{
    private readonly char[,] values;
    
    public Grid(List<string> input)
    {
        Height = input.Count;
        Width = input[0].Length;
        
        values = new char[Height, Width];
        
        ParseInput(input);
    }
    
    public int Height { get; }
    
    public int Width { get; }

    public bool IsPointInsideGrid(GridPoint point)
    {
        var isOnValidRow = point.Row >= 0 && point.Row < Height;
        var isOnValidCol = point.Col >= 0 && point.Col < Width;
        
        return isOnValidRow && isOnValidCol;
    }

    public char GetCell(GridPoint point) => values[point.Row, point.Col];
    
    public void SetCell(GridPoint point, char value) =>
        values[point.Row, point.Col] = value;

    private void ParseInput(List<string> input)
    {
        for (var row = 0; row < Height; row++)
        {
            var rowValues = input[row];
        
            for (var col = 0; col < Width; col++)
            {
                values[row, col] = rowValues[col];
            }
        }
    }
}