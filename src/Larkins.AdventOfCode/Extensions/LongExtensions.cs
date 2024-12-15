namespace Larkins.AdventOfCode.Extensions;

public static class LongExtensions
{
    public static int NumberOfDigits(this long number) =>
        (int)Math.Floor(Math.Log10(number) + 1);
}
