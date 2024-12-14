using Larkins.AdventOfCode.AdventOfCode2024.Day14RestroomRedoubt;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day14RestroomRedoubt;

public class Year2024Day14Part02Tests
{
    [Fact]
    public void Day14Part02()
    {
        var input = """
                    p=4,0 v=0,0
                    p=3,1 v=0,0
                    p=5,1 v=0,0
                    p=2,2 v=0,0
                    p=6,2 v=0,0
                    p=1,3 v=0,0
                    p=7,3 v=0,0
                    p=0,4 v=0,0
                    p=4,4 v=0,0
                    p=8,4 v=0,0
                    p=4,5 v=0,0
                    p=3,6 v=0,0
                    p=4,6 v=0,0
                    p=5,6 v=0,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day14Part02Solver(inputLines, 7, 9, 100);
        var result = solver.Solve();
        // result.Should().Be(12);
    }
}
