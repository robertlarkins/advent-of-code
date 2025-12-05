using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2025.Day04PrintingDepartment;

public class Year2025Day04Part02Solver
{
    private readonly char[,] grid;
    private readonly int rows;
    private readonly int cols;

    public Year2025Day04Part02Solver(string input)
    {
        grid = input.ConvertToRectangularArray();
        rows = grid.GetLength(0);
        cols = grid.GetLength(1);
    }

    public int Solve()
    {
        var count = 0;

        while (true)
        {
            var accessiblePaperCount = CountAccessiblePaper();
            count += accessiblePaperCount;

            if (accessiblePaperCount == 0)
            {
                break;
            }
        }

        return count;
    }

    private int CountAccessiblePaper()
    {
        var count = 0;

        for (var row = 0; row < rows; row++)
        {
            for (var col = 0; col < cols; col++)
            {
                if (grid[row, col] == '.')
                {
                    continue;
                }

                if (HasFewerThanFourRollsAroundIt(row, col))
                {
                    grid[row, col] = '.';
                    count++;
                }
            }
        }

        return count;
    }

    private bool HasFewerThanFourRollsAroundIt(int row, int col)
    {
        var rollCount = 0;

        for (var neighbourRow = Math.Max(row - 1, 0); neighbourRow <= Math.Min(row + 1, rows - 1); neighbourRow++)
        {
            for (var neighbourCol = Math.Max(col - 1, 0); neighbourCol <= Math.Min(col + 1, cols - 1); neighbourCol++)
            {
                if (neighbourRow == row && neighbourCol == col)
                {
                    continue;
                }

                if (grid[neighbourRow, neighbourCol] == '@')
                {
                    rollCount++;
                }
            }
        }

        return rollCount < 4;
    }
}
