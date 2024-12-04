namespace Larkins.AdventOfCode.AdventOfCode2024.Day04;

public class Year2024Day04Part02Solver
{
    public int Solve(IEnumerable<string> input)
    {
        var puzzle = ParseInput(input);
        var xmasCount = 0;

        for (var y = 1; y < puzzle.GetLength(0) - 1; y++)
        {
            for (var x = 1; x < puzzle.GetLength(1) - 1; x++)
            {
                if (puzzle[y, x] != 'A')
                {
                    continue;
                }
                
                // Check if top left is M and bottom right is S
                var temp1 = $"{puzzle[y - 1, x - 1]}{puzzle[y, x]}{puzzle[y + 1, x + 1]}";
                var temp2 = $"{puzzle[y - 1, x + 1]}{puzzle[y, x]}{puzzle[y + 1, x - 1]}";
                if (temp1 == "MAS" || temp1 == "SAM")
                {
                    if (temp2 == "MAS" || temp2 == "SAM")
                    {
                        xmasCount++;
                    }
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
