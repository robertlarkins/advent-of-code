using Larkins.AdventOfCode.AdventOfCode2024.Day04;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day04;

public class Year2024Day04Part01Tests
{
    [Fact]
    public void Day04Part01()
    {
        var input = """
            MMMSXXMASM
            MSAMXMSMSA
            AMXSXMAAMM
            MSAMASMSMX
            XMASAMXAMM
            XXAMMXXAMA
            SMSMSASXSS
            SAXAMASAAA
            MAMMMXMMMM
            MXMXAXMASX
            """;
        
        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day04Part01Solver();
        var result = solver.Solve(inputLines);
        result.Should().Be(18);
    }
}