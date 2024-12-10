namespace Larkins.AdventOfCode.Models;

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
}