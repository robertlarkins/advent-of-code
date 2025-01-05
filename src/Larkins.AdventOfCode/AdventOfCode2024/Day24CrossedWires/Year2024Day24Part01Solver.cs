using System.Collections;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day24CrossedWires;

public class Year2024Day24Part01Solver
{
    private readonly Dictionary<string, bool> wireValues = [];
    private readonly Dictionary<string, List<Gate>> gateLookup = [];
    private readonly Queue<string> wiresToCheck = [];
    private readonly HashSet<Gate> seenGates = [];

    public Year2024Day24Part01Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public long Solve()
    {
        while (wiresToCheck.Count > 0)
        {
            var wireName = wiresToCheck.Dequeue();

            // Check if the wire is an input to a gate
            if (!gateLookup.TryGetValue(wireName, out var listOfGates))
            {
                continue;
            }

            // go through each gate that this wire is an input to
            foreach (var gate in listOfGates)
            {
                // The gate might have already been processed
                // this occurs if both input wires are initially provided
                // in the inputData.
                if (seenGates.Contains(gate))
                {
                    continue;
                }

                var otherWireName = gate.GetOtherWireName(wireName);

                if (!wireValues.TryGetValue(otherWireName, out var valueB))
                {
                    continue;
                }

                var valueA = wireValues[wireName];

                var gateOutput = gate.CalculateOutputValue(valueA, valueB);
                wireValues.Add(gate.OutputWireName, gateOutput);
                wiresToCheck.Enqueue(gate.OutputWireName);
                seenGates.Add(gate);
            }
        }

        var boolArray = wireValues.Where(kvp => kvp.Key.StartsWith("z"))
                             .OrderBy(x => x.Key)
                             .Select(kvp => kvp.Value)
                             .ToArray();
        var bitArray = new BitArray(boolArray);

        var result = GetIntFromBitArray(bitArray);

        return result;

        // Solution provided by CoPilot
        // But similar to this one: https://stackoverflow.com/a/51430897
        static long GetIntFromBitArray(BitArray bitArray)
        {
            // only checking to 63 as the 64th bit is reserved for the sign
            if (bitArray.Length > 63)
            {
                throw new ArgumentException("Can't fit into a long");
            }

            var result = 0L;

            for (var i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    result |= 1L << i;
                }
            }

            return result;
        }
    }

    // Boolean logic gates
    // inputWireA, inputWireB, outputWire
    // cycle through calculating for each wire we have an answer for
    //
    // data structures:
    // dictionary of wires with values.
    // dictionary of wires without values.
    // dictionary of inputA<Dictionary<inputB>>. And Dict inputB<dict<inputA>>
    //   this way we can look up if this wire value creates an input
    // priority queue of inputs, gate, and output. Its priority is determined by

    private void ParseInput(List<string> input)
    {
        var processWireValues = true;

        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                processWireValues = false;
                continue;
            }

            if (processWireValues)
            {
                var wireInfo = line.Split(": ");
                var wireName = wireInfo[0];
                var wireValue = wireInfo[1] == "1";
                wireValues.Add(wireName, wireValue);
                wiresToCheck.Enqueue(wireName);
            }
            else
            {
                var gateInfo = line.Split(" ");
                var inputA = gateInfo[0];
                var inputB = gateInfo[2];
                var output = gateInfo[4];
                var logicOperator = Enum.Parse<LogicOperator>(gateInfo[1], true);

                var gate = new Gate(inputA, inputB, output, logicOperator);

                if (!gateLookup.TryAdd(inputA, [gate]))
                {
                    gateLookup[inputA].Add(gate);
                }

                if (!gateLookup.TryAdd(inputB, [gate]))
                {
                    gateLookup[inputB].Add(gate);
                }
            }
        }
    }

    private enum LogicOperator
    {
        Xor,
        Or,
        And,
    }

    private record Gate(
        string WireAName,
        string WireBName,
        string OutputWireName,
        LogicOperator LogicOperator)
    {
        public bool CalculateOutputValue(bool inputAValue, bool inputBValue)
        {
            return LogicOperator switch
            {
                LogicOperator.Xor => inputAValue ^ inputBValue,
                LogicOperator.Or => inputAValue | inputBValue,
                LogicOperator.And => inputAValue & inputBValue,
                _ => throw new UnreachableException(),
            };
        }

        public string GetOtherWireName(string wireName)
        {
            if (wireName != WireAName && wireName != WireBName)
            {
                throw new ArgumentException($"Wire {wireName} unknown.");
            }

            return WireAName == wireName
                ? WireBName
                : WireAName;
        }
    }
}
