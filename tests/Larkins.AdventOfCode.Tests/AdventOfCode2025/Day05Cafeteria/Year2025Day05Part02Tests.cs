using Larkins.AdventOfCode.AdventOfCode2025.Day05Cafeteria;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day05Cafeteria;

public class Year2025Day05Part02Tests
{
    [Fact]
    public void Count_of_all_items_that_are_deemed_fresh()
    {
        var input = """
            3-5
            10-14
            16-20
            12-18

            1
            5
            8
            11
            17
            32
            """;

        var solver = new Year2025Day05Part02Solver(input);

        var result = solver.Solve();

        result.Should().Be(14);
    }

    public static TheoryData<string, SortedList<long, long>, string> ParseInputScenarios() => new()
    {
        {
            """
            1-2
            4-5

            """,
            new SortedList<long, long>
            {
                { 1, 2 },
                { 4, 5 },
            },
            "no overlap between ranges"
        },
        {
            """
            1-2
            3-4

            """,
            new SortedList<long, long>
            {
                { 1, 4 },
            },
            "ranges can be connected"
        },
        {
            """
            1-6
            3-4

            """,
            new SortedList<long, long>
            {
                { 1, 6 },
            },
            "first range contains second range"
        },
        {
            """
            1-2
            2-3

            """,
            new SortedList<long, long>
            {
                { 1, 3 },
            },
            "ranges have overlap"
        },
        {
            """
            1-4
            3-5

            """,
            new SortedList<long, long>
            {
                { 1, 5 },
            },
            "ranges are next to each other"
        },
        {
            """
            1-4
            2-4

            """,
            new SortedList<long, long>
            {
                { 1, 4 },
            },
            "ranges have same end value"
        },
        {
            """
            1-4
            2-3
            4-5

            """,
            new SortedList<long, long>
            {
                { 1, 5 },
            },
            "annoying case"
        },
        {
            """
            1-6
            3-4
            6-7

            """,
            new SortedList<long, long>
            {
                { 1, 7 },
            },
            "annoying case 2"
        },
    };

    [Theory]
    [MemberData(nameof(ParseInputScenarios))]
    public void Parse_input_ranges(
        string input,
        SortedList<long, long> expected,
        string reasonForTest)
    {
        var result = Year2025Day05Part02Solver.ParseInput(input);

        result.Should().BeEquivalentTo(expected, because: reasonForTest);
    }
}
