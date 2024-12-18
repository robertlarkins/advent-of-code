using Larkins.AdventOfCode.AdventOfCode2024.Day17ChronospatialComputer;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day17ChronospatialComputer;

public class Year2024Day17Part01Tests
{
    [Fact]
    public void Day17Part01()
    {
        var input = """
                    Register A: 729
                    Register B: 0
                    Register C: 0

                    Program: 0,1,5,4,3,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be("4,6,3,5,6,3,5,2,1,0");
    }

    /// <summary>
    /// If register C contains 9, the program 2,6 would set register B to 1.
    /// </summary>
    [Fact]
    public void Day17Part01_example1()
    {
        var input = """
                    Register A: 0
                    Register B: 0
                    Register C: 9

                    Program: 2,6
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part01Solver(inputLines);
        var result = solver.Solve();
        var registers = solver.GetRegisterValues();
        registers.registerB.Should().Be(1);
    }

    /// <summary>
    /// If register A contains 10, the program 5,0,5,1,5,4 would output 0,1,2.
    /// </summary>
    [Fact]
    public void Day17Part01_example2()
    {
        var input = """
                    Register A: 10
                    Register B: 0
                    Register C: 0

                    Program: 5,0,5,1,5,4
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part01Solver(inputLines);
        var result = solver.Solve();
        result.Should().Be("0,1,2");
    }

    /// <summary>
    /// If register A contains 2024, the program 0,1,5,4,3,0 would output 4,2,5,6,7,7,7,7,3,1,0 and leave 0 in register A.
    /// </summary>
    [Fact]
    public void Day17Part01_example3()
    {
        var input = """
                    Register A: 2024
                    Register B: 0
                    Register C: 0

                    Program: 0,1,5,4,3,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part01Solver(inputLines);
        var result = solver.Solve();
        var registers = solver.GetRegisterValues();
        result.Should().Be("4,2,5,6,7,7,7,7,3,1,0");
        registers.registerA.Should().Be(0);
    }

    /// <summary>
    /// If register B contains 29, the program 1,7 would set register B to 26.
    /// </summary>
    [Fact]
    public void Day17Part01_example4()
    {
        var input = """
                    Register A: 0
                    Register B: 29
                    Register C: 0

                    Program: 1,7
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part01Solver(inputLines);
        var result = solver.Solve();
        var registers = solver.GetRegisterValues();
        registers.registerB.Should().Be(26);
    }

    /// <summary>
    /// If register B contains 2024 and register C contains 43690, the program 4,0 would set register B to 44354.
    /// </summary>
    [Fact]
    public void Day17Part01_example5()
    {
        var input = """
                    Register A: 0
                    Register B: 2024
                    Register C: 43690

                    Program: 4,0
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day17Part01Solver(inputLines);
        var result = solver.Solve();
        var registers = solver.GetRegisterValues();
        registers.registerB.Should().Be(44354);
    }
}
