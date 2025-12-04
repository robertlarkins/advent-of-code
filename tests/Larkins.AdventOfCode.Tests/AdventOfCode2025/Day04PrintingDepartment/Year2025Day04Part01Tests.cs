using Larkins.AdventOfCode.AdventOfCode2025.Day04PrintingDepartment;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day04PrintingDepartment;

public class Year2025Day04Part01Tests
{
    [Fact]
    public void Paper_is_accessible_if_fewer_than_four_rolls_around_ir()
    {
        var input = """
            ..@@.@@@@.
            @@@.@.@.@@
            @@@@@.@.@@
            @.@@@@..@.
            @@.@@@@.@@
            .@@@@@@@.@
            .@.@.@.@@@
            @.@@@.@@@@
            .@@@@@@@@.
            @.@.@@@.@.
            """;

        var inputLines = input.Split(Environment.NewLine);
        var solver = new Year2025Day04Part01Solver(inputLines);

        var result = solver.Solve();

        result.Should().Be(13);
    }
}
