using Larkins.AdventOfCode.AdventOfCode2024.Day08ResonantCollinearity;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day08;

public class Year2024Day08Part01Tests
{
    [Fact]
    public void Day08Part01()
    {
        var input = """
                    ............
                    ........0...
                    .....0......
                    .......0....
                    ....0.......
                    ......A.....
                    ............
                    ............
                    ........A...
                    .........A..
                    ............
                    ............
                    """;
        
        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day08Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(14);
    }
}