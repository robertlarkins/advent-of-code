using Larkins.AdventOfCode.AdventOfCode2024.Day09DiskFragmenter;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day09DiskFragmenter;

public class Year2024Day09Part01Tests
{
    /// <summary>
    /// 00...111...2...333.44.5555.6666.777.888899
    /// 009..111...2...333.44.5555.6666.777.88889.
    /// 0099.111...2...333.44.5555.6666.777.8888..
    /// 00998111...2...333.44.5555.6666.777.888...
    /// 009981118..2...333.44.5555.6666.777.88....
    /// 0099811188.2...333.44.5555.6666.777.8.....
    /// 009981118882...333.44.5555.6666.777.......
    /// 0099811188827..333.44.5555.6666.77........
    /// 00998111888277.333.44.5555.6666.7.........
    /// 009981118882777333.44.5555.6666...........
    /// 009981118882777333644.5555.666............
    /// 00998111888277733364465555.66.............
    /// 0099811188827773336446555566..............
    /// </summary>
    [Fact]
    public void Day09Part01_example1()
    {
        var input = "2333133121414131402";

        var solver = new Year2024Day09Part01Solver(input);
        var result = solver.Solve();
        result.Should().Be(1928);
    }

    /// <summary>
    /// 0.11
    /// 011.
    /// </summary>
    [Fact]
    public void Day09Part01_example3()
    {
        var input = "112";

        var solver = new Year2024Day09Part01Solver(input);
        var result = solver.Solve();
        result.Should().Be(3);
    }

    /// <summary>
    /// 0..111....22222
    /// 02.111....2222.
    /// 022111....222..
    /// 0221112...22...
    /// 02211122..2....
    /// 022111222......
    /// </summary>
    [Fact]
    public void Day09Part01_example2()
    {
        var input = "12345";

        var solver = new Year2024Day09Part01Solver(input);
        var result = solver.Solve();
        result.Should().Be(60);
    }
}
