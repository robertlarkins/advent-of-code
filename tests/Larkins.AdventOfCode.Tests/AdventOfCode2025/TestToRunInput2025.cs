using Larkins.AdventOfCode.AdventOfCode2025.Day03Lobby;
using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025;

public class TestToRunInput2025(ITestOutputHelper outputHelper)
{
    [Fact]
    public void Run_input_file()
    {
        var inputFilePath =
            @"C:\dev\robertlarkins\advent-of-code\src\Larkins.AdventOfCode\AdventOfCode2025\PuzzleInput\";
        var inputFileName = "day03_input.txt";
        var reader = new TextFileReader();
        var input = reader.ReadTextToSeparateLines(inputFilePath + inputFileName);
        var solver = new Year2025Day03Part02Solver(input);
        var solvedValue = solver.Solve();

        if (solvedValue <= 4231)
        {
            outputHelper.WriteLine("Not Found: " + solvedValue);
        }

        outputHelper.WriteLine($"Solved value = '{solvedValue}'");
        // 168575096286051
    }
}
