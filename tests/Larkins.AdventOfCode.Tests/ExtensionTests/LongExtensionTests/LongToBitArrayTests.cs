using System.Collections;
using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.Tests.ExtensionTests.LongExtensionTests;

public class LongToBitArrayTests
{
    public static TheoryData<long, BitArray> LongToBitArrayScenarios()
    {
        var boolArray = new bool[63];
        var data = new TheoryData<long, BitArray>
        {
            { 0L, new BitArray(boolArray) },
        };

        boolArray[3] = true;
        data.Add(8L, new BitArray(boolArray));
        boolArray[3] = false;

        boolArray[2] = true;
        boolArray[5] = true;
        boolArray[6] = true;
        data.Add(100L, new BitArray(boolArray));
        boolArray[2] = false;
        boolArray[5] = false;
        boolArray[6] = false;

        return data;
    }

    [Theory]
    [MemberData(nameof(LongToBitArrayScenarios))]
    public void Long_is_converted_to_bit_array_from_least_to_most_significant_bit(
        long value,
        BitArray expected)
    {
        var result = value.ToBitArray();

        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }

    [Fact]
    public void Negative_case()
    {
        var value = 8L;
        var boolArray = new bool[63];
        boolArray[3] = true;
        var expected = new BitArray(boolArray);

        var result = value.ToBitArray();
        result.Should().BeEquivalentTo(expected, options => options.WithStrictOrdering());
    }
}
