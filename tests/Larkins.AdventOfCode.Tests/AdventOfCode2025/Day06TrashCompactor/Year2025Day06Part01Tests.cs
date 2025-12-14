using Larkins.AdventOfCode.AdventOfCode2025.Day06TrashCompactor;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day06TrashCompactor;

public class Year2025Day06Part01Tests
{
    [Fact]
    public void Math_homework_solution()
    {
        var input = """
            123 328  51 64
             45 64  387 23
              6 98  215 314
            *   +   *   +
            """;

        var sut = new Year2025Day06Part01Solver(input);

        var result = sut.Solve();

        result.Should().Be(4277556);
    }
}
