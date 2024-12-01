namespace Larkins.AdventOfCode.AdventOfCode2024.Day01;

public class Year2024Day01Part02Solver
{
    private readonly List<int> leftList = [];
    private readonly Dictionary<int, int> rightList = [];
    
    public int Solve(IEnumerable<string> input)
    {
        PopulateListsFromInput(input);

        return CalculateSimilarity();
    }

    private void PopulateListsFromInput(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var (leftNumber, rightNumber) = ParseInput(line);
            leftList.Add(leftNumber);

            var hasNumberBeenAddedPreviously = !rightList.TryAdd(rightNumber, 1);
            
            if (hasNumberBeenAddedPreviously)
            {
                rightList[rightNumber]++;
            }
        }
    } 
    
    private int CalculateSimilarity()
    {
        var similarity = 0;
        foreach (var number in leftList)
        {
            var hasNumber = rightList.TryGetValue(number, out var count);

            if (hasNumber)
            {
                similarity += number * count;
            }
        }

        return similarity;
    }
    
    private (int, int) ParseInput(string input)
    {
        var twoNumbers = input.Split("   ");
        return (int.Parse(twoNumbers[0]), int.Parse(twoNumbers[1]));
    }
}