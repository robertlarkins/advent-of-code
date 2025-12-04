namespace Larkins.AdventOfCode.AdventOfCode2025.Day04PrintingDepartment;

public class Year2025Day04Part01Solver(IEnumerable<string> input)
{
    private readonly List<string> inputLines = input.ToList();

    public int Solve()
    {
        var total = 0;




        return total;
    }

    private char[,] ConvertInput()
    {
        var grid = new char[inputLines.Count, inputLines[0].Length];

        for (var line = 0; line < inputLines.Count; line++)
        {
            grid[line, ..] = inputLines[line];
        }

    }

}
