namespace Larkins.AdventOfCode.AdventOfCode2024.Day01;

public class Year2024Day01Part01Solver
{
    private readonly List<int> leftList = [];
    private readonly List<int> rightList = [];
    
    public int Solve(IEnumerable<string> input)
    {
        PopulateListsFromInput(input);

        return CalculateTotalDistance();
    }

    private void PopulateListsFromInput(IEnumerable<string> input)
    {
        foreach (var line in input)
        {
            var (leftNumber, rightNumber) = ParseInput(line);
            leftList.Add(leftNumber);
            rightList.Add(rightNumber);
        }
        
        leftList.Sort();
        rightList.Sort();
    }

    private int CalculateTotalDistance()
    {
        var totalDistance = 0;
        
        for (var i = 0; i < leftList.Count; i++)
        {
            var leftNumber = leftList[i];
            var rightNumber = rightList[i];
            
            totalDistance += Math.Abs(leftNumber - rightNumber);
        }
        
        return totalDistance;
    }

    private (int, int) ParseInput(string input)
    {
        var twoNumbers = input.Split("   ");
        return (int.Parse(twoNumbers[0]), int.Parse(twoNumbers[1]));
    }
}