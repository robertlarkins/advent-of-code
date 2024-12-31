using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day21KeypadConundrum;

public class Year2024Day21Part01Solver(IEnumerable<string> input)
{
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

    public int Solve()
    {
        return input.Sum(ProcessCode);
    }

    private int ProcessCode(string code)
    {
        var numericPartOfCode = int.Parse(code[..^1]);
        var sequence1List = GetKeyPressesForSequence(code, CalculateDirectionalToNumericKeyPresses);

        var sequence2List = new List<string>();

        foreach (var sequence1 in sequence1List)
        {
            var sequence2 = GetKeyPressesForSequence(sequence1, CalculateDirectionalToDirectionalKeyPresses);
            sequence2List.AddRange(sequence2);
        }

        var sequence3List = new List<string>();

        foreach (var sequence2 in sequence2List)
        {
            var sequence3 = GetKeyPressesForSequence(sequence2, CalculateDirectionalToDirectionalKeyPresses);
            sequence3List.AddRange(sequence3);
        }

        var smallestSequenceLength = sequence3List.Min(x => x.Length);

        return smallestSequenceLength * numericPartOfCode;
    }

    private List<string> GetKeyPressesForSequence(
        string keySequence,
        Func<char, char, List<string>> baseKeypad)
    {
        char[] buttonLocations = ['A', ..keySequence.ToCharArray()];

        var possibleSequences = new List<string>();

        for (var i = 0; i < buttonLocations.Length - 1; i++)
        {
            var buttonA = buttonLocations[i];
            var buttonB = buttonLocations[i + 1];
            var buttonPresses = baseKeypad(buttonA, buttonB);

            if (possibleSequences.Count == 0)
            {
                possibleSequences = buttonPresses;
                continue;
            }

            var newSequences = new List<string>();

            foreach (var possibleSequence in possibleSequences)
            {
                foreach (var buttonPress in buttonPresses)
                {
                    newSequences.Add(possibleSequence + buttonPress);
                }
            }

            possibleSequences = newSequences;
        }

        return possibleSequences;
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

        if (verticalPresses.Length > 0 && horizontalPresses.Length > 0)
        {
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

        // Either horizontal or vertical will be empty.
        return [horizontalPresses + verticalPresses + 'A'];
    }
}
