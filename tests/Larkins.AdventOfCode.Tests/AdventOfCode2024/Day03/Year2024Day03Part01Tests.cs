using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2024.Day03;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day03;

public class Year2024Day03Part01Tests
{
    [Fact]
    public void Day03Part01()
    {
        var input = """
            xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
            """;
        
        var solver = new Year2024Day03Part01Solver();
        var result = solver.Solve(input);
        result.Should().Be(161);
    }
}