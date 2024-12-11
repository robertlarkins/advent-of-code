using Larkins.AdventOfCode.AdventOfCode2024.Day11PlutonianPebbles;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day11PlutonianPebbles;

public class Year2024Day11Part01Tests
{
    [Fact]
    public void Day11Part01()
    {
        var input = """
                    125 17
                    """;
        
        var solver = new Year2024Day11Part01Solver(input, 25);
        var result = solver.Solve();
        result.Should().Be(55312);
    }
}