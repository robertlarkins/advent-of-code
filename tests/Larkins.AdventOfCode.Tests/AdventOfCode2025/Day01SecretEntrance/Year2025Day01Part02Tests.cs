using Larkins.AdventOfCode.AdventOfCode2025.Day01SecretEntrance;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day01SecretEntrance;

public class Year2025Day01Part02Tests
{
    [Fact]
    public void Secret_code_is_count_of_times_dial_passes_zero()
    {
        var input = """
            L68
            L30
            R48
            L5
            R60
            L55
            L1
            L99
            R14
            L82
            """;

        var inputLines = input.Split(Environment.NewLine);
        var solver = new Year2025Day01Part02Solver(inputLines);

        var result = solver.Solve();

        result.Should().Be(6);
    }

    [Fact]
    public void Secret_code_with_large_rotations()
    {
        var input = """
                    L100
                    R52
                    L2
                    L200
                    """;

        var inputLines = input.Split(Environment.NewLine);
        var solver = new Year2025Day01Part02Solver(inputLines);

        var result = solver.Solve();

        result.Should().Be(5);
    }
}
