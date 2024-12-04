namespace Larkins.AdventOfCode.AdventOfCode2024.Day04;

public class Year2024Day04Part02Solver
{
    public int Solve(IEnumerable<string> input)
    {
        var puzzle = ParseInput(input);
        var xmasCount = 0;
        const string ms = "MS";
        const string sm = "SM";

        for (var y = 1; y < puzzle.GetLength(0) - 1; y++)
        {
            for (var x = 1; x < puzzle.GetLength(1) - 1; x++)
            {
                if (puzzle[y, x] != 'A')
                {
                    continue;
                }
                
                // find diagonal cases of MAS or SAM. Though 'A' is dropped
                // as the current array element is 'A'.
                var diagonal1 = $"{puzzle[y - 1, x - 1]}{puzzle[y + 1, x + 1]}";
                var diagonal2 = $"{puzzle[y - 1, x + 1]}{puzzle[y + 1, x - 1]}";

                if (diagonal1 is ms or sm &&
                    diagonal2 is ms or sm)
                {
                    xmasCount++;
                }
            }   
        }

        return xmasCount;
    }

    private char[,] ParseInput(IEnumerable<string> input)
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
