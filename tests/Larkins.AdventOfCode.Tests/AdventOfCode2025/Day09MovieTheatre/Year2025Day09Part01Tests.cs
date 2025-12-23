using Larkins.AdventOfCode.AdventOfCode2025.Day09MovieTheatre;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day09MovieTheatre;

public class Year2025Day09Part01Tests
{
    [Fact]
    public void Largest_rectangular_area()
    {
        var input = """
            7,1
            11,1
            11,7
            9,7
            9,5
            2,5
            2,3
            7,3
            """;

        var sut = new Year2025Day09Part01Solver(input);

        var result = sut.Solve();

        result.Should().Be(50);
    }
}
