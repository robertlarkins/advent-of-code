using Larkins.AdventOfCode.AdventOfCode2025.Day05Cafeteria;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day05Cafeteria;

public class Year2025Day05Part02Tests
{
    [Fact]
    public void Count_of_all_items_that_are_deemed_fresh()
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

        result.Should().Be(14);
    }
}
