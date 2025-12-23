using Larkins.AdventOfCode.AdventOfCode2025.Day07Laboratories;
using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025;

public class TestToRunInput2025(ITestOutputHelper outputHelper)
{
    [Fact]
    public void Run_input_file()
    {
        var inputFilePath =
            @"C:\dev\robertlarkins\advent-of-code\src\Larkins.AdventOfCode\AdventOfCode2025\PuzzleInput\";
        var inputFileName = "day07_input.txt";
        var reader = new TextFileReader();
        var input = reader.ReadAllTextInFile(inputFilePath + inputFileName);

        var solver = new Year2025Day07Part02Solver(input);
        var solvedValue = solver.Solve();

        outputHelper.WriteLine($"Solved value = '{solvedValue}'");
    }
}
