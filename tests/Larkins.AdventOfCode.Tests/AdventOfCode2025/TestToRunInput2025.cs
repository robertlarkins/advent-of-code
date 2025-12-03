using Larkins.AdventOfCode.AdventOfCode2025.Day01SecretEntrance;
using Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;
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
        var inputFileName = "day02_input.txt";
        var reader = new TextFileReader();
        var input = reader.ReadTextToSeparateLines(inputFilePath + inputFileName);
        var lines = input[0].Split(',');
        var solver = new Year2025Day02Part01Solver(lines);
        var solvedValue = solver.Solve();

        outputHelper.WriteLine($"Solved value = '{solvedValue}'");
    }
}
