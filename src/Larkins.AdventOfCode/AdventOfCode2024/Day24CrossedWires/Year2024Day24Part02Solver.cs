using System.Collections;
using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day24CrossedWires;

/// <summary>
/// Go through the gates
/// The wire off of an OR gate is the carry wire
/// Other than the first group, every group should have 5 gates between the carry wires.
/// </summary>
public class Year2024Day24Part02Solver
{
    private readonly Dictionary<string, bool> wireValues = [];
    private readonly Dictionary<string, List<Gate>> inputGateLookup = [];
    private readonly Dictionary<string, List<Gate>> outputGateLookup = [];
    private readonly Queue<string> wiresToCheck = [];
    private readonly HashSet<Gate> seenGates = [];

    public Year2024Day24Part02Solver(IEnumerable<string> input)
    {
        var inputData = input.ToList();
        ParseInput(inputData);
    }

    public void CheckAllCarryWiresAreCorrect()
    {
        // How to check that a carry wire is correct.

        // Get all carry wires.
        var orGates = seenGates.Where(gate => gate.LogicOperator == LogicOperator.Or).ToList();

    }

    /// <summary>
    /// The gates with switched outputs were found by using knowledge of how gates relate to each
    /// other for summing each bit. If a gate didn't have the expected input wires, then this indicates
    /// an issue with one of these input wires, which is the output of the gate before it.
    /// Looking at the 'parent' gate indicates which gate the output wire should have gone to.
    /// These two wires are then the pair that have been switched.
    /// Fortunately, the switched wires were contained within each bit summation (other than on the OR gate
    /// for the carry bit).
    /// </summary>
    public void ManualGateCheck()
    {
        var inputCarryWire = "";

        // Check that all x y have an AND gate
        for (var i = 0; i <= 44; i++)
        {
            var xWire = $"x{i:D2}";
            var yWire = $"y{i:D2}";
            var zWire = $"z{i:D2}";

            var xInputGates = inputGateLookup[xWire];
            var foundAndGate = xInputGates.Single(gate =>
                gate.LogicOperator == LogicOperator.And &&
                gate.GetOtherWireName(xWire) == yWire);
            var foundXorGate = xInputGates.Single(gate =>
                gate.LogicOperator == LogicOperator.Xor &&
                gate.GetOtherWireName(xWire) == yWire);

            if (i == 0)
            {
                inputCarryWire = foundAndGate.OutputWireName;
                continue;
            }

            Gate foundMiddleAndGate;
            Gate foundSumGate;
            Gate foundOrGate;

            if (i == 17)
            {
                // the problem is actually at bit 16, but the or gate's output goes to the wrong place.
                foundMiddleAndGate = inputGateLookup[foundXorGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.And);
                foundSumGate = inputGateLookup[foundXorGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Xor);
                foundOrGate = inputGateLookup[foundAndGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Or &&
                                    gate.HasInputWire(foundMiddleAndGate.OutputWireName));
            }
            else if (i == 21)
            {
                foundMiddleAndGate = inputGateLookup[inputCarryWire]
                    .Single(gate => gate.LogicOperator == LogicOperator.And);
                foundSumGate = inputGateLookup[inputCarryWire]
                    .Single(gate => gate.LogicOperator == LogicOperator.Xor &&
                                    gate.HasInputWire(inputCarryWire));
                foundOrGate = inputGateLookup[foundMiddleAndGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Or);
            }
            else if (i == 31)
            {
                foundMiddleAndGate = inputGateLookup[inputCarryWire]
                    .Single(gate => gate.LogicOperator == LogicOperator.And &&
                                    gate.HasInputWire(foundXorGate.OutputWireName));
                foundSumGate = inputGateLookup[foundXorGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Xor &&
                                    gate.HasInputWire(inputCarryWire));
                foundOrGate = inputGateLookup[foundAndGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Or);
            }
            else if (i == 37)
            {
                foundMiddleAndGate = inputGateLookup[inputCarryWire]
                    .Single(gate => gate.LogicOperator == LogicOperator.And &&
                                    gate.HasInputWire(foundXorGate.OutputWireName));
                foundSumGate = inputGateLookup[foundXorGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Xor &&
                                    gate.HasInputWire(inputCarryWire));
                foundOrGate = inputGateLookup[foundMiddleAndGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Or);
            }
            else
            {
                foundMiddleAndGate = inputGateLookup[inputCarryWire]
                    .Single(gate => gate.LogicOperator == LogicOperator.And &&
                                    gate.HasInputWire(foundXorGate.OutputWireName));
                foundSumGate = inputGateLookup[foundXorGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Xor &&
                                    gate.HasInputWire(inputCarryWire));
                foundOrGate = inputGateLookup[foundAndGate.OutputWireName]
                    .Single(gate => gate.LogicOperator == LogicOperator.Or &&
                                    gate.HasInputWire(foundMiddleAndGate.OutputWireName));
            }

            var outputCarryWire = foundOrGate.OutputWireName;
            inputCarryWire = outputCarryWire;
        }
    }

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

            if (!processWireValues)
            {
                var gateInfo = line.Split(" ");
                var inputA = gateInfo[0];
                var inputB = gateInfo[2];
                var output = gateInfo[4];
                var logicOperator = Enum.Parse<LogicOperator>(gateInfo[1], true);

                var gate = new Gate(
                    inputA,
                    inputB,
                    output,
                    logicOperator);

                if (!inputGateLookup.TryAdd(inputA, [gate]))
                {
                    inputGateLookup[inputA].Add(gate);
                }

                if (!inputGateLookup.TryAdd(inputB, [gate]))
                {
                    inputGateLookup[inputB].Add(gate);
                }

                if (!outputGateLookup.TryAdd(output, [gate]))
                {
                    inputGateLookup[output].Add(gate);
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

        public bool HasInputWire(string wireName)
        {
            return wireName == WireAName || wireName == WireBName;
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
