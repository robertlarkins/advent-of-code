using Larkins.AdventOfCode.AdventOfCode2024.Day07BridgeRepair;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day07BridgeRepair;

public class Year2024Day07Part02Tests
{
    [Fact]
    public void Day07Part02()
    {
        var input = """
                    190: 10 19
                    3267: 81 40 27
                    83: 17 5
                    156: 15 6
                    7290: 6 8 6 15
                    161011: 16 10 13
                    192: 17 8 14
                    21037: 9 7 18 13
                    292: 11 6 16 20
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day07Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(11387);
    }
}
