using Larkins.AdventOfCode.AdventOfCode2024.Day09DiskFragmenter;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day09DiskFragmenter;

public class Year2024Day09Part02Tests
{
    /// <summary>
    /// 00...111...2...333.44.5555.6666.777.888899
    /// 0099.111...2...333.44.5555.6666.777.8888..
    /// 0099.1117772...333.44.5555.6666.....8888..
    /// 0099.111777244.333....5555.6666.....8888..
    /// 00992111777.44.333....5555.6666.....8888..
    /// </summary>
    [Fact]
    public void Day09Part01_example1()
    {
        var input = "2333133121414131402";

        var solver = new Year2024Day09Part02Solver(input);
        var result = solver.Solve();
        result.Should().Be(2858);
    }

    /// <summary>
    /// 00...111
    /// 00111...
    /// </summary>
    [Fact]
    public void Day09Part01_example2()
    {
        var input = "233";

        var solver = new Year2024Day09Part02Solver(input);
        var result = solver.Solve();
        result.Should().Be(9);
    }
}
