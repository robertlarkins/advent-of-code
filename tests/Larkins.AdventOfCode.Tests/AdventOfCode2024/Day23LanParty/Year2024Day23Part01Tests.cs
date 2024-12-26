using Larkins.AdventOfCode.AdventOfCode2024.Day23LanParty;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2024.Day23LanParty;

public class Year2024Day23Part01Tests
{
    [Fact]
    public void Day23Part01()
    {
        var input = """
                    kh-tc
                    qp-kh
                    de-cg
                    ka-co
                    yn-aq
                    qp-ub
                    cg-tb
                    vc-aq
                    tb-ka
                    wh-tc
                    yn-cg
                    kh-ub
                    ta-co
                    de-co
                    tc-td
                    tb-wq
                    wh-td
                    ta-ka
                    td-qp
                    aq-cg
                    wq-ub
                    ub-vc
                    de-ta
                    wq-aq
                    wq-vc
                    wh-yn
                    ka-de
                    kh-ta
                    co-tc
                    wh-qp
                    tb-vc
                    td-yn
                    """;

        var inputLines = input.Split(Environment.NewLine);

        var solver = new Year2024Day23Part01Solver(inputLines);
        var result = solver.Solve();

        result.Should().Be(7);
    }
}
