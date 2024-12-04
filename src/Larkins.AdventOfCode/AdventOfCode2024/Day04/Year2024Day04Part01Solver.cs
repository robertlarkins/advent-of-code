namespace Larkins.AdventOfCode.AdventOfCode2024.Day04;

public class Year2024Day04Part01Solver
{
    public int Solve(IEnumerable<string> input)
    {
        var puzzle = ParseInput(input);
        var xmasCount = 0;

        for (var y = 0; y < puzzle.GetLength(0); y++)
        {
            for (var x = 0; x < puzzle.GetLength(1); x++)
            {
                if (puzzle[y, x] != 'X')
                {
                    continue;
                }
                
                // look in all directions
                // up
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y - 1, x]}{puzzle[y - 2, x]}{puzzle[y - 3, x]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // up right
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y - 1, x+1]}{puzzle[y - 2, x+2]}{puzzle[y - 3, x+3]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // right
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y, x+1]}{puzzle[y, x+2]}{puzzle[y, x+3]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // down right
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y+1, x+1]}{puzzle[y+2, x+2]}{puzzle[y+3, x+3]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // down
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y+1, x+0]}{puzzle[y+2, x+0]}{puzzle[y+3, x+0]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // down left
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y+1, x-1]}{puzzle[y+2, x-2]}{puzzle[y+3, x-3]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // left
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y+0, x-1]}{puzzle[y+0, x-2]}{puzzle[y+0, x-3]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
                // up left
                try
                {
                    var temp = $"{puzzle[y, x]}{puzzle[y-1, x-1]}{puzzle[y-2, x-2]}{puzzle[y-3, x-3]}";
                    if (temp == "XMAS")
                    {
                        xmasCount++;
                    }
                }
                catch
                {
                }
            }   
        }

        return xmasCount;
    }

    private char[,] ParseInput(IEnumerable<string> input)
    {
        var blah = input.ToList();
        var puzzle = new char[blah.Count, blah[0].Length];

        for (var i = 0; i < blah.Count; i++)
        {
            var line = blah[i];
            var array = line.ToArray();

            for (var j = 0; j < array.Length; j++)
            {
                puzzle[i, j] = array[j];
            }
        }

        return puzzle;
    }
}