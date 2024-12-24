using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day22MonkeyMarket;

public class Year2024Day22Part02Solver
{
    private readonly long secretNumberToGenerateTo;
    private readonly List<long> initialSecretNumbers;
    private readonly Dictionary<(int a, int b, int c, int d), int> totalBananas = [];

    public Year2024Day22Part02Solver(IEnumerable<string> input, long secretNumberToGenerateTo)
    {
        this.secretNumberToGenerateTo = secretNumberToGenerateTo;
        var inputData = input.ToList();
        initialSecretNumbers = ParseInput(inputData);
    }

    public long Solve()
    {
        foreach (var initialSecretNumber in initialSecretNumbers)
        {
            BananasForChanges(initialSecretNumber, secretNumberToGenerateTo);
        }

        return totalBananas.Values.Max();
    }

    /*
     * Calculate the result of multiplying the secret number by 64.
     * Then, mix this result into the secret number.
     * Finally, prune the secret number.
     *
     * Calculate the result of dividing the secret number by 32.
     * Round the result down to the nearest integer.
     * Then, mix this result into the secret number.
     * Finally, prune the secret number.
     *
     * Calculate the result of multiplying the secret number by 2048.
     * Then, mix this result into the secret number.
     * Finally, prune the secret number.
     *
     * Each step of the above process involves mixing and pruning:
     */
    private void BananasForChanges(long initialSecretNumber, long generationIterations)
    {
        var buyerBananas = new Dictionary<(int a, int b, int c, int d), int>();

        var secretNumber = initialSecretNumber;
        var changes = new int[generationIterations];

        for (var i = 0; i < generationIterations; i++)
        {
            var nextSecretNumber = GenerateNextSecretNumber(secretNumber);

            var nextSecretNumberDigit = nextSecretNumber.GetDigitAtPlace(1);
            var secretNumberDigit = secretNumber.GetDigitAtPlace(1);

            changes[i] = nextSecretNumberDigit - secretNumberDigit;

            if (i >= 3)
            {
                var key = (changes[i-3], changes[i-2], changes[i-1], changes[i]);

                buyerBananas.TryAdd(key, nextSecretNumberDigit);
            }

            secretNumber = nextSecretNumber;
        }

        foreach (var kvp in buyerBananas)
        {
            if (!totalBananas.TryAdd(kvp.Key, kvp.Value))
            {
                totalBananas[kvp.Key] += kvp.Value;
            }
        }
    }

    private long GenerateNextSecretNumber(long secretNumber)
    {
        var result = secretNumber * 64;
        secretNumber = Mix(result, secretNumber);
        secretNumber = Prune(secretNumber);

        result = secretNumber / 32;
        secretNumber = Mix(result, secretNumber);
        secretNumber = Prune(secretNumber);

        result = secretNumber * 2048;
        secretNumber = Mix(result, secretNumber);
        secretNumber = Prune(secretNumber);

        return secretNumber;
    }

    /// <summary>
    /// To mix a value into the secret number, calculate the bitwise XOR of the given value and the secret number.
    /// Then, the secret number becomes the result of that operation.
    /// (If the secret number is 42 and you were to mix 15 into the secret number, the secret number would become 37.)
    /// </summary>
    private long Mix(long value, long secretNumber)
    {
        return value ^ secretNumber;
    }

    /// <summary>
    /// To prune the secret number, calculate the value of the secret number modulo 16777216.
    /// Then, the secret number becomes the result of that operation.
    /// (If the secret number is 100000000 and you were to prune the secret number, the secret number would become 16113920.)
    /// </summary>
    /// <returns></returns>
    private long Prune(long secretNumber)
    {
        return secretNumber % 16777216;
    }

    private List<long> ParseInput(List<string> input)
    {
        return input.Select(long.Parse).ToList();
    }
}
