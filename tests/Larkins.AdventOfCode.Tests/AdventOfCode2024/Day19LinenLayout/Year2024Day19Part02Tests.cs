using Larkins.AdventOfCode.AdventOfCode2024.Day19LinenLayout;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day19LinenLayout;

public class Year2024Day19Part02Tests
{
    [Fact]
    public void Day19Part02_example1()
    {
        var input = """
                    r, wr, b, g, bwu, rb, gb, br

                    brwrr
                    bggr
                    gbbr
                    rrbgbr
                    ubwu
                    bwurrg
                    brgr
                    bbrgwb
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day19Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(16);
    }
}
