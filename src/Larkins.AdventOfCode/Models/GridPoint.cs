namespace Larkins.AdventOfCode.Models;

/// <summary>
/// A point in a grid.
/// </summary>
/// <param name="Row">The row (y) the point is on.</param>
/// <param name="Col">The col (x) the point is on.</param>
public record GridPoint(
    int Row,
    int Col)
{
    public bool IsWithinGrid(int height, int width)
    {
        var isOnValidRow = Row >= 0 && Row < height;
        var isOnValidCol = Col >= 0 && Col < width;

        return isOnValidRow && isOnValidCol;
    }

    public int TaxiCabDistanceTo(GridPoint point) =>
        Math.Abs(point.Row - Row) + Math.Abs(point.Col - Col);
}
