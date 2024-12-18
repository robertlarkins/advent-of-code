namespace Larkins.AdventOfCode.Models;

public class Grid<T>
{
    private readonly T[,] values;

    public Grid(int height, int width)
    {
        Height = height;
        Width = width;

        values = new T[Height, Width];
    }

    public int Height { get; }

    public int Width { get; }

    public bool IsPointInsideGrid(GridPoint point)
    {
        var isOnValidRow = point.Row >= 0 && point.Row < Height;
        var isOnValidCol = point.Col >= 0 && point.Col < Width;

        return isOnValidRow && isOnValidCol;
    }

    public List<GridPoint> GetHorizontalAndVerticalNeighbours(GridPoint point)
    {
        return new List<GridPoint>
        {
            point with { Row = point.Row - 1 }, // up
            point with { Row = point.Row + 1 }, // down
            point with { Col = point.Col - 1 }, // left
            point with { Col = point.Col + 1 } // right
        }.Where(IsPointInsideGrid).ToList();
    }

    public T GetCell(GridPoint point) => values[point.Row, point.Col];

    public void SetCell(GridPoint point, T value) =>
        values[point.Row, point.Col] = value;
}
