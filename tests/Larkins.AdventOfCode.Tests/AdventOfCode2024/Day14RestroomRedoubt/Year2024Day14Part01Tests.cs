using Larkins.AdventOfCode.AdventOfCode2024.Day14RestroomRedoubt;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day14RestroomRedoubt;

public class Year2024Day14Part01Tests
{
    [Fact]
    public void Day14Part01()
    {
        var input = """
                    p=0,4 v=3,-3
                    p=6,3 v=-1,-3
                    p=10,3 v=-1,2
                    p=2,0 v=2,-1
                    p=0,0 v=1,3
                    p=3,0 v=-2,-2
                    p=7,6 v=-1,-3
                    p=3,0 v=-1,-2
                    p=9,3 v=2,3
                    p=7,3 v=-1,2
                    p=2,4 v=2,-3
                    p=9,5 v=-3,-3
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day14Part01Solver(inputLines, 7, 11, 100);
        var result = solver.Solve();
        result.Should().Be(12);
    }

    [Fact]
    public void Day14Part01_single()
    {
        var input = """
                    p=3,0 v=-2,-2
                    p=3,0 v=-1,-2
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day14Part01Solver(inputLines, 7, 11, 100);
        var result = solver.Solve();
        result.Should().Be(12);
    }

    //
    [Fact]
    public void Day14Part01_example()
    {
        var input = """
                    p=2,4 v=2,-3
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day14Part01Solver(inputLines, 7, 11, 5);
        var result = solver.Solve();
        result.Should().Be(12);
    }

    [Fact]
    public void Day14Part01_quad()
    {
        var input = """
                    p=0,0 v=1,1
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day14Part01Solver(inputLines, 3, 3, 1);
        var result = solver.Solve();
        result.Should().Be(12);
    }

    [Theory]
    // p=x,y v=x,y
    [InlineData("p=0,0 v=-1,-1", 6, 10)]
    [InlineData("p=0,0 v=1,-1", 6, 1)]
    [InlineData("p=0,0 v=0,-1", 6, 0)]
    [InlineData("p=0,0 v=-1,0", 0, 10)]
    [InlineData("p=0,0 v=1,1", 1, 1)]
    [InlineData("p=0,0 v=0,0", 0, 0)]

    [InlineData("p=10,0 v=-1,-1", 6, 9)]
    [InlineData("p=10,0 v=1,-1", 6, 0)]
    [InlineData("p=10,0 v=0,-1", 6, 10)]
    [InlineData("p=10,0 v=1,0", 0, 0)]
    [InlineData("p=10,0 v=1,1", 1, 0)]
    [InlineData("p=10,0 v=-1,1", 1, 9)]
    [InlineData("p=10,0 v=0,1", 1, 10)]

    [InlineData("p=0,6 v=-1,-1", 5, 10)]
    [InlineData("p=0,6 v=1,1", 0, 1)]
    public void Day14Part01_Debug(string input, int expectedRow, int expectedCol)
    {
        var inputLines = input.Split(Environment.NewLine);
        var seconds = 1;

        var solver = new Year2024Day14Part01Solver(inputLines, 7, 11, seconds);
        var robot = solver.ParseRobotInfo(input);

        var result = solver.CalculateRobotLocation(robot, seconds);

        result.Should().Be((expectedRow, expectedCol));
    }
}
