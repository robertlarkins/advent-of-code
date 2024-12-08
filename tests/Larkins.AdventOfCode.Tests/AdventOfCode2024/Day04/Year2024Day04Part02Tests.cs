using Larkins.AdventOfCode.AdventOfCode2024.Day04;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day04;

public class Year2024Day04Part02Tests
{
    [Fact]
    public void Day04Part02()
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
        
        var solver = new Year2024Day04Part02Solver();
        var result = solver.Solve(inputLines);
        result.Should().Be(9);
    }
}