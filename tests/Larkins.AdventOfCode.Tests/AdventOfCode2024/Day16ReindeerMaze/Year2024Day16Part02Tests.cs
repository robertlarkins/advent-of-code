using Larkins.AdventOfCode.AdventOfCode2024.Day15WarehouseWoes;
using Larkins.AdventOfCode.AdventOfCode2024.Day16ReindeerMaze;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day16ReindeerMaze;

public class Year2024Day16Part02Tests
{
    [Fact]
    public void Day16Part02_example1()
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

        var solver = new Year2024Day16Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(45);
    }

    [Fact]
    public void Day16Part02_example2()
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

        var solver = new Year2024Day16Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(64);
    }
}
