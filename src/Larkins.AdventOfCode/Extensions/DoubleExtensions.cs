namespace Larkins.AdventOfCode.Extensions;

public static class DoubleExtensions
{
    public static bool IsEqualTo(
        this double value,
        double other,
        double tolerance = 0) =>
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
        this double value,
        double tolerance = 0)
    {
        var fractionalPart = value % 1;

        return fractionalPart > tolerance &&
               fractionalPart < 1 - tolerance;
    }

    public static bool TryConvertToInt(
        this double value,
        double tolerance,
        out int result)
    {
        result = 0;

        if (value.HasFractionalPart(tolerance))
        {
            return false;
        }

        result = (int)Math.Round(value);

        return true;
    }

    public static bool TryConvertToLong(
        this double value,
        double tolerance,
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
