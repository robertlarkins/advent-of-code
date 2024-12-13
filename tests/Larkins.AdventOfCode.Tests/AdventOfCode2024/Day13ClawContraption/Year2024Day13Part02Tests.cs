using Larkins.AdventOfCode.AdventOfCode2024.Day13ClawContraption;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day13ClawContraption;

public class Year2024Day13Part02Tests
{
    [Fact]
    public void Day13Part02()
    {
        var input = """
                    Button A: X+94, Y+34
                    Button B: X+22, Y+67
                    Prize: X=8400, Y=5400

                    Button A: X+26, Y+66
                    Button B: X+67, Y+21
                    Prize: X=12748, Y=12176

                    Button A: X+17, Y+86
                    Button B: X+84, Y+37
                    Prize: X=7870, Y=6450

                    Button A: X+69, Y+23
                    Button B: X+27, Y+71
                    Prize: X=18641, Y=10279
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day13Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(875318608908);
    }
}
