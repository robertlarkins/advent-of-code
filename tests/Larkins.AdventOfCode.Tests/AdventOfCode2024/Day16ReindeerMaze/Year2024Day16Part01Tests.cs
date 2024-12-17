using Larkins.AdventOfCode.AdventOfCode2024.Day15WarehouseWoes;
using Larkins.AdventOfCode.AdventOfCode2024.Day16ReindeerMaze;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day16ReindeerMaze;

public class Year2024Day16Part01Tests
{
    [Fact]
    public void Day16Part01_example1()
    {
        var input = """
                    ###############
                    #.......#....E#
                    #.#.###.#.###.#
                    #.....#.#...#.#
                    #.###.#####.#.#
                    #.#.#.......#.#
                    #.#.#####.###.#
                    #...........#.#
                    ###.#.#####.#.#
                    #...#.....#.#.#
                    #.#.#.###.#.#.#
                    #.....#...#.#.#
                    #.###.#.#.#.#.#
                    #S..#.....#...#
                    ###############
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day16Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(7036);
    }

    [Fact]
    public void Day16Part01_example2()
    {
        var input = """
                    #################
                    #...#...#...#..E#
                    #.#.#.#.#.#.#.#.#
                    #.#.#.#...#...#.#
                    #.#.#.#.###.#.#.#
                    #...#.#.#.....#.#
                    #.#.#.#.#.#####.#
                    #.#...#.#.#.....#
                    #.#.#####.#.###.#
                    #.#.#.......#...#
                    #.#.###.#####.###
                    #.#.#...#.....#.#
                    #.#.#.#####.###.#
                    #.#.#.........#.#
                    #.#.#.#########.#
                    #S#.............#
                    #################
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day16Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(11048);
    }
}
