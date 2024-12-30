using System.Text;
using Larkins.AdventOfCode.Models;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day21KeypadConundrum;

public class Year2024Day21Part01Solver
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

    private readonly List<string> keySequences;

    public Year2024Day21Part01Solver(IEnumerable<string> input)
    {
        keySequences = input.ToList();
    }

    public int Solve()
    {

        // use coordinate to represent location of each value
        // calculate the movements needed for each move.

        var totalComplexity = 0;

        foreach (var code in keySequences)
        {
            var complexity = ProcessCode(code);
            totalComplexity += complexity;
        }

        return totalComplexity;
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

        var shortestSequence = sequence3List.OrderBy(sequence => sequence.Length).First();

        var originalCode = CalculateCodeFromSequence(shortestSequence);

        var smallestSequenceLength = sequence3List.Min(x => x.Length);

        var complexity = smallestSequenceLength * numericPartOfCode;
        return complexity;
    }

    private List<string> GetKeyPressesForSequence(
        string keySequence,
        Func<char, char, char, List<string>> baseKeypad)
    {
        var sequenceButtonPresses = new StringBuilder();
        char[] buttonLocations = ['A', ..keySequence.ToCharArray()];
        var armLocation = 'A';

        var possibleSequences = new List<string>();

        for (var i = 0; i < buttonLocations.Length - 1; i++)
        {
            var buttonA = buttonLocations[i];
            var buttonB = buttonLocations[i + 1];
            var buttonPresses = baseKeypad(buttonA, buttonB, armLocation);

            // form every possible sequence

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

    private List<string> CalculateDirectionalToDirectionalKeyPresses(
        char buttonA,
        char buttonB,
        char armLocation)
    {
        var coordA = directionalKeypad[buttonA];
        var coordB = directionalKeypad[buttonB];
        var invalidPoint = new GridPoint(0, 0);

        return DirectionalKeyPressesBetweenButtons(coordA, coordB, invalidPoint, armLocation);
    }

    private List<string> CalculateDirectionalToNumericKeyPresses(
        char buttonA,
        char buttonB,
        char armLocation)
    {
        var coordA = numericKeypad[buttonA];
        var coordB = numericKeypad[buttonB];
        var invalidPoint = new GridPoint(3, 0);

        return DirectionalKeyPressesBetweenButtons(coordA, coordB, invalidPoint, armLocation);
    }

    private List<string> DirectionalKeyPressesBetweenButtons(
        GridPoint buttonA,
        GridPoint buttonB,
        GridPoint invalidPoint,
        char armLocation)
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

        // moving from buttonA to buttonB, which button presses on the
        // direction keypad are closest to where the arm on that directional
        // keypad currently is
        // is armLocation closer to the verticalDirection button or the
        // horizontalDirection button?
        // if verticalDirection, then verticalPresses + horizontalPresses
        // if horizontalDirection, then horizontalPresses + verticalPresses

        // what impact does horizontalPresses + verticalPresses;
        // vs verticalPresses + horizontalPresses;
        // have on the arm movement on the parent directional keypad
        // it depends on where the arm was previously.

        var armPoint = directionalKeypad[armLocation];
        var vertPoint = directionalKeypad[verticalDirection];
        var horiPoint = directionalKeypad[horizontalDirection];

        var vertDist = armPoint.TaxiCabDistanceTo(vertPoint);
        var horiDist = armPoint.TaxiCabDistanceTo(horiPoint);


        // if (vertDist < horiDist)
        // {
        //     return verticalPresses + horizontalPresses;
        // }
        //
        // return horizontalPresses + verticalPresses;

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

        if (verticalPresses.Length > 0)
        {
            return [verticalPresses + 'A'];
        }

        return [horizontalPresses + 'A'];
    }

    private string CalculateCodeFromSequence(string sequence)
    {
        var sequence1 = CalculateCodeOnDirectionalKeypad(sequence);
        var sequence2 = CalculateCodeOnDirectionalKeypad(sequence1);
        var sequence3 = CalculateCodeOnNumericKeypad(sequence2);

        return sequence3;
    }

    private string CalculateCodeOnDirectionalKeypad(string sequence)
    {
        // Calculate movements for next directional keypad
        var position = new GridPoint(0, 2);
        var newSequence = "";

        foreach (var character in sequence)
        {
            if (character == '^')
            {
                position = position with { Row = position.Row - 1 };
            }

            if (character == 'v')
            {
                position = position with { Row = position.Row + 1 };
            }

            if (character == '<')
            {
                position = position with { Col = position.Col - 1 };
            }

            if (character == '>')
            {
                position = position with { Col = position.Col + 1 };
            }

            if (character == 'A')
            {
                var foundDir = directionalKeypad.Single(x => x.Value == position).Key;
                newSequence += foundDir;
            }
        }

        return newSequence;
    }

    private string CalculateCodeOnNumericKeypad(string sequence)
    {
        // Calculate movements for next directional keypad
        var position = new GridPoint(3, 2);
        var newSequence = "";

        foreach (var character in sequence)
        {
            if (character == '^')
            {
                position = position with { Row = position.Row - 1 };
            }

            if (character == 'v')
            {
                position = position with { Row = position.Row + 1 };
            }

            if (character == '<')
            {
                position = position with { Col = position.Col - 1 };
            }

            if (character == '>')
            {
                position = position with { Col = position.Col + 1 };
            }

            if (character == 'A')
            {
                var foundDir = numericKeypad.Single(x => x.Value == position).Key;
                newSequence += foundDir;
            }
        }

        return newSequence;
    }
}
