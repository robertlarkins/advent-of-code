using Larkins.AdventOfCode.AdventOfCode2025.Day03Lobby;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day03Lobby;

public class Year2025Day03Part01Tests
{
    [Fact]
    public void Calculate_Joltage()
    {
        var input = """
            987654321111111
            811111111111119
            234234234234278
            818181911112111
            """;

        var inputLines = input.Split(Environment.NewLine);
        var solver = new Year2025Day03Part01Solver(inputLines);

        var result = solver.Solve();

        result.Should().Be(357);
    }
}
