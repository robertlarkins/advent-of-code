using Larkins.AdventOfCode.AdventOfCode2024.Day11PlutonianPebbles;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day11PlutonianPebbles;

public class Year2024Day11Part02Tests
{
    [Fact]
    public void Day11Part02()
    {
        var input = """
                    125 17
                    """;
        
        var solver = new Year2024Day11Part02Solver(input, 25);
        var result = solver.Solve();
        result.Should().Be(55312);
    }
}