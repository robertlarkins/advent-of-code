namespace Larkins.AdventOfCode.AdventOfCode2024.Day05;

public class Year2024Day05Part01Solver
{
    // For this problem, every page pairing will occur in the rules 
    private readonly Dictionary<int, HashSet<int>> pageOrderingRules = [];
    private readonly List<List<int>> pagesToUpdate = [];

    public Year2024Day05Part01Solver(IEnumerable<string> input)
    {
        ParseInput(input);
    }

    public int Solve()
    {
        return pagesToUpdate.Where(IsCorrectlyOrdered).Sum(GetMiddlePage);
    }

    private bool IsCorrectlyOrdered(List<int> pages)
    {
        for (var i = 0; i < pages.Count - 1; i++)
        {
            if (!pageOrderingRules.TryGetValue(pages[i], out var value) || !value.Contains(pages[i + 1]))
            {
                return false;
            }
        }

        return true;
    }

    private int GetMiddlePage(List<int> pages) => pages[pages.Count / 2];

    private void ParseInput(IEnumerable<string> input)
    {
        var isPagePairProcessing = true;
        
        foreach (var line in input)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                isPagePairProcessing = false;
                continue;
            }

            if (isPagePairProcessing)
            {
                var pagesPairsLine = line.Split('|');
                var pageA = int.Parse(pagesPairsLine[0]);
                var pageB = int.Parse(pagesPairsLine[1]);

                if (!pageOrderingRules.TryAdd(pageA, [pageB]))
                {
                    pageOrderingRules[pageA].Add(pageB);
                }
            }
            else
            {
                var pagesToUpdateSplit = line.Split(',');
                var pagesToUpdateList = pagesToUpdateSplit.Select(int.Parse).ToList();
                pagesToUpdate.Add(pagesToUpdateList);
            }
        }
    }
}