using Larkins.AdventOfCode.AdventOfCode2025.Day01SecretEntrance;
using Larkins.AdventOfCode.Utilities;
using Xunit.Abstractions;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025;

public class TestToRunInput2025(ITestOutputHelper outputHelper)
{
    [Fact]
    public void Run_input_file()
    {
        var inputFilePath =
            @"C:\dev\robertlarkins\advent-of-code\src\Larkins.AdventOfCode\AdventOfCode2025\PuzzleInput\";
        var inputFileName = "day01_input.txt";
        var reader = new TextFileReader();
        var lines = reader.ReadTextToSeparateLines(inputFilePath + inputFileName);
        var solver = new Year2025Day01Part02Solver(lines);
        var solvedValue = solver.Solve();

        if (solvedValue <= 4231)
        {
            outputHelper.WriteLine("Not Found: " + solvedValue);
        }

        outputHelper.WriteLine($"Solved value = '{solvedValue}'");
    }
}
