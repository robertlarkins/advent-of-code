using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2023.Day01;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2023.Day01;

public class Day01Part01Tests
{
    [Theory]
    [InlineData("1abc2", 12)]
    [InlineData("pqr3stu8vwx", 38)]
    [InlineData("a1b2c3d4e5f", 15)]
    [InlineData("treb7uchet", 77)]
    public void Coordinate_is_the_first_and_last_digit_of_each_line_combined(
        string inputLine,
        int expected)
    {
        var solver = new Day01Part01Solver();
        var result = solver.Solve([inputLine]);

        result.Should().Be(expected);
    }
    
    [Fact]
    public void Coordinate_is_the_first_and_last_digit_of_each_line_combined_and_all_lines_summed()
    {
        var input = """
            1abc2
            pqr3stu8vwx
            a1b2c3d4e5f
            treb7uchet
            """;
        var inputLines = input.Split(Environment.NewLine);

        var solver = new Day01Part01Solver();
        var result = solver.Solve(inputLines);

        result.Should().Be(142);
    }
}