using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2024.Day03;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day03;

public class Year2024Day03Part02Tests
{
    [Fact]
    public void Day03Part02()
    {
        var input = """
            xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
            """;

        var solver = new Year2024Day03Part02Solver();
        var result = solver.Solve(input);
        result.Should().Be(48);
    }
    
    [Fact]
    public void Day03Part02_extended_input()
    {
        var input = """
            xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+
            mul(32,64](mul(11,8)undo()?
            mul(8,5))+don't()_mul(5,5)
            """;
        
        var solver = new Year2024Day03Part02Solver();
        var result = solver.Solve(input);
        result.Should().Be(48);
    }
}