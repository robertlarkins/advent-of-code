using Larkins.AdventOfCode.AdventOfCode2025.Day09MovieTheatre;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day09MovieTheatre;

public class Year2025Day09Part02Tests
{
    [Fact]
    public void Largest_rectangular_area_in_boundary()
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

        var sut = new Year2025Day09Part02Solver(input);

        var result = sut.Solve();

        result.Should().Be(24);
    }

    [Fact]
    public void FormBoundary()
    {
        // Col, Row
        string input = """
            4,2
            4,4
            2,4
            2,2
            """;

        var sut = new Year2025Day09Part02Solver(input);

        sut.Solve();
    }
}
