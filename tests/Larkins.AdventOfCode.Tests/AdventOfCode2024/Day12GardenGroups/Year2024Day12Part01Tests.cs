using Larkins.AdventOfCode.AdventOfCode2024.Day10HoofIt;
using Larkins.AdventOfCode.AdventOfCode2024.Day12GardenGroups;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day12GardenGroups;

public class Year2024Day12Part01Tests
{
    [Fact]
    public void Day12Part01()
    {
        var input = """
                    RRRRIICCFF
                    RRRRIICCCF
                    VVRRRCCFFF
                    VVRCCCJFFF
                    VVVVCJJCFE
                    VVIVCCJJEE
                    VVIIICJJEE
                    MIIIIIJJEE
                    MIIISIJEEE
                    MMMISSJEEE
                    """;
        
        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day12Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(1930);
    }
}