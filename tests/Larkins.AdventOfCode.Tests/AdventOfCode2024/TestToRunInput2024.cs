using Larkins.AdventOfCode.AdventOfCode2024.Day01;
using Larkins.AdventOfCode.Utilities;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024;

public class TestToRunInput2024(ITestOutputHelper outputHelper)
{
    // [Fact(Skip = "Used for running against an input file")]
    [Fact]
    public void Run_input_file()
    {
        var inputFilePath = "";
        var reader = new TextFileReader();
        var lines = reader.ReadTextToSeparateLines(inputFilePath);
        var solver = new Year2024Day01Part02Solver();
        var solvedValue = solver.Solve(lines);

        outputHelper.WriteLine($"Solved value = '{solvedValue}'");
    }
}
