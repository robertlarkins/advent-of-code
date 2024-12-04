namespace Larkins.AdventOfCode.AdventOfCode2024.Day04;

public class Year2024Day04Part01Solver
{
    private const string Mas = "MAS";
    private char[,] puzzle;
    private int height;
    private int width;
    
    public int Solve(IEnumerable<string> input)
    {
        puzzle = ParseInput(input);
        var xmasCount = 0;

        height = puzzle.GetLength(0);
        width = puzzle.GetLength(1);

        for (var row = 0; row < height; row++)
        {
            for (var col = 0; col < width; col++)
            {
                xmasCount += XmasFromIndex(row, col);
            }   
        }

        return xmasCount;
    }

    private int XmasFromIndex(int row, int col)
    {
        if (puzzle[row, col] != 'X')
        {
            return 0;
        }

        var canGoUp = row >= 3;
        var canGoDown = row < height - 3;
        var canGoLeft = col >= 3;
        var canGoRight = col < width - 3;

        var directions = new List<(int row, int col, bool canUse)>
        {
            (-1, 0, canGoUp), // up
            (-1, 1, canGoUp && canGoRight), // up right
            (0, 1, canGoRight), // right 
            (1, 1, canGoDown && canGoRight), // down right
            (1, 0, canGoDown), // down
            (1, -1, canGoDown && canGoLeft), // down left
            (0, -1, canGoLeft), // left
            (-1, -1, canGoUp && canGoLeft), // up left
        };

        return directions.Count(direction => IsMasFoundForDirection(row, col, direction));
    }

    private bool IsMasFoundForDirection(int row, int col, (int row, int col, bool canUse) direction)
    {
        if (!direction.canUse)
        {
            return false;
        }

        for (var i = 1; i <= 3; i++)
        {
            var neighbourChar = puzzle[row + direction.row * i, col + direction.col * i];
            if (neighbourChar != Mas[i - 1])
            {
                return false;
            }
        }

        return true;
    }
    
    private static char[,] ParseInput(IEnumerable<string> input)
    {
        var rows = input.ToList();
        var puzzle = new char[rows.Count, rows[0].Length];

        for (var i = 0; i < rows.Count; i++)
        {
            var array = rows[i].ToArray();

            for (var j = 0; j < array.Length; j++)
            {
                puzzle[i, j] = array[j];
            }
        }

        return puzzle;
    }
}