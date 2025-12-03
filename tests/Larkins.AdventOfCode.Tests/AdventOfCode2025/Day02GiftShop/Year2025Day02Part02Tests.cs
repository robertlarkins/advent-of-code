using Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day02GiftShop;

public class Year2025Day02Part02Tests
{
    [Fact]
    public void Invalid_code_has_repeated_pattern()
    {
        var input =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        var solver = new Year2025Day02Part02Solver(input);

        var result = solver.Solve();

        result.Should().Be(4174379265L);
    }

    [Theory]
    [InlineData("1111")]
    [InlineData("1010")]
    [InlineData("121212")]
    [InlineData("12341234")]
    public void Has_repeated_pattern(string numberString)
    {
        var result = Year2025Day02Part02Solver.HasRepeatedPattern(numberString);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData("0")]
    [InlineData("1")]
    [InlineData("10")]
    [InlineData("100")]
    [InlineData("101")]
    [InlineData("1000")]
    [InlineData("1121")]
    [InlineData("123412")]
    [InlineData("12345234")]
    public void Does_not_have_repeated_pattern(string numberString)
    {
        var result = Year2025Day02Part02Solver.HasRepeatedPattern(numberString);
        result.Should().BeFalse();
    }
}
