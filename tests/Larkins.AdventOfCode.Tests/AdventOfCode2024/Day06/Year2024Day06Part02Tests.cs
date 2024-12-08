using Larkins.AdventOfCode.AdventOfCode2024.Day06GuardGallivant;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day06;

public class Year2024Day06Part02Tests
{
    [Fact]
    public void Day06Part02()
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
        
        var solver = new Year2024Day06Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(6);
    }
}