using Larkins.AdventOfCode.AdventOfCode2024.Day05;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day05;

public class Year2024Day05Part01Tests
{
    [Fact]
    public void Day05Part01()
    {
        var input = """
                    47|53
                    97|13
                    97|61
                    97|47
                    75|29
                    61|13
                    75|53
                    29|13
                    97|29
                    53|29
                    61|53
                    97|53
                    61|29
                    47|13
                    75|47
                    97|75
                    47|61
                    75|61
                    47|29
                    75|13
                    53|13

                    75,47,61,53,29
                    97,61,53,29,13
                    75,29,13
                    75,97,47,61,53
                    61,13,29
                    97,13,75,29,47
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day05Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(143);
    }
}
