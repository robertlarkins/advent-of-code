using Larkins.AdventOfCode.AdventOfCode2024.Day06GuardGallivant;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day06;

public class Year2024Day06Part01Tests
{
    [Fact]
    public void Day06Part01()
    {
        var input = """
            ....#.....
            .........#
            ..........
            ..#.......
            .......#..
            ..........
            .#..^.....
            ........#.
            #.........
            ......#...
            """;
        
        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day06Part01Solver();
        var result = solver.Solve(inputLines);
        result.Should().Be(41);
    }
}