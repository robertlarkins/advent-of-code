using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day21KeypadConundrum;

public class Year2024Day21Part02Solver
{
    private readonly int neededRobots;

    /// +---+---+---+
    /// | 7 | 8 | 9 |
    /// +---+---+---+
    /// | 4 | 5 | 6 |
    /// +---+---+---+
    /// | 1 | 2 | 3 |
    /// +---+---+---+
    ///     | 0 | A |
    ///     +---+---+
    private readonly Dictionary<char, GridPoint> numericKeypad = new()
    {
        { 'A', new GridPoint(3, 2) },
        { '0', new GridPoint(3, 1) },
        { '1', new GridPoint(2, 0) },
        { '2', new GridPoint(2, 1) },
        { '3', new GridPoint(2, 2) },
        { '4', new GridPoint(1, 0) },
        { '5', new GridPoint(1, 1) },
        { '6', new GridPoint(1, 2) },
        { '7', new GridPoint(0, 0) },
        { '8', new GridPoint(0, 1) },
        { '9', new GridPoint(0, 2) },
    };

    ///     +---+---+
    ///     | ^ | A |
    /// +---+---+---+
    /// | < | v | > |
    /// +---+---+---+
    private readonly Dictionary<char, GridPoint> directionalKeypad = new()
    {
        { '^', new GridPoint(0, 1) },
        { 'A', new GridPoint(0, 2) },
        { '<', new GridPoint(1, 0) },
        { 'v', new GridPoint(1, 1) },
        { '>', new GridPoint(1, 2) },
    };

    private readonly Dictionary<(char a, char b), List<string>> keypadCache = [];

    private readonly List<string> keySequences;

    public Year2024Day21Part02Solver(IEnumerable<string> input, int neededRobots)
    {
        this.neededRobots = neededRobots;
        keySequences = input.ToList();

        FillNumericKeypadCache();
        FillDirectionalKeypadCache();
    }

    private void FillNumericKeypadCache()
    {
        var keys = numericKeypad.Keys.ToList();

        foreach (var a in keys)
        {
            foreach (var b in keys)
            {
                keypadCache.TryAdd((a, b), CalculateDirectionalToNumericKeyPresses(a, b));
            }
        }
    }

    private void FillDirectionalKeypadCache()
    {
        var keys = directionalKeypad.Keys.ToList();

        foreach (var a in keys)
        {
            foreach (var b in keys)
            {
                keypadCache.TryAdd((a, b), CalculateDirectionalToDirectionalKeyPresses(a, b));
            }
        }
    }

    public long Solve()
    {
        var totalComplexity = 0L;

        foreach (var code in keySequences)
        {
            var numericPartOfCode = int.Parse(code[..^1]);

            var smallestSequenceLength = DirectionKeySequences(code, 0);

            var complexity = smallestSequenceLength * numericPartOfCode;
            totalComplexity += complexity;
        }

        return totalComplexity;
    }

    private Dictionary<((char b1, char b2), int depth), long> cache = [];

    /// <summary>
    /// What needs to be done?
    /// For a single directional keypad, depth = 1, maxDepth = 1.
    /// go through each sequence pair
    /// if it exists, add the return the distance
    /// otherwise calculate the distance between the two, and store in cache, for the level.
    /// </summary>
    private long DirectionKeySequences(string sequence, int depth)
    {
        if (depth == neededRobots)
        {
            return sequence.Length;
        }

        if (sequence.Length == 1)
        {
            return 1;
        }

        sequence = 'A' + sequence;

        var stepsToFormSequence = 0L;

        for (var i = 0; i < sequence.Length - 1; i++)
        {
            var keyPair = (sequence[i], sequence[i + 1]);

            var subsequences = keypadCache[keyPair];

            if (cache.TryGetValue((keyPair, depth + 1), out var minSteps))
            {
                stepsToFormSequence += minSteps;
                continue;
            }

            minSteps = subsequences.Min(subsequence => DirectionKeySequences(subsequence, depth + 1));
            stepsToFormSequence += minSteps;

            cache.Add((keyPair, depth + 1), minSteps);
        }

        return stepsToFormSequence;
    }

    private List<string> CalculateDirectionalToDirectionalKeyPresses(char buttonA, char buttonB)
    {
        var coordA = directionalKeypad[buttonA];
        var coordB = directionalKeypad[buttonB];
        var invalidPoint = new GridPoint(0, 0);

        return DirectionalKeyPressesBetweenButtons(coordA, coordB, invalidPoint);
    }

    private List<string> CalculateDirectionalToNumericKeyPresses(char buttonA, char buttonB)
    {
        var coordA = numericKeypad[buttonA];
        var coordB = numericKeypad[buttonB];
        var invalidPoint = new GridPoint(3, 0);

        return DirectionalKeyPressesBetweenButtons(coordA, coordB, invalidPoint);
    }

    private List<string> DirectionalKeyPressesBetweenButtons(
        GridPoint buttonA,
        GridPoint buttonB,
        GridPoint invalidPoint)
    {
        var rowDiff = buttonA.Row - buttonB.Row; // positive is up, negative down
        var colDiff = buttonA.Col - buttonB.Col; // positive is left, negative right
        var horizontalPresses = "";
        var verticalPresses = "";

        var verticalDirection = Math.Sign(rowDiff) == 1 ? '^' : 'v';
        var horizontalDirection = Math.Sign(colDiff) == 1 ? '<' : '>';

        for (var i = 0; i < Math.Abs(rowDiff); i++)
        {
            verticalPresses += verticalDirection;
        }

        for (var i = 0; i < Math.Abs(colDiff); i++)
        {
            horizontalPresses += horizontalDirection;
        }

        if (string.IsNullOrEmpty(horizontalPresses) ||
            string.IsNullOrEmpty(verticalPresses))
        {
            return [horizontalPresses + verticalPresses + 'A'];
        }

        List<string> results = [];

        // detect if passes over invalid location.
        var check = buttonA with { Col = buttonA.Col - colDiff };
        if (check != invalidPoint)
        {
            results.Add(horizontalPresses + verticalPresses + 'A');
        }

        check = buttonA with { Row = buttonA.Row - rowDiff };
        if (check != invalidPoint)
        {
            results.Add(verticalPresses + horizontalPresses + 'A');
        }

        return results;
    }
}
