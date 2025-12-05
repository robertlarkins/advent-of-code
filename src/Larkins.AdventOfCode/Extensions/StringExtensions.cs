namespace Larkins.AdventOfCode.Extensions;

public static class StringExtensions
{
    public static char[,] ConvertToRectangularArray(this string input)
    {
        var lines = input.Split(["\r\n", "\r", "\n"], StringSplitOptions.None);
        var rowCount = lines.Length;
        var colCount = lines[0].Length;
        var array = new char[rowCount, colCount];

        for (var row = 0; row < lines.Length; row++)
        {
            if (lines[row].Length != colCount)
            {
                throw new ArgumentException("Input lines are not of equal length.");
            }

            for (var col = 0; col < lines[0].Length; col++)
            {
                array[row, col] = lines[row][col];
            }
        }

        return array;
    }
}
