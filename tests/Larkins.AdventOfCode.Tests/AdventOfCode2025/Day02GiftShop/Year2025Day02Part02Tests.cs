using Larkins.AdventOfCode.AdventOfCode2025.Day02GiftShop;

namespace Larkins.AdventOfCode.Tests.AdventOfCode2025.Day02GiftShop;

public class Year2025Day02Part02Tests
{
    [Fact]
    public void Invalid_code_has_repeated_pattern()
    {
        var input =
            "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

        var inputLines = input.Split(',');
        var solver = new Year2025Day02Part02Solver(inputLines);

        var result = solver.Solve();

        result.Should().Be(4174379265L);
    }

    [Theory]
    [InlineData(1111L)]
    [InlineData(1010L)]
    [InlineData(121212L)]
    [InlineData(12341234L)]
    public void Has_repeated_pattern(long number)
    {
        var result = Year2025Day02Part02Solver.HasRepeatedPattern(number);
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(0L)]
    [InlineData(1L)]
    [InlineData(10L)]
    [InlineData(100L)]
    [InlineData(101L)]
    [InlineData(1000L)]
    [InlineData(1121L)]
    [InlineData(123412L)]
    [InlineData(12345234L)]
    public void Does_not_have_repeated_pattern(long number)
    {
        var result = Year2025Day02Part02Solver.HasRepeatedPattern(number);
        result.Should().BeFalse();
    }
}
