using Larkins.AdventOfCode.AdventOfCode2025.Day11Reactor;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day11Reactor;

public class Year2025Day11Part02Tests
{
    [Fact]
    public void Number_of_paths_through_devices_that_visit_fft_and_dac()
    {
        var input = """
            svr: aaa bbb
            aaa: fft
            fft: ccc
            bbb: tty
            tty: ccc
            ccc: ddd eee
            ddd: hub
            hub: fff
            eee: dac
            dac: fff
            fff: ggg hhh
            ggg: out
            hhh: out
            """;

        var sut = new Year2025Day11Part02Solver(input);

        var result = sut.Solve();

        result.Should().Be(2);
    }
}
