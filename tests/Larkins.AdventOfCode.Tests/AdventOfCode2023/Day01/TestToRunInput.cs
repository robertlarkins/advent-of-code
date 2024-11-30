using Larkins.AdventOfCode.AdventOfCode2023.Day01;
using Larkins.AdventOfCode.Utilities;
using Xunit.Abstractions;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2023.Day01;

public class TestToRunInput(ITestOutputHelper outputHelper)
{
    [Fact(Skip = "Used for running against an input file")]
    public void Run_input_file()
    {
        var inputFilePath = "";
        var reader = new TextFileReader();
        var lines = reader.ReadTextToSeparateLines(inputFilePath);
        var solver = new Day01Part01Solver();
        var sumOfCalibrationValues = solver.Solve(lines);
        
        outputHelper.WriteLine($"Sum of calibration values is: '{sumOfCalibrationValues}'");
    }
}