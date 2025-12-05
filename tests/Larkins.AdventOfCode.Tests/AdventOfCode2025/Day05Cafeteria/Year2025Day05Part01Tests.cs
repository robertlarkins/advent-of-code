using Larkins.AdventOfCode.AdventOfCode2025.Day05Cafeteria;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day05Cafeteria;

public class Year2025Day05Part01Tests
{
    [Fact]
    public void Item_is_fresh_if_in_at_least_one_of_the_ranges()
    {
        var input = """
            3-5
            10-14
            16-20
            12-18

            1
            5
            8
            11
            17
            32
            """;

        var solver = new Year2025Day05Part01Solver(input);

        var result = solver.Solve();

        result.Should().Be(3);
    }
}
