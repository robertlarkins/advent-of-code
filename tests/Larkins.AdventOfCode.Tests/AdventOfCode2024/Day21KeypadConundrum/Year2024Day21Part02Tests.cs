using Larkins.AdventOfCode.AdventOfCode2024.Day21KeypadConundrum;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day21KeypadConundrum;

public class Year2024Day21Part02Tests
{
    [Fact]
    public void Day21Part02_example1()
    {
        var input = """
                    029A
                    980A
                    179A
                    456A
                    379A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part02Solver(inputLines, 3); // minimum savings
        var result = solver.Solve();
        result.Should().Be(126_384);
    }

    [Theory]
    [InlineData("029A", 1_972)]
    [InlineData("980A", 58_800)]
    [InlineData("179A", 12_172)]
    [InlineData("456A", 29_184)]
    [InlineData("379A", 24_256)]
    public void Day21Part02_example2(string input, int expectedComplexity)
    {
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part02Solver(inputLines, 3);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }

    [Theory]
    [InlineData(0, 4 * 179)]
    [InlineData(1, 14 * 179)]
    [InlineData(2, 28 * 179)]
    [InlineData(3, 12_172)]
    public void Example_179A(int neededRobots, int expectedComplexity)
    {
        var input = """
                    179A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part02Solver(inputLines, neededRobots);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }

    [Theory]
    [InlineData(0, 980 * 4)]
    [InlineData(1, 980 * 12)]
    [InlineData(2, 980 * 26)]
    [InlineData(3, 980 * 60)] // 58_800
    public void Example_980A(int neededRobots, int expectedComplexity)
    {
        var input = """
                    980A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part02Solver(inputLines, neededRobots);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }

    [Theory]
    [InlineData(0, 9 * 2)]
    [InlineData(1, 9 * 8)]
    [InlineData(2, 9 * 14)]
    [InlineData(3, 9 * 32)]
    public void Example_9A(int neededRobots, int expectedComplexity)
    {
        var input = """
                    9A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part02Solver(inputLines, neededRobots);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }

    [Theory]
    [InlineData("80A", 3, 4080)]
    [InlineData("98A", 3, 4998)]
    [InlineData("8A", 3, 336)]
    [InlineData("0A", 3, 0)]
    [InlineData("20A", 3, 940)]
    [InlineData("30A", 3, 1290)]
    [InlineData("3A", 3, 84)]
    [InlineData("2A", 3, 76)]
    public void Simple_codes(string input, int neededRobots, int expectedComplexity)
    {
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part02Solver(inputLines, neededRobots);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }
}
