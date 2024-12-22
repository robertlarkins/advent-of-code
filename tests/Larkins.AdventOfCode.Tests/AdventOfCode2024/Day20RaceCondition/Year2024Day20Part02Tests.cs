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

    [Theory]
    [InlineData(2, 2, 3)]
    [InlineData(2, 3, 7)]
    [InlineData(2, 4, 9)]
    [InlineData(3, 2, 2)]
    [InlineData(3, 3, 4)]
    [InlineData(3, 4, 4)]
    [InlineData(4, 3, 4)]
    [InlineData(4, 4, 4)]
    [InlineData(5, 2, 1)]
    [InlineData(5, 3, 1)]
    [InlineData(6, 2, 1)]
    public void Day20Part02_example4(int minimumSavings, int cheatLength, int expected)
    {
        var input = """
                    ######
                    #S...#
                    ####.#
                    #E...#
                    ######
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day20Part02Solver(inputLines, minimumSavings, cheatLength);
        var result = solver.Solve();
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2, 2, 2)]
    [InlineData(2, 3, 5)]
    [InlineData(3, 3, 2)]
    [InlineData(3, 4, 3)]
    [InlineData(4, 3, 2)]
    [InlineData(4, 4, 3)]
    [InlineData(4, 5, 4)]
    public void Day20Part02_example2(int minimumSavings, int cheatLength, int expected)
    {
        var input = """
                    ########
                    #S.....#
                    ######.#
                    ####E..#
                    ########
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day20Part02Solver(inputLines, minimumSavings, cheatLength);
        var result = solver.Solve();
        result.Should().Be(expected);
    }

    [Theory]
    [InlineData(2, 2, 0)]
    [InlineData(2, 3, 2)]
    [InlineData(3, 3, 1)]
    [InlineData(3, 4, 2)]
    [InlineData(4, 3, 1)]
    [InlineData(4, 4, 2)]
    [InlineData(4, 5, 3)]
    public void Day20Part02_example3(int minimumSavings, int cheatLength, int expected)
    {
        var input = """
                    ########
                    #S.....#
                    ######.#
                    ######.#
                    ####E..#
                    ########
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day20Part02Solver(inputLines, minimumSavings, cheatLength);
        var result = solver.Solve();
        result.Should().Be(expected);
    }
}
