using Larkins.AdventOfCode.AdventOfCode2024.Day21KeypadConundrum;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day21KeypadConundrum;

public class Year2024Day21Part01Tests
{
    [Fact]
    public void Day21Part01_example1()
    {
        var input = """
                    029A
                    980A
                    179A
                    456A
                    379A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part01Solver(inputLines); // minimum savings
        var result = solver.Solve();
        result.Should().Be(126_384);
    }

    [Theory]
    [InlineData("029A", 1_972)]
    [InlineData("980A", 58_800)]
    [InlineData("179A", 12_172)]
    [InlineData("456A", 29_184)]
    [InlineData("379A", 24_256)]
    public void Individual_codes(string input, int expectedComplexity)
    {
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }
}
