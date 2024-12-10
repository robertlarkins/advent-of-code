using Larkins.AdventOfCode.AdventOfCode2024.Day10HoofIt;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day10HoofIt;

public class Year2024Day10Part02Tests
{
    [Fact]
    public void Day08Part02()
    {
        var input = """
                    89010123
                    78121874
                    87430965
                    96549874
                    45678903
                    32019012
                    01329801
                    10456732
                    """;
        
        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day10Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(81);
    }
}