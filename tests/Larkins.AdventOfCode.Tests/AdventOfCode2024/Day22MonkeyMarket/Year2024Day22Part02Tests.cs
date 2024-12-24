using Larkins.AdventOfCode.AdventOfCode2024.Day22MonkeyMarket;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day22MonkeyMarket;

public class Year2024Day22Part02Tests
{
    [Fact]
    public void Day22Part02_example1()
    {
        var input = """
                    1
                    2
                    3
                    2024
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day22Part02Solver(inputLines, 2000);
        var result = solver.Solve();

        result.Should().Be(23);
    }
}
