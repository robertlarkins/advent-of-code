using Larkins.AdventOfCode.AdventOfCode2024.Day17ChronospatialComputer;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day17ChronospatialComputer;

public class Year2024Day17Part02Tests
{
    [Fact]
    public void Day17Part01()
    {
        var input = """
                    Register A: 2024
                    Register B: 0
                    Register C: 0

                    Program: 0,3,5,4,3,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(117440);
    }

    [Fact]
    public void RunProgram()
    {
        var input = """
                    Register A: 63281501
                    Register B: 0
                    Register C: 0

                    Program: 2,4,1,5,7,5,4,5,0,3,1,6,5,5,3,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part02Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be(109019930331546);
    }
}
