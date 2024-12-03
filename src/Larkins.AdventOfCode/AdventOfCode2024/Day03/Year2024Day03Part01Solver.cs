using System.Text.RegularExpressions;

namespace Larkins.AdventOfCode.AdventOfCode2024.Day03;

public class Year2024Day03Part01Solver
{
    public int Solve(string input)
    {
        var pattern = @"mul\((\d{1,3}),(\d{1,3})\)";
        var patternMatches = Regex.Matches(input, pattern);

        var total = 0;
        
        foreach (Match match in patternMatches)
        {
            var number1 = int.Parse(match.Groups[1].Value);
            var number2 = int.Parse(match.Groups[2].Value);
            
            total += number1 * number2;
        }
        
        return total;
    }
}