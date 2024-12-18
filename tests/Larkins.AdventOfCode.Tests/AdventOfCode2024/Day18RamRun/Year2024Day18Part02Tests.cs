using Larkins.AdventOfCode.AdventOfCode2024.Day18RamRun;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day18RamRun;

public class Year2024Day18Part02Tests
{
    [Fact]
    public void Day18Part02_example1()
    {
        var input = """
                    5,4
                    4,2
                    4,5
                    3,0
                    2,1
                    6,3
                    2,4
                    1,5
                    0,6
                    3,3
                    2,6
                    5,1
                    1,2
                    5,5
                    2,5
                    6,5
                    1,4
                    0,4
                    6,4
                    1,1
                    6,1
                    1,0
                    0,5
                    1,6
                    2,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day18Part02Solver(inputLines, 7, 7);
        var result = solver.Solve();
        var resultString = $"{result.Col},{result.Row}";
        resultString.Should().Be("6,1");
    }
}
