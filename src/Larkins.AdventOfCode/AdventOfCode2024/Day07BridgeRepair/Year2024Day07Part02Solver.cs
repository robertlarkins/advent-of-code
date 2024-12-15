using Larkins.AdventOfCode.Extensions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day07BridgeRepair;

public class Year2024Day07Part02Solver
{
    private readonly List<Equation> equations;

    public Year2024Day07Part02Solver(IEnumerable<string> input)
    {
        equations = ParseInput(input.ToList());
    }

    public long Solve()
    {
        return equations.Where(IsValidEquation).Sum(eq => eq.TestValue);
    }

    private bool IsValidEquation(Equation equation)
    {
        var testValue = equation.TestValue;
        var numbers = equation.Numbers;

        if (numbers.Sum() < testValue)
        {
            return false;
        }

        var set = new HashSet<long>
        {
            numbers[0]
        };

        foreach (var number in numbers.Skip(1))
        {
            var set2 = new HashSet<long>();

            foreach (var valueSoFar in set)
            {
                var newNum1 = valueSoFar * number;
                var newNum2 = valueSoFar + number;
                var newNum3 = (long)(valueSoFar * Math.Pow(10, number.NumberOfDigits()) + number);

                if (newNum1 <= testValue)
                {
                    set2.Add(newNum1);
                }

                if (newNum2 <= testValue)
                {
                    set2.Add(newNum2);
                }

                if (newNum3 <= testValue)
                {
                    set2.Add(newNum3);
                }
            }

            set = set2;
        }

        return set.Count(results => results == testValue) >= 1;
    }

    private List<Equation> ParseInput(List<string> input)
    {
        var equations = new List<Equation>();

        foreach (var line in input)
        {
            var parts = line.Split(": ");
            var testValue = long.Parse(parts[0]);
            var numbers = parts[1].Split(" ").Select(long.Parse).ToList();

            var equation = new Equation(testValue, numbers);
            equations.Add(equation);
        }

        return equations;
    }

    private record Equation(long TestValue, List<long> Numbers);
}
