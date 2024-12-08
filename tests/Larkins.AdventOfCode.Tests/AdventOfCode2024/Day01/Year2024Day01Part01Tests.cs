using Larkins.AdventOfCode.AdventOfCode2024.Day01;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day01;

public class Year2024Day01Part01Tests
{
    [Fact]
    public void Total_distance_is_the_numbers_lists_ordered_and_their_distances_summed()
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
        
        var solver = new Year2024Day01Part01Solver();
        var result = solver.Solve(inputLines);
        result.Should().Be(11);
    }
    
}