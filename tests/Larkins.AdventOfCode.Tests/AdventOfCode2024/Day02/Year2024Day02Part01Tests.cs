using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2024.Day02;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day02;

public class Year2024Day02Part01Tests
{
    [Theory]
    [InlineData("7 6 4 2 1")]
    [InlineData("1 3 6 7 9")]
    public void Reports_that_are_safe(string report)
    {
        var solver = new Year2024Day02Part01Solver();
        var result = solver.Solve([report]);
        result.Should().Be(1);        
    }

    [Theory]
    [InlineData("1 2 7 8 9")]
    [InlineData("9 7 6 2 1")]
    [InlineData("1 3 2 4 5")]
    [InlineData("8 6 4 4 1")]
    [InlineData("12 13 27 15 18")]
    [InlineData("1 13 14 15 18")]
    public void Reports_that_are_unsafe(string report)
    {
        var solver = new Year2024Day02Part01Solver();
        var result = solver.Solve([report]);
        result.Should().Be(0);        
    }
    
    [Fact]
    public void Each_report_must_go_up_or_down_and_each_level_must_not_differ_by_more_than_three()
    {
        // each row is a report
        // each value is a level in a report
        var input = """
            7 6 4 2 1
            1 2 7 8 9
            9 7 6 2 1
            1 3 2 4 5
            8 6 4 4 1
            1 3 6 7 9
            """;
        
        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day02Part01Solver();
        var result = solver.Solve(inputLines);
        result.Should().Be(2);
    }
}