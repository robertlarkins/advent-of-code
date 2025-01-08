using Larkins.AdventOfCode.AdventOfCode2024.Day24CrossedWires;
using Larkins.AdventOfCode.Utilities;
using Xunit.Abstractions;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day24CrossedWires;

public class Year2024Day24Part02Tests
{
    [Fact]
    public void FindBadSums()
    {
        var inputFilePath = @"path_to_day24_input.txt";
        var reader = new TextFileReader();
        var lines = reader.ReadTextToSeparateLines(inputFilePath);
        var solver = new Year2024Day24Part02Solver(lines);

        solver.ManualGateCheck();
    }
}
