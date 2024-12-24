namespace Larkins.AdventOfCode.Extensions;

public static class LongExtensions
{
    public static int NumberOfDigits(this long number) =>
        (int)Math.Floor(Math.Log10(number) + 1);

    public static int GetDigitAtPlace(this long number, int places) =>
        (int)(number % (places * 10));
}
