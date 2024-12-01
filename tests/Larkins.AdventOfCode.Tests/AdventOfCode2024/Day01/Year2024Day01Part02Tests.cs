using FluentAssertions;
using Larkins.AdventOfCode.AdventOfCode2024.Day01;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day01;

public class Year2024Day01Part02Tests
{
    [Fact]
    public void Similarity_is_sum_of_each_number_in_left_lists_multiplied_by_its_count_in_right_list()
    {
        var input = """
            3   4
            4   3
            2   5
            1   3
            3   9
            3   3
            """;

        var inputLines = input.Split(Environment.NewLine);
        
        var solver = new Year2024Day01Part02Solver();
        var result = solver.Solve(inputLines);
        result.Should().Be(31);
    }
}
