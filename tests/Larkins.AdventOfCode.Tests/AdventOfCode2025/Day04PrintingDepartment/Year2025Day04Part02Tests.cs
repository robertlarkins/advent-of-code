using Larkins.AdventOfCode.AdventOfCode2025.Day04PrintingDepartment;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day04PrintingDepartment;

public class Year2025Day04Part02Tests
{
    [Fact]
    public void Paper_becomes_accessible_if_fewer_than_four_rolls_around_it()
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

        var solver = new Year2025Day04Part02Solver(input);

        var result = solver.Solve();

        result.Should().Be(43);
    }
}
