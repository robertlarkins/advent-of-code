using Larkins.AdventOfCode.AdventOfCode2025.Day11Reactor;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day11Reactor;

public class Year2025Day11Part01Tests
{
    [Fact]
    public void Number_of_paths_through_devices()
    {
        var input = """
            aaa: you hhh
            you: bbb ccc
            bbb: ddd eee
            ccc: ddd eee fff
            ddd: ggg
            eee: out
            fff: out
            ggg: out
            hhh: ccc fff iii
            iii: out
            """;

        var sut = new Year2025Day11Part01Solver(input);

        var result = sut.Solve();

        result.Should().Be(5);
    }
}
