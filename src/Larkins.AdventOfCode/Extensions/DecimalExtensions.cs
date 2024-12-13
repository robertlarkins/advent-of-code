namespace Larkins.AdventOfCode.Extensions;

public static class DecimalExtensions
{
    public static bool IsEqualTo(
        this decimal value,
        decimal other,
        decimal tolerance = 0) =>
        Math.Abs(value - other) <= tolerance;

    /// <summary>
    /// Check if double value has a fractional part greater than some tolerance from a whole number.
    /// </summary>
    /// <param name="value">The double value to check.</param>
    /// <param name="tolerance">
    /// The allowable fractional amount from a whole number to considered that there is no fractional part.
    /// </param>
    /// <returns>
    /// <c>true</c> if fractional part is greater than the tolerance from a whole number;
    /// otherwise, <c>false</c>.
    /// </returns>
    public static bool HasFractionalPart(
        this decimal value,
        decimal tolerance = 0)
    {
        var fractionalPart = value % 1;

        return fractionalPart > tolerance &&
               fractionalPart < 1 - tolerance;
    }

    public static bool TryConvertToLong(
        this decimal value,
        decimal tolerance,
        out long result)
    {
        result = 0;

        if (value.HasFractionalPart(tolerance))
        {
            return false;
        }

        result = (long)Math.Round(value);

        return true;
    }
}
