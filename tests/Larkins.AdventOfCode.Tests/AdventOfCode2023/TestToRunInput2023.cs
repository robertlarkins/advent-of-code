using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2023.Day01;
using Larkins.AdventOfCode.Utilities;
using Xunit.Abstractions;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2023;

public class TestToRunInput2023(ITestOutputHelper outputHelper)
{
    // [Fact(Skip = "Used for running against an input file")]
    [Fact]
    public void Run_input_file()
    {
        var inputFilePath = @"";
        var reader = new TextFileReader();
        var lines = reader.ReadTextToSeparateLines(inputFilePath);
        var solver = new Year2023Day01Part02Solver();
        var solvedValue = solver.Solve(lines);
        
        outputHelper.WriteLine($"Calculated value =  '{solvedValue}'");

        // solvedValue.Should().NotBe();
    }
}