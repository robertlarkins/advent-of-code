using Larkins.AdventOfCode.AdventOfCode2024.Day15WarehouseWoes;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day15WarehouseWoes;

public class Year2024Day15Part02Tests
{
    [Fact]
    public void Day15Part02_example1()
    {
        var input = """
                    ##########
                    #..O..O.O#
                    #......O.#
                    #.OO..O.O#
                    #..O@..O.#
                    #O#..O...#
                    #O..O..O.#
                    #.OO.O.OO#
                    #....O...#
                    ##########

                    <vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
                    vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
                    ><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
                    <<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
                    ^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
                    ^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
                    >^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
                    <><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
                    ^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
                    v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day15Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(9021);
    }

    [Fact]
    public void Day15Part02_example2()
    {
        var input = """
                    #######
                    #...#.#
                    #.....#
                    #..OO@#
                    #..O..#
                    #.....#
                    #######

                    <vv<<^^<<^^
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day15Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(618);
    }
}
