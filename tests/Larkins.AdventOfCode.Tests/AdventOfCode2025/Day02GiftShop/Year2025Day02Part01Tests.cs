using Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day02GiftShop;

public class Year2025Day02Part01Tests
{
    [Fact]
    public void Invalid_code_has_double_pattern()
    {
        var input =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        var inputLines = input.Split(',');
        var solver = new Year2025Day02Part01Solver(inputLines);

        var result = solver.Solve();

        result.Should().Be(1227775554L);
    }
}
