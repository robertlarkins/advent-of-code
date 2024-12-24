using Larkins.AdventOfCode.AdventOfCode2024.Day22MonkeyMarket;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day22MonkeyMarket;

public class Year2024Day22Part01Tests
{
    [Theory]
    [InlineData(1, 15887950)]
    [InlineData(2, 16495136)]
    [InlineData(3, 527345)]
    [InlineData(4, 704524)]
    [InlineData(5, 1553684)]
    [InlineData(6, 12683156)]
    [InlineData(7, 11100544)]
    [InlineData(8, 12249484)]
    [InlineData(9, 7753432)]
    [InlineData(10, 5908254)]
    public void Day22Part01_example1(int generationIterations, long expected)
    {
        var input = """
                    123
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day22Part01Solver(inputLines, generationIterations);
        var result = solver.Solve();

        result.Should().Be(expected);
    }

    [Fact]
    public void Day22Part01_example2()
    {
        var input = """
                    1
                    10
                    100
                    2024
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day22Part01Solver(inputLines, 2000);
        var result = solver.Solve();

        result.Should().Be(37327623);
    }
}
