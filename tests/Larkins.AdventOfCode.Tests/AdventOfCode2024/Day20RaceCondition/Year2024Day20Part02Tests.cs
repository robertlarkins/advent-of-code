using Larkins.AdventOfCode.AdventOfCode2024.Day19LinenLayout;
using Larkins.AdventOfCode.AdventOfCode2024.Day20RaceCondition;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day20RaceCondition;

public class Year2024Day20Part02Tests
{
    [Fact]
    public void Day20Part02_example1()
    {
        var input = """
                    ###############
                    #...#...#.....#
                    #.#.#.#.#.###.#
                    #S#...#.#.#...#
                    #######.#.#.###
                    #######.#.#...#
                    #######.#.###.#
                    ###..E#...#...#
                    ###.#######.###
                    #...###...#...#
                    #.#####.#.###.#
                    #.#...#.#.#...#
                    #.#.#.#.#.#.###
                    #...#...#...###
                    ###############
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day20Part02Solver(inputLines, 76, 20); // minimum savings
        var result = solver.Solve();
        result.Should().Be(3);
    }
}
