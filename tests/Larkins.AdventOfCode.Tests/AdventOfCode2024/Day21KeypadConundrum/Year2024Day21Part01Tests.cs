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
        result.Should().Be(126384);
    }

    [Theory]
    [InlineData("029A", 1_972)]
    [InlineData("980A", 58_800)]
    [InlineData("179A", 12_172)]
    [InlineData("456A", 29_184)]
    [InlineData("379A", 24_256)]
    public void Day21Part01_example2(string input, int expectedComplexity)
    {
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(expectedComplexity);
    }

    /// <summary>
    /// ^A^^<<A>>AvvvA
    /// <A>A<AA<vAA^>>AvAA^Av<AAA>^A
    ///
    /// ^A<<^^A>>AvvvA
    /// <A>A<<vAA^>AA>AvAA^Av<AAA^>A
    ///
    ///
    /// <v<A>>^AvA^A<vA<AA>>^AAvA<^A>AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A
    /// <<vA>>^AvA^A<<vAA>A>^AAvA<^A>AAvA^A<vA>^AA<A>A<<vA>A>^AAAvA<^A>A
    /// </summary>
    [Fact]
    public void Day21Part01_example3()
    {
        var input = """
                    379A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(24256);
    }

    /// <summary>
    ///
    /// <v<A>>^A<vA<A>>^AAvAA<^A>A<v<A>>^AAvA^A<vA>^AA<A>A<v<A>A>^AAAvA<^A>A  -28
    ///     <<vAA>A>^AAvA<^A>AvA^A<<vA>>^AAvA^A<vA>^AA<A>A<<vA>A>^AAAvA<^A>A  -28
    /// </summary>
    [Fact]
    public void Day21Part01_179A()
    {
        var input = """
                    179A
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day21Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(12_172);
    }
}
