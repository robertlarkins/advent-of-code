using Larkins.AdventOfCode.AdventOfCode2024.Day25CodeChronicle;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day25CodeChronicle;

public class Year2024Day25Part01Tests
{
    [Fact]
    public void Day25Part01_example01()
    {
        var input = """
                    #####
                    .####
                    .####
                    .####
                    .#.#.
                    .#...
                    .....

                    #####
                    ##.##
                    .#.##
                    ...##
                    ...#.
                    ...#.
                    .....

                    .....
                    #....
                    #....
                    #...#
                    #.#.#
                    #.###
                    #####

                    .....
                    .....
                    #.#..
                    ###..
                    ###.#
                    ###.#
                    #####

                    .....
                    .....
                    .....
                    #....
                    #.#..
                    #.#.#
                    #####
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day25Part01Solver(inputLines);
        var result = solver.Solve();

        result.Should().Be(3);
    }
}
