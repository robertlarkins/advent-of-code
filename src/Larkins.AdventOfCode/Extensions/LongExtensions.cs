using System.Collections;

namespace Larkins.AdventOfCode.Extensions;

public static class LongExtensions
{
    public static int NumberOfDigits(this long number) =>
        (int)Math.Floor(Math.Log10(number) + 1);

    public static int GetDigitAtPlace(this long number, int places) =>
        (int)(number % (places * 10));

    /// <summary>
    /// Convert long to BitArray, ignoring the sign bit.
    /// </summary>
    public static BitArray ToBitArray(this long number)
    {
        const int bitsInLong = 63;
        // These will be stored least significant to most
        var boolArray = new bool[bitsInLong];
        for (var i = 0; i < 63; i++)
        {
            var bitShiftedNumber = number >> i;
            var isBitSet = (bitShiftedNumber & 1) == 1;
            boolArray[i] = isBitSet;
        }

        return new BitArray(boolArray);
    }

}
